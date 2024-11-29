using DanceConnect.Server.Dtos;
using DanceConnect.Server.Entities;
using DanceConnect.Server.Helpers;
using DanceConnect.Server.Models;
using DanceConnect.Server.Response.Dtos;
using DanceConnect.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using System.Reflection;

namespace DanceConnect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;

        public InstructorController(IInstructorService instructorService,
            IUserService userService,
            IWebHostEnvironment environment)
        {
            _instructorService = instructorService;
            _userService = userService;
            _environment = environment;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetInstructors([FromQuery] string? searchTerm,
                                                        [FromQuery] string? gender,
                                                        [FromQuery] string? city,
                                                        [FromQuery] string? province)
        {
            try
            {
                var query = await _instructorService.GetAll();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(i => i.Name.Contains(searchTerm));
                }
                if (!string.IsNullOrWhiteSpace(gender))
                {
                    query = query.Where(i => i.Gender == gender);
                }
                if (!string.IsNullOrWhiteSpace(city))
                {
                    query = query.Where(i => i.City == city);
                }
                if (!string.IsNullOrWhiteSpace(province))
                {
                    query = query.Where(i => i.Province == province);
                }

                //var instructors = await _instructorService.GetAllInstructorsAsync();
                var instructorResponses = query.ToList().Select(x => new InstructorResponseDto()
                {
                    Id = x.InstructorId,
                    Name = x.Name,
                    Gender = x.Gender,
                    Dob = x.Dob,
                    Phone = x.Phone,
                    Email = x.AppUser?.Email,
                    HourlyRate = x.HourlyRate,
                    AverageRating = x.Ratings.Count() > 0 ? x.Ratings.Average(x => x.RatingValue) : 0,
                    ProfileStatus = x.ProfileStatus.ToString(),
                    ProfilePic = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{x.ProfilePic}",
                    IdentityDocument = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{x.IdentityDocument}",
                    ShortIntroVideo = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/videos/{x.IntroVideo}",
                    Street = x.Street,
                    City = x.City,
                    PostalCode = x.PostalCode,
                    Province = x.Province,
                }).ToList();
                return Ok(instructorResponses);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstructorById(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null)
                return NotFound();

            var instructorResponse = new InstructorResponseDto()
            {
                Id = instructor.InstructorId,
                Name = instructor.Name,
                Gender = instructor.Gender,
                Dob = instructor.Dob,
                Phone = instructor.Phone,
                Email = instructor.AppUser?.Email,
                AverageRating = instructor.Ratings.Count() > 0 ? instructor.Ratings.Average(x => x.RatingValue) : 0,
                ProfileStatus = instructor.ProfileStatus.ToString(),
                ProfilePic = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{instructor.ProfilePic}",
                IdentityDocument = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{instructor.IdentityDocument}",
                ShortIntroVideo = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/videos/{instructor.IntroVideo}",
                Street = instructor.Street,
                City = instructor.City,
                PostalCode = instructor.PostalCode,
                Province = instructor.Province,
            };

            return Ok(instructorResponse);
        }

        [HttpPost(Name = "Save Instructor")]
        [Authorize]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddInstructor([FromForm] InstructorDto instructorDto)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

                if (string.IsNullOrEmpty(userId.ToString()))
                {
                    return Unauthorized("CustomerId claim is missing.");
                }

                string profileFileName = null;
                string identityFileName = null;
                string shortVideoFileName = null;

                if (instructorDto.ProfilePic != null)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    profileFileName = Guid.NewGuid().ToString() + Path.GetExtension(instructorDto.ProfilePic.FileName);
                    var profilePicturePath = Path.Combine(uploadsFolder, profileFileName);

                    using (var fileStream = new FileStream(profilePicturePath, FileMode.Create))
                    {
                        await instructorDto.ProfilePic.CopyToAsync(fileStream);
                    }
                }

                if (instructorDto.IdentityDocument != null)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    identityFileName = Guid.NewGuid().ToString() + Path.GetExtension(instructorDto.IdentityDocument.FileName);
                    string identityDocumentPath = Path.Combine(uploadsFolder, identityFileName);

                    using (var fileStream = new FileStream(identityDocumentPath, FileMode.Create))
                    {
                        await instructorDto.IdentityDocument.CopyToAsync(fileStream);
                    }
                }

                if (instructorDto.ShortIntroVideo != null)
                {
                    string uploadsFolder = Path.Combine(_environment.WebRootPath, @"uploads/videos");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    shortVideoFileName = Guid.NewGuid().ToString() + Path.GetExtension(instructorDto.ShortIntroVideo.FileName);
                    string shortIntroVideoPath = Path.Combine(uploadsFolder, shortVideoFileName);

                    using (var fileStream = new FileStream(shortIntroVideoPath, FileMode.Create))
                    {
                        await instructorDto.ShortIntroVideo.CopyToAsync(fileStream);
                    }
                }

                var instructor = new Instructor
                {
                    Name = instructorDto.Name,
                    Gender = instructorDto.Gender,
                    Phone = instructorDto.Phone,
                    Dob = instructorDto.Dob,
                    HourlyRate = instructorDto.HourlyRate,
                    ProfileStatus = Enums.ProfileStatus.ProfileCreated,
                    ProfilePic = profileFileName,
                    IdentityDocument = identityFileName,
                    IntroVideo = shortVideoFileName,
                    Street = instructorDto.Street,
                    City = instructorDto.City,
                    Province = instructorDto.Province,
                    PostalCode = instructorDto.PostalCode,
                    AppUserId = userId
                };

                var createdInstructor = await _instructorService.AddInstructorAsync(instructor);
                return CreatedAtAction(nameof(GetInstructorById), new { id = createdInstructor.InstructorId }, createdInstructor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(EmailMessageDto emailMessageDto)
        {
            try
            {
                var instructor = await _instructorService.GetInstructorByIdAsync(emailMessageDto.emailTo) ?? throw new Exception($"Instructor with id {emailMessageDto.emailTo} does not found");
                var user = await _userService.GetUserByIdAsync(1) ?? throw new Exception($"User with id {1} does not found");

                var emailMessage = new EmailMessage()
                {
                    Sender = 1,
                    SendingUser = user,
                    Receiver = emailMessageDto.emailTo,
                    ReceivingUser = instructor,
                    Message = emailMessageDto.message
                };
                bool isEmailSent = EmailHelper.SendEmail(emailMessage, "Confirmation Message");

                if (isEmailSent)
                {
                    //Add Email Sent Record to DB
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("approve/{instructorId:int}")]
        [Authorize]
        public async Task<IActionResult> ApproveInstructor(int instructorId)
        {
            try
            {
                //Admin can only approve
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

                if (string.IsNullOrEmpty(instructorId.ToString()))
                {
                    return Unauthorized("CustomerId claim is missing.");
                }

                var instructor = await _instructorService.ApproveInstructorAsync(instructorId);
                var instructorResponse = new InstructorResponseDto()
                {
                    Id = instructor.InstructorId,
                    Name = instructor.Name,
                    Gender = instructor.Gender,
                    Dob = instructor.Dob,
                    Phone = instructor.Phone,
                    Email = instructor.AppUser?.Email,
                    AverageRating = instructor.Ratings.Count() > 0 ? instructor.Ratings.Average(x => x.RatingValue) : 0,
                    ProfileStatus = instructor.ProfileStatus.ToString(),
                    ProfilePic = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{instructor.ProfilePic}",
                    IdentityDocument = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{instructor.IdentityDocument}",
                    ShortIntroVideo = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/videos/{instructor.IntroVideo}",
                    Street = instructor.Street,
                    City = instructor.City,
                    PostalCode = instructor.PostalCode,
                    Province = instructor.Province,
                };

                return Ok(instructorResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("decline/{instructorId:int}")]
        [Authorize]
        public async Task<IActionResult> DeclineInstructor(int instructorId)
        {
            try
            {
                //Admin can only approve
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

                if (string.IsNullOrEmpty(instructorId.ToString()))
                {
                    return Unauthorized("CustomerId claim is missing.");
                }

                var instructor = await _instructorService.DeclineInstructorAsync(instructorId);
                var instructorResponse = new InstructorResponseDto()
                {
                    Id = instructor.InstructorId,
                    Name = instructor.Name,
                    Gender = instructor.Gender,
                    Dob = instructor.Dob,
                    Phone = instructor.Phone,
                    Email = instructor.AppUser?.Email,
                    AverageRating = instructor.Ratings.Count() > 0 ? instructor.Ratings.Average(x => x.RatingValue) : 0,
                    ProfileStatus = instructor.ProfileStatus.ToString(),
                    ProfilePic = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{instructor.ProfilePic}",
                    IdentityDocument = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/{instructor.IdentityDocument}",
                    ShortIntroVideo = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/uploads/videos/{instructor.IntroVideo}",
                    Street = instructor.Street,
                    City = instructor.City,
                    PostalCode = instructor.PostalCode,
                    Province = instructor.Province,
                };

                return Ok(instructorResponse);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet("download-docs/{instructorId:int}")]
        [Authorize]
        public async Task<IActionResult> DownloadIdentityDocument(int instructorId)
        {
            try
            {
                //Admin can only approve
                var adminId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value);

                if (string.IsNullOrEmpty(instructorId.ToString()))
                {
                    return Unauthorized("CustomerId claim is missing.");
                }

                var instructor = await _instructorService.GetInstructorByIdAsync(instructorId);

                var filePaths = new List<string>
                {
                Path.Combine(_environment.WebRootPath, "uploads", instructor.ProfilePic),
                Path.Combine(_environment.WebRootPath, "uploads", instructor.IdentityDocument),
                Path.Combine(_environment.WebRootPath, "uploads/videos", instructor.IntroVideo)
                };
                filePaths.ForEach(x =>
                {
                    if (!System.IO.File.Exists(x))
                    {
                        NotFound("Identity document not found.");
                    }
                });

                // Create a temporary ZIP file
                var zipFilePath = Path.Combine(Path.GetTempPath(), "UserDocuments.zip");

                using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    foreach (var filePath in filePaths)
                    {
                        if (System.IO.File.Exists(filePath))
                        {
                            zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                        }
                    }
                }

                // Return the ZIP file as a download
                var fileBytes = System.IO.File.ReadAllBytes(zipFilePath);
                System.IO.File.Delete(zipFilePath); // Clean up the temporary file
                return File(fileBytes, "application/zip", "UserDocuments.zip");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
