
namespace ProjectManagementApplication.Errors.AuthErrors
{
    public class AuthError
    {
        public static Error NotFound => new Error("Auth.NotFound", "User Not Found",  404 );
        public static Error BadRequest => new Error("Auth.BadRequest","In Correct Request", 400 );
        public static Error EmailNotConfirmed => new Error("Auth.EmailNotConfirmed","Please Confirm Your Email then Sign In", 400 );
        public static Error InCorrectUserNameOrPassword => new Error("Auth.InCorrectUserNameOrPassword","InCorrect Username or Password", 400 );
        public static Error AlreadyExist => new Error("Auth.EmailAlreadyInUse","Email already in use", 409 );
        public static Error EmailConfirmConflict => new Error("Auth.EmailConfirmConflict", "EmailAlreadyConfirmed", 409);
    }
}
