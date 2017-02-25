using cache_lab5.Common;
using cache_lab5.Models;
using cache_lab5.Views;
using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace cache_lab5.ViewModels
{

    public class ReservationViewModel : ViewModelBase, IPageBase
    {
        private readonly string SQL_SELECT_ALL = "select ID, Email, Mobile, TimeEnd from Reservations";
        // private readonly string SQL_SELECT_ALL_T = "select * from Order";
        private List<User.Reservations> _reservations;

        private List<User.Ticket> _tickets;

        public List<User.Ticket> Tickets
        {
            get { return _tickets; }
            set
            {
                _tickets = value;
                OnPropertyChanged("Tickets");
            }
        }

        public List<User.Reservations> Reservations
        {
            get { return _reservations; }
            set
            {
                _reservations = value;
                OnPropertyChanged("Reservations");
            }
        }

        public User.Reservations CurrentOrder { get; set; }
        public User.Reservations SelectedOrder { get; set; }
        public string Price;
        

        public string Header { get; set; }

        protected override void initializeDataSource()
        {
            try
            {


                _dataAdapter = new CacheDataAdapter(SQL_SELECT_ALL, DbHelper.GetConnection());
                ItemsDataTable = new DataTable("RESERVATIONS");
                _dataAdapter.Fill(ItemsDataTable);

                Reservations = new List<User.Reservations>();

                foreach (DataRow row in ItemsDataTable.Rows)
                {
                    Reservations.Add(User.Reservations.OpenId(DbHelper.GetConnection(), row["ID"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }



        protected override void showEditView(EditMode editMode)
        {
            try
            {
                if (editMode == EditMode.Add)
                {
                    CurrentOrder = new User.Reservations(DbHelper.GetConnection());
                    //CurrentOrder.Tickets.Re
                }
                else
                {
                    CurrentOrder = User.Reservations.OpenId(DbHelper.GetConnection(), SelectedRow.Row["ID"].ToString());
                }

                IsViewMode = true;
                OrderViewDialog view = new OrderViewDialog();

                view.Closing += (sender, args) =>
                {
                    CurrentOrder.Close();
                };
                CancelCommand = new Command((o) => view.Close());

                Tickets = new List<User.Ticket>();
                foreach(User.Ticket ticket in CurrentOrder.Tickets)
                {
                    Tickets.Add(ticket);
                }
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
                                var state = CurrentOrder.Save();
                                initializeDataSource();
                                view.Close();
                            });
                            break;
                        }
                    case EditMode.Edit:
                        {
                            //  MessageBox.Show("CurrentUser.Phone.ToList()[0].ToString()");
                            IsViewMode = false;


                            EditFormTitle = "Редагування категорії: ";
                            ActionTitle = "Зберегти";

                            ActionCommand = new Command((o) =>
                            {
                                //      toCacheList();
                                var state = CurrentOrder.Save();
                                initializeDataSource();
                                view.Close();
                            });

                            break;
                        }
                    case EditMode.Delete:
                        {
                            EditFormTitle = "Видалення категорії: ";
                            ActionTitle = "Видалити";
                            ActionCommand = new Command((o) =>
                            {
                                if (User.Reservations.DeleteId(DbHelper.GetConnection(), CurrentOrder.Id()).IsOK)
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
                            EditFormTitle = "Перегляд категорії: ";
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

        public ReservationViewModel() : base()
        {
            Header = "Reservations";
        }


    }
}
