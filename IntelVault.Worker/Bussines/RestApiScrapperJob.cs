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
using Microsoft.Extensions.Configuration.UserSecrets;
namespace IntelVault.Worker.Bussines;

public class RestApiScrapperJob : RequestJob, IJob
{

    private readonly ILogger? _logger;
    private PoolRequests? _poolRequests;
    private IIntelService<NewsArticle>? _intelService;
    private readonly string? _apikeynews;
    /// <inheritdoc />
    public RestApiScrapperJob(ILogger<RestApiScrapperJob>? logger, PoolRequests? poolRequests, IIntelService<NewsArticle>? intelService)
    {
        _logger = logger;
        _poolRequests = poolRequests;
        _intelService = intelService;
        var pik = new ConfigurationBuilder().AddUserSecrets<SecretApiSetting>().Build();
        _apikeynews = pik["Intelvault:NewsApi"];

    }


    public override async Task Execute(IJobExecutionContext context)
    {
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;
        var jobDetailJobData = context.JobDetail.JobDataMap[nameof(OpenSourceRequest)] as OpenSourceRequest;
        var newsApiClient = new NewsApiClient(_apikeynews);
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
                    foreach (var article in articlesResponse.Articles)
                    {
                        if (_intelService != null)
                        {
                            await _intelService.Add(new NewsArticle()
                            {
                                Author = article.Author,
                                PublishedDate = article.PublishedAt,
                                Source = article.Source.Name,
                                ShortContent = article.Description,
                                keywords = [key],
                                Title = article.Title,
                                IntelType = TypeIntel.NewsArticle,
                                Content = article.Content,
                                Url = article.Url,
                                CreatedDtg = DateTime.Now,
                                
                            });
                        }

                    }
                }
            }
        await Task.Delay(1000, token);
    }
}