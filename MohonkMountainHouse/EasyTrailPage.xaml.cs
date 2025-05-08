using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MohonkMountainHouse
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EasyTrailPage : Page
    {
        private MapIcon userIcon;
        private Geolocator geolocator;

        public EasyTrailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (geolocator != null)
            {
                geolocator.PositionChanged -= Geolocator_PositionChanged;
            }
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TrailsPage));
        }

        private void pictureButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PicturePage), "Easy");
        }

        private void mapLoaded(object sender, RoutedEventArgs e)
        {
            List<BasicGeoposition> trailPoints = new List<BasicGeoposition>
            {
                new BasicGeoposition { Latitude = 41.73733000, Longitude = -74.1974400 },
                new BasicGeoposition { Latitude = 41.73687, Longitude = -74.19406 },
                new BasicGeoposition { Latitude = 41.7404, Longitude = -74.18262 },
                new BasicGeoposition { Latitude = 41.75007, Longitude = -74.17317 },
                new BasicGeoposition { Latitude = 41.75825, Longitude = -74.16539 },
                new BasicGeoposition { Latitude = 41.75626, Longitude = -74.1714 },
                new BasicGeoposition { Latitude = 41.7551, Longitude = -74.17321 },
                new BasicGeoposition { Latitude = 41.75227, Longitude = -74.17584 },
                new BasicGeoposition { Latitude = 41.74716, Longitude = -74.18031 },
                new BasicGeoposition { Latitude = 41.7376746, Longitude = -74.195587 }
            };

            var path = new MapPolyline
            {
                Path = new Geopath(trailPoints),
                StrokeColor = Colors.Green,
                StrokeThickness = 4,
                ZIndex = 1
            };
            mapBox.MapElements.Add(path);

            ZoomToFitMultiple(trailPoints);

            StartTracking();

        }

        private void StartTracking()
        {
            geolocator = new Geolocator
            {
                DesiredAccuracyInMeters = 10,
                ReportInterval = 1000 
            };

            geolocator.PositionChanged += Geolocator_PositionChanged;

            userIcon = new MapIcon
            {
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                Title = "You",
                ZIndex = 10
            };

            mapBox.MapElements.Add(userIcon);
        }

        private async void ZoomToFitMultiple(List<BasicGeoposition> points)
        {
            if (points.Count < 2) return;

            double north = points.Max(p => p.Latitude);
            double south = points.Min(p => p.Latitude);
            double east = points.Max(p => p.Longitude);
            double west = points.Min(p => p.Longitude);

            var boundingBox = new GeoboundingBox(
                new BasicGeoposition { Latitude = north, Longitude = west },
                new BasicGeoposition { Latitude = south, Longitude = east });

            await mapBox.TrySetViewBoundsAsync(boundingBox, null, MapAnimationKind.Default);
        }

        private async void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            var pos = args.Position.Coordinate.Point;

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                userIcon.Location = pos;
            });
        }


    }
}
