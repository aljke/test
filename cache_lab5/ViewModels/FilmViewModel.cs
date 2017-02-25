using cache_lab5.Common;
using cache_lab5.Interfaces;
using cache_lab5.Models;
using cache_lab5.Views;
using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace cache_lab5.ViewModels
{
    public class FilmViewModel //: ViewModelBase, IPageBase
    {
        /*
        private User.Film _film;

        private string _id;
        private string _category;
        private string _description;
        private string _length;
        private string _ratings;
        private string _trailers;

        private CacheConnection connection;

        private ICommand _showAddCommand;
        private ICommand _findCommand;
        private ICommand _removeCommand;
        private ICommand _updateCommand;

        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("Category");
            }
        }

        public string Length
        {
            get { return _length; }
            set
            {
                _length = value;
                OnPropertyChanged("Length");
            }
        }

        public string Trailers
        {
            get { return _trailers; }
            set
            {
                _trailers = value;
                OnPropertyChanged("Trailers");
            }
        }

        public string Ratings
        {
            get { return _ratings; }
            set
            {
                _ratings = value;
                OnPropertyChanged("Ratings");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public User.Film Film
        {
            get { return _film; }
            set
            {
                _film = value;
                OnPropertyChanged("Film");
            }
        }

        public string Header { get; set; }

        public FilmViewModel()
        {
            Header = "Film";
            connection = DbHelper.getConnection();
            connection.Open();
        }

        public ICommand ShowAddCommand
        {
            get
            {
                if (_showAddCommand == null)
                {
                    _showAddCommand = new CommandBase(i => this.ShowAddDialog(), null);
                }
                return _showAddCommand;
            }
        }

        public ICommand FindCommand
        {
            get
            {
                if (_findCommand == null)
                {
                    _findCommand = new CommandBase(i => this.Find(), null);
                }
                return _findCommand;
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                {
                    _updateCommand = new CommandBase(i => this.ShowUpdateDialog(), null);
                }
                return _updateCommand;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new CommandBase(i => this.Delete(), null);
                }
                return _removeCommand;
            }
        }

        private void ShowAddDialog()
        {

            FilmDialogViewModel viewModel = new FilmDialogViewModel(connection);
            viewModel.Mode = Mode.Add;
            IModalDialog dialog = new FilmViewLogic();
            dialog.BindViewModel(viewModel);
            dialog.ShowDialog();
        }

        private void ShowUpdateDialog()
        {
            if (_film != null)
            {
                FilmDialogViewModel viewModel = new FilmDialogViewModel(_film);
                viewModel.Mode = Mode.Edit;
                IModalDialog dialog = new FilmViewLogic();
                dialog.BindViewModel(viewModel);
                dialog.ShowDialog();
            }
        }

        private void Find()
        {
            try
            {
                Category = "";
                _film = User.Film.OpenId(connection, this.Id);
                Description = _film.Description;
                List<User.Category> categories = _film.Category.ToList();
                foreach(User.Category category in categories)
                {
                    Category += category.CategoryName + ",";
                }
                if (Category.Length > 1)
                {
                    Category = Category.Remove(Category.Length - 1);
                }
                Length =  _film.Length.ToString();
                var keys = _film.Ratings; 
                Ratings = "";
                foreach(var str in keys)
                {
                    Ratings += str.Key + " - " + str.Value + ", ";
                }
                if (Ratings.Length > 1)
                {
                    Ratings = Ratings.Remove(Ratings.Length - 2);
                }
                Trailers = _film.Trailers.Filename();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void Delete()
        {
            if (_film != null)
            {
                User.User.DeleteId(connection, _id);
            }
        }*/
    }
}
