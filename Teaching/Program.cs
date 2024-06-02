using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Teaching.Common.Data;
using Teaching.Common.Entities.Users;
using Teaching.Common.SwaggerVersioning;
using Teaching.V1.Auth.CQRS.CommandHandlers;
using Teaching.V1.Auth.CQRS.CommandHandlers.UserCommandHandler;
using Teaching.V1.Auth.CQRS.Commands;
using Teaching.V1.Auth.CQRS.Commands.UserCommands;
using Teaching.V1.Auth.CQRS.Queries.UserQueries;
using Teaching.V1.Auth.CQRS.Services;
using Teaching.V1.Auth.Models.AuthModels;
using Teaching.V1.Auth.Services.Extensions;
using Teaching.V1.Auth.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(Program)));
builder.Services.AddTransient<IRequestHandler<LoginCommand, TokenModel>, LoginCommandHandler>();
builder.Services.AddTransient<IRequestHandler<RegisterCommand, UserModel>, RegisterCommandHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();
builder.Services.AddTransient<IRequestHandler<AssignRoleCommand, UsersModel>, AssignRoleCommandHandler>();
builder.Services.AddTransient<IRequestHandler<RemoveRoleCommand, UsersModel>, RemoveRoleCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllUsersQuery, IList<UsersModel>>, GetAllUsersQueryHandler>();
builder.Services.AddScoped<IDataConnection, DataConnection>();
builder.Services.AddScoped<IUzdevumi, Uzdevumi>();
builder.Services.AddEntityFrameworkCore();
builder.Services.AddServiceConfiguration()
    .AddSwaggerService(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
