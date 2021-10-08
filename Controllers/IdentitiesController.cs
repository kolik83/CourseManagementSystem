using CourseManagementSystem.Models.Dtos;
using CourseManagementSystem.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentitiesController : Controller
    {
        private readonly IdentityRepository _identityRepository;
        public IdentitiesController(IdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;    
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> SignIn(UserDto userDto)
        {
            var result = await _identityRepository.SignInAsync(userDto);
            if (result == null)
                return NotFound();
            return result;
        }
        [HttpGet]
        public async Task<ActionResult> SignOut()
        {
            var result = await _identityRepository.SignOutAsync();
            return Ok();
        }

        [HttpPost("changePassword")]
        [Authorize]
        public async Task<ActionResult<ChangePasswordObj>> ChangePassword(ChangePasswordObj userDto)
        {
            var result = await _identityRepository.ChangePassword(userDto, HttpContext.User);
            if (result == null)
                return NotFound();
            return result;
        }
    }
}
