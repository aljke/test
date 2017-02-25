using cache_lab5.Common;
using cache_lab5.Interfaces;
using cache_lab5.Models;
using cache_lab5.Views;
using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using User;

namespace cache_lab5.ViewModels
{
    public class CategoryViewModel : ViewModelBase, IPageBase
    {

        private readonly string SQL_SELECT_ALL = "select * from Category";
        //private List<Category> _categories;

        public string Header {get; set; }
        /*
        public List<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }*/

        public Category CurrentCategory { get; set; }

        protected override void initializeDataSource()
        {
            _dataAdapter = new CacheDataAdapter(SQL_SELECT_ALL, DbHelper.GetConnection());
            ItemsDataTable = new DataTable("CATEGORIES");
            _dataAdapter.Fill(ItemsDataTable);
            /*
            Categories = new List<Category>();
                
            foreach (DataRow row in ItemsDataTable.Rows)
            {
                Categories.Add(Category.OpenId(DbHelper.GetConnection(), row["ID"].ToString()));
            }*/
        }

        protected override void showEditView(EditMode editMode)
        {
            if (editMode == EditMode.Add)
            {
                CurrentCategory = new Category(DbHelper.GetConnection());
            }
            else
            {
                CurrentCategory = Category.OpenId(DbHelper.GetConnection(), SelectedRow.Row["ID"].ToString());
            }
           
            IsViewMode = true;
            CategoryViewDialog view = new CategoryViewDialog();

            view.Closing += (sender, args) =>
            {
                CurrentCategory.Close();
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
                            var state = CurrentCategory.Save();
                            initializeDataSource();
                            view.Close();
                        });
                        break;
                    }
                case EditMode.Edit:
                    {
                        IsViewMode = false;
                        EditFormTitle = "Редагування категорії: " + CurrentCategory.CategoryName;
                        ActionTitle = "Зберегти";
                        ActionCommand = new Command((o) =>
                        {
                            var status = CurrentCategory.Save();
                            initializeDataSource();
                            view.Close();
                        });
                        break;
                    }
                case EditMode.Delete:
                    {
                        EditFormTitle = "Видалення категорії: " + CurrentCategory.CategoryName;
                        ActionTitle = "Видалити";
                        ActionCommand = new Command((o) =>
                        {
                            if (Category.DeleteId(DbHelper.GetConnection(), CurrentCategory.Id()).IsOK)
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
                        EditFormTitle = "Перегляд категорії: " + CurrentCategory.CategoryName;
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

        public CategoryViewModel() : base()
        {
            Header = "Category";
        }
    }
}
