using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class SingleFontIconViewModel : ViewModelBase
    {
        #region .ctor

        public SingleFontIconViewModel()
        {
            this.Tags = new ObservableCollection<string>();
            ((ObservableCollection<string>)this.Tags).CollectionChanged += (s, e) =>
            {
                // ReSharper disable once ExplicitCallerInfoArgument
                this.RaisePropertyChanged(nameof(this.JoinedTags));
            };
            this.CopyCommand = new RelayCommand<string>(this.ExecuteCopy);
            this.NavigateBackCommand = new RelayCommand(NavigateBack);
            this.SaveFontIconImageCommand = new RelayCommand<FontIcon>(async fi => await this.SaveFontImageIconAsync(fi));

            if (this.IsInDesignMode)
            {
                this.Chars.Add('\ue00a');
                this.Chars.Add('\ue082');
                this.Chars.Add('\ue0b4');
                this.Chars.Add('\ue0b5');
                this.Chars.Add('\ue113');
                this.Chars.Add('\ue1cf');
                this.Chars.Add('\ue249');
                this.Chars.Add('\ue735');
                this.Tags.Add("star");
                this.Tags.Add("favorite");
                this.Tags.Add("fill");
                this.EnumValue = "RatingStarFillLegacy";
                this.Description = "Solid star";
            }
        }

        public SingleFontIconViewModel(params char[] icons) : this()
        {
            this.Chars = new List<char>(icons);
            this.SelectedChar = this.Chars.FirstOrDefault();
        }

        public SingleFontIconViewModel(char[] icons, IList<string> tags, string description) : this(icons)
        {
            this.Tags = tags;
            this.Description = description;
        }

        #endregion

        #region Properties

        public IList<char> Chars { get; }

        public IList<string> Tags { get; }

        public string JoinedTags => string.Join(", ", this.Tags);

        public string Description { get; }

        public string EnumValue { get; set; }

        public ICommand CopyCommand { get; }

        public ICommand NavigateBackCommand { get; }

        public ICommand SaveFontIconImageCommand { get; }

        public char SelectedChar { get; set; }

        public IReadOnlyCollection<double> CommonFontSizes { get; } =
            new double[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 96, 144, 192, 288, 384, 576 };

        #endregion

        private void ExecuteCopy(string s)
        {
            var code = Convert.ToString(this.SelectedChar, 16);
            switch (s)
            {
                case "XAML": // XAML / XML
                    s = "&#x" + code + ";";
                    break;
                case "C#": // C# / C++ / F#
                    s = "\\u" + code + "";
                    break;
                case "VB": // Visual Basic
                    s = "ChrW(&H" + code + ")";
                    break;
                case "FontIconWithFontFamily":
                    s = "<FontIcon FontFamily=\"Segoe MDL2 Assets\" Glyph=\"&#x" + code + ";\" />";
                    break;
                case "FontIcon":
                    s = "<FontIcon Glyph=\"&#x" + code + ";\" />";
                    break;
                case "EnumValue":
                    s = this.EnumValue;
                    break;
                case "Description":
                    s = this.Description;
                    break;
                case "JoinedTags":
                    s = this.JoinedTags;
                    break;
                default:
                    throw new ArgumentException();
            }

            var dataPackage = new DataPackage();
            dataPackage.SetText(s);
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            Clipboard.SetContent(dataPackage);
        }

        private static void NavigateBack()
        {
            MainViewModel.Instance.Navigate(MainViewModel.FontIconViewModel);
        }

        private async Task SaveFontImageIconAsync(FontIcon fi)
        {
            var file = await this.PickImageFileAsync();
            if (file == null)
            {
                return;
            }
            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(fi, (int)fi.ActualWidth, (int)fi.ActualHeight);
            await this.SaveSoftwareBitmapToFile(rtb, file);
        }

        private async Task<StorageFile> PickImageFileAsync()
        {
            var fsp = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                SuggestedFileName = "MDL2 Asset " + CharToHex(this.SelectedChar),
            };
            fsp.FileTypeChoices.Add("Portable Network Graphic - PNG Image", new List<string>() { ".png" });
            fsp.FileTypeChoices.Add("JPEG Image", new List<string>() { ".jpg", ".jpeg" });
            fsp.FileTypeChoices.Add("TIFF Image", new List<string>() { ".tif", ".tiff" });
            fsp.FileTypeChoices.Add("BMP Image", new List<string>() { ".bmp" });
            fsp.FileTypeChoices.Add("GIF Image", new List<string>() { ".gif" });
            return await fsp.PickSaveFileAsync();
        }

        private async Task SaveSoftwareBitmapToFile(RenderTargetBitmap rtb, IStorageFile outputFile)
        {
            using (var stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                Guid encoderId;
                switch (outputFile.FileType.ToLower())
                {
                    case ".png":
                        encoderId = BitmapEncoder.PngEncoderId;
                        break;
                    case ".jpg":
                    case ".jpeg":
                        encoderId = BitmapEncoder.JpegEncoderId;
                        break;
                    case ".gif":
                        encoderId = BitmapEncoder.GifEncoderId;
                        break;
                    case ".tif":
                    case ".tiff":
                        encoderId = BitmapEncoder.TiffEncoderId;
                        break;
                    case ".bmp":
                        encoderId = BitmapEncoder.BmpEncoderId;
                        break;
                    default:
                        encoderId = BitmapEncoder.PngEncoderId;
                        break;
                }
                var encoder = await BitmapEncoder.CreateAsync(encoderId, stream);
                var bytes = (await rtb.GetPixelsAsync()).ToArray();
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)rtb.PixelWidth, (uint)rtb.PixelHeight, 96, 96, bytes);
                encoder.IsThumbnailGenerated = true;

                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception err) when (err.HResult == unchecked((int)0x88982F81))
                {
                    encoder.IsThumbnailGenerated = false;
                }

                if (encoder.IsThumbnailGenerated == false)
                {
                    await encoder.FlushAsync();
                }
            }
        }

        private static string CharToHex(char chr)
        {
            return Convert.ToString(chr, 16);
        }
    }
}