using EventManager.Entities;
using EventManager.Helpers;
using EventManager.Services;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;


namespace EventManager.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        
        public RelayCommand AddOfficerGrade { get; private set; }
        public RelayCommand DeleteOfficerGrade { get; private set; }
        public RelayCommand SaveOfficerGrade { get; private set; }

        public RelayCommand AddNewOfficer { get; private set; }
        public RelayCommand SaveOfficerDetail { get; private set; }
        public RelayCommand DeleteOfficer { get; private set; }
        IOfficerGrade repo;

        IOfficers officerRepo;

        #region Constructor
        public AdminViewModel()
        {
            if (!ViewModelBase.IsInDesignModeStatic)
            {

                AddOfficerGrade = new RelayCommand(OnAdd);
                DeleteOfficerGrade = new RelayCommand(OnDelete, CanDelete);
                SaveOfficerGrade = new RelayCommand(OnSave, CanSave);

                AddNewOfficer = new RelayCommand(OnAddOfficer,canAddOfficer);
                SaveOfficerDetail = new RelayCommand(OnSaveOfficerDetail, canSaveOfficer);
                DeleteOfficer = new RelayCommand(OnDeleteOfficer, canDeleteOfficer);
                repo = new OfficerGradeRepo();
                officerRepo = new OfficerRepo();
            }
        }

        private bool canDeleteOfficer()
        {
            return SelectedOfficer != null;
        }

        private void OnDeleteOfficer()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                officerRepo.DeleteOfficer(SelectedOfficer);
                SelectedOfficerGrade = null;
                LoadData();
            }
        }

        private bool canSaveOfficer()
        {
            return SelectedOfficer != null;
        }

        private void OnSaveOfficerDetail()
        {
            officerRepo.UpdateOfficer(SelectedOfficer);
        }




        #endregion

        #region Helper methods
        private bool CanSave()
        {
            return SelectedOfficerGrade != null;
        }

        private void OnSave()
        {
            repo.UpdateOfficerGrade(SelectedOfficerGrade);
            LoadData();
        }

        private bool CanDelete()
        {
            return SelectedOfficerGrade != null;
        }

        private void OnDelete()
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                repo.DeleteOfficerGrader(SelectedOfficerGrade);
                SelectedOfficerGrade = null;
                LoadData();
            }
            
        }

        private void OnAdd()
        {
            OfficerGradeType officerGrade = new OfficerGradeType { OfficerGrade = NewGradeName };
            repo.AddOfficerGradeAsync(officerGrade);
            LoadData();
            NewGradeName = "";
        }

        private void OnAddOfficer()
        {
            officerRepo.AddOfficer(new Officers
            {
                Name = NewOfficerName,
                PhoneNumber = NewOfficerPhoneNumber,
                OfficerGrade = GradeOfNewOfficer
            });
            NewOfficerName = "";
            NewOfficerPhoneNumber = "";
            LoadData();
        }

        private bool canAddOfficer()
        {
            return (NewOfficerName!=null? NewOfficerName.Length > 0:false) && GradeOfNewOfficer != null;
        }
        #endregion

        #region Observable collections

        //for storing new grades
        private ObservableCollection<OfficerGradeType> _officerGrade;
        public ObservableCollection<OfficerGradeType> OfficerGradeTypes
        {
            get { return _officerGrade; }
            set
            {
                _officerGrade = value;
                RaisePropertyChanged("OfficerGradeTypes");
            }
        }

        //For getting selected officer grade
        private OfficerGradeType _selectedOfficerGrade;
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
                RaisePropertyChanged("SelectedOfficerGrade");
            }
        }
        //for storing new grades 
        private string _newGradeName = "";
        public string NewGradeName
        {
            get
            {
                return _newGradeName;
            }
            set
            {
                _newGradeName = value;
                RaisePropertyChanged("NewGradeName");
            }

        }

        //for adding grades in officer list. Selected item while adding new officer
        private OfficerGradeType _gradeOfNewOfficer;
        public OfficerGradeType GradeOfNewOfficer
        {
            get
            {
                return _gradeOfNewOfficer;
            }
            set
            {
                _gradeOfNewOfficer = value;
                RaisePropertyChanged("GradeOfNewOfficer");
                AddNewOfficer.RaiseCanExecuteChanged();
                RefreshList();
            }
        }

        private string _officerName;
        public string NewOfficerName
        {
            get
            {
                return _officerName;
            }
            set
            {
                _officerName = value;
                RaisePropertyChanged("NewOfficerName");
                AddNewOfficer.RaiseCanExecuteChanged();
            }
        }

        private string _officerPhoneNumber;
        public string NewOfficerPhoneNumber
        {
            get
            {
                return _officerPhoneNumber;
            }
            set
            {
                _officerPhoneNumber = value;
                RaisePropertyChanged("NewOfficerPhoneNumber");
            }
        }

        private ObservableCollection<Officers> _officerList;
        public ObservableCollection<Officers> OfficerList
        {
            get
            {
                return _officerList;
            }
            set
            {
                _officerList = value;
                RaisePropertyChanged("OfficerList");
            }
        } 

        private ObservableCollection<Officers> _filteredList;
        public ObservableCollection<Officers> FilteredOfficerList
        {
            get
            {
                return _filteredList;
            }
            set
            {
                _filteredList = value;
                RaisePropertyChanged("FilteredOfficerList");
            }
        }

        private Officers _selectedOfficer;
        public Officers SelectedOfficer
        {
            get
            {
                return _selectedOfficer;
            }
            set
            {
                _selectedOfficer = value;
                RaisePropertyChanged("SelectedOfficer");
                SaveOfficerDetail.RaiseCanExecuteChanged();
                DeleteOfficer.RaiseCanExecuteChanged();
                
            }
        }
      


        private bool Filter(object item )
        {
            Officers officer = item as Officers;
            if (officer.OfficerGrade.Equals(GradeOfNewOfficer))
            {
                return true;
            }
            return false;
        }
        


        #endregion

        #region methods
        public void LoadData()
        {
            ObservableCollection<OfficerGradeType> Temp= new ObservableCollection<OfficerGradeType>();
            var data=repo.GetAllOfficerGrades();
            foreach (var item in data)
            {
                    Temp.Add(item);
            }
            OfficerGradeTypes = Temp;
            GradeOfNewOfficer = OfficerGradeTypes.First();


            ObservableCollection<Officers> Temp1 = new ObservableCollection<Officers>();
            var officerData = officerRepo.GetAllOfficers();
            foreach (var item in officerData)
            {

                Temp1.Add(item);
            }
            OfficerList = Temp1;
            FilteredOfficerList = new ObservableCollection<Officers>();
            RefreshList();
            
        }


        public void RefreshList()
        {
            if(FilteredOfficerList!=null)
                FilteredOfficerList.Clear();

            if(OfficerList != null)
            {
                foreach (var item in OfficerList)
                {
                    
                    if (item.OfficerGrade.JsonCompare(GradeOfNewOfficer))
                    {
                        FilteredOfficerList.Add(item);
                    }
                }
            }
           
        }
        
        #endregion



    }
}
