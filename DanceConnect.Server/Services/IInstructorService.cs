using DanceConnect.Server.Entities;

namespace DanceConnect.Server.Services
{
    public interface IInstructorService
    {
        Task<List<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> GetInstructorByIdAsync(int id);
        Task<Instructor> AddInstructorAsync(Instructor instructor);
    }
}
