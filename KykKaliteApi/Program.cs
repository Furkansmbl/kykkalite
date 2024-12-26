using KykKaliteApi.Models.DapperContext;
using KykKaliteApi.Repositories.FabrikalarRepository;

using KykKaliteApi.Repositories.CihazlarRepository;
using KykKaliteApi.Repositories.HammaddelerRepository;
using KykKaliteApi.Repositories.HammaddeGruplariRepository;
using KykKaliteApi.Repositories.HMnumuneRepository;
using KykKaliteApi.Repositories.HMPNvalueRepository;
using KykKaliteApi.Repositories.KullaniciRepository;
using KykKaliteApi.Repositories.ParametrelerRepository;
using KykKaliteApi.Repositories.UnumuneRepository;
using KykKaliteApi.Repositories.UPatamaRepository;
using KykKaliteApi.Repositories.UPNvalueRepository;
using KykKaliteApi.Repositories.UretimHatlariRepository;
using KykKaliteApi.Repositories.UrunGruplariRepository;
using KykKaliteApi.Repositories.HMPatamaRepository;
using KykKaliteApi.Repositories.UrunlerRepository;
using KykKaliteApi.Repositories.NewRepository;
using KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<Context>();
builder.Services.AddTransient<IFabrikalarRepository, FabrikalarRepository>();
builder.Services.AddTransient<ICihazlarRepository, CihazlarRepository>();
builder.Services.AddTransient<IHammaddeGruplariRespository,HammaddeGruplariRespository>();
builder.Services.AddTransient<IHammaddelerRepository, HammaddelerRepository>();
builder.Services.AddTransient<IHMnumuneRepository, HMnumuneRepository>();
builder.Services.AddTransient<IHMPNvalueRepository, HMPNvalueRepository>();
builder.Services.AddTransient<IKullaniciRepository, KullaniciRepository>();
builder.Services.AddTransient<IParametrelerRepository, ParametrelerRepository>();
builder.Services.AddTransient<IUnumuneRepository, UNumuneRepository>();
builder.Services.AddTransient<IUPatamaRepository, UPatamaRepository>();
builder.Services.AddTransient<IUPNvalueRepository, UPNvalueRepository>();
builder.Services.AddTransient<IUretimHatlariRepository, UretimHatlariRepository>();
builder.Services.AddTransient<IUrunGruplariRepository, UrunGruplariRepository>();
builder.Services.AddTransient<IHMPatamaRepository, HMPatamaRepository>();
builder.Services.AddTransient<IUrunlerRepository, UrunlerRepository>();
builder.Services.AddTransient<INewwRepository, NewwRepository>();
builder.Services.AddTransient<IGetUpatamaKodlariByUrunIDRepository, GetUpatamaKodlariByUrunIDRepository>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
