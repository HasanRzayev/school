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
    public class StudentViewModel : INotifyPropertyChanged
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
            set { firstname = value; }
        }


        private string lastname;

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        private string homeadress;

        public string HomeAdress
        {
            get { return homeadress; }
            set { homeadress = value; }
        }



        private string selected_parent;

        public string Selected_Parent
        {
            get { return selected_parent; }
            set { selected_parent = value; OnPropertyChanged(); }
        }
        private Student selected_student;

        public Student Selected_Student
        {
            get { return selected_student; }
            set { selected_student = value; OnPropertyChanged(); }
        }
        private string selected_class;

        public string Selected_class
        {
            get { return selected_class; }
            set { selected_class = value; OnPropertyChanged(); }
        }

        public RelayCommand Add { get; set; }
        public RelayCommand Exit { get; set; }
        public RelayCommand Create { get; set; }
        public RelayCommand Remove { get; set; }
        public ObservableCollection<Student> students { get; set; }
        public ObservableCollection<string> parents { get; set; }
        public ObservableCollection<string> classes { get; set; }

        public StudentViewModel()
        {
            Add=new RelayCommand(add);
            Exit=new RelayCommand(exit);
            Create=new RelayCommand(create);
            Remove = new RelayCommand(remove);
            students=new ObservableCollection<Student>();
            parents =new ObservableCollection<string>();
            classes =new ObservableCollection<string>();
            (DATABAZA.GetBaza()).SaveChanges();
            try
            {
                foreach (var item in (DATABAZA.GetBaza()).Students.ToList())
                {
                    students.Add(item);
                }
                foreach (var items in (DATABAZA.GetBaza()).Parents.ToList())
                {
                    parents?.Add(items.FirstName.ToString());
                }
                foreach (var items in (DATABAZA.GetBaza()).Groups.ToList())
                {
                    classes?.Add(items.Title.ToString());
                }
                foreach (var item in (DATABAZA.GetBaza()).Students.ToList())
                {
                    if (parents.Contains(item.Parent.FirstName)==true) parents.Remove(item.Parent.FirstName);
                }

                foreach (var item in (DATABAZA.GetBaza()).Students.ToList())
                {
                    if (classes.Contains(item.Group.Title)==true) parents.Remove(item.Group.Title);
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
            Selected_Student = obj as Student;
            DATABAZA.GetBaza().Students.Remove(Selected_Student);
            students.Clear();
            parents.Add(Selected_Student.Parent.FirstName);
            classes.Add(Selected_Student.Group.Title);

            (DATABAZA.GetBaza()).SaveChanges();
            foreach (var item in (DATABAZA.GetBaza()).Students.ToList())
            {
                students.Add(item);
            }

        }
        public void create(object p)
        {
            var copyparent = (DATABAZA.GetBaza()).Parents.FirstOrDefault(n => n.FirstName==Selected_Parent);
            var copyclass = (DATABAZA.GetBaza()).Groups.FirstOrDefault(n => n.Title==Selected_class);
            if (ButtonText=="Create")
            {
                if (FirstName==null) MaterialMessageBox.ShowError(@"Enter FirstName !!!!!!");
                else if (LastName==null) MaterialMessageBox.ShowError(@"Enter LastName !!!!!!");
                else if (Selected_class==null) MaterialMessageBox.ShowError(@"Enter class !!!!!!");
                else if (Selected_Parent==null) MaterialMessageBox.ShowError(@"Enter Parent !!!!!!");
                else if (copyparent!=null  && copyclass!=null)
                {

                    var student = new Student
                    {
                        FirstName = this.FirstName,
                        LastName = this.LastName


                    };

                    (DATABAZA.GetBaza()).SaveChanges();


                    (DATABAZA.GetBaza()).Add(student);
                    (DATABAZA.GetBaza()).SaveChanges();

                    student.Parent=copyparent;
                    student.Group=copyclass;

                    (DATABAZA.GetBaza()).Update(DATABAZA.GetBaza().Students.FirstOrDefault(d => d.FirstName == this.FirstName));
                    (DATABAZA.GetBaza()).SaveChanges();

                    parents.Remove(copyparent.FirstName);
                    classes.Remove(copyclass.Title);
                    students.Clear();

                    foreach (var item in (DATABAZA.GetBaza()).Students)
                    {
                        students.Add(item);
                    }

                    FirstName =null;
                    LastName =  null;


                }
            }

            else if (ButtonText=="Update")
            {
                //if (FirstName==null) MaterialMessageBox.ShowError(@"Enter FirstName !!!!!!");
                //else if (LastName==null) MaterialMessageBox.ShowError(@"Enter LastName !!!!!!");
                //else if (Selected_class==null) MaterialMessageBox.ShowError(@"Enter class !!!!!!");
                //else if (Selected_Parent==null) MaterialMessageBox.ShowError(@"Enter Parent !!!!!!");
                //else
                //{
                //    Driver LAZIMLIdriver = (DATABAZA.GetBaza()).Drivers.FirstOrDefault(car => car == Selected_Parent);
                //    cars.Remove(carcopy.Title);
                //    FirstName = this.FirstName;
                //    LastName = this.LastName;
                //    UserName = this.UserName;
                //    Phone= this.Phone;
                //    Passwordd=this.Passwordd;
                //    License= this.License;
                //    LAZIMLIdriver.Car=carcopy;

                //    drivers.Clear();
                //    (DATABAZA.GetBaza()).Drivers.Update(LAZIMLIdriver);
                //    (DATABAZA.GetBaza()).SaveChanges();
                //    foreach (var item in (DATABAZA.GetBaza()).Drivers.ToList())
                //    {
                //        drivers.Add(item);
                //    }
                //    cars.Add(carcopy.Title);


                //    FirstName =null;
                //    LastName =  null;
                //}
            }
        }
    }
}
