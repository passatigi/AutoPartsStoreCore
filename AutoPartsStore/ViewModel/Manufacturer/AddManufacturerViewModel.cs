using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using AutoPartsStore.BusinessLogicLayer.Service;
using AutoPartsStore.Command;
using AutoPartsStore.Model;

namespace AutoPartsStore.ViewModel
{
    public class AddManufacturerViewModel : BaseViewModel
    {
        IStoreService storeService;
        public AddManufacturerViewModel()
        {
            this.storeService = StoreService.GetStoreService();
            manufacturer = new Manufacturer();
        }
        private Manufacturer manufacturer;
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
        private RelayCommand addManufacturerCommand;
        public RelayCommand AddManufacturerCommand
        {
            get
            {
                return addManufacturerCommand ?? (addManufacturerCommand = new RelayCommand(action =>
                {
                    if (manufacturer.Name != null || manufacturer.Name != "") {
                        storeService.ManufacturerService.AddManufacturer(Manufacturer);
                        Manufacturer = new Manufacturer();
                    }
                    else
                    {
                        MessageBox.Show("gde ima");
                    }
                }, func =>
                {
                    return true;
                }));
            }
        }
    }
}
