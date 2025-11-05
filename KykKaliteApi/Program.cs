using HalApi.Models.DapperContext;

using HalApi.Repositories.AlimRepository;
using HalApi.Repositories.FaturaRepository;
using HalApi.Repositories.AlimDetayRepository;
using HalApi.Repositories.CariRepository;
using HalApi.Repositories.KasaTipiRepository;
using HalApi.Repositories.KullaniciRepository;
using HalApi.Repositories.SatisRepository;
using HalApi.Repositories.SatisDetayRepository;
using HalApi.Repositories.StokHareketRepository;
using HalApi.Repositories.UrunRepository;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<Context>();
builder.Services.AddTransient<IAlimRepository, AlimRepository>();
builder.Services.AddTransient<IFaturaRepository, FaturaRepository>();
builder.Services.AddTransient<IAlimDetayRepository, AlimDetayRepository>();
builder.Services.AddTransient<ICariRepository, CariRepository>();
builder.Services.AddTransient<IKasaTipiRepository, KasaTipiRepository>();
builder.Services.AddTransient<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddTransient<ISatisRepository, SatisRepository>();
builder.Services.AddTransient<ISatisDetayRepository, SatisDetayRepository>();
builder.Services.AddTransient<IStokHareketRepository, StokHareketRepository>();
builder.Services.AddTransient<IUrunRepository, UrunRepository>();



// CORS ekle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()  // Herhangi bir origin'e izin ver
               .AllowAnyMethod()  // Herhangi bir HTTP metoduna izin ver
               .AllowAnyHeader(); // Herhangi bir header'a izin ver
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

// CORS middleware'ini kullan
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
