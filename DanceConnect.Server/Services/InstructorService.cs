using DanceConnect.Server.DataContext;
using DanceConnect.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace DanceConnect.Server.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly DanceConnectContext _context;

        public InstructorService(DanceConnectContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Instructor>> GetAll()
        {
            return  _context.Instructors.Include(x => x.AppUser).Include(x => x.Ratings).AsQueryable();
        }
        public async Task<Instructor> AddInstructorAsync(Instructor instructor)
        {
            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();
            return instructor;
        }

        public async Task<List<Instructor>> GetAllInstructorsAsync()
        {
            return await _context.Instructors.Include(x=>x.AppUser).Include(x=>x.Ratings).ToListAsync();
        }

        public async Task<Instructor> GetInstructorByIdAsync(int id)
        {
            return await _context.Instructors.Include(x=>x.AppUser).Include(x => x.Ratings).Where(x=>x.InstructorId ==id).FirstAsync()?? throw new Exception($"Instructor with id {id} does not exists");
        }

        public async Task<Instructor> DeclineInstructorAsync(int id)
        {
            var instructor = await GetInstructorByIdAsync(id);
            if (instructor == null)
            {
                throw new Exception("Instructor does not found");
            }
            instructor.ProfileStatus = Enums.ProfileStatus.Declined;
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
            return instructor;
        }

        public async Task<Instructor> ApproveInstructorAsync(int id)
        {
            var instructor = await GetInstructorByIdAsync(id);
            if (instructor == null)
            {
                throw new Exception("Instructor does not found");
            }
            instructor.ProfileStatus = Enums.ProfileStatus.Approved;
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
            return instructor;
        }

        public Task<Instructor> UpdateInstructorAsync(Instructor instructor)
        {
            throw new NotImplementedException();
        }

        
    }
}
