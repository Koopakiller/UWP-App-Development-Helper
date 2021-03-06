using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using GalaSoft.MvvmLight.Command;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Converter;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Helper;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Model;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class SingleFontIconViewModel : ViewModelBase, IHistoryItemTarget
    {
        private IList<char> _chars;

        #region .ctor

        public SingleFontIconViewModel()
        {
            this.Tags = new ObservableCollection<string>();
            ((ObservableCollection<string>)this.Tags).CollectionChanged += (s, e) =>
            {
                // ReSharper disable once ExplicitCallerInfoArgument
                this.RaisePropertyChanged(nameof(this.JoinedTags));
            };
            this.Chars = new List<char>();
            this.CopyCommand = new RelayCommand<string>(this.ExecuteCopy);
            this.NavigateBackCommand = new RelayCommand(this.NavigateBack);
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

        #endregion

        #region Properties

        public IList<char> Chars
        {
            get { return this._chars; }
            set
            {
                this._chars = value;
                this.RaisePropertyChanged();
                if (this._chars != null)
                {
                    this.SelectedChar = this.Chars.FirstOrDefault();
                }
            }
        }

        public IList<string> Tags { get; set; }

        public string JoinedTags => string.Join(", ", this.Tags);

        public string Description { get; set; }

        public string EnumValue { get; set; }

        public char SelectedChar { get; set; }

        public ICommand CopyCommand { get; }

        public ICommand NavigateBackCommand { get; }

        public ICommand SaveFontIconImageCommand { get; }

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

        private void NavigateBack()
        {
            NavigationHelper.NavigateToExisting(this.Caller);
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

        #region IHistoryItemTarget

        public XElement Serialize()
        {
            var cth = new CharToHexConverter();
            var el = new XElement(nameof(SingleFontIconViewModel));
            el.Add(new XElement("Chars", this.Chars.Select(x => new XElement("Char", cth.Convert(x)))));
            el.Add(new XElement("Tags", this.Tags.Select(x => new XElement("Tag", x))));
            el.Add(new XElement("Description", this.Description));
            return el;
        }

        public void Load(XElement data)
        {
            var cth = new CharToHexConverter();
            this.Chars.Clear();
            foreach (var chr in data.Element("Chars").Elements("Char").Select(x => cth.ConvertBack(x.Value)))
            {
                this.Chars.Add(chr);
            }
            this.Tags.Clear();
            foreach (var tag in data.Element("Tags").Elements("Tag").Select(x => x.Value))
            {
                this.Tags.Add(tag);
            }
            this.Description = data.Element("Description").Value;
        }

        public ViewModelBase PreviewViewModel
        {
            get
            {
                var vm = new SingleFontIconPreviewViewModel()
                {
                    Char = this.Chars.FirstOrDefault()
                };
                return vm;
            }
        }


        #endregion
    }
}