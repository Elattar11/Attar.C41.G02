using Attar.C41.G02.BLL.Interfaces;
using Attar.C41.G02.BLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Attar.C41.G02.PL.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
