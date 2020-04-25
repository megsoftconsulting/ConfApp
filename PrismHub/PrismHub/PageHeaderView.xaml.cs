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
            "[Title]",
            BindingMode.TwoWay);

        public static readonly BindableProperty IsAvatarVisibleProperty = BindableProperty.Create(nameof(IsAvatarVisible),
            typeof(bool),
            typeof(PageHeaderView),
            false,
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

        public string Title
        {
            get =>
                GetValue(TitleProperty)
                    .ToString();
            set => SetValue(TitleProperty, value);
        }
    }
}