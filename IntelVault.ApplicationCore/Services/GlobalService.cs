using System.Collections;
using IntelVault.ApplicationCore.IntelData;
using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.Infrastructure.repos;
using MongoDB.Bson;

namespace IntelVault.ApplicationCore.Services;

public class GlobalService(
    IIntelService<SocialMedia> socialMediaService,
    IIntelService<PersonOfInterest> personOfInterestService,
    IIntelService<HumInt> humIntService,
    IIntelService<CybInt> cybIntServiceService,
    IIntelService<GeneralIntel> generalIntelService,
    IIntelService<OpenSourceInt> openSourceService)
    : IGlobalService
{
    private readonly IIntelService<SocialMedia>? _socialMediaService = socialMediaService;
    private readonly IIntelService<PersonOfInterest>? _personOfInterestService = personOfInterestService;
    private readonly IIntelService<HumInt>? _humIntService = humIntService;
    private readonly IIntelService<CybInt>? _cybIntServiceService = cybIntServiceService;
    private readonly IIntelService<GeneralIntel>? _generalIntelService = generalIntelService;
    private readonly IIntelService<OpenSourceInt>? _openSourceService = openSourceService;


    public Task Add<T>(T entity) where T : BaseIntel
    {
        throw new NotImplementedException();
    }

    public Task Update<T>(T entity) where T : BaseIntel
    {
        throw new NotImplementedException();
    }

    public Task Delete<T>(T entity) where T : BaseIntel
    {
        throw new NotImplementedException();
    }

    public Task DeleteAll<T>()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAll<T>() where T : BaseIntel
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BaseIntel>> GetAll()
    {
        List<BaseIntel> baseIntels=
        [
            .. await _humIntService?.GetAll(),
            .. await _socialMediaService?.GetAll(),
           
            .. await _cybIntServiceService?.GetAll(),
            .. await _generalIntelService?.GetAll(),
            .. await _openSourceService?.GetAll(),
        ];
        return baseIntels;
    }
}