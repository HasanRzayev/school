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
        public RelayCommand Holidays { get; set; }
        public RelayCommand carrelay { get; set; }
        public RelayCommand driverrelay { get; set; }
        public RelayCommand toggledark { get; set; }
        private bool togglechange;

        public bool Tooglechange
        {
            get { return togglechange; }
            set { togglechange = value; }
        }



        public WindowViewModel(){
            Holidays= new RelayCommand(Holidaykecidd);
            carrelay= new RelayCommand(Carkecid);
            driverrelay= new RelayCommand(Driverkecid);
            toggledark= new RelayCommand(toggle);

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

    
        public void Holidaykecidd(object p)
        {
           
            holidays= new HolidaysViewModel();
            Holidays lazim = new Holidays();
            lazim.DataContext = holidays;
            DisplayPage = lazim;

        }
        public void Carkecid(object p)
        {

            carviewmoedel= new CarViewModel();
            Carpage lazim = new Carpage();
            lazim.DataContext = carviewmoedel;
            DisplayPage = lazim;

        }
        public void Driverkecid(object p)
        {

            driverviewmoedel= new DriverViewModel();
            DriverPage lazim = new DriverPage();
            lazim.DataContext = driverviewmoedel;
            DisplayPage = lazim;

        }
    }
}
