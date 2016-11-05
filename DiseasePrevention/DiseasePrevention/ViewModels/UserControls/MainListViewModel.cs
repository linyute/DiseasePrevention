using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DiseasePrevention.Models;

namespace DiseasePrevention.ViewModels.UserControls
{
    public class MainListViewModel : BindableBase
    {
        public MainListViewModel()
        {

        }

        #region List

        private ObservableCollection<MainListItem> _itemsSource = new ObservableCollection<MainListItem>();
        public ObservableCollection<MainListItem> ItemsSource
        {
            get { return _itemsSource; }
            set { SetProperty(ref _itemsSource, value); }
        }

        private MainListItem _selectedItem;
        public MainListItem SelectedItem
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
