﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MohonkMountainHouse
{
    /// <summary>  
    /// An empty page that can be used on its own or navigated to within a Frame.  
    /// </summary>  
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void golfPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GolfPage));
        }

        private void trailsPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TrailsPage));
        }

        private void directionsPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DirectionsPage));
        }

        private void websitePageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebsitePage));
        }

        private void menuPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        private void bookingPageButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BookingPage));
        }
    }
}
