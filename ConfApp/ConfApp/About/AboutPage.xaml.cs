using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace ConfApp.About
{
    public partial class AboutPage : ContentPage, IDestructible
    {
        public AboutPage()
        {
            InitializeComponent();
            map.IsIndoorEnabled = true;
            map.MyLocationEnabled = true;
            map.UiSettings.MapToolbarEnabled = true;
            map.UiSettings.CompassEnabled = true;
          


        }


        public void Destroy()
        {
            if (!(BindingContext is AboutViewModel context)) return;
            context.PropertyChanged -= OnPropertyChanged;
            context.Polygons.CollectionChanged -= OnPolygonsChanged;
            context.Circles.CollectionChanged -= OnCirclesChanged;
        }

        private void OnPolygonsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                foreach (var eNewItem in e.NewItems) map.Polygons.Add(eNewItem as Polygon);

                foreach (var eOldItem in e.OldItems) map.Polygons.Remove(eOldItem as Polygon);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void OnCirclesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            try
            {
                foreach (var eNewItem in e.NewItems) map.Circles.Add(eNewItem as Circle);

                foreach (var eOldItem in e.OldItems) map.Circles.Remove(eOldItem as Circle);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        protected override void OnBindingContextChanged()
        {
            if (!(BindingContext is AboutViewModel context)) return;
            context.PropertyChanged += OnPropertyChanged;
            context.Polygons.CollectionChanged += OnPolygonsChanged;
            context.Circles.CollectionChanged += OnCirclesChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "RegionSpan") map.MoveToRegion((BindingContext as AboutViewModel)?.RegionSpan);
        }
    }
}