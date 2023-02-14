using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface ICaseService
    {
        Task<CaseModel> GetCase(string caseId);
        Task<IEnumerable<CaseModel>> GetCases();
        Task UpdateCase(CaseModel Case);
        Task CreateCase(CaseModel Case);
        Task DeleteCase(string caseId);
        Task<IEnumerable<CaseModel>> GetCasesByValue();
    }
}
