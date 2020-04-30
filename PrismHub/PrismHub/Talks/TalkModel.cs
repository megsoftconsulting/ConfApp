using Prism.Mvvm;

namespace ConfApp.Talks
{
    public class TalkModel : BindableBase
    {
        public string Employer { get; set; }
        public string Title { get; set; }
        public string SpeakerName { get; set; }
        public string When { get; set; }
        public string Where { get; set; }
        public string AvatarImage { get; set; }
        public string SessionType { get; set; }
    }
}