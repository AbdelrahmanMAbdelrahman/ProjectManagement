var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(); 
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
try
{
    app.MapControllers();
}
catch (ReflectionTypeLoadException ex)
{
    foreach (var e in ex.LoaderExceptions)
    {
        Console.WriteLine(e?.Message);
    }

    throw;
}
//app.MapControllers();//System.Reflection.ReflectionTypeLoadException
//HResult = 0x80131602
//  Message = Unable to load one or more of the requested types.
//  Source=<Cannot evaluate the exception source>
//  StackTrace:
//< Cannot evaluate the exception stack trace>


app.Run();
