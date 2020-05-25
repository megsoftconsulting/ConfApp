using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ConfApp.Speakers;
using ConfApp.Talks;

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
            var speaker = GetData()
                .FirstOrDefault(s => s.Id.Equals(id));

            if (speaker != null)
                speaker.Talks =
                    new ObservableCollection<TalkModel>
                        (GetTalksBySpeakerId(speaker.Id));

            return Task.FromResult(speaker);
        }

        public Task<IEnumerable<SpeakerModel>> GetSpeakersAsync()
        {
            return Task
                .FromResult(GetData());
        }

        private IEnumerable<SpeakerModel> GetData()
        {
            yield return new SpeakerModel("01", "Jimmy", "Jansen", "Developer Advocate", "Grace", "BigGrace", "IBM");
            yield return new SpeakerModel("02", "Grace", "Jansen", "Developer Advocate", "Grace", "BigGrace", "IBM");
            yield return new SpeakerModel("03", "Jamie", "Lanister", "The King Slayer", "Rob", "Placeholder",
                "Casterly Rock");
            yield return new SpeakerModel("04", "Ned", "Stark", "The Hand of the King", "Scott", "Placeholder",
                "King's Landing");
            yield return new SpeakerModel("05", "Jon", "Snow", "The true king", "Motica", "Placeholder", "Winterfell");
            yield return new SpeakerModel("06", "Jamie", "Lanister", "The King Slayer", "Rob", "Placeholder",
                "Casterly Rock");
            yield return new SpeakerModel("07", "Ned", "Stark", "The Hand of the King", "Scott", "Placeholder",
                "King's Landing");
            yield return new SpeakerModel("08", "Jon", "Snow", "The true king", "Mofi", "Placeholder", "Winterfell");
            yield return new SpeakerModel("08", "Jamie", "Lanister", "The King Slayer", "Rob", "Placeholder",
                "Casterly Rock");
            yield return new SpeakerModel("09", "Ned", "Stark", "The Hand of the King", "Scott", "Placeholder",
                "King's Landing");
            yield return new SpeakerModel("10", "Jon", "Snow", "The true king", "Jessica", "Placeholder", "Winterfell");
            yield return new SpeakerModel("11", "Jamie", "Lanister", "The King Slayer", "Rob", "Placeholder",
                "Casterly Rock");
            yield return new SpeakerModel("12", "Ned", "Stark", "The Hand of the King", "Mofi", "Placeholder",
                "King's Landing");
            yield return new SpeakerModel("13", "Jon", "Snow", "The true king", "Jessica", "Placeholder", "Winterfell");
            yield return new SpeakerModel("14", "Jamie", "Lanister", "The King Slayer", "Rob", "Placeholder",
                "Casterly Rock");
            yield return new SpeakerModel("15", "Ned", "Stark", "The Hand of the King", "Scott", "Placeholder",
                "King's Landing");
            yield return new SpeakerModel("16", "Jon", "Snow", "The true king", "Motica", "Placeholder", "Winterfell");
            yield return new SpeakerModel("17", "Jamie", "Lanister", "The King Slayer", "Rob", "Placeholder",
                "Casterly Rock");
            yield return new SpeakerModel("18", "Ned", " Stark", "The Hand of the King", "Scott", "Placeholder",
                "King's Landing");
            yield return new SpeakerModel("18", "Jon", " Snow", "The true king", "Grace", "Placeholder", "Winterfell");
            yield return new SpeakerModel("19", "Jamie", "Lanister", "The King Slayer", "Rob", "Placeholder",
                "Casterly Rock");
            yield return new SpeakerModel("20", "Ned", "Stark", "The Hand of the King", "Mofi", "Placeholder",
                "King's Landing");
        }

        private IList<TalkModel> GetTalksBySpeakerId(string id)
        {
            var list = new List<TalkModel>
            {
                new TalkModel
                {
                    AvatarImage = "Placeholder",
                    Employer = "SomeCompany",
                    SpeakerName = "Some Speaker",
                    SessionType = "workshop",
                    Title = "Solving world problems with planet sized databases",
                    When = "10:00 am - 11:30 am",
                    Where = "Catalina Room"
                },
                new TalkModel
                {
                    AvatarImage = "Placeholder",
                    Employer = "Other Company",
                    SpeakerName = "The Same Speaker",
                    SessionType = "Talk",
                    Title = "Hello World",
                    When = "10:00 pm - 11:30 pm",
                    Where = "Catalina Room"
                }
            };
            return list;
        }
    }
}