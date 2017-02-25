using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cache_lab5.ViewModels
{
    public class ViewModel
    {
        private ObservableCollection<IPageBase> _pages;

        public ObservableCollection<IPageBase> Pages
        {
            get { return _pages; }
            set { _pages = value; }
        }

        public ViewModel()
        {
            _pages = new ObservableCollection<IPageBase>();
            _pages.Add(new CategoryViewModel());
            _pages.Add(new UserViewModel());
            _pages.Add(new ReservationViewModel());
            /*
            _pages.Add(new FilmViewModel());
           // _pages.Add(new CategoryViewModel());*/
        }
    }
}
