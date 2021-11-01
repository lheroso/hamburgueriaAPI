using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiContextDb;
using Microsoft.EntityFrameworkCore;
using IngredientModel;
using HamburgerModel;

namespace hamburgueriaAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                                  });
            });
            services.AddControllers();
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("test"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "hamburgueriaAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "hamburgueriaAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var context = serviceProvider.GetService<ApiContext>();

            AddMockupData(context);

        }

        private static Ingredient CloneIngredient(Ingredient ingredient)
        {
            return new Ingredient { id = ingredient.id, name = ingredient.name, price = ingredient.price, quantity = 1 };
        }

        private static void AddMockupData(ApiContext context)
        {
            var alface = new Ingredient
            {
                id = 1,
                name = "Alface",
                price = 0.4,
                quantity = 0
            };

            var bacon = new Ingredient
            {
                id = 2,
                name = "Bacon",
                price = 2,
                quantity = 0
            };

            var burger = new Ingredient
            {
                id = 3,
                name = "Hamburguer de carne",
                price = 3,
                quantity = 0
            };

            var egg = new Ingredient
            {
                id = 4,
                name = "Ovo",
                price = 0.8,
                quantity = 0
            };

            var cheese = new Ingredient
            {
                id = 5,
                name = "Queijo",
                price = 1.5,
                quantity = 0
            };

            context.Ingredient.Add(alface);
            context.Ingredient.Add(bacon);
            context.Ingredient.Add(burger);
            context.Ingredient.Add(egg);
            context.Ingredient.Add(cheese);

            context.SaveChanges();


            var xBacon = new Hamburger
            {
                id = 1,
                name = "X-Bacon",
                ingredients = new List<Ingredient> { CloneIngredient(bacon), CloneIngredient(burger) }
            };

            var xBurger = new Hamburger
            {
                id = 2,
                name = "X-Burger",
                ingredients = new List<Ingredient> { CloneIngredient(burger), CloneIngredient(cheese) }
            };

            context.Hamburger.Add(xBacon);
            context.Hamburger.Add(xBurger);
            context.SaveChanges();
/*
           // mockIngredients.Clear();
            mockIngredients.Add(cheese);
            mockIngredients.Add(burger);
            mockIngredients.Add(egg);

            var xEgg = new Hamburger
            {
                id = 3,
                name = "X-Egg",
                ingredients = mockIngredients
            };

            context.Hamburger.Add(xEgg);
            context.SaveChanges();
            //mockIngredients.Clear();
            mockIngredients.Add(cheese);
            mockIngredients.Add(burger);
            mockIngredients.Add(egg);
            mockIngredients.Add(bacon);

            var xEggBacon = new Hamburger
            {
                id = 4,
                name = "X-Egg Bacon",
                ingredients = mockIngredients
            };

            context.Hamburger.Add(xEggBacon);
            context.SaveChanges();*/
        }

    }
}
