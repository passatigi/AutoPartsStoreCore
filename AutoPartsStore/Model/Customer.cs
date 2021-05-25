using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPartsStore.Model
{
    public class Customer : BasicModel
    {
        public void Update(Customer customer)
        {
            this.Address = customer.Address;
            this.FullName = customer.FullName;
            this.Mail = customer.Mail;
            this.PhoneNumber = customer.PhoneNumber;
            this.Password = customer.password;
        }
        private int id;
        public int Id
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

        private string fullName;
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                SetProperty(ref fullName, value);
            }
        }

        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                SetProperty(ref address, value);
            }
        }

        private string mail;
        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                SetProperty(ref mail, value);
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                SetProperty(ref phoneNumber, value);
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password, value);
            }
        }




    }
}
