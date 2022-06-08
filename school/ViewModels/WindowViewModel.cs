using Bogus;
using Sb.Models.Entities;
using school;
using school.Pages;
using school.ViewModels;
using SCHOOL_BUS.Commands;
using SCHOOL_BUS.Pages;
using SCHOOL_BUS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SCHOOL_BUS.ViewModels
{
    public class WindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public HolidaysViewModel holidays { get; set; }
        public CarViewModel carviewmoedel { get; set; }
        public DriverViewModel driverviewmoedel { get; set; }
        public ParentViewModel parentviewmodel  { get; set; }
        public ClassViewModel classviewmodel  { get; set; }
        public StudentViewModel studentviewmodel  { get; set; }
        public CreateRideViewModel createrideviewmodel  { get; set; }
        public RidesViewModel rideviewmodel  { get; set; }
        public loadingviewModel loadviewmodel  { get; set; }



        public RelayCommand Holidays { get; set; }
        public RelayCommand carrelay { get; set; }
        public RelayCommand driverrelay { get; set; }
        public RelayCommand parentrelay { get; set; }
        public RelayCommand classrelay { get; set; }
        public RelayCommand studentrelay { get; set; }
        public RelayCommand createriderelay { get; set; }
        public RelayCommand riderelay { get; set; }
        public RelayCommand CloseAppCommand { get; set; }


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
        public RelayCommand toggledark { get; set; }
        private bool togglechange;

        public bool Tooglechange
        {
            get { return togglechange; }
            set { togglechange = value; }
        }

        public WindowViewModel()
        {
            Holidays= new RelayCommand(Holidaykecidd);
            carrelay= new RelayCommand(Carkecid);
            driverrelay= new RelayCommand(Driverkecid);
            parentrelay= new RelayCommand(Parentkecid);
            classrelay= new RelayCommand(classkecidd);
            studentrelay= new RelayCommand(studentkecidd);
            createriderelay= new RelayCommand(createridekecidd);
            riderelay= new RelayCommand(ridekecidd);
            CloseAppCommand= new RelayCommand(CloseApp);
            toggledark= new RelayCommand(toggle);
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            //var Carmercedes = new Car { Title="Mercedes", Number="10-DA-123", SeatCount=4 };
            //var CarHyunday = new Car { Title="Hyunday", Number="10-KK-123", SeatCount=4 };
            //var CarBmw = new Car { Title="BMW", Number="10-QE-123", SeatCount=4 };
            //var CarAUDI = new Car { Title="AUDI", Number="10-RT-123", SeatCount=4 };
            //(Database.GetBaza()).Cars.Add(Carmercedes);
            //(Database.GetBaza()).Cars.Add(CarHyunday);
            //(Database.GetBaza()).Cars.Add(CarBmw);
            //(Database.GetBaza()).Cars.Add(CarAUDI);
            //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //var faker = new Faker("az");
            //for (int i = 0; i < 5; i++)
            //{
            //    (Database.GetBaza()).Parents.Add(new Parent
            //    {
            //        FirstName=faker.Name.FirstName(),
            //        LastName=faker.Name.LastName(),
            //        Phone=faker.Phone.PhoneNumber(),
            //        UserName=faker.Name.FullName(),
            //        Password="1234",
            //    });
            //}

            //////////////////////////////////////////////////////////////////////////////////////////////////////////

            //int norma = 100;
            //for (int i = 0; i < 10; i++)
            //{
            //    (Database.GetBaza()).Groups.Add(new Group
            //    {
            //        Title=$"class{norma}",

            //    });
            //    norma++;
            //}
            //(Database.GetBaza()).Holidays.Add(new Holiday { Date= new DateTime(2022, 6, 8) });


          (Database.GetBaza()).SaveChanges(); 









            ////////////////////////////////////////////////////////////////////////////////////////////////////////

        }
        private Page Displaypage;

        public Page DisplayPage
        {
            get { return Displaypage; }
            set
            {
                Displaypage = value;
                OnPropertyChanged();
            }
        }
        public void toggle(object p)
        {
            var lazim = Application.Current.Resources["ikincireng"];
            if (Tooglechange==true)
            {
                
                Application.Current.Resources["esascolor"] = Application.Current.Resources["darkmode_2"];
                Application.Current.Resources["ikincireng"] = Application.Current.Resources["darkmode_1"];
                Application.Current.Resources["writecolor"] = Application.Current.Resources["writecolordark"];

            }

            else 
            {
                Application.Current.Resources["esascolor"] = Application.Current.Resources["esascolorcopy"];
                Application.Current.Resources["ikincireng"] =  Application.Current.Resources["ikincirengcopy"];
                Application.Current.Resources["writecolordark"] =  Application.Current.Resources["writecolorcopy"];
            }
        }

        public async void loadkecid(object p)
        {
            Popupisopen=true;
            await Task.Delay(2000);
            Popupisopen=false;
            loadviewmodel= new loadingviewModel();
            LoadingPage lazim = new LoadingPage();
            lazim.DataContext = loadviewmodel;
            DisplayPage = lazim;
        }     
        public async void  ridekecidd(object p)
        {
            Popupisopen=true;
            await Task.Delay(2000);
            Popupisopen=false;
            rideviewmodel= new RidesViewModel();
            RidesPage lazim = new RidesPage();
            lazim.DataContext = rideviewmodel;
            DisplayPage = lazim;

        }

        public async void createridekecidd(object p)
        {
            Popupisopen=true;
            await Task.Delay(2000);
            Popupisopen=false;
            createrideviewmodel= new CreateRideViewModel();
            CreateRidePage lazim = new CreateRidePage();
            lazim.DataContext = createrideviewmodel;
            DisplayPage = lazim;

        }
        public async void studentkecidd(object p)
        {
            Popupisopen=true;
            await Task.Delay(2000);
            Popupisopen=false;
            studentviewmodel= new StudentViewModel();
            StudentPage lazim = new StudentPage();
            lazim.DataContext = studentviewmodel;
            DisplayPage = lazim;

        }
        public async void classkecidd(object p)
        {
            Popupisopen=true;
            await Task.Delay(2000);
            Popupisopen=false;
            classviewmodel= new ClassViewModel();
            ClassPage lazim = new ClassPage();
            lazim.DataContext = classviewmodel;
            DisplayPage = lazim;

        }
        public async void Holidaykecidd(object p)
        {
            Popupisopen=true;
            await Task.Delay(2000);
            Popupisopen=false;
            holidays= new HolidaysViewModel();
            Holidays lazim = new Holidays();
            lazim.DataContext = holidays;
            DisplayPage = lazim;

        }
        public async void Carkecid(object p)
        {
            Popupisopen=true;
            await Task.Delay(2000);
            Popupisopen=false;
            carviewmoedel = new CarViewModel();
            Carpage lazim = new Carpage();
            lazim.DataContext = carviewmoedel;
            DisplayPage = lazim;

        }
        public async void Driverkecid(object p)
        {
            Popupisopen=true;
            await Task.Delay(2000);
            Popupisopen=false;
            driverviewmoedel= new DriverViewModel();
            DriverPage lazim = new DriverPage();
            lazim.DataContext = driverviewmoedel;
            DisplayPage = lazim;

        }

        public void Parentkecid(object p)
        {

            parentviewmodel= new ParentViewModel();
            ParentPage lazim = new ParentPage();
            lazim.DataContext = parentviewmodel;
            DisplayPage = lazim;

        }

        public void CloseApp(object obj)
        {
            MainWindow win = obj as MainWindow;
            win.Close();
        }
    }
}
