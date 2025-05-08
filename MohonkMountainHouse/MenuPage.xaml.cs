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
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
            this.InitializeComponent();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        /*
        private void breakfastButton_Click(object sender, RoutedEventArgs e)
        {
            menuBox.Text = "Pancakes \n$5.99\n" +
                "Omelette \n$7.99\n" +
                "French Toast \n$6.99\n" +
                "Sausage Burrito \n$8.99\n";
        }

        private void lunchButton_Click(object sender, RoutedEventArgs e)
        {
            menuBox.Text = "Caesar Salad \n$9.99\n" +
                "Grilled Cheese \n$7.99\n" +
                "Chicken Sandwich \n$10.99\n" +
                "Veggie Wrap \n$8.99\n";
        }

        private void dinnerButton_Click(object sender, RoutedEventArgs e)
        {
            menuBox.Text = "Ribeye Steak \n$24.99\n" +
                "Salmon Fillet \n$22.99\n" +
                "Vegetarian Pasta \n$18.99\n" +
                "Chicken Alfredo \n$19.99\n";
        } */

        private void breakfastButton_Click(object sender, RoutedEventArgs e)
        {
            menuStackPanel.Children.Clear();
            AddMenuItem("Pancakes", 5.99);
            AddMenuItem("Omelette", 7.99);
            AddMenuItem("French Toast", 6.99);
            AddMenuItem("Sausage Burrito", 8.99);
        }

        private void lunchButton_Click(object sender, RoutedEventArgs e)
        {
            menuStackPanel.Children.Clear();
            AddMenuItem("Caesar Salad", 9.99);
            AddMenuItem("Grilled Cheese", 7.99);
            AddMenuItem("Chicken Cutlet", 10.99);
            AddMenuItem("Veggie Wrap", 8.99);
        }

        private void dinnerButton_Click(object sender, RoutedEventArgs e)
        {
            menuStackPanel.Children.Clear();
            AddMenuItem("Ribeye Steak", 24.99);
            AddMenuItem("Salmon Fillet", 22.99);
            AddMenuItem("Vegetarian Pasta", 18.99);
            AddMenuItem("Chicken Alfredo", 19.99);
        }

        private void AddMenuItem(string name, double price)
        {
            CheckBox checkBox = new CheckBox
            {
                Content = $"{name} \n${price:F2}",
                FontSize = 70,
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("Felix Titling"),
                Tag = price
            };
            menuStackPanel.Children.Add(checkBox);
        }

        private async void SubmitOrder_Click(object sender, RoutedEventArgs e)
        {
            double total = 0;
            List<string> selectedItems = new List<string>();

            foreach (var item in menuStackPanel.Children)
            {
                if (item is CheckBox checkBox && checkBox.IsChecked == true)
                {
                    selectedItems.Add(checkBox.Content.ToString());
                    total += (double)checkBox.Tag;
                }
            }

            string message;
            if (selectedItems.Count == 0)
            {
                message = "No items selected.";
            }
            else
            {
                message = "Order Received:\n\n";
                message += string.Join("\n", selectedItems);
                message += $"\n\nTotal: ${total:F2}";
            }

            ContentDialog receiptDialog = new ContentDialog
            {
                Title = "Your Receipt",
                Content = message,
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close
            };

            await receiptDialog.ShowAsync();
        }

    }
}
