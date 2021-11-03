using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
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
            services.AddScoped<IHamburgerBusiness, HamburgerBusiness>();
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
            
            var xBurger = new Hamburger
            {
                HamburgerID = 2,
                Name = "X-Burger"
                
            };

            context.Hamburger.Add(xBurger);

            var xEgg = new Hamburger
            {
                HamburgerID = 3,
                Name = "X-Egg"

            };

            context.Hamburger.Add(xEgg);

            var xEggBacon = new Hamburger
            {
                HamburgerID = 4,
                Name = "X-Egg Bacon"

            };

            context.Hamburger.Add(xEggBacon);

            var relation1 = new HamburgerIngredient(){
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
                IngredientID = 5,
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
                IngredientID = 5,
                IngredientQuantity = 1
            };
            
            var relation6 = new HamburgerIngredient()
            {
                HamburgerID = 3,
                IngredientID = 3,
                IngredientQuantity = 1
            };

            var relation7 = new HamburgerIngredient()
            {
                HamburgerID = 3,
                IngredientID = 4,
                IngredientQuantity = 1
            };

            var relation8 = new HamburgerIngredient()
            {
                HamburgerID = 3,
                IngredientID = 5,
                IngredientQuantity = 1
            };

            var relation9 = new HamburgerIngredient()
            {
                HamburgerID = 4,
                IngredientID = 2,
                IngredientQuantity = 1
            };

            var relation10 = new HamburgerIngredient()
            {
                HamburgerID = 4,
                IngredientID = 3,
                IngredientQuantity = 1
            };

            var relation11 = new HamburgerIngredient()
            {
                HamburgerID = 4,
                IngredientID = 4,
                IngredientQuantity = 1
            };

            var relation12 = new HamburgerIngredient()
            {
                HamburgerID = 4,
                IngredientID = 5,
                IngredientQuantity = 1
            };

            context.HamburgerIngredients.Add(relation1);
            context.HamburgerIngredients.Add(relation2);
            context.HamburgerIngredients.Add(relation3);
            context.HamburgerIngredients.Add(relation4);
            context.HamburgerIngredients.Add(relation5);
            context.HamburgerIngredients.Add(relation6);
            context.HamburgerIngredients.Add(relation7);
            context.HamburgerIngredients.Add(relation8);
            context.HamburgerIngredients.Add(relation9);
            context.HamburgerIngredients.Add(relation10);
            context.HamburgerIngredients.Add(relation11);
            context.HamburgerIngredients.Add(relation12);
            context.SaveChanges();

        }

    }
}
