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
    public sealed partial class WebsitePage : Page
    {
        public WebsitePage()
        {
            this.InitializeComponent();
            mohonkWebview.Navigate(new Uri("https://linktr.ee/mohonkmountainhouse"));
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void mohonkWebview_LoadCompleted(object sender, NavigationEventArgs e)
        {
            
        }

        private void emptyButtonClick()
        {

        }

        private void igButton_Click(object sender, RoutedEventArgs e)
        {
            mohonkWebview.Navigate(new Uri("https://www.instagram.com/mohonkmountainhouse"));
        }

        private void websiteButton_Click(object sender, RoutedEventArgs e)
        {
            mohonkWebview.Navigate(new Uri("https://www.mohonk.com/"));
        }

        private void fbButton_Click(object sender, RoutedEventArgs e)
        {
            mohonkWebview.Navigate(new Uri("https://www.facebook.com/Mohonk"));
        }

        private void blogButton_Click(object sender, RoutedEventArgs e)
        {
            mohonkWebview.Navigate(new Uri("https://www.mohonk.com/blog/"));
        }
    }
}
