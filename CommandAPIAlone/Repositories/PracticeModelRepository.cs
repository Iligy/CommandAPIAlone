using CommandAPIAlone.Data;
using CommandAPIAlone.Interfaces;
using CommandAPIAlone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPIAlone.Repositories
{
    public class PracticeModelRepository : IPracticeModelRepository
    {
        private readonly MainDbContext _context;

        public PracticeModelRepository(MainDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PracticeModel>> GetAllPracticeModelsAsync()
        {
            return await _context.PracticeModels.ToListAsync();
        }

        public async Task<PracticeModel?> GetPracticeModelByIdAsync(int id)
        {
            return await _context.PracticeModels.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreatePracticeModelAsync(PracticeModel practiceModel)
        {
            if (practiceModel == null) 
            {
                throw new NullReferenceException(nameof(practiceModel));
            }

            await _context.PracticeModels.AddAsync(practiceModel);
        }

        public void DeletePracticeModel(PracticeModel practiceModel)
        {
            if (practiceModel == null)
            {
                throw new NullReferenceException(nameof(practiceModel));
            }

            _context.Remove(practiceModel);
        }

        public async Task UpdatePracticeModelAsync(PracticeModel practiceModel)
        {
            // EF elvégzi a munkát
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
