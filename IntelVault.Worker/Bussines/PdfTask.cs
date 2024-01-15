using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using Quartz;
using PdfSharp.Pdf.Content;
using System.Text;
using IntelVault.Infrastructure.Workers;
using Microsoft.Extensions.Logging;
using NewsAPI;
using System.IO;
using System.Text.RegularExpressions;

namespace IntelVault.Worker.Bussines;


public class PdfTask(ILogger<PdfTask>? logger, PoolRequests? poolRequests, IIntelService<IntelDocument>? intelService)
    : RequestJob, IJob
{
    private PoolRequests? _poolRequests = poolRequests;

    private readonly string _folderPath = "c:/temp/pdf";


    public override async Task Execute(IJobExecutionContext context)
    {
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;
        var jobDetailJobData = context.JobDetail.JobDataMap[nameof(OpenSourceRequest)] as OpenSourceRequest;

        await Task.Run(() =>
        {
            try
            {
                var files = Directory.GetFiles(_folderPath);
                foreach (var file in files)
                {
                    string? ret = SearchPdfFile(file);
                    var doc = Analyse(filePath: file);

                    doc.LongDescription = ret;
                    doc.Keywords = GetSearchWords(jobDetailJobData?.KeyWords, ret);

                    intelService?.Add(doc);
                }
            }
            catch (Exception ex)
            {
               logger?.LogError(ex.Message);
            }
        
           
        }, token);

    }
    private  List<string> GetSearchWords(List<string>? text, string? totalText)
    {
       var words = new List<string>();
       if (text == null) return words;
       foreach (var word in text)
       {
           if (totalText != null && totalText.Contains(word))
           {
               words.Add(word);
           }
       }
       return words;
    }


    private IntelDocument Analyse(string filePath)
    {
        var doc = new IntelDocument
        {
            TimeCreated = DateTime.Now
        };
        using FileStream fsSource = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        doc.Content = UseMemoryStream(fsSource);
        doc.DocumentType = DocumentType.PDF;
        doc.FileName = filePath;
        return doc;

    }
    public byte[] UseMemoryStream(Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        var bytes = memoryStream.ToArray();
        return bytes;
    }

    private string? SearchPdfFile(string filePath)
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
            logger?.LogError($"Error processing PDF file: {filePath} Error: {ex.Message}");
        }

        return null;
    }




}