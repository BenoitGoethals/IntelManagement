using System.Collections;
using FluentValidation;
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
    IIntelService<OpenSourceInt> openSourceService,
    IIntelService<Informant> InformantService)
    : IGlobalService
{
    private readonly IIntelService<SocialMedia>? _socialMediaService = socialMediaService;
    private readonly IIntelService<PersonOfInterest>? _personOfInterestService = personOfInterestService;
    private readonly IIntelService<HumInt?>? _humIntService = humIntService;
    private readonly IIntelService<CybInt>? _cybIntServiceService = cybIntServiceService;
    private readonly IIntelService<GeneralIntel>? _generalIntelService = generalIntelService;
    private readonly IIntelService<OpenSourceInt>? _openSourceService = openSourceService;
    private readonly IIntelService<Informant>? _informantService = InformantService;


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

    public async Task<IEnumerable<BaseIntel>> GetAll(int page, int pageSize, string field, string? sText)
    {
        List<BaseIntel> baseIntels = new List<BaseIntel>();

        IEnumerable<HumInt?> humit = await _humIntService?.GetAll(page, pageSize, field, sText);
        baseIntels.AddRange(humit);
        IEnumerable<SocialMedia> soc = await _socialMediaService?.GetAll(page, pageSize, field, sText);
        baseIntels.AddRange(soc);
        IEnumerable<PersonOfInterest> pers = await _personOfInterestService?.GetAll(page, pageSize, field, sText);
        baseIntels.AddRange(pers);
        IEnumerable<CybInt> cyb = await _cybIntServiceService?.GetAll(page, pageSize, field, sText);
        baseIntels.AddRange(cyb);
        IEnumerable<GeneralIntel> gen = await _generalIntelService?.GetAll(page, pageSize, field, sText);
        baseIntels.AddRange(gen);
        IEnumerable<OpenSourceInt> op = await _openSourceService?.GetAll(page, pageSize, field, sText);
        baseIntels.AddRange(op);
        IEnumerable<Informant> info = await _informantService?.GetAll(page, pageSize, field, sText);
        baseIntels.AddRange(info);

        return baseIntels.Skip(page).Take(pageSize);
    }

    public async Task<IEnumerable<BaseIntel>> GetAll()
    {
       
            List<BaseIntel> baseIntels = new List<BaseIntel>();

            var humit = await _humIntService?.GetAll();
            baseIntels.AddRange(humit);
            var soc = await _socialMediaService?.GetAll();
            baseIntels.AddRange(soc);
            var pers = await _personOfInterestService?.GetAll();
            baseIntels.AddRange(pers);
            var cyb = await _cybIntServiceService?.GetAll();
            baseIntels.AddRange(cyb);
            var gen = await _generalIntelService?.GetAll();
            baseIntels.AddRange(gen);
            var op = await _openSourceService?.GetAll();
            baseIntels.AddRange(op);
            var info = await _informantService?.GetAll();
            baseIntels.AddRange(info);

        return baseIntels;
    }

    public async Task<IEnumerable<Tuple<TypeIntel, long>>> GetAllCount()
    {
        List<Tuple<TypeIntel,long>> tuples=new List<Tuple<TypeIntel, long>>
        {
            new Tuple<TypeIntel, long>(TypeIntel.Humint, await _humIntService?.Count()),
            new Tuple<TypeIntel, long>(TypeIntel.SocialMedia, await _socialMediaService?.Count()),
            new Tuple<TypeIntel, long>(TypeIntel.CyberInt, await _cybIntServiceService?.Count()),
            new Tuple<TypeIntel, long>(TypeIntel.Other, await _generalIntelService?.Count()),
            new Tuple<TypeIntel, long>(TypeIntel.OpenSource, await _openSourceService?.Count()),
            new Tuple<TypeIntel, long>(TypeIntel.Informant, await _informantService?.Count())
        };


        return tuples;
    }
}