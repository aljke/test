using cache_lab5.Common;
using cache_lab5.Interfaces;
using cache_lab5.Models;
using cache_lab5.Views;
using InterSystems.Data.CacheClient;
using InterSystems.Data.CacheTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace cache_lab5.ViewModels
{
    public class UserViewModel : ViewModelBase, IPageBase
    {
        private readonly string SQL_SELECT_ALL = "select * from \"User\" ";
        private List<User.User> _users;
        private User.User _user;
        private string _phones;

        public string Header { get; set; }
        public User.User CurrentUser
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("CurrentUser");
            }
        }

        public List<User.User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                OnPropertyChanged("Users");
            }
        }

        public string Phones
        {
            get
            {
                return _phones;
            }
            set
            {
                _phones = value;
                OnPropertyChanged("Phones");
            }
        }
        
        private CacheListOfStrings toCacheList()
        {
            CacheListOfStrings res = null;
            List<string> phones = Phones.Split(',').ToList();
            if (phones.Count > 0)
            {
                res.Clear();
                foreach (string str in phones)
                {
                    res.Add(str);
                    //MessageBox.Show(str);
                }
            }
            return res;
        }

        protected override void initializeDataSource()
        {
            _dataAdapter = new CacheDataAdapter(SQL_SELECT_ALL, DbHelper.GetConnection());
            ItemsDataTable = new DataTable("USERS");
            _dataAdapter.Fill(ItemsDataTable);
            /*
            Users = new List<User.User>();

            foreach (DataRow row in ItemsDataTable.Rows)
            {
                Users.Add(User.User.OpenId(DbHelper.GetConnection(), row["ID"].ToString()));
            }*/
            
        }

        
        private void EditComand(UserViewDialog view)
        {
          
            CurrentUser.Phone.Add("0-1");
            CurrentUser.Save();
            initializeDataSource();
            view.Close();
        }

        protected override void showEditView(EditMode editMode)
        {
            try
            {
                if (editMode == EditMode.Add)
                {
                    CurrentUser = new User.User(DbHelper.GetConnection());
                }
                else
                {
                    CurrentUser = User.User.OpenId(DbHelper.GetConnection(), SelectedRow.Row["ID"].ToString());
                    Phones = string.Join(",", CurrentUser.Phone.ToList());
                }

                IsViewMode = true;
                UserViewDialog view = new UserViewDialog();

                view.Closing += (sender, args) =>
                {
                    CurrentUser.Close();
                };
                CancelCommand = new Command((o) => view.Close());
                switch (editMode)
                {
                    case EditMode.Add:
                        {
                           
                            IsViewMode = false;
                            EditFormTitle = "Додавання нової категорії";
                            ActionTitle = "Додати";
                            ActionCommand = new Command((o) =>
                            {
                          //      toCacheList();
                                var state = CurrentUser.Save();
                                initializeDataSource();
                                view.Close();
                            });
                            break;
                        }
                    case EditMode.Edit:
                        {
                          //  MessageBox.Show("CurrentUser.Phone.ToList()[0].ToString()");
                            IsViewMode = false;
                            
                            
                            EditFormTitle = "Редагування категорії: " + CurrentUser.Name;
                            ActionTitle = "Зберегти";

                            ActionCommand = new Command((o) => EditComand(view));
                          
                            break;
                        }
                    case EditMode.Delete:
                        {
                                EditFormTitle = "Видалення категорії: " + CurrentUser.Name;
                                ActionTitle = "Видалити";
                                ActionCommand = new Command((o) =>
                                {
                                    if (User.User.DeleteId(DbHelper.GetConnection(), CurrentUser.Id()).IsOK)
                                    {
                                        initializeDataSource();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Неможливо видалити обраний елемент.");
                                    }
                                    view.Close();
                                });
                            break;
                        }
                    case EditMode.Show:
                        {
                          //  MessageBox.Show(CurrentUser.Phone.ToList()[0].ToString());
                            EditFormTitle = "Перегляд категорії: " + CurrentUser.Name;
                            ActionTitle = "ОК";
                            ActionCommand = new Command((o) =>
                            {
                                view.Close();
                            });
                            break;
                        }
                }
                view.DataContext = this;
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public UserViewModel() : base()
        {
            Header = "User";
        }

       
    }
}
