using Microsoft.AspNetCore.Identity;
using NegosudAPI.Repositories.Implementations;
using NegosudAPI.Repositories.Interfaces;
using NegosudAPI.Services.Implementations;
using NegosudAPI.Services.Interfaces;
using NegosudModel.Context;
using NegosudModel.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<NegosudContext>()
    .AddApiEndpoints();

// Add services to the container.
builder.Services.AddDbContext<NegosudContext>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IReasonRepository, ReasonRepository>();
builder.Services.AddScoped<IReasonService, ReasonService>();
builder.Services.AddScoped<IArticleOrderRepository, ArticleOrderRepository>();
builder.Services.AddScoped<IArticleOrderService, ArticleOrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IArticleInventoryRepository, ArticleInventoryRepository>();
builder.Services.AddScoped<IArticleInventoryService, ArticleInventoryService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();
builder.Services.AddScoped<IStockMovementService, StockMovementService>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapIdentityApi<User>();

// J'ai commenté cette condition pour activer swagger lors du déploiement
//if (app.Environment.IsDevelopment())
//{
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// J'ai commenté cette ligne pour pouvoir déployer sans HTTPS
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
