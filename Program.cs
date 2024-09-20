using EmployeeManagement.Data;
using EmployeeManagement.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Adicionando o Builder o dbcontext
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("EmployeeDb"));

//Adicionando Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors", builder =>
    {
        builder.WithOrigins("https://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//colcoando o repositorio de employee no DI(Dependency Injection)
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;
    });
}

//Aplicando o Cors Criado
app.UseCors("MyCors");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
