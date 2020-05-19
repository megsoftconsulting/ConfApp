using System.Threading.Tasks;
using Xamarin.Forms;

namespace ConfApp.Services
{
    public class PromptService : IPromptService
    {
        public async Task DisplayAlert(string title, string body, string buttonText)
        {
            await Application
                .Current
                .MainPage
                .DisplayAlert(title, body, buttonText);
        }

        public async ValueTask<bool> DisplayAlertWithYesAndNo(string title, string body, string yesText, string noText)
        {
            var answer = await Application
                .Current
                .MainPage
                .DisplayAlert(title, body, yesText, noText);

            return answer;
        }
    }
}