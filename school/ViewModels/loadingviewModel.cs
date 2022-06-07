using Bogus;
using Sb.Models.Entities;
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

namespace school.ViewModels
{
    public class loadingviewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public int Value { get; set; } = 0;
        public loadingviewModel()
        {
            for (int i = 0; i < 100; i++)
            {
                Value+=1;
                Thread.Sleep(200);
            }
        }
    }
}
