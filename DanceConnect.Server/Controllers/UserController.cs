﻿using DanceConnect.Server.Dtos;
using DanceConnect.Server.Entities;
using DanceConnect.Server.Response.Dtos;
using DanceConnect.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DanceConnect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;

        public UserController(IUserService userService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _environment = environment;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

            if (string.IsNullOrEmpty(userId.ToString()))
            {
                return Unauthorized("CustomerId claim is missing.");
            }
            var allHeaders = HttpContext.Request.Headers;
            var authHeader = HttpContext.Request.Headers.Authorization;

            var users = await _userService.GetAllUsersAsync();

            var usersResponse = users.Select(x=> new UserResponseDto()
            {
                Id = x.UserId,
                Name = x.Name,
                Gender = x.Gender,
                Dob = x.Dob,
                Phone = x.Phone,
                Email = x.AppUser?.Email,
                ProfileStatus = x.ProfileStatus.ToString(),
                ProfilePic = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{x.ProfilePic}",
                IdentityDocument = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{x.IdentityDocument}",
                Street = x.Street,
                City = x.City,
                PostalCode = x.PostalCode,
                Province = x.Province,
            }).ToList();
            return Ok(usersResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var userResponse = new UserResponseDto()
            {
                Id = user.UserId,
                Name = user.Name,
                Gender = user.Gender,
                Dob = user.Dob,
                Phone = user.Phone,
                Email = user.AppUser?.Email,
                ProfileStatus = user.ProfileStatus.ToString(),
                ProfilePic = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{user.ProfilePic}",
                IdentityDocument = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{user.IdentityDocument}",
                Street = user.Street,
                City = user.City,
                PostalCode = user.PostalCode,
                Province = user.Province,
            };

            return Ok(userResponse);
        }

        [HttpPost]
        [Authorize]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddUser([FromForm] UserDto userDto)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

                if (string.IsNullOrEmpty(userId.ToString()))
                {
                    return Unauthorized("CustomerId claim is missing.");
                }

                string profilePicFileName = null;
                string identityDocFileName = null;

                if (userDto.ProfilePic != null)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    profilePicFileName = Guid.NewGuid().ToString() + Path.GetExtension(userDto.ProfilePic.FileName);
                    var profilePicturePath = Path.Combine(uploadsFolder, profilePicFileName);

                    using (var fileStream = new FileStream(profilePicturePath, FileMode.Create))
                    {
                        await userDto.ProfilePic.CopyToAsync(fileStream);
                    }
                }

                if (userDto.IdentityDocument != null)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    identityDocFileName = Guid.NewGuid().ToString() + Path.GetExtension(userDto.IdentityDocument.FileName);
                    string identityDocumentPath = Path.Combine(uploadsFolder, identityDocFileName);

                    using (var fileStream = new FileStream(identityDocumentPath, FileMode.Create))
                    {
                        await userDto.IdentityDocument.CopyToAsync(fileStream);
                    }
                }

                var user = new User
                {
                    Name = userDto.Name,
                    Gender = userDto.Gender,
                    Phone = userDto.Phone,
                    Dob = userDto.Dob,
                    ProfileStatus = Enums.ProfileStatus.ProfileCreated,
                    ProfilePic = profilePicFileName,
                    IdentityDocument = identityDocFileName,
                    Street = userDto.Street,
                    City = userDto.City,
                    Province = userDto.Province,
                    PostalCode = userDto.PostalCode,
                    AppUserId = userId
                };

                var createdUser = await _userService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.AppUserId }, createdUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("approve/{userId:int}")]
        [Authorize]
        public async Task<IActionResult> ApproveUser(int userId)
        {
            try
            {
                //Admin can only approve
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

                if (string.IsNullOrEmpty(userId.ToString()))
                {
                    return Unauthorized("CustomerId claim is missing.");
                }

                var user = await _userService.ApproveUserAsync(userId);
                var userResponse = new UserResponseDto()
                {
                    Id = user.UserId,
                    Name = user.Name,
                    Gender = user.Gender,
                    Dob = user.Dob,
                    Phone = user.Phone,
                    Email = user.AppUser?.Email,
                    ProfileStatus = user.ProfileStatus.ToString(),
                    ProfilePic = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{user.ProfilePic}",
                    IdentityDocument = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{user.IdentityDocument}",
                    Street = user.Street,
                    City = user.City,
                    PostalCode = user.PostalCode,
                    Province = user.Province,
                };

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("decline/{userId:int}")]
        [Authorize]
        public async Task<IActionResult> DeclineUser(int userId)
        {
            try
            {
                //Admin can only approve
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

                if (string.IsNullOrEmpty(userId.ToString()))
                {
                    return Unauthorized("CustomerId claim is missing.");
                }

                var user = await _userService.DeclineUserAsync(userId);
                var userResponse = new UserResponseDto()
                {
                    Id = user.UserId,
                    Name = user.Name,
                    Gender = user.Gender,
                    Dob = user.Dob,
                    Phone = user.Phone,
                    Email = user.AppUser?.Email,
                    ProfileStatus = user.ProfileStatus.ToString(),
                    ProfilePic = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{user.ProfilePic}",
                    IdentityDocument = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{user.IdentityDocument}",
                    Street = user.Street,
                    City = user.City,
                    PostalCode = user.PostalCode,
                    Province = user.Province,
                };

                return Ok(userResponse);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("download-docs/{userId}")]
        [Authorize]
        public async Task<IActionResult> DownloadIdentityDocument(int userId)
        {
            try
            {
                //Admin can only approve
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

                if (string.IsNullOrEmpty(userId.ToString()))
                {
                    return Unauthorized("CustomerId claim is missing.");
                }

                var user = await _userService.GetUserByIdAsync(userId);

                string filePath = Path.Combine(_environment.WebRootPath, "uploads", $"{user.IdentityDocument}");

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("Identity document not found.");
                }

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var fileExtension = Path.GetExtension(filePath).ToLower();

                string mimeType = fileExtension switch
                {
                    ".pdf" => "application/pdf",
                    ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    ".doc" => "application/msword",
                    ".jpg" => "image/jpeg",
                    ".jpeg" => "image/jpeg",
                    ".png" => "image/png",
                    ".txt" => "text/plain",
                    _ => "application/octet-stream" // Default fallback
                };

                var fileName = Path.GetFileName(filePath);
                return File(fileBytes, mimeType, fileName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
