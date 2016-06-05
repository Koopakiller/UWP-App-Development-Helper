using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Koopakiller.Apps.UwpAppDevelopmentHelper.Converter;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class SingleFontIconViewModel : ViewModelBase
    {
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

        public IList<char> Chars { get; }

        public IList<string> Tags { get; }

        public string JoinedTags => string.Join(", ", this.Tags);

        public string Description { get; }

        public string EnumValue { get; set; }

        public ICommand CopyCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public char SelectedChar { get; set; }

        private void ExecuteCopy(string s)
        {
            var code = (string)new CharToHeyConverter().Convert(this.SelectedChar);
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


    }
}