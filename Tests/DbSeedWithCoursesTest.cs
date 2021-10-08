using CourseManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Tests
{
    public class DbSeedWithCoursesTest
    {
        private RequestDelegate nextDelegate;
        public DbSeedWithCoursesTest(RequestDelegate next)
        {
            nextDelegate = next;
        }
        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            if (context.Request.Path == "/getNumberOfCoursesTest")
            {
                await context.Response.WriteAsync( $"There are {dataContext.Courses.Count()} courses\n");
            }
            else
            {
                await nextDelegate(context);
            }
        }

    }
}
