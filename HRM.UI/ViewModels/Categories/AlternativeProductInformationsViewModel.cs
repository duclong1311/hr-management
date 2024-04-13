using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
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
    public class AlternativeProductInformationsViewModel : BaseViewModel
    {
        private ObservableCollection<Product> _list;
        public ObservableCollection<Product> ListProduct 
        {
            get => _list;
            set 
            {
                _list = value;
                OnPropertyChanged(); 
            }
        }

        private ObservableCollection<AlternativeProductInformation> _listAlternative;
        public ObservableCollection<AlternativeProductInformation> ListAlternative 
        {
            get => _listAlternative;
            set 
            { 
                _listAlternative = value;
                OnPropertyChanged(); 
            } 
        }

        private string _codeAlternative;

        public string CodeAlternative
        {
            get 
            {
                return _codeAlternative; 
            }
            set 
            {
                _codeAlternative = value;
                OnPropertyChanged(); 
            }
        }

        private string _filter;

        public string Filter
        {
            get 
            {
                return _filter; 
            }
            set 
            {
                _filter = value;
                OnPropertyChanged();
                LoadData();
            }
        }
        private string _codeProduct;

        public string CodeProduct
        {
            get 
            {
                return _codeProduct; 
            }
            set 
            {
                _codeProduct = value;
                OnPropertyChanged(); 
            }
        }

        private Product _selectedProduct;

        public Product SelectedProduct
        {
            get 
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }
        private AlternativeProductInformation _selectedCode;

        public AlternativeProductInformation SelectedCode
        {
            get { return _selectedCode; }
            set
            {
                _selectedCode = value;
                if (SelectedCode != null)
                {
                    CodeAlternative = SelectedCode.AlternativeProductCode;
                    CodeProduct = SelectedCode.OriginalProductCode;
                }
                OnPropertyChanged();
            }
        }


        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddOldCodeCommand { get; set; }
        public ICommand AddNewCodeCommand { get; set; }

        private IRepository<Product> _repositoryProduct;
        private IRepository<AlternativeProductInformation> _repositoryAlternativeProductInformation;
        private IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public AlternativeProductInformationsViewModel(IRepository<Product> repositoryProduct, IRepository<AlternativeProductInformation> repositoryAlternativeProductInformation, IUnitOfWork unitOfWork,ILogger logger)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryAlternativeProductInformation = repositoryAlternativeProductInformation;
            _unitOfWork = unitOfWork;
            _logger = logger;
            AddCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(CodeAlternative))
                    return false;
                if (_repositoryAlternativeProductInformation.AsQueryable().Any(x => x.OriginalProductCode == CodeProduct && x.AlternativeProductCode == CodeAlternative))
                    return false;
                if (CodeAlternative.Trim() == CodeProduct.Trim())
                    return false;
                return true;

            }, async (p) =>
            {
                var alternative = new AlternativeProductInformation() { OriginalProductCode = CodeProduct, AlternativeProductCode = CodeAlternative };

                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    alternative = await _repositoryAlternativeProductInformation.AddAsync(alternative);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(alternative, Defines.EModifyTypes.Added);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });
            AddOldCodeCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (SelectedProduct == null)
                    return false;
                return true;

            }, (p) =>
            {

                try
                {
                    CodeProduct = SelectedProduct.Code;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });

            AddNewCodeCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (SelectedProduct == null)
                    return false;
                return true;

            }, (p) =>
            {
                try
                {
                    CodeAlternative = SelectedProduct.Code;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });

            DeleteCommand = new Abstract.RelayCommand<object>((p) =>
            {
                if (SelectedCode == null)
                    return false;

                return true;

            }, async (p) =>
            {
                if (!_repositoryAlternativeProductInformation.AsQueryable().Any(x => x.Id == SelectedCode.Id))
                    return;

                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var alternative = await _repositoryAlternativeProductInformation.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedCode.Id);
                    await _repositoryAlternativeProductInformation.DeleteAsync(alternative);
                    await _unitOfWork.CommitAsync();
                    LoadData();

                    _logger.LogWrite(alternative, Defines.EModifyTypes.Deleted);
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
            });
            LoadData();
        }
        private void LoadData()
        {
            ListProduct = new ObservableCollection<Product>(_repositoryProduct.AsQueryable().Include(p => p.Unit).ToList());

            ListAlternative = new ObservableCollection<AlternativeProductInformation>(_repositoryAlternativeProductInformation.AsQueryable().ToList());
            if (!String.IsNullOrWhiteSpace(Filter))
            {
                ListProduct = new ObservableCollection<Product>(_repositoryProduct.AsQueryable().Include(p => p.Unit).Where(x => x.Code.Contains(Filter)).ToList());
                ListAlternative = new ObservableCollection<AlternativeProductInformation>(_repositoryAlternativeProductInformation.AsQueryable().Where(x => x.OriginalProductCode.Contains(Filter) || x.AlternativeProductCode.Contains(Filter)).ToList());

            }
        }
    }
}
