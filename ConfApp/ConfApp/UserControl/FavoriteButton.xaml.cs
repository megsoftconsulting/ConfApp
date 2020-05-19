using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfApp.UserControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteButton : ContentView
    {
        public FavoriteButton()
        {
            BindingContext = new FavoriteButtonViewModel();
            InitializeComponent();
        }
    }

    public class FavoriteButtonViewModel:BindableBase
    {
        public FavoriteButtonViewModel()
        {
            
        }
    }
}