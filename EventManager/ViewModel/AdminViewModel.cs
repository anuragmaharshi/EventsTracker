using EventManager.Entities;
using EventManager.Helpers;
using EventManager.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EventManager.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        ObservableCollection<OfficerGradeType> _officerGrade;
        public RelayCommand AddOfficerGrade { get; private set; }
        public RelayCommand DeleteOfficerGrade { get; private set; }
        public RelayCommand SaveOfficerGrade { get; private set; }
        private OfficerGradeType _selectedOfficerGrade;
        private string _officerGradeName = "";
        IOfficerGrade repo;
        public AdminViewModel()
        {
            if (!ViewModelBase.IsInDesignModeStatic)
            {

                AddOfficerGrade = new RelayCommand(OnAdd);
                DeleteOfficerGrade = new RelayCommand(OnDelete, CanDelete);
                SaveOfficerGrade = new RelayCommand(OnSave, CanSave);
                repo = new OfficerGradeRepo();
            }

        }

        #region Helper methods
        private bool CanSave()
        {
            throw new NotImplementedException();
        }

        private void OnSave()
        {
            throw new NotImplementedException();
        }

        private bool CanDelete()
        {
            throw new NotImplementedException();
        }

        private void OnDelete()
        {
            throw new NotImplementedException();
        }

        private void OnAdd()
        {
            OfficerGradeType officerGrade = new OfficerGradeType { OfficerGrade = NewOfficerGradeName };
            repo.AddOfficerGradeAsync(officerGrade);
        }
        #endregion

        public ObservableCollection<OfficerGradeType> OfficerGradeTypes
        {
            get { return _officerGrade; }
            set { _officerGrade = value; RaisePropertyChanged("OfficerGradeTypes"); }
        }
        public OfficerGradeType SelectedOfficerGrade
        {
            get
            {
                return _selectedOfficerGrade;
            }
            set
            {
                _selectedOfficerGrade = value;
                DeleteOfficerGrade.RaiseCanExecuteChanged();
                SaveOfficerGrade.RaiseCanExecuteChanged();
            }
        }

        public string NewOfficerGradeName
        {
            get
            {
                return _officerGradeName;
            }
            set
            {
                _officerGradeName = value;
                RaisePropertyChanged(NewOfficerGradeName);
            }

        }


        public void LoadData()
        {
            ObservableCollection<OfficerGradeType> Temp= new ObservableCollection<OfficerGradeType>();
            var data=repo.GetAllOfficerGrades();
            foreach (var item in data)
            {
                    Temp.Add(item);
            }
            OfficerGradeTypes = Temp;
        }

    }
}
