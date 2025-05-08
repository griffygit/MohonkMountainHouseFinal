using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using System.ServiceModel.Channels;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MohonkMountainHouse
{



    public sealed partial class PicturePage : Page
    {
        private SoftwareBitmap capturedSoftwareBitmap;
        private string previousPage;

        public PicturePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is string fromPage)
            {
                previousPage = fromPage;
            }
        }


        private async void captureButton_Click(object sender, RoutedEventArgs e)
        {
            var captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            // StorageFile photo = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/SplashScreen.png"));
            if (photo == null) return;

            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            capturedSoftwareBitmap = await decoder.GetSoftwareBitmapAsync();

            if (capturedSoftwareBitmap == null)
            {
                return;
            }

            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(
                capturedSoftwareBitmap,
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Premultiplied);

            SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
            await bitmapSource.SetBitmapAsync(softwareBitmapBGR8);

            imgBox.Source = bitmapSource;

        }

        private async void goBackButton_Click(object sender, RoutedEventArgs e)
        {

            MessageDialog dialog = new MessageDialog("Thank you for submitting your photo!");
            await dialog.ShowAsync();

            if (previousPage == "Easy")
                Frame.Navigate(typeof(EasyTrailPage));
            else if (previousPage == "Medium")
                Frame.Navigate(typeof(MediumTrailPage));
            else if (previousPage == "Hard")
                Frame.Navigate(typeof(HardTrailPage));
            else
                Frame.GoBack();
        }
    }
}
