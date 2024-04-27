using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows.Input;
using DocumentFormat.OpenXml.Wordprocessing;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Linq.Expressions;
using HRM.UI.Commands;
using System.Collections.ObjectModel;
using HRM.UI.Factories;
using HRM.UI.Stores;
using Microsoft.EntityFrameworkCore;

namespace HRM.UI.ViewModels
{
    public class TrainingProcessViewModel : BaseViewModel
    {
        public ICommand DeleteCommand { get; set; }
        public ICommand WorkProcessCommand { get; set; }
        private ObservableCollection<QuaTrinhDaoTao> _list = new ObservableCollection<QuaTrinhDaoTao>();
        public ObservableCollection<QuaTrinhDaoTao> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private IUnitOfWork _unitOfWork;

        private IRepository<QuaTrinhDaoTao> _quaTrinhDaoTaoRepository;
        public ICommand AddCommand { get; set; }
        public ObservableCollection<string> HinhThucDaoTaoData { get; set; }
        public ObservableCollection<string> VanBangChungChiData { get; set; }

        private string _tuNgayDenNgay;
        public string TuNgayDenNgay
        {
            get { return _tuNgayDenNgay; }
            set { _tuNgayDenNgay = value; OnPropertyChanged(); }
        }

        private string _noiDaoTao;
        public string NoiDaoTao
        {
            get { return _noiDaoTao; }
            set { _noiDaoTao = value; OnPropertyChanged(); }
        }
        private string _nganhHoc;
        public string NganhHoc
        {
            get { return _nganhHoc; }
            set { _nganhHoc = value; OnPropertyChanged(); }
        }

        private string _hinhThucDaoTao;
        public string HinhThucDaoTao
        {
            get { return _hinhThucDaoTao; }
            set { _hinhThucDaoTao = value; OnPropertyChanged(); }
        }

        private string _vanBangChungChi;
        public string VanBangChungChi
        {
            get { return _vanBangChungChi; }
            set { _vanBangChungChi = value; OnPropertyChanged(); }
        }
        private QuaTrinhDaoTao _selectedItem;
        public QuaTrinhDaoTao SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    TuNgayDenNgay = SelectedItem.TuNgayDenNgay;
                    HinhThucDaoTao = SelectedItem.HinhThucDaoTao;
                    NganhHoc = SelectedItem.NganhHoc;
                    NoiDaoTao = SelectedItem.NoiDaoTao;
                    VanBangChungChi = SelectedItem.VanBangChungChi;
                }
                OnPropertyChanged();
            }
        }
        private void LoadComboBoxData()
        {
            HinhThucDaoTaoData = new ObservableCollection<string>();
            HinhThucDaoTaoData.Add("Chính quy");
            HinhThucDaoTaoData.Add("Không chính quy");
            HinhThucDaoTaoData.Add("Đào tạo từ xa");

            VanBangChungChiData = new ObservableCollection<string>();
            VanBangChungChiData.Add("Cử nhân");
            VanBangChungChiData.Add("Kỹ sư");
            VanBangChungChiData.Add("Thạc sĩ");
            VanBangChungChiData.Add("Tiến sĩ");

        }

        private readonly IViewModelFactory _viewModelFactory;
        private readonly MainContentStore _mainContentStore;
        private readonly ChildContentStore _childContentStore;

        public TrainingProcessViewModel(IViewModelFactory viewModelFactory, MainContentStore mainContentStore, IRepository<QuaTrinhDaoTao> quaTrinhDaoTaoRepository, IUnitOfWork unitOfWork, ChildContentStore childContentStore)
        {
            _viewModelFactory = viewModelFactory;
            _mainContentStore = mainContentStore;
            _quaTrinhDaoTaoRepository = quaTrinhDaoTaoRepository;
            _childContentStore = childContentStore;
            _unitOfWork = unitOfWork;
            LoadComboBoxData();
            LoadData();
            WorkProcessCommand = new Commands.RelayCommand<object>(p => true, p =>
            {
                _childContentStore.CurrentViewModel = _viewModelFactory.CreateViewModel(Defines.EViewTypes.WorkProcess);
            });
            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
                // can excute?
                return true;
            }, async (p) =>
            {
                var QuaTrinhDaoTao = new QuaTrinhDaoTao()
                {
                    TuNgayDenNgay = TuNgayDenNgay,
                    NoiDaoTao = NoiDaoTao,
                    NganhHoc = NganhHoc,
                    HinhThucDaoTao = HinhThucDaoTao,
                    VanBangChungChi = VanBangChungChi,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    QuaTrinhDaoTao = await _quaTrinhDaoTaoRepository.AddAsync(QuaTrinhDaoTao);
                    LoadData();
                    if (QuaTrinhDaoTao != null)
                    {
                        MessageBox.Show("Thêm thành công");
                        LoadData();

                    }

                    else
                        MessageBox.Show("Lỗi hệ thống");
                    await _unitOfWork.CommitAsync();
                }
                catch (Exception e)
                {
                    await _unitOfWork.RollbackAsync();

                }
            });

            DeleteCommand = new Commands.RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            }, async (p) =>
            {
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    var quaTrinhDaoTao = await _quaTrinhDaoTaoRepository.AsQueryable().FirstOrDefaultAsync(x => x.Id == SelectedItem.Id);
                    await _quaTrinhDaoTaoRepository.DeleteAsync(quaTrinhDaoTao);
                    await _unitOfWork.CommitAsync();
                    LoadData();
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
            List = new ObservableCollection<QuaTrinhDaoTao>(_quaTrinhDaoTaoRepository.AsQueryable().ToList());
            /*            if (!String.IsNullOrWhiteSpace(Filter))
                        {
                            List = new ObservableCollection<NhanSu>(_repository.AsQueryable().Where(x => x.MaNhanVien.Contains(Filter) || x.HoTen.Contains(Filter)).ToList());
                        }*/
        }
    }
}
