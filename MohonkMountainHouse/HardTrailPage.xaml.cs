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
    public sealed partial class HardTrailPage : Page
    {
        private MapIcon userIcon;
        private Geolocator geolocator;

        public HardTrailPage()
        {
            this.InitializeComponent();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TrailsPage));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (geolocator != null)
            {
                geolocator.PositionChanged -= Geolocator_PositionChanged;
            }
        }

        private void pictureButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PicturePage), "Hard");
        }

        private void mapLoaded(object sender, RoutedEventArgs e)
        {
            List<BasicGeoposition> trailPoints = new List<BasicGeoposition>
            {
                new BasicGeoposition { Latitude = 41.75632, Longitude = -74.13789 },
                new BasicGeoposition { Latitude = 41.75354, Longitude = -74.14522 },
                new BasicGeoposition { Latitude = 41.76226, Longitude = -74.15304 },
                new BasicGeoposition { Latitude = 41.7631028, Longitude = -74.1527687 },
                new BasicGeoposition { Latitude = 41.76295, Longitude = -74.15629 },
                new BasicGeoposition { Latitude = 41.76433, Longitude = -74.15582 },
                new BasicGeoposition { Latitude = 41.7681615, Longitude = -74.1558266 },
                new BasicGeoposition { Latitude = 41.76404, Longitude = -74.16049 },
                new BasicGeoposition { Latitude = 41.76164, Longitude = -74.16201 },
                new BasicGeoposition { Latitude = 41.75813, Longitude = -74.16215 },
                new BasicGeoposition { Latitude = 41.7500054, Longitude = -74.1466396 }
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
