using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vn.com.pnsuite.common.dataaccess;
using vn.com.pnsuite.common.dataaccess.interfaces;
using vn.com.pnsuite.common.dataaccess.repositories;
using vn.com.pnsuite.common.models;
using vn.com.pnsuite.toankhoan.dataaccess.interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces;
using vn.com.pnsuite.toankhoan.dataaccess.Interfaces.Categories;
using vn.com.pnsuite.toankhoan.dataaccess.repositories;
using vn.com.pnsuite.toankhoan.dataaccess.Repositories;
using vn.com.pnsuite.toankhoan.dataaccess.Repositories.Categories;
using vn.com.pnsuite.toankhoan.Helpers;

namespace vn.com.pnsuite.toankhoan
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<ApiContext>(options =>
                      options.UseSqlServer(
                          Configuration.GetConnectionString("DefaultConnection")));

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFunctionService, FunctionService>();
            services.AddScoped<IVersionService, VersionService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ICategoryCostService, CategoryCostService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPurchasingService, PurchasingService>();
            services.AddScoped<IPricebookService, PricebookService>();
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IListService, ListService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ICashService, CashService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAdjustService, AdjustService>();
            services.AddScoped<IOpenBalanceService, OpenBalanceService>();
            services.AddScoped<IPeriodService, PeriodService>();
            services.AddScoped<IProductPriceService, ProductPriceService>();
            services.AddScoped<IQuotationService, QuotationService>();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
