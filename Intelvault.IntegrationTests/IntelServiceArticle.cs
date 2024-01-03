using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.ApplicationCore.validation;
using NuGet.Protocol.Core.Types;

namespace IntelVault.IntegrationTests;
[Trait("Category", "Integration")]
public class IntelServiceArticleTest:MongoDbTestBase<NewsArticle>
{
    public IntelServiceArticleTest()
    {
        _service = new IntelService<NewsArticle>(DbRepository, validator: new NewsArticleValidator());
    }
    private readonly IIntelService<NewsArticle> _service;

    [Trait("Category", "Integration")]
    [Fact()]
    public async void AddTest()
    {

        await _service.Add(GetNewsArticle());
        DbRepository.GetAllAsync()?.Result?.FirstOrDefault().Should().BeOfType<NewsArticle>();
    }

    private NewsArticle GetNewsArticle()
    {
        return new NewsArticle()
        {
            Content = "test",
            Author = "testll",
            IntelType = TypeIntel.NewsArticle,
            PublishedDate = DateTime.Now,
            ShortContent = "fdsfdsf",
            Source = "ww.google.be",
            keywords = ["lul"],
            Title = "testcaseAuthorTitle"
        };
    }

    [Trait("Category", "Integration")]
    [Fact()]
    public async void UpdateTest()
    {
        NewsArticle? newsArticle = GetNewsArticle();
        await _service.Add(newsArticle);

        DbRepository.GetAllAsync()?.Result?.FirstOrDefault().Should().BeOfType<NewsArticle>();
        newsArticle.Author = "changed";
        await _service.Update(newsArticle);
        DbRepository.GetAllAsync()?.Result?.FirstOrDefault()?.Author.Should().BeEquivalentTo("changed");
    }

    [Trait("Category", "Integration")]
    [Fact()]
    public async void DeleteTest()
    {
        NewsArticle? newsArticle = GetNewsArticle();
        await _service.Add(newsArticle);
        var newsArticles = await DbRepository.GetAllAsync();
        newsArticles.FirstOrDefault().Should().BeOfType<NewsArticle>();

        await _service.Delete(newsArticle);

        var newsArticlesdel = DbRepository.GetAllAsync()?.Result;
        newsArticlesdel?.FirstOrDefault().Should().BeNull();
    }

    [Trait("Category", "Integration")]
    [Fact()]
    public async Task DeleteAllTest()
    {
        for (int i = 0; i < 20; i++)
        {
            await _service.Add(GetNewsArticle());

        }
        DbRepository.GetAllAsync()?.Result?.Should().HaveCount(20);

        await _service.DeleteAll();

        DbRepository.GetAllAsync()?.Result?.FirstOrDefault().Should().BeNull();
    }

    [Trait("Category", "Integration")]
    [Fact()]
    public async Task CountTest()
    {
        for (int i = 0; i < 20; i++)
        {
            await _service.Add(GetNewsArticle());
        }

        DbRepository.Count().Result.Should().Be(20);
        _service.Count().Result.Should().Be(20);
        await _service.DeleteAll();

        DbRepository.GetAllAsync()?.Result?.FirstOrDefault().Should().BeNull();
    }

    [Trait("Category", "Integration")]
    [Fact()]
    public async void GetAllTest()
    {
        for (var i = 0; i < 20; i++)
        {
            await _service.Add(GetNewsArticle());
        }

        _service.GetAll()?.Result?.Should().HaveCount(20);

    }
    [Trait("Category", "Integration")]
    [Fact()]
    public async void GetAllWithSearchTextTest()
    {
        for (var i = 0; i < 15; i++)
        {
            await _service.Add(GetNewsArticle());
        }

        _service.GetAll(1, 10, nameof(NewsArticle.Title), "testcaseAuthorTitle")?.Result?.Should().HaveCount(10);

    }
}