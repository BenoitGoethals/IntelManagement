using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using Quartz;
using PdfSharp.Pdf.Content;
using System.Text;
using Microsoft.Extensions.Logging;

namespace IntelVault.Worker.Bussines;


public class PdfTask : RequestJob, IJob
{

    private readonly ILogger<PdfTask>? _logger;
    private PoolRequests? _poolRequests;
    private IIntelService<IntelDocument>? _intelService;
    private FileSystemWatcher watcher;
    private string folderPath = "c:/temp";


    public PdfTask(ILogger<PdfTask>? logger, PoolRequests? poolRequests, IIntelService<IntelDocument>? intelService)
    {
        _logger = logger;
        _poolRequests = poolRequests;
        _intelService = intelService;

    }

    public override async Task Execute(IJobExecutionContext context)
    {
        await Task.Run(() =>
        {
            var files = Directory.GetFiles(folderPath);
            foreach (var file in files)
            {
                string ret = SearchPdfFile(file);
                
            }
        });
      
    }




    private string SearchPdfFile(string filePath)
    {
        try
        {
            using var doc = PdfReader.Open(filePath, PdfDocumentOpenMode.ReadOnly);
            StringBuilder ta = new StringBuilder();
            using (PdfSharpTextExtractor.Extractor extractor = new PdfSharpTextExtractor.Extractor(doc))
            {
                foreach (PdfPage page in doc.Pages)
                {
                    extractor.ExtractText(page, ta);
                }

            }
            return ta.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing PDF file: {e.FullPath}. Error: {ex.Message}");
        }
    }




}