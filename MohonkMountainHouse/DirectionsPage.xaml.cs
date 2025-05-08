using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MohonkMountainHouse
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DirectionsPage : Page
    {
        public DirectionsPage()
        {
            this.InitializeComponent();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void mapLoaded(object sender, RoutedEventArgs e)
        {
            getLocation();
        }

        private async void getLocation()
        {
            Geolocator gl = new Geolocator
            {
                DesiredAccuracy = PositionAccuracy.High
            };
            try
            {
                Geoposition gp = await gl.GetGeopositionAsync(
                    maximumAge: TimeSpan.FromMinutes(1),
                    timeout: TimeSpan.FromSeconds(20));

                double userLat = gp.Coordinate.Point.Position.Latitude;
                double userLon = gp.Coordinate.Point.Position.Longitude;

                // Destination coords
                double destLat = 41.76845;
                double destLon = -74.15592;

                // Add pushpins
                AddPushpin(userLat, userLon, Colors.Blue);         // You
                AddPushpin(destLat, destLon, Colors.Red);          // Destination

                // Center map between them
                ZoomToFit(userLat, userLon, destLat, destLon);
            }
            catch (Exception e)
            {
                message(e.Message, "ERROR!");
            }
        }

        public void AddPushpin(double lat, double lon, Color c)
        {
            BasicGeoposition location = new BasicGeoposition();
            location.Latitude = lat;
            location.Longitude = lon;
            var pin = new Ellipse()
            {
                Fill = new SolidColorBrush(c),
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = 1,
                Width = 40,
                Height = 40,
            };
            pin.Tapped += pin_Tapped;
            Windows.UI.Xaml.Controls.Maps.MapControl.SetLocation(pin, new Geopoint(location));
            directionsMap.Children.Add(pin);
        }

        private async void message(string body, string title)
        {
            var dlg = new MessageDialog(
            string.Format(body), title);
            try
            {
                await dlg.ShowAsync();
            }
            catch (Exception) { }
        }

        void pin_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            message("This is your location.", "");
        }


        private async void ZoomToFit(double lat1, double lon1, double lat2, double lon2)
        {
            var north = Math.Max(lat1, lat2);
            var south = Math.Min(lat1, lat2);
            var east = Math.Max(lon1, lon2);
            var west = Math.Min(lon1, lon2);

            var boundingBox = new GeoboundingBox(
                new BasicGeoposition { Latitude = north, Longitude = west },
                new BasicGeoposition { Latitude = south, Longitude = east });

            await directionsMap.TrySetViewBoundsAsync(boundingBox, null, MapAnimationKind.Default);
        }


        private void slider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (slider != null)
                directionsMap.ZoomLevel = (int)slider.Value;
        }


    }
}
