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
            return Task.FromResult(GetData()
                .FirstOrDefault(s=> s.Id.Equals(id)));
        }

        public Task<IEnumerable<SpeakerModel>> GetSpeakersAsync()
        {
            return Task
                .FromResult(GetData());
        }

        private IEnumerable<SpeakerModel> GetData()
        {
            yield return new SpeakerModel("01","Jimmy", "Jansen", "Developer Advocate", "Grace", "IBM");
            yield return new SpeakerModel("02","Grace", "Jansen", "Developer Advocate", "Grace", "IBM");
            yield return new SpeakerModel("03","Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("04","Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("05","Jon", "Snow", "The true king", "Motica", "Winterfell");
            yield return new SpeakerModel("06","Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("07","Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("08","Jon", "Snow", "The true king", "Mofi", "Winterfell");
            yield return new SpeakerModel("08","Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("09","Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("10","Jon", "Snow", "The true king", "Jessica", "Winterfell");
            yield return new SpeakerModel("11","Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("12","Ned", "Stark", "The Hand of the King", "Mofi", "King's Landing");
            yield return new SpeakerModel("13","Jon", "Snow", "The true king", "Jessica", "Winterfell");
            yield return new SpeakerModel("14","Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("15","Ned", "Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("16","Jon", "Snow", "The true king", "Motica", "Winterfell");
            yield return new SpeakerModel("17","Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("18","Ned", " Stark", "The Hand of the King", "Scott", "King's Landing");
            yield return new SpeakerModel("18","Jon", " Snow", "The true king", "Grace", "Winterfell");
            yield return new SpeakerModel("19","Jamie", "Lanister", "The King Slayer", "Rob", "Casterly Rock");
            yield return new SpeakerModel("20","Ned", "Stark", "The Hand of the King", "Mofi", "King's Landing");
        }
    }
}