using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoPartsStore.Model
{
    public class Review : BasicModel
    {
        public Review()
        {
            rating = 5;
        }
        public long Id { get; set; }

        public Product Product { get; set; }

        public int CustomerId { get; set; }

        private Customer customer;
        public Customer Customer
        {
            get
            {
                return customer;
            }
            set
            {
                customer = value;
                if (value != null)
                {
                    CustomerId = value.Id;
                }
            }
        }
        [NotMapped]
        public DateTime DateTime { get; set; }

        private byte rating;
        public byte Rating
        {
            get
            {
                return rating;
            }
            set
            {
                if(value < 0 || value > 10)
                {
                    
                }
                else
                {
                    SetProperty(ref rating, value);
                }
            }
        }

        private string reviewText;
        public string ReviewText
        {
            get
            {
                return reviewText;
            }
            set
            {
                SetProperty(ref reviewText, value);
            }
        }

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
                        NotifyPropertyChanged(nameof(Image));
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
    }
}
