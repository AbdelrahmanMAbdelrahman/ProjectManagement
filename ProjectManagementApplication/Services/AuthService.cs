using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities; 
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using ProjectManagementApplication.Common.Results;
using ProjectManagementApplication.DTOs.Auth;
using ProjectManagementApplication.Errors.AuthErrors;
using ProjectManagementApplication.Interfaces;
using ProjectManagementDomain.Models.Auth;
using ProjectManagementDomain.Models.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace ProjectManagementApplication.Services
{
    public class AuthService(UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        ILogger<AuthService> logger
        , IOptions<MailOptions> mailOptions
        , IOptions<JwtOptions> Options) : IAuth
    {
      

         

            public  async Task<Result> ConfirmEmail(EmailConfirmReq req,CancellationToken ct)
            {
                AppUser? user = await userManager.FindByIdAsync(req.id);
                if (user is null) return Result.Fail(AuthError.NotFound);
                bool IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);
                if (IsEmailConfirmed) return Result.Fail(AuthError.EmailConfirmConflict);
                string code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(req.code));
                var result = await userManager.ConfirmEmailAsync(user, code);
                return result.Succeeded ?
                    Result.Success() :
                    Result.Fail(AuthError.BadRequest);
            }

              string GetRefreshToken(CancellationToken ct)
            {
                byte[] data = new byte[32];
                RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
                provider.GetBytes(data);
                return Convert.ToBase64String(data);
            }

              async Task<string> GetToken(AppUser appUser, CancellationToken ct)
            {
                List<Claim> Claims = new List<Claim>() {
            new Claim(JwtRegisteredClaimNames.Sub,appUser.Id),
            new Claim(JwtRegisteredClaimNames.GivenName,appUser.Name!),
            new Claim(JwtRegisteredClaimNames.Email,appUser.Email!),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
                IList<Claim> UserClaims = await userManager.GetClaimsAsync(appUser);
                IList<string> Roles = await userManager.GetRolesAsync(appUser);
                foreach (var roleName in Roles)
                {
                    AppRole? role = await roleManager.FindByNameAsync(roleName);
                    IList<Claim> roleClaim = await roleManager.GetClaimsAsync(role!);
                    Claims.AddRange(roleClaim);
                }
                //IEnumerable<Claim> UserRoles = Roles.Select(r => new Claim(ClaimTypes.Role, r));
                Claims.AddRange(UserClaims);
                //Claims.AddRange(UserRoles);
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.Value.Key));
                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: Options.Value.Issuer,
                    audience: Options.Value.Audience,
                    signingCredentials: signingCredentials,
                    expires: DateTime.UtcNow.AddMinutes(Options.Value.Period),
                    claims: Claims
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }



            public async Task<Result<LoginRes>> SignIn(LoginReq req, CancellationToken ct)
            {
                AppUser? appUser = await userManager.FindByEmailAsync(req.email);
                if (appUser is null) return Result.Fail<LoginRes>(AuthError.NotFound);
                if (!appUser.EmailConfirmed) return Result.Fail<LoginRes>(AuthError.EmailNotConfirmed);
                bool IsPassValid = await userManager.CheckPasswordAsync(appUser, req.password);
                if (!IsPassValid) { return Result.Fail<LoginRes>(AuthError.InCorrectUserNameOrPassword); }
                var roles = await userManager.GetRolesAsync(appUser);
                string token = await GetToken(appUser, ct);
                if (appUser.RefreshTokens.Any(rf => rf.IsActive))
                {
                    RefreshToken refreshToken = appUser.RefreshTokens.Single(rf => rf.IsActive)!;
                    LoginRes response = new LoginRes(
                        appUser.Name,
                        appUser.Email,
                        appUser.UserName,
                        appUser.PhoneNumber,
                        token,
                        roles.ToList(),
                        refreshToken.Token
                        );
                    return Result.Success(response);
                }
                else
                {
                    foreach (var rf in appUser.RefreshTokens.Where(rf => rf.RevokedOn is null))
                    {
                        rf.RevokedOn = DateTime.UtcNow;
                    }
                    RefreshToken refreshToken = new RefreshToken()
                    {
                        Token = GetRefreshToken(ct),
                        ExpirationDate = DateTime.UtcNow.AddDays(Options.Value.Period),
                        CreatedOn = DateTime.UtcNow
                    };
                    appUser.RefreshTokens.Add(refreshToken);
                    await userManager.UpdateAsync(appUser);
                    LoginRes response = new LoginRes(
                        appUser.Name,
                        appUser.Email,
                        appUser.UserName,
                        appUser.PhoneNumber,
                        token,
                        roles.ToList(),
                        refreshToken.Token
                        );
                    return Result.Success(response);
                }
            }

            public async Task<Result> SignUp(RegisterReq req, CancellationToken ct)
            {
                AppUser? appUser = await userManager.FindByEmailAsync(req.email);
                if (appUser is not null) return Result.Fail<LoginRes>(AuthError.AlreadyExist);
                appUser = new AppUser();
                appUser.Email = req.email;
                appUser.Name = req.name;
                appUser.PhoneNumber = req.phone;
                appUser.UserName = req.userName;
                appUser.Email = req.email;
                var res = await userManager.CreateAsync(appUser, req.password);
                if (res.Succeeded)
                {
                    RefreshToken refreshToken = new RefreshToken
                    {
                        Token = GetRefreshToken(ct),
                        ExpirationDate = DateTime.UtcNow.AddDays(Options.Value.Period),
                        CreatedOn = DateTime.UtcNow
                    };
                    await userManager.AddToRoleAsync(appUser, "User");
                    //  IList<string> UserRoles =await userManager.GetRolesAsync(appUser);
                    appUser.RefreshTokens.Add(refreshToken);
                    await userManager.UpdateAsync(appUser);
                    string code = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    //send email verfication
                    await sendEmailVerfication(new EmailVerifyReq(req.email, mailOptions.Value.Email, appUser.Id, code), ct);

                    return Result.Success();
                }
                return Result.Fail<LoginRes>(AuthError.BadRequest);
            }
              async Task<Result> sendEmailVerfication(EmailVerifyReq req, CancellationToken ct)
            {
                MimeMessage mimeMessage = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(req.mailFrom),
                    Subject = "verify your email"
                };
                mimeMessage.To.Add(MailboxAddress.Parse(req.mailTo));
                mimeMessage.From.Add(MailboxAddress.Parse(req.mailFrom));
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(),"..","ProjectManagementApplication","Templates","MailTemplate.html");
            using StreamReader streamReader = new StreamReader(templatePath);
                var content = streamReader.ReadToEnd();
                streamReader.Close();

            content = content.Replace("{{confirmUrl}}",$"https://localhost:7279/api/Auth/Confirm?id={req.id}&code={req.code}");
            var bodyBuilder = new BodyBuilder { HtmlBody = content };
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                using MailKit.Net.Smtp.SmtpClient smtpClient = new MailKit.Net.Smtp.SmtpClient();
                smtpClient.Connect(mailOptions.Value.Host, mailOptions.Value.Port, SecureSocketOptions.StartTls, ct);
                await smtpClient.AuthenticateAsync(mailOptions.Value.Email, mailOptions.Value.Password, ct);
                await smtpClient.SendAsync(mimeMessage, ct);
                smtpClient.Disconnect(true, ct);
                return Result.Success();
            }

        public async Task<Result> AssignRoleAsync(UserRoleReq req,CancellationToken ct)
        {
            AppUser? appUser = await userManager.FindByEmailAsync(req.email);
            if (appUser is null) return Result.Fail(AuthError.NotFound);
            IdentityResult result =await userManager.AddToRoleAsync(  appUser,req.role);
            return result.Succeeded ?
                Result.Success() :
                Result.Fail(AuthError.BadRequest);
        }

        public async Task<Result<RoleRes>>AddRole(string role,CancellationToken ct)
        {
            AppRole appRole = new AppRole() {
                Name = role,
                NormalizedName = role.ToUpper(),
                IsDefault = true,
                IsDeleted = true,
          
            };
            IdentityResult result = await roleManager.CreateAsync(appRole);
            if(! result.Succeeded)
                Result.Fail<RoleRes>(AuthError.BadRequest);
            RoleRes res = new RoleRes(role, role.ToUpper(), true, true);

           return Result.Success(res);
        }

             
        }
    }
 
