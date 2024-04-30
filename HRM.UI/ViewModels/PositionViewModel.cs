using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HRM.UI.ViewModels
{
    public class PositionViewModel : BaseViewModel
    {
        private ObservableCollection<ChucVu> _list = new ObservableCollection<ChucVu>();
        public ObservableCollection<ChucVu> List { get => _list; set { _list = value; OnPropertyChanged(); } }
        private ChucVu _selectedItem;
        public ChucVu SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    DisplayName = _selectedItem.TenChucVu;
                }
                OnPropertyChanged();
            }
        }
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private IRepository<ChucVu> _repository;
        private IUnitOfWork _unitOfWork;

        public PositionViewModel(IRepository<ChucVu> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            LoadData();
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                if (_repository.AsQueryable().Any(x => x.TenChucVu == DisplayName))
                    return false;
                return true;
            }, async (p) =>
            {
                var unit = new ChucVu() { TenChucVu = DisplayName };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    unit = await _repository.AddAsync(unit);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            UpdateCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                if (!_repository.AsQueryable().Any(x => x.Id == SelectedItem.Id))
                    return false;

                return true;

            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var unit = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    unit.TenChucVu = DisplayName;
                    await _repository.UpdateAsync(unit);
                    await _unitOfWork.CommitAsync();

                    LoadData();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

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
                    var unit = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    await _repository.DeleteAsync(unit);
                    await _unitOfWork.CommitAsync();

                    LoadData();
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

        }
        private void LoadData()
        {
            List = new ObservableCollection<ChucVu>(_repository.AsQueryable().ToList());
        }
    }
}

