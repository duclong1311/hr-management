using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.States.Authenticator;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class StaffCVViewModel : BaseViewModel
    {
        private ObservableCollection<NhanSu> _list;
        public ObservableCollection<NhanSu> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private NhanSu _selectedItem;
        public NhanSu SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private string _filter;
        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                OnPropertyChanged();
                //LoadData();
            }
        }
        private readonly IRepository<NhanSu> _repository;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private readonly IAuthenticator _authenticator;
        private IUnitOfWork _unitOfWork;
        public ICommand StaffViewCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        //public ICommand AddCVCommand { get; set; }
        public StaffCVViewModel(IAuthenticator authenticator, IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<NhanSu> repository)
        {
            _authenticator = authenticator;
            _repository = repository;
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;

            LoadData();

            StaffViewCommand = new StaffViewCommand(authenticator, this, mainContentStore, viewModelFactory);

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                return true;

            }, async (p) =>
            {
                if (!_repository.AsQueryable().Any(x => x.Id == SelectedItem.Id))
                    return;

                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var NhanSu = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    await _repository.UpdateAsync(NhanSu);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });
        }

        public void LoadData()
        {
            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Include(x => x.QuaTrinhDaoTao).Include(x => x.BoPhan).ToList()); 

            if (!String.IsNullOrWhiteSpace(Filter))
            {
                List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter) || x.QuaTrinhDaoTao.NganhHoc.Contains(Filter)).ToList());
            }
        }
    }
}
