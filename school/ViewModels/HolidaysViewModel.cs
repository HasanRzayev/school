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

namespace SCHOOL_BUS.ViewModels
{
    public class HolidaysViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<DateTime> Dates { get; set; }
        public HolidaysViewModel()
        {
            Dates=new ObservableCollection<DateTime> { DateTime.MinValue, DateTime.MaxValue };
            foreach (var item in (Database.GetBaza()).Holidays)
            {
                Dates.Add(item.Date);
            }
        }
    }
}