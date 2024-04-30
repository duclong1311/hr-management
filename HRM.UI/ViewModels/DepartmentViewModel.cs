using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using HRM.UI.Commands;
using HRM.UI.Factories;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HRM.UI.ViewModels
{
    public class DepartmentViewModel : BaseViewModel
    {
        private ObservableCollection<BoPhan> _list = new ObservableCollection<BoPhan>();
        public ObservableCollection<BoPhan> List { get => _list; set { _list = value; OnPropertyChanged(); } }
        private BoPhan _selectedItem;
        public BoPhan SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    DisplayName = _selectedItem.TenBoPhan;
                }
                OnPropertyChanged();
            }
        }
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private IRepository<BoPhan> _repository;
        private IUnitOfWork _unitOfWork;

        public DepartmentViewModel(IRepository<BoPhan> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            LoadData();
            AddCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                if (_repository.AsQueryable().Any(x => x.TenBoPhan == DisplayName))
                    return false;
                return true;
            }, async (p) =>
            {
                var unit = new BoPhan() { TenBoPhan = DisplayName };
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
                    unit.TenBoPhan = DisplayName;
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
            List = new ObservableCollection<BoPhan>(_repository.AsQueryable().ToList());
        }
    }
}
