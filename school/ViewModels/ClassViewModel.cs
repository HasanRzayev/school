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
    public class ClassViewModel : INotifyPropertyChanged
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
        private Group selected_class;

        public Group Selected_class
        {
            get { return selected_class; }
            set { selected_class = value; OnPropertyChanged(); }
        }
        private string classname;

        public string ClassName
        {
            get { return classname; }
            set { classname = value; }
        }

        public RelayCommand Add { get; set; }
        public RelayCommand Exit { get; set; }
        public RelayCommand Create { get; set; }
        public RelayCommand Remove { get; set; }
        public ObservableCollection<Group> classs { get; set; }

        public ClassViewModel()
        {
            Add=new RelayCommand(add);
            Exit=new RelayCommand(exit);
            Create=new RelayCommand(create);
            Remove = new RelayCommand(remove);
            classs=new ObservableCollection<Group>();
            try
            {
                foreach (var item in (Database.GetBaza()).Groups.ToList())
                {
                    classs.Add(item);
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
            selected_class = obj as Group;
            if ((Database.GetBaza()).Students.Count()!=0)
            {
                if ((Database.GetBaza()).Students.First(n => n.GroupId==Selected_class.Id)!=null) MaterialMessageBox.ShowError(@"
You can't delete, because he is studying in the same class !!!!!!");
            }
            else
            {
                Database.GetBaza().Groups.Remove(Selected_class);
                classs.Clear();

                (Database.GetBaza()).SaveChanges();
                foreach (var item in (Database.GetBaza()).Groups.ToList())
                {
                    classs.Add(item);
                }

            }

        }
        public void create(object p)
        {
            if (ButtonText=="Create")
            {


                if (ClassName==null) MaterialMessageBox.ShowError(@"Enter ClassName !!!!!!");
               
                else
                {

                    var lazimliclass = new Group
                    {
                        Title = this.ClassName,
                       

                    };

                    (Database.GetBaza()).SaveChanges();


                    (Database.GetBaza()).Add(lazimliclass);
                    (Database.GetBaza()).SaveChanges();

         

                    (Database.GetBaza()).Update(Database.GetBaza().Groups.FirstOrDefault(d => d.Title == this.ClassName));
                    (Database.GetBaza()).SaveChanges();

                    classs.Clear();

                    foreach (var item in (Database.GetBaza()).Groups.ToList())
                    {
                        classs.Add(item);
                    }

                    ClassName =null;
                   

                }
            }

            else if (ButtonText=="Update")
            {
                if (ClassName==null) MaterialMessageBox.ShowError(@"Enter ClassName !!!!!!");
                else
                {
                    Group LAZIMLIclass = (Database.GetBaza()).Groups.FirstOrDefault(car => car== Selected_class);
                    ClassName = this.ClassName;
                   

                    classs.Clear();
                    (Database.GetBaza()).Groups.Update(LAZIMLIclass);
                    (Database.GetBaza()).SaveChanges();
                    foreach (var item in (Database.GetBaza()).Groups.ToList())
                    {
                        classs.Add(item);
                    }

                    ClassName =null;
                    
                }
            }
        }
    }
}
