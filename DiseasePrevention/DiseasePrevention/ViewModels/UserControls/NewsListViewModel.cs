using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiseasePrevention.Models;

namespace DiseasePrevention.ViewModels.UserControls
{
    public class NewsListViewModel : BindableBase
    {
        public NewsListViewModel()
        {

        }

        #region List

        private ObservableCollection<NewsListItem> _itemsSource = new ObservableCollection<NewsListItem>();
        public ObservableCollection<NewsListItem> ItemsSource
        {
            get { return _itemsSource; }
            set { SetProperty(ref _itemsSource, value); }
        }

        private NewsListItem _selectedItem;
        public NewsListItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        #endregion

        #region Command

        public DelegateCommand ItemSelectedCommand { get; set; }

        #endregion
    }
}
