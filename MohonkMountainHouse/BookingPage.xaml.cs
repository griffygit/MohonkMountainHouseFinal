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
    public sealed partial class BookingPage : Page
    {
        public BookingPage()
        {
            this.InitializeComponent();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        async private void reserveButton_Click(object sender, RoutedEventArgs e)
        {
            var startDate = startDatePicker.Date;
            var endDate = endDatePicker.Date;

            if (startDate == null || endDate == null)
            {
                await new ContentDialog
                {
                    Title = "Error",
                    Content = "Please select both a start and end date.",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }

            if (endDate < startDate)
            {
                await new ContentDialog
                {
                    Title = "Invalid Range",
                    Content = "End date must be after start date.",
                    CloseButtonText = "OK"
                }.ShowAsync();
                return;
            }

            // Confirmation dialog
            await new ContentDialog
            {
                Title = "Reservation Confirmed",
                Content = $"Thank you for your reservation from {startDate.Value:MMMM dd} to {endDate.Value:MMMM dd}.",
                CloseButtonText = "OK"
            }.ShowAsync();

            Frame.Navigate(typeof(MainPage));

        }
    }
}
