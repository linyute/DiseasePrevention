using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;

namespace DiseasePrevention.ViewModels.Vaccines
{
    public class ChildVaccinePageViewModel : BindableBase, INavigationAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ChildVaccinePageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;

            this.BirthDay = DateTime.Today;

            this.CaculateDate();
        }

        #region Navigation

        private readonly INavigationService _navigationService;

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Title")) { this.Title = (string)parameters["Title"]; }
        }

        #endregion

        #region Property

        private DateTime _birthDay;

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDay
        {
            get { return _birthDay; }
            set { SetProperty(ref _birthDay, value); }
        }

        private DateTime _birthDayPlus1Day;

        /// <summary>
        /// 生日+1日
        /// </summary>
        public DateTime BirthDayPlus1Day
        {
            get { return _birthDayPlus1Day; }
            set { SetProperty(ref _birthDayPlus1Day, value); }
        }

        private DateTime _birthDayPlus1Month;

        /// <summary>
        /// 生日+1月
        /// </summary>
        public DateTime BirthDayPlus1Month
        {
            get { return _birthDayPlus1Month; }
            set { SetProperty(ref _birthDayPlus1Month, value); }
        }

        private DateTime _birthDayPlus2Month;

        /// <summary>
        /// 生日+2月
        /// </summary>
        public DateTime BirthDayPlus2Month
        {
            get { return _birthDayPlus2Month; }
            set { SetProperty(ref _birthDayPlus2Month, value); }
        }

        private DateTime _birthDayPlus4Month;

        /// <summary>
        /// 生日+4月
        /// </summary>
        public DateTime BirthDayPlus4Month
        {
            get { return _birthDayPlus4Month; }
            set { SetProperty(ref _birthDayPlus4Month, value); }
        }

        private DateTime _birthDayPlus6Month;

        /// <summary>
        /// 生日+6月
        /// </summary>
        public DateTime BirthDayPlus6Month
        {
            get { return _birthDayPlus6Month; }
            set { SetProperty(ref _birthDayPlus6Month, value); }
        }

        private DateTime _birthDayPlus1Year;

        /// <summary>
        /// 生日+1年
        /// </summary>
        public DateTime BirthDayPlus1Year
        {
            get { return _birthDayPlus1Year; }
            set { SetProperty(ref _birthDayPlus1Year, value); }
        }

        private DateTime _birthDayPlus1Year3Month;

        /// <summary>
        /// 生日+1年又3月
        /// </summary>
        public DateTime BirthDayPlus1Year3Month
        {
            get { return _birthDayPlus1Year3Month; }
            set { SetProperty(ref _birthDayPlus1Year3Month, value); }
        }

        private DateTime _birthDayPlus1Year6Month;

        /// <summary>
        /// 生日+1年又6月
        /// </summary>
        public DateTime BirthDayPlus1Year6Month
        {
            get { return _birthDayPlus1Year6Month; }
            set { SetProperty(ref _birthDayPlus1Year6Month, value); }
        }

        private DateTime _birthDayPlus2Year3Month;

        /// <summary>
        /// 生日+2年又3月
        /// </summary>
        public DateTime BirthDayPlus2Year3Month
        {
            get { return _birthDayPlus2Year3Month; }
            set { SetProperty(ref _birthDayPlus2Year3Month, value); }
        }

        private DateTime _birthDayPlus5Year;

        /// <summary>
        /// 生日+5年
        /// </summary>
        public DateTime BirthDayPlus5Year
        {
            get { return _birthDayPlus5Year; }
            set { SetProperty(ref _birthDayPlus5Year, value); }
        }

        #endregion

        #region Command

        public DelegateCommand CaculateDateCommand => new DelegateCommand(CaculateDate);

        private void CaculateDate()
        {
            this.BirthDayPlus1Day = this.BirthDay.AddDays(1);
            this.BirthDayPlus1Month = this.BirthDay.AddMonths(1);
            this.BirthDayPlus2Month = this.BirthDay.AddMonths(2);
            this.BirthDayPlus4Month = this.BirthDay.AddMonths(4);
            this.BirthDayPlus6Month = this.BirthDay.AddMonths(6);
            this.BirthDayPlus1Year = this.BirthDay.AddYears(1);
            this.BirthDayPlus1Year3Month = this.BirthDay.AddYears(1).AddMonths(3);
            this.BirthDayPlus1Year6Month = this.BirthDay.AddYears(1).AddMonths(6);
            this.BirthDayPlus2Year3Month = this.BirthDay.AddYears(2).AddMonths(3);
            this.BirthDayPlus5Year = this.BirthDay.AddYears(5);
        }

        #endregion
    }
}
