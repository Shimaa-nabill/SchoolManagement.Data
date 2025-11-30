using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Repository.Interfaces;
using SchoolManagement.Repository.Repositories;
using SchoolManagement.Service.AdminService;
using SchoolManagement.Service.AuthService;
using SchoolManagement.Service.CourseService.Dtos;
using SchoolManagement.Service.DepartmentService.Dtos;
using SchoolManagement.Service.TokenService;
using SchoolManagement.Service.UserService.Dtos;
//using AutoMapper.Extensions.Microsoft.DependencyInjection;
using AutoMapper;
using SchoolManagement.Service.TeacherService;
using SchoolManagement.Service.StudentService;
using SchoolManagement.Service.CourseService;
using SchoolManagement.Service.HandleResponse;
using SchoolManagement.Service.DepartmentService;



namespace SchoolManagement.Web.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
           services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IDepartmentService,DepartmentService>();
            

            services.AddMemoryCache();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

























            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                .Where(model => model.Value?.Errors.Count() > 0)
                                .SelectMany(model => model.Value.Errors)
                                .Select(error => error.ErrorMessage).ToList();

                    var errorRespone = new ValidationErrorResopnse { Errors = errors };

                    return new BadRequestObjectResult(errorRespone);
                };
            });

            return services;
        }
    }
 }
