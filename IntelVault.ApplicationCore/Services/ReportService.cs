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
        var dataGlobal = await globalService.GetAllCount();
        var docs = await documentService.GetAll();
       var rest = dataGlobal.GroupBy(x => x.Item1).Select(group => group.ToList())
            .ToList();

        int tel = 0;
        foreach (var item in rest)
        {
            data.Add(new ReportData() { Id = ++tel, TypeBaseLine = item[0].Item1, Count = item[0].Item2, Description = $"{item[0].Item1} lomg" });
        }

        var intelDocuments = docs as IntelDocument[] ?? docs.ToArray();
        if (intelDocuments.Any())
        {
            data.Add(new ReportData() { Id = ++tel, TypeBaseLine = TypeIntel.Other, Count = intelDocuments.Count(), Description = "Doncumnt Int" });
        }
       


        return data;
    }

}