using SurveyBasket;
 // مهم عشان AddProjectServices تشتغل (Namespace بتاعك)

namespace SurveyBasket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            // 👇 تسجّل كل الخدمات (Controllers + FluentValidation + Mapster + Swagger)
            builder.Services.AddProjectServices(builder.Configuration);

            var app = builder.Build();

            // 👇 إعداد Swagger في وضع التطوير
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
