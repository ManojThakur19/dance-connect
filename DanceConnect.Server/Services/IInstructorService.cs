using DanceConnect.Server.Entities;

namespace DanceConnect.Server.Services
{
    public interface IInstructorService
    {
        Task<IQueryable<Instructor>> GetAll();
        Task<List<Instructor>> GetAllInstructorsAsync();
        Task<Instructor> GetInstructorByIdAsync(int id);
        Task<Instructor> AddInstructorAsync(Instructor instructor);
        Task<Instructor> UpdateInstructorAsync(Instructor instructor);
        Task<Instructor> ApproveInstructorAsync(int id);
        Task<Instructor> DeclineInstructorAsync(int id);
    }
}
