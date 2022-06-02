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
using SCHOOL_BUS;

namespace school.ViewModels
{
    public class CreateRideViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
       

        private string fullname;

        public string FullName
        {
            get { return fullname; }
            set { fullname = value; OnPropertyChanged(); }
        }


        private string carname;

        public string CarName
        {
            get { return carname; }
            set { carname = value; OnPropertyChanged(); }
        }

        private string carnumber;

        public string CarNumber
        {
            get { return carnumber; }
            set { carnumber = value; OnPropertyChanged(); }
        }


        private string attend;

        public string Attend
        {
            get { return attend; }
            set { attend = value; OnPropertyChanged(); }
        }
        private string max_seat;

        public string Max_seat
        {
            get { return max_seat; }
            set { max_seat = value; OnPropertyChanged(); }
        }
        private string selected_driver;

        public string Selected_Driver
        {
            get { return selected_driver; }
            set { selected_driver = value; OnPropertyChanged(); }
        }
        private Student selected_student;

        public Student Selected_Student
        {
            get { return selected_student; }
            set { selected_student = value; OnPropertyChanged(); }
        }
        private Student selected_student_elave;

        public Student Selected_Student_elave
        {
            get { return selected_student_elave; }
            set { selected_student_elave = value; OnPropertyChanged(); }
        }

        public RelayCommand Add { get; set; }
        public RelayCommand Exit { get; set; }
        public RelayCommand Create { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Update { get; set; }
        public ObservableCollection<Student> students_drivers { get; set; }
        public ObservableCollection<Student> allstudents { get; set; }
        private ObservableCollection<string> driverslist { get; set; }
 

        
        public void create(object p)
        {
           
            var driver = (Database.GetBaza()).Drivers.FirstOrDefault(n => n.FirstName==Selected_Driver);

            if (Selected_Driver!=null){
                var ride = new Ride
                {

                    Type="go",
                    DriverId=driver.Id

                };
                (Database.GetBaza()).Add(ride);
                (Database.GetBaza()).SaveChanges();

                ride.Driver=driver;

                (Database.GetBaza()).Update(ride);
     
            }
               
         }

        
        public CreateRideViewModel()
        {
           
            Create=new RelayCommand(create);
            driverslist=new ObservableCollection<string>();
            students_drivers =new ObservableCollection<Student>();
            allstudents=new ObservableCollection<Student>();

            try
            {
                foreach (var item in (Database.GetBaza()).Students.ToList())
                {
                    allstudents.Add(item);
                }
                foreach (var item in (Database.GetBaza()).Drivers.ToList())
                {
                    driverslist.Add(item.FirstName.ToString());
                }
            }
            catch (Exception)
            {


            }

        }
       
    }
}
