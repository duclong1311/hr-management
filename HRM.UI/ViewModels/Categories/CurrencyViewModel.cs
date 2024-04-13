using CommunityToolkit.Mvvm.Input;
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
using Terp.UI.Services;
using Terp.UI.Services.LogService;
using Terp.UI.ViewModels.Abstract;

namespace Terp.UI.ViewModels
{
    public class CurrencyViewModel : BaseViewModel
    {
        private string _currencyCodeBase;
        public string CurrencyCodeBase
        {
            get 
            {
                return _currencyCodeBase; 
            }
            set 
            {
                _currencyCodeBase = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> CurrencyCodeBaseList { get; set; }
        private ObservableCollection<Currency> _list = new ObservableCollection<Currency>();
        public ObservableCollection<Currency> List 
        {
            get => _list;
            set 
            { 
                _list = value;
                OnPropertyChanged();
            }
        }
        private Currency _selectedItem;
        public Currency SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    DisplayName = _selectedItem.Name;
                    Code = _selectedItem.Code;
                    ExchangeRate = _selectedItem.ExchangeRate.ToString();
                }
                OnPropertyChanged();
            }
        }
        private string _displayName;
        public string DisplayName 
        {
            get => _displayName;
            set 
            {
                _displayName = value;
                OnPropertyChanged(); 
            } 
        }
        private string _code;
        public string Code 
        {
            get => _code;
            set 
            {
                _code = value;
                OnPropertyChanged(); 
            }
        }
        private string _exchangeRate = "0";
        public string ExchangeRate 
        {
            get => _exchangeRate;
            set 
            {
                _exchangeRate = value;
                OnPropertyChanged(); 
            }
        }


        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand UpdateExchangeRateCommand
        {
            get
            {
                return new Abstract.RelayCommand<object>((p) =>
                {
                    return CurrencyCodeBase != null;
                }, (p) =>
                {
                    _exchangeRateCurrencyService.Update(CurrencyCodeBase);
                    LoadData();
                });
            }
        }
        private IRepository<Currency> _currencyRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IExchangeRateCurrencyService _exchangeRateCurrencyService;
        private readonly ILogger _logger;

        public CurrencyViewModel(IRepository<Currency> currencyRepository, IUnitOfWork unitOfWork,IExchangeRateCurrencyService exchangeRateCurrencyService,ILogger logger)
        {
            _currencyRepository = currencyRepository;
            _unitOfWork = unitOfWork;
            _exchangeRateCurrencyService = exchangeRateCurrencyService;
            _logger = logger;
            //_exchangeRateCurrencyService.Update(CurrencyCodeBase);
            LoadData();
            CurrencyCodeBaseList = new ObservableCollection<string>();
            foreach(var  currency in List)
            {
                CurrencyCodeBaseList.Add(currency.Code);
            }
            AddCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Code) || string.IsNullOrEmpty(ExchangeRate))
                    return false;

                if (_currencyRepository.AsQueryable().Any(x => x.Name == DisplayName))
                    return false;

                return true;

            }, async (p) =>
            {
                var currency = new Currency() { Name = DisplayName, ExchangeRate = double.Parse(ExchangeRate), Code = Code };

                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    currency = await _currencyRepository.AddAsync(currency);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(currency, Defines.EModifyTypes.Added);
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

                if (!_currencyRepository.AsQueryable().Any(x => x.Id == SelectedItem.Id))
                    return false;

                return true;

            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var currency = await _currencyRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    currency.Name = DisplayName;
                    currency.ExchangeRate = double.Parse(ExchangeRate);
                    currency.Code = Code;
                    await _currencyRepository.UpdateAsync(currency);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(currency, Defines.EModifyTypes.Updated);

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
                if (!_currencyRepository.AsQueryable().Any(x => x.Id == SelectedItem.Id))
                    return;

                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var currency = await _currencyRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    currency.IsDelete = true;
                    await _currencyRepository.UpdateAsync(currency);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(currency, Defines.EModifyTypes.Deleted);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

        }

        private void LoadData()
        {
            List = new ObservableCollection<Currency>(_currencyRepository.AsQueryable().Where(x => !x.IsDelete).ToList());
        }
    }
}
