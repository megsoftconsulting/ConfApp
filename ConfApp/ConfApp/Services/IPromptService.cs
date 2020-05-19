using System.Threading.Tasks;

namespace ConfApp.Services
{
    public interface IPromptService
    {
        Task DisplayAlert(string title, string body, string buttonText);
        ValueTask<bool> DisplayAlertWithYesAndNo(string title, string body, string yesText, string noText);
    }
}