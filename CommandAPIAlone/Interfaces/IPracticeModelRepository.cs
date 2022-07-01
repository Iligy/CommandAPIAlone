using CommandAPIAlone.Models;

namespace CommandAPIAlone.Interfaces
{
    public interface IPracticeModelRepository
    {
        Task<IEnumerable<PracticeModel>> GetAllPracticeModelsAsync();
        Task<PracticeModel?> GetPracticeModelByIdAsync(int id);
        Task CreatePracticeModelAsync(PracticeModel practiceModel);
        Task UpdatePracticeModelAsync(PracticeModel practiceModel);
        void DeletePracticeModel(PracticeModel practiceModel);
        Task<bool> SaveChangesAsync();
    }
}
