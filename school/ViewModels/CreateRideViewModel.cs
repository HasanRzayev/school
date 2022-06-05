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
            set { selected_driver = value; OnPropertyChanged();
           
            }
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
        public RelayCommand ComboSelectionChangedCommand { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Update { get; set; }
        public ObservableCollection<Student> students_drivers { get; set; }
        public ObservableCollection<Student> allstudents { get; set; }
        public  ObservableCollection<string> driverslist { get; set; }

        public void comboSelectionChangedCommand(object p)
        {

            var driver = (Database.GetBaza()).Drivers.FirstOrDefault(n => n.FirstName==Selected_Driver);
            if(driver !=null)
            {
                int zero = 0;
                FullName=driver.FirstName;
                CarName=(Database.GetBaza()).Cars.First(n=>n.DriverId==driver.Id).Title;
                CarNumber=(Database.GetBaza()).Cars.First(n => n.DriverId==driver.Id).Number;
                Attend=zero.ToString();
                Max_seat=driver.Car.SeatCount.ToString();
                
            }


        }
        public void remove(object p)
        {
            Selected_Student_elave  = p as Student;
            students_drivers.Remove(Selected_Student_elave);
            allstudents.Add(Selected_Student_elave);

            Attend=(int.Parse(Attend)-1).ToString();
        }
        public void add(object p)
        {
            if(students_drivers.Count+1>students_drivers.Count) MaterialMessageBox.ShowError(@"FULL  !!!!!!");
            else
                    {
                        Selected_Student = p as Student;
                        students_drivers.Add(Selected_Student);
                        allstudents.Remove(Selected_Student);

                        Attend=(int.Parse(Attend)+1).ToString();
                    }
            


        }
        public void create(object p)
        {



            var driver = (Database.GetBaza()).Drivers.First(n => n.FirstName==Selected_Driver);
            var ride = new Ride { Type="go",Driver=driver,DriverId=driver.Id};
            if (Database.GetBaza().Rides.FirstOrDefault(d => d.DriverId ==driver.Id)!=null)
            {
                MaterialMessageBox.ShowError(@"Enter FirstName !!!!!!");
            }
            else
            {
                (Database.GetBaza()).SaveChanges();

                (Database.GetBaza()).Add(ride);




                ride.Driver= driver;
                ride.Students=new List<Student>();
                foreach (var item in students_drivers)
                {
                    ride.Students.Add(item);
                }
                foreach (var item in ride.Students)
                {
                    item.Ride=ride;
                }


            //(Database.GetBaza()).Update(Database.GetBaza().Rides.FirstOrDefault(d => d.DriverId ==driver.Id));

            (Database.GetBaza()).SaveChanges();
            }
           
            Selected_Driver=null;
            students_drivers.Clear();
            Attend=null;
       
        }

        
        public CreateRideViewModel()
        {
           
            Create=new RelayCommand(create);
            Add=new RelayCommand(add);
            Remove=new RelayCommand(remove);
            ComboSelectionChangedCommand=new RelayCommand(comboSelectionChangedCommand);
            driverslist=new ObservableCollection<string>();
            students_drivers =new ObservableCollection<Student>();
            allstudents=new ObservableCollection<Student>();

            try
            {
               
                foreach (var item in (Database.GetBaza()).Students.Where(n=>n.RideId==null).ToList())
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
