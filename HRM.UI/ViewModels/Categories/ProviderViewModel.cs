using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Terp.Core.Repositories;
using Terp.Core.UnitOfWorks;
using Terp.Domain.Models;
using Terp.UI.Services.LogService;
using Terp.UI.ViewModels.Abstract;

namespace Terp.UI.ViewModels
{
    public class ProviderViewModel : BaseViewModel
    {
        private ObservableCollection<Provider> _list = new ObservableCollection<Provider>();
        public ObservableCollection<Provider> List { get => _list; set { _list = value; OnPropertyChanged(); } }
        private Provider _selectedItem;
        public Provider SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    DisplayName = _selectedItem.Name;
                    Code = _selectedItem.Code;
                    Email = _selectedItem.Email;
                    PhoneNumber = _selectedItem.PhoneNumber;
                    Address = _selectedItem.Address;
                }
                OnPropertyChanged();
            }
        }
        private string _filter;
        public string Filter { get => _filter; set { _filter = value; OnPropertyChanged(); LoadData(); } }

        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }
        private string _code;
        public string Code { get => _code; set { _code = value; OnPropertyChanged(); } }
        private string _phoneNumber;
        public string PhoneNumber { get => _phoneNumber; set { _phoneNumber = value; OnPropertyChanged(); } }
        private string _address;
        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }
        private string _email;
        public string Email { get => _email; set { _email = value; OnPropertyChanged(); } }
        private bool _canReadOnly = false;
        public bool CanReadOnly { get => _canReadOnly; set { _canReadOnly = value; OnPropertyChanged(); } }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        private IRepository<Provider> _repository;
        private IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public ProviderViewModel(IRepository<Provider> repository, IUnitOfWork unitOfWork, ILogger logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            LoadData();
            AddCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Address))
                    return false;

                if (_repository.AsQueryable().Any(x => x.Code == Code))
                    return false;

                return true;

            }, async (p) =>
            {
                var provider = new Provider() { Name = DisplayName, PhoneNumber = PhoneNumber, Code = Code, Address = Address, Email = Email };

                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    provider = await _repository.AddAsync(provider);
                    await _unitOfWork.CommitAsync();
                    LoadData();
                    _logger.LogWrite(provider, Defines.EModifyTypes.Added);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            UpdateCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                if (_repository.AsQueryable().FirstOrDefaultAsync(x => x.Code == SelectedItem.Code && x.Name == SelectedItem.Name) != null)
                {
                    CanReadOnly = true;
                    return true;

                }
                if (!_repository.AsQueryable().Any(x => x.Id == SelectedItem.Id))
                    return false;

                return true;

            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var provider = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    provider.Name = DisplayName;
                    provider.Code = Code;
                    provider.Address = Address;
                    provider.Email = Email;
                    provider.PhoneNumber = PhoneNumber;
                    await _repository.UpdateAsync(provider);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(provider, Defines.EModifyTypes.Updated);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            DeleteCommand = new Abstract.RelayCommand<object>((p) =>
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
                    var provider = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    await _repository.DeleteAsync(provider);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(provider, Defines.EModifyTypes.Deleted);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            ResetCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (String.IsNullOrEmpty(DisplayName) && String.IsNullOrEmpty(Code) && String.IsNullOrEmpty(Address) && String.IsNullOrEmpty(Email) && String.IsNullOrEmpty(PhoneNumber))
                    return false;
                return true;
            }, async (p) =>
            { 
                try
                {
                    DisplayName = String.Empty;
                    Code = String.Empty;
                    Address = String.Empty;
                    Email = String.Empty;
                    PhoneNumber = String.Empty;
                    CanReadOnly = false;
                    SelectedItem = null;
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            }
            );

        }
        private void LoadData()
        {
            List = new ObservableCollection<Provider>(_repository.AsQueryable().ToList());
            if (!String.IsNullOrWhiteSpace(Filter))
            {
                List = new ObservableCollection<Provider>(_repository.AsQueryable().Where(x => x.Name.Contains(Filter) || x.Code.Contains(Filter)).ToList());
            }
        }
    }
}
