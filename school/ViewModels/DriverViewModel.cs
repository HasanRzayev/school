using SCHOOL_BUS.Commands;
using Sb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BespokeFusion;

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
            set { firstname = value; OnPropertyChanged(); }
        }


        private string lastname;

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; OnPropertyChanged(); }
        }
        private string username;

        public string UserName
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }


        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(); }
        }
        private string homeadress;

        public string HomeAdress
        {
            get { return homeadress; }
            set { homeadress = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> cars { get; set; }

        private string license;

        public string License
        {
            get { return license; }
            set { license = value; OnPropertyChanged(); }
        }
        private string password;

        public string Passwordd
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
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
        public RelayCommand Update { get; set; }
        public ObservableCollection<Driver> drivers { get; set; }

        public DriverViewModel()
        {
            Add=new RelayCommand(add);
            Exit=new RelayCommand(exit);
            Create=new RelayCommand(create);
            Remove = new RelayCommand(remove);
            Update = new RelayCommand(update);
            drivers=new ObservableCollection<Driver>();
            cars =new ObservableCollection<string>(); 
            try
            {
                foreach (var item in (Database.GetBaza()).Drivers.ToList())
                {
                    drivers.Add(item); 
                }
                foreach (var items in (Database.GetBaza()).Cars.ToList())
                {
                    cars?.Add(items.Title.ToString());
                }
                foreach (var item in (Database.GetBaza()).Drivers.ToList())
                {
                    if (cars.Contains(item.Car.Title)==true) cars.Remove(item.Car.Title);
                }
            }
            catch (Exception)
            {

                
            }
           
        }
        private string buttontext;

        public string ButtonText
        {
            get { return buttontext; }
            set { buttontext = value; OnPropertyChanged(); }
        }
        private void update(object obj)
        {
            ButtonText = "Update";
            Selected_driver = obj as Driver;
            Popupisopen = true;
             Driver LAZIMLIdriver = (Database.GetBaza()).Drivers.FirstOrDefault(car => car == Selected_driver);
            FirstName = LAZIMLIdriver.FirstName;
            LastName = LAZIMLIdriver.LastName;
            UserName = LAZIMLIdriver.UserName;
            Phone= LAZIMLIdriver.Phone;
            Passwordd=LAZIMLIdriver.Password;
            License= LAZIMLIdriver.License;



            cars.Add(LAZIMLIdriver.Car.Title) ;
            
           

            Selected_car=LAZIMLIdriver.Car.Title;
            

        }
        public void add(object p)
        {
            ButtonText="Create";
            Popupisopen=true;

        }
        public void exit(object p)
        {
            var carcopy = (Database.GetBaza()).Cars.FirstOrDefault(n => n.Title==Selected_car);
            if (ButtonText=="Update")
            {
                cars.Remove(carcopy.Title);

            }
            Popupisopen=false;

        }
        private void remove(object obj)
        {
            selected_driver = obj as Driver;
            Database.GetBaza().Drivers.Remove(selected_driver);
            drivers.Clear();
            //cars.Add(selected_driver.Car.Title);

            (Database.GetBaza()).SaveChanges();
            foreach (var item in (Database.GetBaza()).Drivers.ToList())
            {
                drivers.Add(item);
            }
            
        }
        public void create(object p)
        {
            var carcopy = (Database.GetBaza()).Cars.FirstOrDefault(n => n.Title==Selected_car);
            if (ButtonText=="Create")
            {

                
                if (FirstName==null) MaterialMessageBox.ShowError(@"Enter FirstName !!!!!!");
                else if (LastName==null) MaterialMessageBox.ShowError(@"Enter LastName !!!!!!");
                else if (UserName==null) MaterialMessageBox.ShowError(@"Enter UserName !!!!!!");
                else if (Phone==null) MaterialMessageBox.ShowError(@"Enter Phone !!!!!!");
                else if (Passwordd==null) MaterialMessageBox.ShowError(@"Enter Password !!!!!!");
                else if (Selected_car==null) MaterialMessageBox.ShowError(@"Enter car !!!!!!");
                else if (License==null) MaterialMessageBox.ShowError(@"Enter License !!!!!!");
                else if  (carcopy!=null)
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

                    (Database.GetBaza()).SaveChanges();


                    (Database.GetBaza()).Add(driver);
                    (Database.GetBaza()).SaveChanges();

                    driver.Car=carcopy;

                    (Database.GetBaza()).Update(Database.GetBaza().Drivers.FirstOrDefault(d => d.FirstName == this.FirstName));
                    (Database.GetBaza()).SaveChanges();

                    cars.Remove(carcopy.Title);
                    drivers.Clear();

                    foreach (var item in (Database.GetBaza()).Drivers)
                    {
                        drivers.Add(item);
                    }

                         FirstName =null;
                        LastName =  null;
                        UserName =  null;
                        Phone=      null;
                        Passwordd=   null;
                    License=    null;
                    selected_car=null;

                }
            }

            else if (ButtonText=="Update")
            {
                int numericValue;
                if (FirstName==null) MaterialMessageBox.ShowError(@"Enter FirstName !!!!!!");
                else if (LastName==null) MaterialMessageBox.ShowError(@"Enter LastName !!!!!!");
                else if (UserName==null) MaterialMessageBox.ShowError(@"Enter UserName !!!!!!");
                else if (Phone==null) MaterialMessageBox.ShowError(@"Enter Phone !!!!!!");
                else if (Passwordd==null) MaterialMessageBox.ShowError(@"Enter Password !!!!!!");
                else if (Selected_car==null) MaterialMessageBox.ShowError(@"Enter car !!!!!!");
                else if (License==null) MaterialMessageBox.ShowError(@"Enter License !!!!!!");
                else
                {
                    Driver LAZIMLIdriver = (Database.GetBaza()).Drivers.FirstOrDefault(car => car == Selected_driver);
                    cars.Remove(carcopy.Title);
                    FirstName = this.FirstName;
                    LastName = this.LastName;
                    UserName = this.UserName;
                    Phone= this.Phone;
                    Passwordd=this.Passwordd;
                    License= this.License;
                    LAZIMLIdriver.Car=carcopy;
                    
                    drivers.Clear();
                    (Database.GetBaza()).Drivers.Update(LAZIMLIdriver);
                    (Database.GetBaza()).SaveChanges();
                    foreach (var item in (Database.GetBaza()).Drivers.ToList())
                    {
                        drivers.Add(item);
                    }
          

                    FirstName =null;
                    LastName =  null;
                    UserName =  null;
                    Phone=      null;
                    Passwordd=   null;
                    License=    null;
                    selected_car=null;
                    Popupisopen=false;
                }
            }
        }
    }
}
