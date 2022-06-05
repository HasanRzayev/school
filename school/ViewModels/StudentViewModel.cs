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
            set { firstname = value; OnPropertyChanged(); }
        }


        private string lastname;

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; OnPropertyChanged(); }
        }

        private string homeadress;

        public string HomeAdress
        {
            get { return homeadress; }
            set { homeadress = value; OnPropertyChanged(); }
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
        public RelayCommand Update { get; set; }
        public ObservableCollection<Student> students { get; set; }
        private ObservableCollection<string> parentlist;

        public ObservableCollection<string> parents
        {
            get { return parentlist; }
            set { parentlist = value; }
        }
        private ObservableCollection<string> classeslist;

        public ObservableCollection<string> classes
        {
            get { return classeslist; }
            set { classeslist = value; }
        }

        public StudentViewModel()
        {
            Add=new RelayCommand(add);
            Exit=new RelayCommand(exit);
            Create=new RelayCommand(create);
            Update = new RelayCommand(update);

            Remove = new RelayCommand(remove);
            students=new ObservableCollection<Student>();
            parents =new ObservableCollection<string>();
            classes =new ObservableCollection<string>();
            try
            {
                foreach (var item in (Database.GetBaza()).Students.ToList())
                {
                    students.Add(item);
                }
                foreach (var items in (Database.GetBaza()).Parents.ToList())
                {
                    parents?.Add(items.FirstName.ToString());
                }
                foreach (var items in (Database.GetBaza()).Groups.ToList())
                {
                    classes?.Add(items.Title.ToString());
                }
                foreach (var item in (Database.GetBaza()).Students.ToList())
                {
                    if (parents.Contains(item.Parent.FirstName)==true) parents.Remove(item.Parent.FirstName);
                }

                foreach (var item in (Database.GetBaza()).Students.ToList())
                {
                    if (classes.Contains(item.Group.Title)==true) classes.Remove(item.Group.Title);
                }
            }
            catch (Exception)
            {


            }

        }
        private void update(object obj)
        {
            ButtonText = "Update";
            Selected_Student = obj as Student;
            Popupisopen = true;
            Student LAZIMLICAR = (Database.GetBaza()).Students.FirstOrDefault(car => car == Selected_Student);
            FirstName = LAZIMLICAR.FirstName;
            LastName = LAZIMLICAR.LastName;
            HomeAdress=LAZIMLICAR.Home;

            parents.Add(LAZIMLICAR.Parent.FirstName);
            classes.Add(LAZIMLICAR.Group.Title);

            Selected_Parent=LAZIMLICAR.Parent.FirstName;
            Selected_class=LAZIMLICAR.Group.Title;

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
            var copyparent = (Database.GetBaza()).Parents.FirstOrDefault(n => n.FirstName==Selected_Parent);
            var copyclass = (Database.GetBaza()).Groups.FirstOrDefault(n => n.Title==Selected_class);
            if (ButtonText=="Update")
            {
                parents.Remove(copyparent.FirstName);
                classes.Remove(copyclass.Title);
            }
                Popupisopen=false;

        }
        private void remove(object obj)
        {
            Selected_Student = obj as Student;
         
         
         
            parents.Add(Selected_Student.Parent.FirstName);
            classes.Add(Selected_Student.Group.Title);
            Database.GetBaza().Students.Remove(Selected_Student);
            students.Clear();
            (Database.GetBaza()).SaveChanges();
            foreach (var item in (Database.GetBaza()).Students.ToList())
            {
                students.Add(item);
            }

        }
        public void create(object p)
        {
            var copyparent = (Database.GetBaza()).Parents.FirstOrDefault(n => n.FirstName==Selected_Parent);
            var copyclass = (Database.GetBaza()).Groups.FirstOrDefault(n => n.Title==Selected_class);
            if (ButtonText=="Create")
            {
                if (FirstName==null) MaterialMessageBox.ShowError(@"Enter FirstName !!!!!!");
                else if (LastName==null) MaterialMessageBox.ShowError(@"Enter LastName !!!!!!");
                else if (HomeAdress==null) MaterialMessageBox.ShowError(@"Enter HomeAdress !!!!!!");
                else if (Selected_class==null) MaterialMessageBox.ShowError(@"Enter class !!!!!!");
                else if (Selected_Parent==null) MaterialMessageBox.ShowError(@"Enter Parent !!!!!!");
                else if (copyparent!=null  && copyclass!=null)
                {

                    var student = new Student
                    {
                        FirstName = this.FirstName,
                        LastName = this.LastName,
                        Home=this.HomeAdress,
                        HomeDescription=this.HomeAdress,
                        OtherAddressDescription=this.HomeAdress,
                        OtherAddress=this.HomeAdress,
                        GroupId=copyclass.Id,
                        ParentId=copyparent.Id


                    };

                    (Database.GetBaza()).SaveChanges();


                    (Database.GetBaza()).Add(student);
                    (Database.GetBaza()).SaveChanges();

                    student.Parent=copyparent;
                    student.Group=copyclass;

                    (Database.GetBaza()).Update(Database.GetBaza().Students.FirstOrDefault(d => d.FirstName == this.FirstName));
                    (Database.GetBaza()).SaveChanges();

                    parents.Remove(copyparent.FirstName);
                    classes.Remove(copyclass.Title);
                    students.Clear();

                    foreach (var item in (Database.GetBaza()).Students)
                    {
                        students.Add(item);
                    }

                    FirstName =null;
                    LastName =  null;


                }
            }

            else if (ButtonText=="Update")
            {
                if (FirstName==null) MaterialMessageBox.ShowError(@"Enter FirstName !!!!!!");
                else if (LastName==null) MaterialMessageBox.ShowError(@"Enter LastName !!!!!!");
                else if (HomeAdress==null) MaterialMessageBox.ShowError(@"Enter HomeAdress !!!!!!");
                else if (Selected_class==null) MaterialMessageBox.ShowError(@"Enter class !!!!!!");
                else if (Selected_Parent==null) MaterialMessageBox.ShowError(@"Enter Parent !!!!!!");
                else
                {

                    Student student = (Database.GetBaza()).Students.FirstOrDefault(car => car == Selected_Student);
                    parents.Remove(copyparent.FirstName);
                    classes.Remove(copyclass.Title);
                    student.FirstName = this.FirstName;
                    student.LastName = this.LastName;
                    student.Home=this.HomeAdress;
                    student.HomeDescription=this.HomeAdress;
                    student.OtherAddressDescription=this.HomeAdress;
                    student.OtherAddress=this.HomeAdress;
                    
                    student.Parent=copyparent;
                    student.Group=copyclass;
                    students.Clear();
                    (Database.GetBaza()).Students.Update(student);
                    (Database.GetBaza()).SaveChanges();
                    foreach (var item in (Database.GetBaza()).Students.ToList())
                    {
                        students.Add(item);
                    }

                   

                    FirstName =null;
                    LastName =  null;
                    Popupisopen=false;
                }
            }
        }
    }
}
