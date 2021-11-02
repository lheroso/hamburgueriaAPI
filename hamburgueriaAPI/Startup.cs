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

        private static void AddMockupData(ApiContext context)
        {
            var alface = new Ingredient
            {
                IngredientID = 1,
                Name = "Alface",
                Price = 0.4
            };

            var bacon = new Ingredient
            {
                IngredientID = 2,
                Name = "Bacon",
                Price = 2
            };

            var burger = new Ingredient
            {
                IngredientID = 3,
                Name = "Hamburguer de carne",
                Price = 3
            };

            var egg = new Ingredient
            {
                IngredientID = 4,
                Name = "Ovo",
                Price = 0.8
            };

            var cheese = new Ingredient
            {
                IngredientID = 5,
                Name = "Queijo",
                Price = 1.5
            };

            context.Ingredient.Add(alface);
            context.Ingredient.Add(bacon);
            context.Ingredient.Add(burger);
            context.Ingredient.Add(egg);
            context.Ingredient.Add(cheese);
            
            var xBacon = new Hamburger
            {
                HamburgerID = 1,
                Name = "X-Bacon"
                
            };

            context.Hamburger.Add(xBacon);
            
            var xEgg = new Hamburger
            {
                HamburgerID = 2,
                Name = "X-Egg"
                
            };

            context.Hamburger.Add(xEgg);

            var relation1 = new HamburgerIngredient()
            {
                HamburgerID = 1,
                IngredientID = 2,
                IngredientQuantity = 1
            };
            
            var relation2 = new HamburgerIngredient()
            {
                HamburgerID = 1,
                IngredientID = 3,
                IngredientQuantity = 1
            };
            
            var relation3 = new HamburgerIngredient()
            {
                HamburgerID = 1,
                IngredientID = 4,
                IngredientQuantity = 1
            };
            
            var relation4 = new HamburgerIngredient()
            {
                HamburgerID = 2,
                IngredientID = 3,
                IngredientQuantity = 1
            };
            
            var relation5 = new HamburgerIngredient()
            {
                HamburgerID = 2,
                IngredientID = 2,
                IngredientQuantity = 1
            };
            
            var relation6 = new HamburgerIngredient()
            {
                HamburgerID = 2,
                IngredientID = 1,
                IngredientQuantity = 1
            };

            context.HamburgerIngredients.Add(relation1);
            context.HamburgerIngredients.Add(relation2);
            context.HamburgerIngredients.Add(relation3);
            context.HamburgerIngredients.Add(relation4);
            context.HamburgerIngredients.Add(relation5);
            context.HamburgerIngredients.Add(relation6);
            
            context.SaveChanges();

        }

    }
}
