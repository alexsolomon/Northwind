using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Northwind.Api.Validators;
using Northwind.Lib.Business;
using Northwind.Lib.Extensions;
using Northwind.Lib.Repository.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ConfigurationManager configuration = builder.Configuration;
builder.Services.ConfigureLibService(configuration);
builder.Services.AddScoped<IValidator<Customer>, CustomerValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/customers/{id}", async (string id, ICustomerBusiness customerBusiness) =>
{

    return await customerBusiness.GetCustomer(id);
})
.WithName("GetCustomer")
.WithOpenApi();

app.MapPost("/customers", async (Customer customer, IValidator<Customer> validator, ICustomerBusiness customerBusiness) =>
{
    ValidationResult result = await validator.ValidateAsync(customer);
    if(!result.IsValid)
    {
        return Results.ValidationProblem(result.ToDictionary());
    }
    return Results.Ok();
})
.WithName("AddCustomer")
.WithOpenApi();

app.MapGet("/orders/{id}", async (int id, IOrderBusiness orderBusiness) =>
{

    return await orderBusiness.GetOrder(id);
})
.WithName("GetOrder")
.WithOpenApi();

app.MapGet("/products", async ([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, IOrderBusiness orderBusiness) =>
{

    return await orderBusiness.GetProductOrderDetailsForDate(startDate, endDate);
})
.WithName("GetProductOrderDetailsForDate")
.WithOpenApi();

app.Run();

