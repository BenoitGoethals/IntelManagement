using IntelVault.ApplicationCore.IntelData;
using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.ApplicationCore.validation;

namespace IntelVault.IntegrationTests
{
    [Trait("Category", "Integration")]
    public class IntelServiceTests : MongoDbTestBase<HumInt>
    {
        public IntelServiceTests()
        {
            _service = new IntelService<HumInt>(DbRepository, validator: new HumIntValidator());
        }

        private readonly IIntelService<HumInt> _service;

        [Trait("Category", "Integration")]
        [Fact()]
        public async void AddTest()
        {

            await _service.Add(GetHumit());
            DbRepository.GetAllAsync()?.Result?.FirstOrDefault().Should().BeOfType<HumInt>();

        }

        [Trait("Category", "Integration")]
        [Fact()]
        public async void UpdateTest()
        {
            HumInt? humInt = GetHumit();
            await _service.Add(humInt);

            DbRepository.GetAllAsync()?.Result?.FirstOrDefault().Should().BeOfType<HumInt>();
            humInt.AccessLevel = "changed";
            await _service.Update(humInt);
            DbRepository.GetAllAsync()?.Result?.FirstOrDefault()?.AccessLevel.Should().BeEquivalentTo("changed");
        }

        [Trait("Category", "Integration")]
        [Fact()]
        public async void DeleteTest()
        {
            HumInt? humInt = GetHumit();
            await _service.Add(humInt);
            var humInts = await DbRepository.GetAllAsync();
            humInts.FirstOrDefault().Should().BeOfType<HumInt>();

            await _service.Delete(humInt);

            var humIntsdel = DbRepository.GetAllAsync()?.Result;
            humIntsdel?.FirstOrDefault().Should().BeNull();
        }

        [Trait("Category", "Integration")]
        [Fact()]
        public async Task DeleteAllTest()
        {
            for (int i = 0; i < 20; i++)
            {
                await _service.Add(GetHumit());

            }
            DbRepository.GetAllAsync()?.Result?.Should().HaveCount(20);

            await _service.DeleteAll();

            DbRepository.GetAllAsync()?.Result?.FirstOrDefault().Should().BeNull();
        }

        [Trait("Category", "Integration")]
        [Fact()]
        public async void GetAllTest()
        {
            for (var i = 0; i < 20; i++)
            {
                await _service.Add(GetHumit());
            }

            _service.GetAll()?.Result?.Should().HaveCount(20);

        }


        private HumInt GetHumit()
        {
            return new HumInt()
            {
                Id = ObjectId.GenerateNewId(),
                AccessLevel = "test",
                AccuracyOfDetails = "sdfds",
                AssessmentAndAnalysis = "sfdsfds",
                ClassificationHandlingInstructions = "dsfdsf",
                ContactEmail = "sdfdsfds@fdfs.be",
                ContactName = "fsdfsd",
                ContactPhoneNumber = "dsfdsf",
                ContactTitle = "John Doe",
                ContextBackground = "sdfsdf",
                IntelligenceDetails = new List<ListItem>() , 
                LastContactDate = DateTime.Now,
                OperationalStatus = "asdsa",
                ReliabilityCredibility = "fdsds",
                ReliabilityRating = 100,
                SourceAffiliation = "dsad",
                SourceInformation = "sdfdsf",
                SourceName = "sdfdsf",
                SourceType = "sadsa",
                TimeLocation = "uk",
                HumIntType = HumIntType.Advisors
            };
        }
    }
}