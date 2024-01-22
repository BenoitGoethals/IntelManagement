using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using LinqToTwitter.OAuth;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
using Quartz;
using System.Reactive.Subjects;
using Amazon.Runtime.Internal;
using LinqToTwitter;

namespace IntelVault.Worker.Bussines;

public class TwitterTask: RequestJob, IJob
{
    private readonly ILogger<TwitterTask>? _logger;
    private readonly PoolRequests? _poolRequests;
    private readonly IIntelService<IntelDocument>? _intelService;
    // Replace these with your own Twitter API credentials
    private string _apiKey = "zPIXWSoFS9dEeIhzuQKruQQVd";
    private string _apiSecretKey = "zCAjV5y6bNyINebLEBmJ6Hy0VEkV3COXeNwJnAVTaSgggaHito";
    private  string _accessToken = "1440997510273712128-N8iy5eHJlG8vAjLISotg3ykYWg0iiu";
    private string _accessTokenSecret = "KtjPbsI4M7nwqevgh0mGgisVXlcdAwycXjEwJJbBBq1ub";
    private readonly TwitterContext _twitterContext;


    public TwitterTask(ILogger<TwitterTask>? logger, PoolRequests? poolRequests, IIntelService<IntelDocument>? intelService)
    {
        _logger = logger;
        _poolRequests = poolRequests;
        _intelService = intelService;
        var auth = new SingleUserAuthorizer
        {
            CredentialStore = new SingleUserInMemoryCredentialStore
            {
                ConsumerKey = _apiKey,
                ConsumerSecret = _apiSecretKey,
                AccessToken = _accessToken,
                AccessTokenSecret = _accessTokenSecret
            },
            ForceLogin = true
        };
        auth.AuthorizeAsync();
         _twitterContext = new TwitterContext(auth);
        _twitterContext.Log = Console.Out;
    }


    private Task<List<Tweet>> GetTweets(string subject)
    {
        List<Tweet> tweets = new AutoConstructedList<Tweet>();
                    
        var searchResponse =
            (from search in _twitterContext.Search
                where search.Type == SearchType.Search &&
                      search.Query == subject &&
                      search.Count == 100 
                select search)
            .SingleOrDefault();

        // Display the tweets
        if (searchResponse == null && searchResponse?.Statuses == null)
        {
            _logger?.LogInformation("No tweets found.");
        }
       
        return Task.FromResult(tweets);
    }
    public override async Task Execute(IJobExecutionContext context)
    {
        
        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        CancellationToken token = cancelTokenSource.Token;
        try
        {
            if (context.JobDetail.JobDataMap["subjects"] is List<string> jobDetailJobData)
                foreach (var keyword in jobDetailJobData)
                {
                    var tweets = await GetTweets(keyword);

                    foreach (var tweet in tweets)
                    {
                        Console.WriteLine($"{tweet}: {tweet.Text}");
                    }
                }
        }
        catch (Exception e)
        {
            _logger?.LogError(e.Message);
        }
       
    }
}


