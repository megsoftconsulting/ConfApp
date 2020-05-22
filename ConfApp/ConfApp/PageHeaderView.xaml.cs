using System.Windows.Input;
using Prism.Commands;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ConfApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHeaderView : ContentView
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title",
            typeof(string),
            typeof(PageHeaderView),
            "",
            BindingMode.TwoWay);

        public static readonly BindableProperty IsAvatarVisibleProperty = BindableProperty.Create(
            "IsAvatarVisible",
            typeof(bool),
            typeof(PageHeaderView),
            false,
            BindingMode.TwoWay);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create("InternalCommand",
            typeof(ICommand),
            typeof(PageHeaderView),
            new DelegateCommand(() => { }),
            BindingMode.TwoWay);


        public PageHeaderView()
        {
            InitializeComponent();
        }

        public bool IsAvatarVisible
        {
            get =>
                (bool) GetValue(IsAvatarVisibleProperty);
            set => SetValue(IsAvatarVisibleProperty, value);
        }

        public ICommand Command
        {
            get =>
                (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public string Title
        {
            get =>
                GetValue(TitleProperty)
                    .ToString();
            set => SetValue(TitleProperty, value);
        }
    }
}