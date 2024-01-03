using FluentValidation;
using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.validation;

public class NewsArticleValidator:AbstractValidator<NewsArticle>
{
    public NewsArticleValidator()
    {
    }
}