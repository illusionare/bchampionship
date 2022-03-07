using System.Globalization;

namespace BeardChamp.Data
{
    /// <summary>
    /// Mock for data retreiving
    /// </summary>
    public class CompetitionRepositoryMock : ICompetitionRepository
    {
        private const string Format = "yyyy-MM-dd HH:mm:ss zzz";
        private readonly List<Competition> _competitionList = new List<Competition>() {
            new Competition() {
                Id = 0,
                EventStartDate = DateTimeOffset.ParseExact("2012-02-12 12:00:00 +02:00",Format, CultureInfo.InvariantCulture.DateTimeFormat),
                Created = DateTimeOffset.Now,
                Name = "Beard & Moustache Competition",
                Address = "Minsk, Belarus",
                LogoFileName = @"minsk.png",
            },
            new Competition() {
                Id = 0, 
                EventStartDate = DateTimeOffset.ParseExact("2019-05-17 12:00:00 +01:00",Format, CultureInfo.InvariantCulture.DateTimeFormat), 
                Created = DateTimeOffset.Now,
                Name = "World Beard Moustache Championship 2019",
                Address = "Antwerp, Belgium",
                LogoFileName = @"",
            },
            new Competition() {
                Id = 0,
                EventStartDate = DateTimeOffset.ParseExact("2017-09-01 12:00:00 -03:00",Format, CultureInfo.InvariantCulture.DateTimeFormat),
                Created = DateTimeOffset.Now,
                Name = "World Beard Moustache Championship 2017",
                Address = "Austin, Texas",
                LogoFileName = @"",
                OfficialPageURI = "https://www.austinfacialhairclub.com/"
            },
            new Competition() {
                Id = 0,
                EventStartDate = DateTimeOffset.ParseExact("2011-05-16 12:00:00 +01:00",Format, CultureInfo.InvariantCulture.DateTimeFormat),
                Created = DateTimeOffset.Now,
                Name = "World Beard Moustache Championship 2011",
                Address = "Trondheim, Norway",
                LogoFileName = @"",
                OfficialPageURI = ""
            }

        };
        public async Task Delete(int id)
        {
            await Task.Run(() => {
                var itemToDelete = _competitionList.First(item => item.Id == id);
                _competitionList.Remove(itemToDelete);
            });
        }

        public async Task<Competition> Get(int id)
        {
            return await Task.Run(() => _competitionList.First(c => c.Id == id));
        }

        public async Task<List<Competition>> GetAll()
        {
            return await Task.Run(() => _competitionList);
        }

        public async Task<Competition> Update(Competition competition)
        {
            return await Task.Run(() => {
                var itemToUpdate = _competitionList.First(item => item.Id == competition.Id);
                itemToUpdate.EventStartDate = competition.EventStartDate;
                itemToUpdate.Updated = DateTimeOffset.Now;
                itemToUpdate.Name = competition.Name;
                itemToUpdate.Address = competition.Address;
                itemToUpdate.OfficialPageURI = competition.OfficialPageURI;
                itemToUpdate.LogoFileName = competition.LogoFileName;
                itemToUpdate.AnnonceDate = competition.AnnonceDate;
                return itemToUpdate;
            });
        }
    }
}
