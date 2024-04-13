using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Packaging;
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
    public class ProductViewModel : BaseViewModel
    {
        private ObservableCollection<Product> _list = new ObservableCollection<Product>();
        public ObservableCollection<Product> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Unit> _listUnit = new ObservableCollection<Unit>();
        public ObservableCollection<Unit> ListUnit
        {
            get => _listUnit;
            set
            {
                _listUnit = value;
                OnPropertyChanged();
            }
        }
        private Unit _selectedCbo;

        public Unit SelectedCbo
        {
            get { return _selectedCbo; }
            set { _selectedCbo = value; }
        }

        private Product _selectedItem;
        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    DisplayName = _selectedItem.Name;
                    Code = _selectedItem.Code;
                    Description = _selectedItem.Description;

                }
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
                LoadData();
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
        private string _description;
        public string Description { get => _description; set { _description = value; OnPropertyChanged(); } }
        private bool _canReadOnly = false;
        public bool CanReadOnly { get => _canReadOnly; set { _canReadOnly = value; OnPropertyChanged(); } }
        private string _filterUnit = "";
        public string FilterUnit { get => _filterUnit; set { _filterUnit = value; OnPropertyChanged(); LoadCombobox(); } }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        private IRepository<Product> _repository;
        private IRepository<Unit> _unitRepository;
        private readonly ILogger _logger;
        private IUnitOfWork _unitOfWork;

        public ProductViewModel(IRepository<Product> repository, IUnitOfWork unitOfWork, IRepository<Unit> repositoryUnit,ILogger logger)
        {
            _repository = repository;
            _unitRepository = repositoryUnit;
            _logger = logger;
            _unitOfWork = unitOfWork;
            LoadCombobox();
            LoadData();
            AddCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                if (_repository.AsQueryable().Any(x => x.Code == Code))
                    return false;
                if(SelectedCbo == null) return false;
                return true;
            }, async (p) =>
            {
                var product = new Product() { Name = DisplayName, Code = Code, Description = Description, UnitId = SelectedCbo.Id };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    product = await _repository.AddAsync(product);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(product, Defines.EModifyTypes.Added);
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
                if(_repository.AsQueryable().FirstOrDefaultAsync(x => x.Code != SelectedItem.Code && x.Name == SelectedItem.Name) != null)
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
                    var product = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    product.Name = DisplayName;
                    product.Code = Code;
                    product.Description = Description;
                    product.UnitId = SelectedCbo.Id;
                    await _repository.UpdateAsync(product);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(product, Defines.EModifyTypes.Updated);
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
                    var product = await _repository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    product.IsDeleted = true;
                    await _repository.UpdateAsync(product);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(product, Defines.EModifyTypes.Deleted);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });

            ResetCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if(String.IsNullOrEmpty(DisplayName) && String.IsNullOrEmpty(Description) && String.IsNullOrEmpty(Code))
                    return false;
                return true;
            }, async (p) =>
            {
                try
                {
                    DisplayName = String.Empty;
                    Code = String.Empty;
                    Description = String.Empty;
                    CanReadOnly = false;
                    SelectedItem = null;
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            }
            );
            LoadData();
        }
        private void LoadCombobox()
        {
            ListUnit = new ObservableCollection<Unit>(_unitRepository.AsQueryable().Where(x => x.Name.Contains(FilterUnit)).ToList());
            SelectedCbo = ListUnit.FirstOrDefault();
        }

        private void LoadData()
        {
            List = new ObservableCollection<Product>(_repository.AsQueryable().Where(x => !x.IsDeleted).Include(p => p.Unit).ToList());
            if (!String.IsNullOrWhiteSpace(Filter))
            {
                List = new ObservableCollection<Product>(_repository.AsQueryable().Where(x => !x.IsDeleted).Where(x => x.Name.Contains(Filter) || x.Code.Contains(Filter)).ToList());
            }
        }
    }
}
