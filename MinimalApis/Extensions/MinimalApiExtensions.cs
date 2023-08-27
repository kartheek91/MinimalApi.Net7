using Application.Abstractions;
using Application.Posts.Commands;
using DataAccess.Repositories;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApis.Abstractions;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MinimalApis.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(cs));
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddMediatR(typeof(CreatePost));
            //builder.Services.AddSwaggerGen(c => {
            //    c.SwaggerDoc("v1", new() { Title = "Minimal API", Version = "v1" });
            //});
        }

        public static void RegisterEndpointsDefinitions(this WebApplication app)
        {
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinimalAPIS"));
            //}   
            var endpointDefinitions = typeof(Program).Assembly.GetTypes().
                Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface).
                Select(Activator.CreateInstance).Cast<IEndpointDefinition>();
            foreach (var endpointDefinition  in endpointDefinitions)
            {
                endpointDefinition.RegisterEndpoints(app);
            }

        }
    }
}
