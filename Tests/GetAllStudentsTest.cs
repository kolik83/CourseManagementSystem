using CourseManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Tests
{
    public class GetAllStudentsTest
    {
        private RequestDelegate nextDelegate;
        private SignInManager<IdentityUser> signInManager;

        /*[BindProperty]
        [Required]
        public string UserName { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }
        [BindProperty(SupportsGet = true)]
        public string ReturnUrl { get; set; }*/

        public GetAllStudentsTest(RequestDelegate next, SignInManager<IdentityUser> signinMgr)
        {
            signInManager = signinMgr;
            nextDelegate = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/getAllStudentsTest")
            {   /*
                Microsoft.AspNetCore.Identity.SignInResult res  ult =
                    await signInManager.PasswordSignInAsync(UserName,Password, false, false);
                if (result.Succeeded)
                {
                    await context.Response.WriteAsync("Success");
                }
                else*/
                    await context.Response.WriteAsync("Fail");
            }
            else
            {
                await nextDelegate(context);
            }
        }

    }
}
