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



        public RelayCommand Add { get; set; }
        public RelayCommand Exit { get; set; }
        public RelayCommand Create { get; set; }
        public RelayCommand Remove { get; set; }

        private string name;

        public  string Name
        {
            get { return name; }
            set { name = value; }
        }


        private  string number;

        public  string  Number
        {
            get { return number; }
            set { number = value; }
        }
        private string seat_count;

        public string Seat_count
        {
            get { return seat_count; }
            set { seat_count = value; }
        }

        private Car selected_car;

        public Car Selected_car
        {
            get { return selected_car; }
            set { selected_car = value;OnPropertyChanged(); }
        }


        public ObservableCollection<Car> cars { get; set; }
        public CarViewModel()
        {
            
            Add=new RelayCommand(add);
            Exit=new RelayCommand(exit);
            Create=new RelayCommand(create);
            Remove = new RelayCommand(remove);
            cars=new ObservableCollection<Car>();
            
            (DATABAZA.GetBaza()).SaveChanges();
            foreach (var item in (DATABAZA.GetBaza()).Cars.ToList())
            {
               cars.Add(item);
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

            selected_car = obj as Car;
            DATABAZA.GetBaza().Cars.Remove(selected_car);
            cars.Clear();
            (DATABAZA.GetBaza()).SaveChanges();
            foreach (var item in (DATABAZA.GetBaza()).Cars.ToList())
            {
                cars.Add(item);
            }
        }
        public void create(object p)
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
        }
    }
}
