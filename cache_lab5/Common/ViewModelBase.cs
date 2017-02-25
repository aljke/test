using cache_lab5.ViewModels;
using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace cache_lab5.Common
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected DataTable _itemsDataTable;
        protected DataRowView _selectedRow;
        protected CacheDataAdapter _dataAdapter;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public DataTable ItemsDataTable
        {
            get { return _itemsDataTable; }
            set
            {
                _itemsDataTable = value;
                OnPropertyChanged("ItemsDataTable");
            }
        }

        public DataRowView SelectedRow
        {
            get { return _selectedRow; }
            set
            {
                _selectedRow = value;
                OnPropertyChanged("SelectedRow");
            }
        }

        public string EditFormTitle { get; set; }
        public string ActionTitle { get; set; }

        public ICommand CancelCommand { get; set; }
        public ICommand ActionCommand { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand ShowCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public bool IsViewMode { get; set; }
        public bool IsEditMode => !IsViewMode;

        protected ViewModelBase()
        {
            initializeDataSource();
            AddCommand = new Command((o) => showEditView(EditMode.Add));
            ShowCommand = new Command((o) => showEditView(EditMode.Show));
            EditCommand = new Command((o) => showEditView(EditMode.Edit));
            DeleteCommand = new Command((o) => showEditView(EditMode.Delete));
        }

        protected abstract void initializeDataSource();
        protected abstract void showEditView(EditMode editMode);
    }
}
