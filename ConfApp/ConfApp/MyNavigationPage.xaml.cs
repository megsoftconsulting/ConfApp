using Xamarin.Forms;

namespace ConfApp
{
    public partial class MyNavigationPage : NavigationPage
    {
        public MyNavigationPage()
        {
            InitializeComponent();
        }

        public MyNavigationPage(Page root):base(root)
        {
        }
    }
}
