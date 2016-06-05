using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

//! WARNING: This files contains a lot of untested and dirty code

namespace Koopakiller.Apps.UwpAppDevelopmentHelper.DevelopmentHelperCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
        }

        private void buttonOpenXmlAndWriteImages_Click(object sender, EventArgs e)
        {
            var font = new Font("Segoe MDL2 Assets", 40F / 96 * 72);
            //    var doc = XDocument.Load("chars.xml", loadop);
            var doc = XDocument.Parse(File.ReadAllText("chars.xml", Encoding.UTF8));

            if (doc.Root == null) { return; }

            foreach (var fi in doc.Root.Elements("FontIcon"))
            {
                foreach (var code in fi.Elements("Code"))
                {
                    var chr = (char)Convert.ToInt32(code.Value, 16);

                    using (var bmp = new Bitmap(96, 64))
                    {
                        using (var g = Graphics.FromImage(bmp))
                        {
                            g.DrawString(chr.ToString(), font, Brushes.Black, 8, 8);
                        }
                        bmp.Save("Images\\" + code.Value + ".png");
                    }
                }
            }
        }

        private void buttonCompareImages_Click(object sender, EventArgs e)
        {
            var l = new Dictionary<string, List<string>>();

            var files = Directory.GetFiles("Images", "*.png");
            for (var i = 0; i < files.Length - 1; ++i)
            {
                var hex1 = new FileInfo(files[i]).Name;
                hex1 = hex1.Substring(0, hex1.IndexOf(".", StringComparison.Ordinal));
                if (!l.Any(x => x.Value.Any(y => y == hex1)))
                {
                    l.Add(hex1, new List<string>());
                }
                for (var j = i + 1; j < files.Length; ++j)
                {
                    var hex2 = new FileInfo(files[j]).Name;
                    hex2 = hex2.Substring(0, hex2.IndexOf(".", StringComparison.Ordinal));
                    if (l.Any(x => x.Value.Any(y => y == hex2)))
                    {
                        continue;
                    }
                    using (var a = new Bitmap(files[i]))
                    {
                        using (var b = new Bitmap(files[j]))
                        {
                            if (Compare(a, b))
                            {
                                l[hex1].Add(hex2);
                            }
                        }
                    }
                }
            }

            var sb = new StringBuilder();
            foreach (var line in l)
            {
                sb.AppendLine($"{line.Key}, {string.Join(", ", line.Value)}");
            }
            File.WriteAllText("sorted.txt", sb.ToString());
        }

        public static bool Compare(Bitmap b1, Bitmap b2)
        {
            if (b1 == null)
            {
                return b2 == null;
            }
            if (b2 == null)
            {
                return false;
            }
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                var byteLength = b1.Width * b1.Height * 4;
                var bytes1 = new byte[byteLength];
                var bytes2 = new byte[byteLength];

                Marshal.Copy(bd1.Scan0, bytes1, 0, byteLength);

                Marshal.Copy(bd2.Scan0, bytes2, 0, byteLength);

                return bytes1.SequenceEqual(bytes2);
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }

        private void buttonCreateXML_Click(object sender, EventArgs e)
        {
            var source = XDocument.Parse(File.ReadAllText("chars.xml", Encoding.UTF8));

            var sb = new StringBuilder();
            sb.AppendLine(@"<?xml version=""1.0"" encoding=""utf - 8""?>");
            sb.AppendLine("<FontIcons>");
            foreach (var line in File.ReadLines("sorted.txt"))
            {
                var nums = line.Split(',')
                               .Select(x => x.Trim())
                               .Where(x => !string.IsNullOrWhiteSpace(x))
                               .ToList();

                sb.AppendLine(@"  <FontIcon>");

                foreach (var num in nums)
                {
                    sb.AppendLine($"     <!-- {(char)Convert.ToInt32(num, 16)} -->");
                    sb.AppendLine($@"    <Code>{num}</Code>");
                }

                if (source.Root != null)
                {
                    var tags = source.Root
                        .Elements("FontIcon")
                        .Where(x => x.Elements("Code").Any(y => nums.Contains(y.Value)))
                        .SelectMany(x => x.Descendants("Tag"))
                        .Select(x => x.Value)
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList();

                    sb.AppendLine(@"    <Tags Language=""1033"">");
                    if (tags.Count == 0)
                    {
                        sb.AppendLine("      <Tag>Other</Tag>");
                    }
                    else
                    {
                        foreach (var tag in tags)
                        {
                            sb.AppendLine($"      <Tag>{tag}</Tag>");
                        }
                    }
                    sb.AppendLine(@"    </Tags>");
                }

                sb.AppendLine(@"  </FontIcon>");
            }
            sb.AppendLine("</FontIcons>");
            File.WriteAllText("chars2.xml", sb.ToString());
        }

        private void buttonAddMSDNIconInfo_Click(object sender, EventArgs e)
        {
            var source = XDocument.Parse(File.ReadAllText("chars2.xml", Encoding.UTF8));
            var msdnXml = XDocument.Load(@"MSDN MDL2 Font Icon.xml");

            //Code
            //Enum
            //Desc

            var msdn = msdnXml.Descendants("tr").Select(x =>
              {
                  var td = x.Elements("td").ToList();
                  switch (td.Count)
                  {
                      case 0://Header
                          return new Tuple<string, string, string>[0];
                      case 3:
                          return new[]
                          {
                            Tuple.Create(td[0].Value, td[2].Value, ""),
                          };
                      case 4:

                          return new[]
                          {
                            Tuple.Create(td[0].Value, td[2].Value, td[3].Value),
                          };
                      case 6:
                          return new[]
                          {
                            Tuple.Create(td[0].Value, td[2].Value, ""),
                            Tuple.Create(td[3].Value, td[5].Value, ""),
                          };
                      default:
                          throw new ArgumentOutOfRangeException();
                  }
              }).SelectMany(x => x).ToDictionary(x =>
              {
                  Debug.WriteLine(x.Item1);
                  return x.Item1.Trim(' ', '\n', '\r', '\t');
              }, x => Tuple.Create(x.Item2, x.Item3));

            if (source.Root == null) return;
            {
                foreach (var fi in source.Root.Elements("FontIcon"))
                {
                    var codes = fi.Elements("Code").Select(x => x.Value).ToList();
                    var d =
                        msdn.FirstOrDefault(
                            x => codes.Any(y => string.Equals(x.Key, y, StringComparison.CurrentCultureIgnoreCase)));
                    if (!string.IsNullOrWhiteSpace(d.Key))
                    {
                        var enu = new XElement("EnumValue", d.Value.Item1);
                        fi.Add(enu);

                        if (!string.IsNullOrWhiteSpace(d.Value.Item2))
                        {
                            var desc = new XElement("Description", new XAttribute("Language", "1033"), d.Value.Item2);
                            fi.Add(desc);
                        }
                    }
                }

                source.Save("chars3.xml");
            }
        }

        private void buttonLowerCaseTags_Click(object sender, EventArgs e)
        {
            var source = XDocument.Parse(File.ReadAllText("chars3.xml", Encoding.UTF8));

            foreach (var tag in source.Descendants("Tag"))
            {
                tag.Value = tag.Value.ToLower();
            }

            source.Save("chars4.xml");
        }

        private void buttonDistinctTags_Click(object sender, EventArgs e)
        {
            var source = XDocument.Parse(File.ReadAllText("chars4.xml", Encoding.UTF8));

            foreach (var tags in source.Descendants("Tags"))
            {
                var tagNames = tags.Elements("Tag").Select(x => x.Value).Distinct().ToList();
                tags.RemoveAll();
                tags.Add(new XAttribute("Language", "1033"));
                foreach (var tag in tagNames)
                {
                    tags.Add(new XElement("Tag", tag));
                }
            }

            source.Save("chars5.xml");
        }
    }
}
