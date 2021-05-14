using AutoPartsStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class Product
    {
        private long id;
        private Category category;
        private Manufacturer manufacturer;
        private VendorCode vendorCode;
        private string imagePath;
        private Decimal price;
        private int availability;
        private string description;
        private ObservableCollection<Feature> features;

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
                id = value;
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
                category = value;

            }
        }
        public string CategoryString
        {
            get
            {
                return category.Name;
            }
            set
            {
                //Name = value;

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
                manufacturer = value;
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
                vendorCode = value;

            }
        }
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
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
                price = value;

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
                availability = value;

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
                description = value;

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
                features = value;

            }
        }
        public string FeaturesString {
            get
            {
                return Feature.GetStringByFeatures(features);
            }
            set
            {
                features = Feature.GetFeaturesByString(value);
            }
        }

    }
}
