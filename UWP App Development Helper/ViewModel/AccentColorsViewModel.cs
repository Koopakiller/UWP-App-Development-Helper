﻿using System.Collections.ObjectModel;

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.ViewModel
{
    public class AccentColorsViewModel : HeaderViewModelBase
    {
        public AccentColorsViewModel()
        {
            this.Header = "Accent Colors";

            //Source: https://msdn.microsoft.com/en-us/windows/uwp/style/color
            this.DefaultAccentColors = new ObservableCollection<ColorViewModel>()
            {
                new ColorViewModel(0xFF, 0xFF, 0xB9, 0x00),
                new ColorViewModel(0xFF, 0xE7, 0x48, 0x56),
                new ColorViewModel(0xFF, 0x00, 0x78, 0xd7),
                new ColorViewModel(0xFF, 0x00, 0x99, 0xbc),
                new ColorViewModel(0xFF, 0x7a, 0x75, 0x74),
                new ColorViewModel(0xFF, 0x76, 0x76, 0x76),

                new ColorViewModel(0xFF, 0xff, 0x8c, 0x00),
                new ColorViewModel(0xFF, 0xe8, 0x11, 0x23),
                new ColorViewModel(0xFF, 0x00, 0x63, 0xb1),
                new ColorViewModel(0xFF, 0x2d, 0x7d, 0x9a),
                new ColorViewModel(0xFF, 0x5d, 0x5a, 0x58),
                new ColorViewModel(0xFF, 0x4c, 0x4a, 0x48),

                new ColorViewModel(0xFF, 0xf7, 0x63, 0x0c),
                new ColorViewModel(0xFF, 0xea, 0x00, 0x5e),
                new ColorViewModel(0xFF, 0x8e, 0x8c, 0xd8),
                new ColorViewModel(0xFF, 0x00, 0xb7, 0xc3),
                new ColorViewModel(0xFF, 0x68, 0x76, 0x8a),
                new ColorViewModel(0xFF, 0x69, 0x79, 0x7e),

                new ColorViewModel(0xFF, 0xca, 0x50, 0x10),
                new ColorViewModel(0xFF, 0xc3, 0x00, 0x52),
                new ColorViewModel(0xFF, 0x6b, 0x69, 0xd6),
                new ColorViewModel(0xFF, 0x03, 0x83, 0x87),
                new ColorViewModel(0xFF, 0x51, 0x5c, 0x6b),
                new ColorViewModel(0xFF, 0x4a, 0x54, 0x59),

                new ColorViewModel(0xFF, 0xda, 0x3b, 0x01),
                new ColorViewModel(0xFF, 0xe3, 0x00, 0x8c),
                new ColorViewModel(0xFF, 0x87, 0x64, 0xb8),
                new ColorViewModel(0xFF, 0x00, 0xb2, 0x94),
                new ColorViewModel(0xFF, 0x56, 0x7c, 0x73),
                new ColorViewModel(0xFF, 0x64, 0x7c, 0x64),

                new ColorViewModel(0xFF, 0xef, 0x69, 0x50),
                new ColorViewModel(0xFF, 0xbf, 0x00, 0x77),
                new ColorViewModel(0xFF, 0x74, 0x4d, 0xa9),
                new ColorViewModel(0xFF, 0x01, 0x85, 0x74),
                new ColorViewModel(0xFF, 0x48, 0x68, 0x60),
                new ColorViewModel(0xFF, 0x52, 0x5e, 0x54),

                new ColorViewModel(0xFF, 0xd1, 0x34, 0x38),
                new ColorViewModel(0xFF, 0xc2, 0x39, 0xb3),
                new ColorViewModel(0xFF, 0xb1, 0x46, 0xc2),
                new ColorViewModel(0xFF, 0x00, 0xcc, 0x6a),
                new ColorViewModel(0xFF, 0x49, 0x82, 0x05),
                new ColorViewModel(0xFF, 0x84, 0x75, 0x45),

                new ColorViewModel(0xFF, 0xff, 0x43, 0x43),
                new ColorViewModel(0xFF, 0x9a, 0x00, 0x89),
                new ColorViewModel(0xFF, 0x88, 0x17, 0x98),
                new ColorViewModel(0xFF, 0x10, 0x89, 0x3e),
                new ColorViewModel(0xFF, 0x10, 0x7c, 0x10),
                new ColorViewModel(0xFF, 0x7e, 0x73, 0x5f),
            };
            this.XboxAccentColors = new ObservableCollection<ColorViewModel>()
            {
                new ColorViewModel(0xFF, 0xeb, 0x8c, 0x10),
                new ColorViewModel(0xFF, 0xed, 0x55, 0x88),
                new ColorViewModel(0xFF, 0x10, 0x73, 0xd6),
                new ColorViewModel(0xFF, 0x14, 0x82, 0x82),
                new ColorViewModel(0xFF, 0x10, 0x7c, 0x10),
                new ColorViewModel(0xFF, 0x4c, 0x4a, 0x4b),

                new ColorViewModel(0xFF, 0xeb, 0x49, 0x10),
                new ColorViewModel(0xFF, 0xbf, 0x10, 0x77),
                new ColorViewModel(0xFF, 0x19, 0x3e, 0x91),
                new ColorViewModel(0xFF, 0x54, 0xa8, 0x1b),
                new ColorViewModel(0xFF, 0x73, 0x73, 0x73),
                new ColorViewModel(0xFF, 0x7e, 0x71, 0x5c),

                new ColorViewModel(0xFF, 0xe3, 0x11, 0x23),
                new ColorViewModel(0xFF, 0xb1, 0x44, 0xc0),
                new ColorViewModel(0xFF, 0x10, 0x81, 0xca),
                new ColorViewModel(0xFF, 0x54, 0x7a, 0x72),
                new ColorViewModel(0xFF, 0x67, 0x74, 0x88),
                new ColorViewModel(0xFF, 0x72, 0x4f, 0x2f),

                new ColorViewModel(0xFF, 0xa2, 0x10, 0x25),
                new ColorViewModel(0xFF, 0x74, 0x4d, 0xa9),
                new ColorViewModel(0xFF, 0x10, 0x82, 0x72),
            };
        }

        public ObservableCollection<ColorViewModel> DefaultAccentColors { get; }
        public ObservableCollection<ColorViewModel> XboxAccentColors { get; }
    }
}
