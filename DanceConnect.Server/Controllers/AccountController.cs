using DanceConnect.Server.ApiModel;
using DanceConnect.Server.Authorization;
using DanceConnect.Server.Entities;
using DanceConnect.Server.Response.Dtos;
using DanceConnect.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DanceConnect.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly IInstructorService instructorService;
        private readonly IUserService userService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole<int>> roleManager,
            IInstructorService instructorService, IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.instructorService = instructorService;
            this.userService = userService;
        }

        [AllowAnonymous]
        [Route("token")]
        [HttpGet]
        public async Task<dynamic> Token()
        {
            var token = new JwtTokenBuilder()
                          .AddSecurityKey(JwtSecurityKey.Create("This is my Dance Connect key"))
                          .AddSubject("Dance-Connect")
                          .AddIssuer("Dance-Connect")
                          .AddAudience("Dance-Connect")
                          .AddClaim("Id", "0")
                          .AddClaim("Role", "0")
                          .AddExpiry(1)
                          .Build();

            return Ok();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] LoginApiModel model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user is null)
                {
                    return BadRequest($"User Not Found!");
                }

                var result = await signInManager.PasswordSignInAsync(user, model.Password,
                                                    isPersistent: model.RememberMe, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    return BadRequest($"Login Unsuccessful");
                }
                 
                if (user.UserType == UserType.User) { 
                    var users = await userService.GetAllUsersAsync();
                    user.User = users.Where(x=>x.AppUserId == user.Id).FirstOrDefault();
                }
                else if (user.UserType == UserType.Instructor)
                {
                    var instructors = await instructorService.GetAllInstructorsAsync();
                    user.Instructor = instructors.Where(x => x.AppUserId == user.Id).FirstOrDefault();
                }

                var token = new JwtTokenBuilder()
                                      .AddSecurityKey(JwtSecurityKey.Create("This is my secret key of Dance Connect application"))
                                      .AddSubject("Dance-Connect")
                                      .AddIssuer("Dance-Connect")
                                      .AddAudience("Dance-Connect")
                                      .AddClaim("Id", user.Id.ToString())
                                      .AddClaim("role", user.UserType.ToString())
                                      .AddClaim("UserName", user.UserName)
                                      .AddExpiry(1)
                                      .Build();

                //if((user.UserType == UserType.User || user.UserType == UserType.Instructor) && (user.User is null && user.Instructor is null)){
                //    //Yet to create the profile
                //    return BadRequest($"Please Complete the profile first!");
                //}

                bool isAdmin = user.UserType == UserType.Admin;
                string userName = string.Empty;
                string phone = string.Empty;
                string profilePhoto = string.Empty;
                bool profileCompleted = true;
                int id = 0;

                if (isAdmin) {
                    userName = "Admin";
                    phone = "3828852737";
                    profilePhoto = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/193f93b0-ec6d-4431-aaed-1fbc4580bfad.jpg";
                }
                else
                {
                    if(user.User is null && user.Instructor is null)
                    {
                        userName = user.Email;
                        phone = "";
                        profilePhoto = "";
                        id = 0;
                        profileCompleted = false;
                    }
                    else
                    {
                        userName = user.UserType == UserType.User ? user.User?.Name : user.Instructor?.Name;
                        phone = user.UserType == UserType.User ? user.User?.Phone : user.Instructor?.Phone;
                        profilePhoto = user.UserType == UserType.User ? $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{user.User?.ProfilePic}" :
                            $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{user.Instructor?.ProfilePic}";
                        id = user.UserType == UserType.User ? user.User.UserId : user.Instructor.InstructorId;
                    }
                }
                var loginResponse = new LoginResponse()
                {
                    IdentityId = user.Id,
                    Id = id,
                    IsAdmin = isAdmin,
                    Name = userName,
                    Email = user.Email,
                    Phone = phone,
                    Role = user.UserType.ToString(),
                    ProfilePhoto = profilePhoto,
                    Token = token.Value,
                    IsProfileCompleted = profileCompleted
                };
                return Ok(loginResponse);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { Message = "An internal error occurred. Please try again later." });
            }
        }

        [AllowAnonymous]
        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterApiModel model)
        {
            try
            {
                var existingUser = await userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    return Conflict(new { Message = "Username already exists." });
                }

                //Create a user
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    UserType = model.UserType ?? UserType.User
                };

                var responce = await userManager.CreateAsync(user, model.Password);

                if (!responce.Succeeded)
                {
                    return BadRequest(new { Message = "User creation failed.", responce.Errors });
                }

                return Ok(new { Message = "User registered successfully.", UserId = user.Id });
            }
            catch (Exception ex)
            {
                //Log Exception to Logger (e.g. Serilogger)
                return StatusCode(500, new { Message = "An internal error occurred. Please try again later." });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAllUsers()
        {
            return Ok(userManager.Users.Where(x => x.UserName != "Prawisti").Select(x => new UserApiModel { UserName = x.UserName, PhoneNumber = x.PhoneNumber, Active = x.Active }).ToList());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            return Ok(roleManager.Roles.Where(x => x.Name != "SuperAdmin").Select(x => new { Id = x.Id, Name = x.Name }).ToList());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/auth/GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await userManager.FindByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/auth/update")]
        public async Task<IActionResult> Update([FromBody] UserApiModel user)
        {
            var oldUser = await userManager.FindByIdAsync(user.Id);
            oldUser.UserName = user.UserName;
            oldUser.PhoneNumber = user.PhoneNumber;
            oldUser.Active = user.Active;
            var result = await userManager.UpdateAsync(oldUser);
            return Ok(result.Succeeded);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetApiModel resetViewModel)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(resetViewModel.Email);

                if (user is null)
                {
                    return BadRequest($"User with email {resetViewModel.Email} does not exits");
                }

                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.ResetPasswordAsync(user, token, resetViewModel.Password);

                if (!result.Succeeded)
                {
                    return BadRequest($"Password reset process is failed");
                }
                return Ok(result.Succeeded);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An internal error occurred. Please try again later." });
            }
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("api/auth/delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(user);
            return Ok(result.Succeeded);
        }
    }
}
