using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfApp.Speakers;

namespace ConfApp.Services
{
    public interface ISpeakerService
    {
        Task<SpeakerModel> GetSpeakerByIdAsync(string id);
        Task<IEnumerable<SpeakerModel>> GetSpeakersAsync();
    }

    public class SpeakerService : ISpeakerService
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
    }
}