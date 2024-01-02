using Amazon.Runtime;
using IntelVault.Worker.model;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using Quartz;
using RestSharp;
using System.Threading;
using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using static System.Net.WebRequestMethods;

namespace IntelVault.Worker.Bussines;
[DisallowConcurrentExecution]
public class RestApiScrapperJob : RequestJob, IJob
{
    
    private readonly ILogger _logger;
    private OpenSourceRequest _openSourceRequest;
    private IIntelService<NewsArticle> IntelService;

    /// <inheritdoc />
    public RestApiScrapperJob(ILogger logger, OpenSourceRequest openSourceRequest, IIntelService<NewsArticle> intelService)
    {
        _logger = logger;
        _openSourceRequest = openSourceRequest;
        IntelService = intelService;
    }

    public override async Task Execute(IJobExecutionContext context)
    { //api  e00c6f3f1ff748c0b1604c6dc07c5fac
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;

        
        var jobDetailJobData = context.JobDetail.JobDataMap[nameof(OpenSourceRequest)] as OpenSourceRequest;
        _openSourceRequest = jobDetailJobData;
        //var client = new RestClient();
        //var response = await client.ExecuteGetAsync(new RestRequest(){Resource = "https://newsapi.org/v2/everything?q=tesla&from=2023-12-02&sortBy=publishedAt&apiKey=e00c6f3f1ff748c0b1604c6dc07c5fac" }, cancellationToken: token);
        var newsApiClient = new NewsApiClient("e00c6f3f1ff748c0b1604c6dc07c5fac");
        if (jobDetailJobData?.KeyWords != null)
            foreach (var key in jobDetailJobData.KeyWords)
            {
                var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
                {
                    Q = key,
                    SortBy = SortBys.Popularity,
                    Language = Languages.NL,
                    
                });
                if (articlesResponse.Status == Statuses.Ok)
                {
                    // total results found  
                    Console.WriteLine(articlesResponse.TotalResults);
                    // here's the first 20
                    foreach (var article in articlesResponse.Articles)
                    {
                        // title
                        Console.WriteLine(article.Title);
                        // author
                        Console.WriteLine(article.Author);
                        // description
                        Console.WriteLine(article.Description);
                        // url
                        Console.WriteLine(article.Url);
                        // published at
                        Console.WriteLine(article.PublishedAt);
                    }
                }
            }
        // init with your API key
       
       

        
        await Task.Delay(1000);

    }

}