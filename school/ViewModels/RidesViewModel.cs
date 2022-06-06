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
using SCHOOL_BUS;
namespace school.ViewModels
{
    public class RidesViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
       
        private Ride selected_ride;

        public Ride Selected_ride
        {
            get { return selected_ride; }
            set { selected_ride = value; OnPropertyChanged(); }
        }
 
       
        public RelayCommand Remove { get; set; }
        public ObservableCollection<Ride> rides { get; set; }

        public RidesViewModel()
        {
         
            Remove = new RelayCommand(remove);
            rides=new ObservableCollection<Ride>();
            try
            {
                foreach (var item in (Database.GetBaza()).Rides.ToList())
                {
                    rides.Add(item);
                }

            }
            catch (Exception)
            {


            }

        }
   

    
        private void remove(object obj)
        {
            Selected_ride = obj as Ride;

            foreach (var item in (Database.GetBaza()).Students)
            {
                if (item.RideId==Selected_ride.Id)
                {
                    item.RideId=null;
                }
            }
            Database.GetBaza().Rides.Remove(Selected_ride);
            rides.Clear();
           
            (Database.GetBaza()).SaveChanges();
            foreach (var item in (Database.GetBaza()).Rides.ToList())
            {
                rides.Add(item);
            }

        }
     
    }
}
