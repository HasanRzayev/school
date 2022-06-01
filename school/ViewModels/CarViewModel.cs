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
using BespokeFusion;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;

namespace SCHOOL_BUS.ViewModels
{
    public class CarViewModel : INotifyPropertyChanged
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
       
        private bool userFilter(object item)
        {
            if (String.IsNullOrEmpty(SearchText))
                return true;
            else
                return ((item as Car).Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

        }
        private string serachtext;

        public string SearchText
        {
            get { return serachtext; }
            set
            {
                serachtext = value; OnPropertyChanged();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(cars);
                view.Filter = userFilter;
            }
        }

        public RelayCommand Add { get; set; }
        public RelayCommand Exit { get; set; }
        public RelayCommand Create { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Update { get; set; }

        private string name;

        public  string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }


        private  string number;

        public  string  Number
        {
            get { return number; }
            set { number = value; OnPropertyChanged(); }
        }
        private string seat_count;

        public string Seat_count
        {
            get { return seat_count; }
            set { seat_count = value; OnPropertyChanged(); }
        }

        private Car selected_car;

        public Car Selected_car
        {
            get { return selected_car; }
            set { selected_car = value;OnPropertyChanged(); }
        }
        private string buttontext;

        public string ButtonText
        {
            get { return buttontext; }
            set { buttontext = value; OnPropertyChanged(); }
        }


        public ObservableCollection<Car> cars { get; set; }
        public CarViewModel()
        {
            
            Add=new RelayCommand(add);
            Exit=new RelayCommand(exit);
            Create=new RelayCommand(create);
            Remove = new RelayCommand(remove);
            Update = new RelayCommand(update);
            cars=new ObservableCollection<Car>();
            try
            {

                (DATABAZA.GetBaza()).SaveChanges();
                foreach (var item in (DATABAZA.GetBaza()).Cars.ToList())
                {
                    cars.Add(item);
                }
            }
            catch (Exception)
            {

                
            }
            
            
        }

        
        public void add(object p)
        {
            ButtonText="Create";
            Popupisopen=true;

        }
        public void exit(object p)
        {
            Popupisopen=false;
        }
        private void remove(object obj)
        {

            selected_car = obj as Car;
            DATABAZA.GetBaza().Cars.Remove(selected_car);
            cars.Clear();
            (DATABAZA.GetBaza()).SaveChanges();
            foreach (var item in (DATABAZA.GetBaza()).Cars.ToList())
            {
                cars.Add(item);
            }
        }
        private void update(object obj)
        {
            ButtonText="Update";
            selected_car = obj as Car;
            Popupisopen=true;
            Car LAZIMLICAR=(DATABAZA.GetBaza()).Cars.FirstOrDefault(car => car == Selected_car);
            Name=LAZIMLICAR.Title.ToString();
            Number=LAZIMLICAR.Number.ToString();
            Seat_count=LAZIMLICAR.SeatCount.ToString();

        }



        public void create(object p)
        {
            if (ButtonText=="Create")
            {
                int numericValue;
                if (Name==null) MaterialMessageBox.ShowError(@"Enter Name !!!!!!");
                else if (Number==null) MaterialMessageBox.ShowError(@"Enter Number !!!!!!");
                else if (Seat_count==null) MaterialMessageBox.ShowError(@"Seat_count !!!!!!");
                else if (int.TryParse(Seat_count, out numericValue)==false) MaterialMessageBox.ShowError(@"Enter the seat_count number!!!!!!!!!!!!");
                else
                {
                    (DATABAZA.GetBaza()).Cars.Add(new Car
                    {
                        Title = Name,
                        Number = Number,
                        SeatCount = int.Parse(Seat_count)
                    });

                    cars.Clear();

                    (DATABAZA.GetBaza()).SaveChanges();
                    foreach (var item in (DATABAZA.GetBaza()).Cars.ToList())
                    {
                        cars.Add(item);
                    }

                    Name=null;
                    Number=null;
                    Seat_count=null;
                }
            }
            else if(ButtonText=="Update")
            {
                int numericValue;
                if (Name==null) MaterialMessageBox.ShowError(@"Enter Name !!!!!!");
                else if (Number==null) MaterialMessageBox.ShowError(@"Enter Number !!!!!!");
                else if (Seat_count==null) MaterialMessageBox.ShowError(@"Seat_count !!!!!!");
                else if (int.TryParse(Seat_count, out numericValue)==false) MaterialMessageBox.ShowError(@"Enter the seat_count number");
                else
                {
                    Car LAZIMLICAR = (DATABAZA.GetBaza()).Cars.FirstOrDefault(car => car == Selected_car);
                   
                    LAZIMLICAR.Title=Name;
                    LAZIMLICAR.Number=Number;
                    LAZIMLICAR.SeatCount=int.Parse(Seat_count);

                    cars.Clear();
                    (DATABAZA.GetBaza()).Cars.Update(LAZIMLICAR);
                    (DATABAZA.GetBaza()).SaveChanges();
                    foreach (var item in (DATABAZA.GetBaza()).Cars.ToList())
                    {
                        cars.Add(item);
                    }

                    Name=null;
                    Number=null;
                    Seat_count=null;
                    Popupisopen=false;
                }
            }
           

        }
    }
}
