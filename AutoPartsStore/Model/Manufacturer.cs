using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoPartsStore.Model
{
    public class Manufacturer : BasicModel
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; }

        public byte[] ImageByteArray
        {
            get
            {
                if (image != null)
                {
                    byte[] data;
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.QualityLevel = 30;
                    encoder.Frames.Add(BitmapFrame.Create(image as BitmapImage));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);
                        data = ms.ToArray();
                    }
                    return data;
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    using (var ms = new MemoryStream(value))
                    {
                        BitmapImage image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad; // here
                        image.StreamSource = ms;
                        image.EndInit();
                        Image = image;
                        //NotifyPropertyChanged(nameof(Image));
                    }
                }
            }
        }

        private ImageSource image;

        public ImageSource Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                NotifyPropertyChanged(nameof(Image));
            }
        }
        public string Description { get; set; }
    }
}
