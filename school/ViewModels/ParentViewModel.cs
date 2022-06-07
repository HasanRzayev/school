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
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
using SCHOOL_BUS;

namespace school.ViewModels
{
    public class ParentViewModel : INotifyPropertyChanged
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
                return ((item as Parent).FirstName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

        }
        private string serachtext;

        public string SearchText
        {
            get { return serachtext; }
            set
            {
                serachtext = value; OnPropertyChanged();
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(parents);
                view.Filter = userFilter;
            }
        }

        public RelayCommand Add { get; set; }
        public RelayCommand Exit { get; set; }
        public RelayCommand Create { get; set; }
        public RelayCommand Remove { get; set; }
        public RelayCommand Update { get; set; }

      

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
        private string username;

        public string UserName
        {
            get { return username; }
            set { username = value; }
        }


        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private Parent selected_parent;

        public Parent Selected_parent
        {
            get { return selected_parent; }
            set { selected_parent = value; OnPropertyChanged(); }
        }
        private string buttontext;

        public string ButtonText
        {
            get { return buttontext; }
            set { buttontext = value; OnPropertyChanged(); }
        }


        public ObservableCollection<Parent> parents { get; set; }
        public ParentViewModel()
        {

            Add=new RelayCommand(add);
            Exit=new RelayCommand(exit);
            Create=new RelayCommand(create);
            Remove = new RelayCommand(remove);
            Update = new RelayCommand(update);
            parents=new ObservableCollection<Parent>();

            foreach (var item in (Database.GetBaza()).Parents.ToList())
            {
                parents.Add(item);
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
            Selected_parent = obj as Parent;
            if ((Database.GetBaza()).Students.Count()!=0)
            {
               
                if ((Database.GetBaza()).Students.FirstOrDefault(n => n.ParentId==Selected_parent.Id)!=null) MaterialMessageBox.ShowError(@"
You can't delete, because he is studying in the same class !!!!!!");
                else
                {
                    Database.GetBaza().Parents.Remove(Selected_parent);
                    parents.Clear();
                    (Database.GetBaza()).SaveChanges();
                    foreach (var item in (Database.GetBaza()).Parents.ToList())
                    {
                        parents.Add(item);
                    }
                }
            }
         
            else
            {
                Database.GetBaza().Parents.Remove(Selected_parent);
                parents.Clear();
                (Database.GetBaza()).SaveChanges();
                foreach (var item in (Database.GetBaza()).Parents.ToList())
                {
                    parents.Add(item);
                }
            }
           
        }
        private void update(object obj)
        {
            ButtonText="Update";
            Selected_parent = obj as Parent;
            Popupisopen=true;
            Parent lazimliparent = (Database.GetBaza()).Parents.FirstOrDefault(parent =>parent.Id  == Selected_parent.Id);
            FirstName=lazimliparent.FirstName.ToString();
            LastName =lazimliparent.LastName.ToString();
            Phone    =lazimliparent.Phone.ToString();
            UserName =lazimliparent.UserName.ToString();

        }



        public void create(object p)
        {
            if (ButtonText=="Create")
            {
                if (FirstName==null) MaterialMessageBox.ShowError(@"Enter FirstName !!!!!!");
                else if (LastName==null) MaterialMessageBox.ShowError(@"Enter LastName !!!!!!");
                else if (Phone==null) MaterialMessageBox.ShowError(@"Enter Phone !!!!!!");
                else if (UserName==null) MaterialMessageBox.ShowError(@"Enter UserName !!!!!!");
                else if (Password==null) MaterialMessageBox.ShowError(@"Enter Password !!!!!!");
                else
                {
                    (Database.GetBaza()).Parents.Add(new Parent
                    {
                        FirstName=this.FirstName,
                        LastName =this.LastName ,
                        Phone    =this.Phone    ,
                        UserName =this.UserName,
                        Password=this.Password
                    });

                    parents.Clear();

                    (Database.GetBaza()).SaveChanges();
                    foreach (var item in (Database.GetBaza()).Parents.ToList())
                    {
                        parents.Add(item);
                    }

                    FirstName=null;
                    LastName =null;
                    Phone    =null;
                    UserName =null;
                    Password =null;

                }
            }
            else if (ButtonText=="Update")
            {
                if (FirstName==null) MaterialMessageBox.ShowError(@"Enter FirstName !!!!!!");
                else if (LastName==null) MaterialMessageBox.ShowError(@"Enter LastName !!!!!!");
                else if (Phone==null) MaterialMessageBox.ShowError(@"Enter Phone !!!!!!");
                else if (UserName==null) MaterialMessageBox.ShowError(@"Enter UserName !!!!!!");
                else if (Password==null) MaterialMessageBox.ShowError(@"Enter Password !!!!!!");
                else
                {
                    Parent LAZIMLIParent = (Database.GetBaza()).Parents.FirstOrDefault(Parent => Parent == Selected_parent);

                    LAZIMLIParent.FirstName=this.FirstName;
                    LAZIMLIParent.LastName=this.LastName;
                    LAZIMLIParent.Phone   =this.Phone   ;
                    LAZIMLIParent.UserName=this.UserName;
                    LAZIMLIParent.Password=this.Password;

                    parents.Clear();
                    (Database.GetBaza()).Parents.Update(LAZIMLIParent);
                    (Database.GetBaza()).SaveChanges();
                    foreach (var item in (Database.GetBaza()).Parents.ToList())
                    {
                        parents.Add(item);
                    }

                    FirstName=null;
                    LastName =null;
                    Phone    =null;
                    UserName =null;
                    Password =null;

                    Popupisopen=false;
                }
            }


        }
    }
}
