using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.Services;

public interface IReportService
{
    Task<List<ReportData>> IntelReportCount();
}

public class ReportService(IIntelService<CybInt>? cybIntService, IIntelService<HumInt>? humitService,
        IIntelService<PersonOfInterest>? personOfInterestService, IIntelService<GeneralIntel>? generalIntelService)
    : IReportService
{
    public async Task<List<ReportData>> IntelReportCount()
    {

        List<ReportData> data = new List<ReportData>();
        var humit = await humitService?.GetAll();
        var cybint = await cybIntService?.GetAll();
        var intrest = await cybIntService?.GetAll();
        var generalIntelService = await personOfInterestService.GetAll();
        data.Add(new ReportData() { Id = 1, TypeBaseLine = typeof(HumInt), Count = humit.Count(), Description = "Human Int" });
        data.Add(new ReportData() { Id = 1, TypeBaseLine = typeof(CybInt), Count = cybint.Count(), Description = "Cyber Intel" });
        data.Add(new ReportData() { Id = 1, TypeBaseLine = typeof(PersonOfInterest), Count = intrest.Count(), Description = "Person Of Interest" });
        data.Add(new ReportData() { Id = 1, TypeBaseLine = typeof(GeneralIntel), Count = generalIntelService.Count(), Description = "General Intel" });
        return data;
    }

}