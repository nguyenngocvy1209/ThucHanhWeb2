using _2301010045_NguyenNgocVy_Buoi1.Data;
using _2301010045_NguyenNgocVy_Buoi1.Reponsitory;
using _2301010045_NguyenNgocVy_Buoi1.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IBookRepository, SQLBookRepository>();
builder.Services.AddScoped<IPublisherRepository, SQLPublisherRepository>();  // <-- TH?M D?NG N?Y
builder.Services.AddScoped<IAuthorRepository, SQLAuthorRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();