using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.Services;

public interface IReportService
{
    Task<List<ReportData>> IntelReportCount();
}

public class ReportService(IGlobalService globalService, IDocumentService documentService)
    : IReportService
{
    public async Task<List<ReportData>> IntelReportCount()
    {

        List<ReportData> data = new List<ReportData>();
        var dataGlobal = await globalService.GetAll();
        var docs = await documentService.GetAll();
        var rest = dataGlobal.GroupBy(x => x.IntelType);

        int tel = 0;
        foreach (var item in rest)
        {
            data.Add(new ReportData() { Id = ++tel, TypeBaseLine = item.Key, Count = item.Count(), Description = $"{item.Key} Int" });
        }

        var intelDocuments = docs as IntelDocument[] ?? docs.ToArray();
        if (intelDocuments.Any())
        {
            data.Add(new ReportData() { Id = ++tel, TypeBaseLine = TypeIntel.Other, Count = intelDocuments.Count(), Description = "Doncumnt Int" });
        }
       


        return data;
    }

}