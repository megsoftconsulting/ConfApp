using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfApp.Speakers;
using ConfApp.ViewModels;

namespace ConfApp.Services
{
    public interface ISpeakerService : IDataService<SpeakerModel>
    {
        Task<SpeakerModel> GetSpeakerByIdAsync(string id);
        Task<IEnumerable<SpeakerModel>> GetSpeakersAsync();
    }

    public class SpeakerService : DataServiceBase<SpeakerModel>, ISpeakerService
    {
        public Task<SpeakerModel> GetSpeakerByIdAsync(string id)
        {
            return Task.FromResult(GetData().First());
        }

        public Task<IEnumerable<SpeakerModel>> GetSpeakersAsync()
        {
            return Task
                .FromResult(GetData());
        }

        private IEnumerable<SpeakerModel> GetData()
        {
            yield return new SpeakerModel("Grace", "Jansen", "Developer Advocate", "Grace", "IBM");
            yield return new SpeakerModel("Grace", "Jansen", "Developer Advocate", "Grace", "IBM");
            yield return new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("Jon", "Snow", "The true king", "Motica", "Winterfell");
            yield return new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("Jon", "Snow", "The true king", "Mofi", "Winterfell");
            yield return new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("Jon", "Snow", "The true king", "Jessica", "Winterfell");
            yield return new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("Ned", "Stark", "The Hand of the King", "Mofi", "King's Landing");
            yield return new SpeakerModel("Jon", "Snow", "The true king", "Jessica", "Winterfell");
            yield return new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("Jon", "Snow", "The true king", "Motica", "Winterfell");
            yield return new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("Ned", " Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("Jon", " Snow", "The true king", "Grace", "Winterfell");
            yield return new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("Ned", "Stark", "The Hand of the King", "Mofi", "King's Landing");
        }

        public SpeakerService(IClient client)
            : base(client)
        {
        }

        public class SpeakerClientMock : ClientMock<SpeakerModel>
        {
            public SpeakerClientMock()
            {
                Items = new List<SpeakerModel>
                {
                    new SpeakerModel("Grace", "Jansen", "Developer Advocate", "Grace", "IBM"),
                    new SpeakerModel("Grace", "Jansen", "Developer Advocate", "Grace", "IBM"),
                    new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock"),
                    new SpeakerModel("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing"),
                    new SpeakerModel("Jon", "Snow", "The true king", "Motica", "Winterfell"),
                    new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock"),
                    new SpeakerModel("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing"),
                    new SpeakerModel("Jon", "Snow", "The true king", "Mofi", "Winterfell"),
                    new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock"),
                    new SpeakerModel("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing"),
                    new SpeakerModel("Jon", "Snow", "The true king", "Jessica", "Winterfell"),
                    new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock"),
                    new SpeakerModel("Ned", "Stark", "The Hand of the King", "Mofi", "King's Landing"),
                    new SpeakerModel("Jon", "Snow", "The true king", "Jessica", "Winterfell"),
                    new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock"),
                    new SpeakerModel("Ned", "Stark", "The Hand of the King", "Scott", "King's Landing"),
                    new SpeakerModel("Jon", "Snow", "The true king", "Motica", "Winterfell"),
                    new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock"),
                    new SpeakerModel("Ned", " Stark", "The Hand of the King", "Scott", "King's Landing"),
                    new SpeakerModel("Jon", " Snow", "The true king", "Grace", "Winterfell"),
                    new SpeakerModel("Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock"),
                    new SpeakerModel("Ned", "Stark", "The Hand of the King", "Mofi", "King's Landing")
                };
            }
        }
    }
}