using SCHOOL_BUS.Commands;
using school.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SCHOOL_BUS.ViewModels
{
    public class DriverViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool popupisopen;

        public bool Popupisopen
        {
            get { return popupisopen; }
            set
            {
                popupisopen = value;
                OnPropertyChanged();
            }
        }

        private string firstname;

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }


        private string lastname;

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        private string username;

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }


        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string homeadress;

        public string HomeAdress
        {
            get { return homeadress; }
            set { homeadress = value; }
        }

        public ObservableCollection<string> cars { get; set; }

        private string license;

        public string License
        {
            get { return license; }
            set { license = value; }
        }
        private string password;

        public string Passwordd
        {
            get { return password; }
            set { password = value; }
        }
        private Driver selected_driver;

        public Driver Selected_driver
        {
            get { return selected_driver; }
            set { selected_driver = value; OnPropertyChanged(); }
        }
        private string selected_car;

        public string Selected_car
        {
            get { return selected_car; }
            set { selected_car = value; OnPropertyChanged(); }
        }

        public RelayCommand Add { get; set; }
        public RelayCommand Exit { get; set; }
        public RelayCommand Create { get; set; }
        public RelayCommand Remove { get; set; }
        public ObservableCollection<Driver> drivers { get; set; }

        public DriverViewModel()
        {
            Add=new RelayCommand(add);
            Exit=new RelayCommand(exit);
            Create=new RelayCommand(create);
            Remove = new RelayCommand(remove);
            drivers=new ObservableCollection<Driver>();
            cars =new ObservableCollection<string>(); 
            (DATABAZA.GetBaza()).SaveChanges();
            foreach (var item in (DATABAZA.GetBaza()).Drivers.ToList())
            {
                drivers.Add(item);
            }
            foreach (var items in (DATABAZA.GetBaza()).Cars.ToList())
            {
                cars?.Add(items.Title.ToString());
            }
        }
        public void add(object p)
        {
            Popupisopen=true;

        }
        public void exit(object p)
        {
            Popupisopen=false;

        }
        private void remove(object obj)
        {

            selected_driver = obj as Driver;
            DATABAZA.GetBaza().Drivers.Remove(selected_driver);
            drivers.Clear();
            (DATABAZA.GetBaza()).SaveChanges();
            foreach (var item in (DATABAZA.GetBaza()).Drivers.ToList())
            {
                drivers.Add(item);
            }

        }
        public void create(object p)
        {

            var carcopy = (DATABAZA.GetBaza()).Cars.FirstOrDefault(n=>n.Title==Selected_car);
            if (carcopy!=null)
            {


                

                
                var driver = new Driver
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    UserName = this.UserName,
                    Phone= this.Phone,
                    Password=this.Passwordd,
                    License= this.License,
                    
                };
                
                (DATABAZA.GetBaza()).SaveChanges();


                (DATABAZA.GetBaza()).Add(driver);
                (DATABAZA.GetBaza()).SaveChanges();

                driver.Car=carcopy;

                (DATABAZA.GetBaza()).Update (DATABAZA.GetBaza().Drivers.FirstOrDefault(d => d.FirstName == this.FirstName));
                (DATABAZA.GetBaza()).SaveChanges();
               
                foreach (var item in (DATABAZA.GetBaza()).Drivers)
                {
                    drivers.Add(item);
                }
                
            }
            


        }
    }
}
