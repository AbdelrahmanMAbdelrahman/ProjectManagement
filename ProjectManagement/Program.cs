using Microsoft.OpenApi.Models;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1",
            new Microsoft.OpenApi.Models.OpenApiInfo()
            {
                Version = "v1",
                Title = "project managment api",
                Description = "this is swagger api documentation",
                TermsOfService = new Uri("https://aps.net"),
                Contact=new OpenApiContact()
                {
                    Name="project managment contact",
                    Url=new Uri("https://aps.net")
                },
              
                License=new OpenApiLicense()
                {
                    Name="project managment license",
                    Url=new Uri("https://aps.net")
                }

            });
        var xmlfileNeme = $"{Assembly.GetAssembly(typeof(AppSettings)).GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,xmlfileNeme));
    }
    );

 
builder.Services.PrepareAppServices(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}
app.UseExceptionHandler();
app.UseCors("ProjectManagement");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


 


app.Run();
