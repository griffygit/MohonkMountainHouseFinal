using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class TrailsPage : Page
    {
        public TrailsPage()
        {
            this.InitializeComponent();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void easyTrailButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EasyTrailPage));
        }

        private void mediumTrailButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MediumTrailPage));
        }

        private void hardTrailButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HardTrailPage));
        }
    }
}
