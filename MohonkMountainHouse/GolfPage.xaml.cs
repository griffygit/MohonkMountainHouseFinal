using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace MohonkMountainHouse
{

    public sealed partial class GolfPage : Page
    {

        private List<int> parList = new List<int> { 4, 3, 5, 4, 4, 3, 4, 5, 4 };
        private List<TextBox> scoreInputs = new List<TextBox>();
        private List<TextBox> puttInputs = new List<TextBox>();


        public GolfPage()
        {
            this.InitializeComponent();
            GenerateRows();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void GenerateRows()
        {
            for (int i = 0; i < 9; i++)
            {
                Grid row = new Grid
                {
                    Margin = new Thickness(0, 5, 0, 5),
                    MinHeight = 100  // adjust as needed
                };

                row.ColumnDefinitions.Add(new ColumnDefinition());
                row.ColumnDefinitions.Add(new ColumnDefinition());
                row.ColumnDefinitions.Add(new ColumnDefinition());
                row.ColumnDefinitions.Add(new ColumnDefinition());

                row.Children.Add(new TextBlock { Text = (i + 1).ToString(), FontSize = 22, VerticalAlignment = VerticalAlignment.Center });
                row.Children.Add(new TextBlock { Text = parList[i].ToString(), FontSize = 22, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 0, 0) });
                TextBox scoreBox = new TextBox
                {
                    Height = 40,
                    FontSize = 22,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5, 0, 0, 0),
                    InputScope = new InputScope { Names = { new InputScopeName(InputScopeNameValue.Number) } }
                };
                TextBox puttBox = new TextBox
                {
                    Height = 40,
                    FontSize = 22,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(5, 0, 0, 0),
                    InputScope = new InputScope { Names = { new InputScopeName(InputScopeNameValue.Number) } }
                };

                Grid.SetColumn((FrameworkElement)row.Children[0], 0);
                Grid.SetColumn((FrameworkElement)row.Children[1], 1);
                Grid.SetColumn(scoreBox, 2);
                Grid.SetColumn(puttBox, 3);

                row.Children.Add(scoreBox);
                row.Children.Add(puttBox);

                ScoreRows.Items.Add(row);
                scoreInputs.Add(scoreBox);
                puttInputs.Add(puttBox);
            }
        }

            private async void FinishRound_Click(object sender, RoutedEventArgs e)
        {
            int totalScore = 0;
            int totalPutts = 0;
            int totalPar = 0;

            for (int i = 0; i < 9; i++)
            {
                int score = int.TryParse(scoreInputs[i].Text, out int s) ? s : 0;
                int putts = int.TryParse(puttInputs[i].Text, out int p) ? p : 0;
                totalScore += score;
                totalPutts += putts;
                totalPar += parList[i];
            }

            int scoreDiff = totalScore - totalPar;
            string result = $"Final Score: {totalScore} ({(scoreDiff >= 0 ? "+" + scoreDiff : scoreDiff.ToString())})\nTotal Putts: {totalPutts}";

            var dialog = new MessageDialog(result, "Round Complete");
            await dialog.ShowAsync();
        }

    }
}
