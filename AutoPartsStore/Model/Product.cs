using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutoPartsStore.Model
{
    public class Feature
    {
        public int Id { get; set; }
        private string _parameter;
        public string Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                _parameter = value;

            }
        }
        private string _value;
        public string Description
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;

            }
        }
        public static ObservableCollection<Feature> GetFeaturesByString(string featuresString)
        {
            ObservableCollection<Feature> features = new ObservableCollection<Feature>();
            bool isParameter = true;
            Feature currentFeature = new Feature();
            foreach(string str in featuresString.Split(':', ';'))
            {
                if (isParameter)
                {
                    currentFeature = new Feature();
                    currentFeature.Parameter = str;
                    isParameter = false;
                }
                else
                {
                    currentFeature.Description = str;
                    features.Add(currentFeature);
                    isParameter = true;
                }
            }
            return features;
        }
        public static string GetStringByFeatures(ObservableCollection<Feature> features)
        {
            string outputFeatureString = "";
            foreach (Feature feature in features)
            {
                outputFeatureString += feature.Parameter + ":" + feature.Description + ";";
            }
            return outputFeatureString;
        }

    }
    public class Product : BasicModel
    {
        private long id;
        private Category category;
        private Manufacturer manufacturer;
        private VendorCode vendorCode;
        

        private Decimal price;
        private int availability;
        private string description;
        private ObservableCollection<Feature> features;


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

        public Product()
        {

        }

        public long Id
        {
            get
            {
                return id;
            }
            set
            {
                SetProperty(ref id, value);
            }
        }
        

        public Category Category
        {
            get
            {
                return category;
            }
            set
            {
                SetProperty(ref category, value);

            }
        }

        public Manufacturer Manufacturer
        {
            get
            {
                return manufacturer;
            }
            set
            {
                SetProperty(ref manufacturer, value);
            }
        }
        public VendorCode VendorCode
        {
            get
            {
                return vendorCode;
            }
            set
            {
                SetProperty(ref vendorCode, value);
            }
        }

        public Decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                SetProperty(ref price, value);

            }
        }
        public int Availability
        {
            get
            {
                return availability;
            }
            set
            {
                SetProperty(ref availability, value);

            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                SetProperty(ref description, value);
            }
        }
        public ObservableCollection<Feature> Features
        {
            get
            {
                return features;
            }
            set
            {
                SetProperty(ref features, value);
            }
        }
        public string FeaturesString {
            get
            {
                return Feature.GetStringByFeatures(features);
            }
            set
            {
                Features = Feature.GetFeaturesByString(value);
                NotifyPropertyChanged(nameof(FeaturesString));
            }
        }

    }
}
