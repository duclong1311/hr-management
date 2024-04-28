using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Repositories;
using HRM.Core.UnitOfWorks;
using HRM.Domain.Models;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using HRM.UI.States.Users;

namespace HRM.UI.ViewModels
{
    public class RemunerativeViewModel : BaseViewModel
    {
        private ObservableCollection<KhenThuongKyLuat> _list = new ObservableCollection<KhenThuongKyLuat>();
        public ObservableCollection<KhenThuongKyLuat> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private IUnitOfWork _unitOfWork;

        private IRepository<KhenThuongKyLuat> _khenThuongRespository;
        public ICommand AddCommand { get; set; }
        public ObservableCollection<string> CapKhenThuongData { get; set; }
        private int _nhanSuId { get; set; }
        public int NhanSuId
        {
            get { return _nhanSuId; }
            set
            {
                _nhanSuId = value;
                OnPropertyChanged();
            }
        }

        private string _capKhenThuong;
        public string CapKhenThuong
        {
            get { return _capKhenThuong; }
            set
            {
                _capKhenThuong = value;
                OnPropertyChanged();
            }
        }
        private string _capKyLuat;
        public string CapKyLuat
        {
            get { return _capKyLuat; }
            set
            {
                _capKyLuat = value;
                OnPropertyChanged();
            }
        }

        private string _tenHinhThucKhenThuong;
        public string TenHinhThucKhenThuong
        {
            get { return _tenHinhThucKhenThuong; }
            set
            {
                _tenHinhThucKhenThuong = value;
                OnPropertyChanged();
            }
        }
        private string _tenHinhThucKyLuat;
        public string TenHinhThucKyLuat
        {
            get { return _tenHinhThucKyLuat; }
            set
            {
                _tenHinhThucKyLuat = value;
                OnPropertyChanged();
            }
        }

        private DateTime _ngayQuyetDinh;
        public DateTime NgayQuyetDinh
        {
            get { return _ngayQuyetDinh; }
            set
            {
                _ngayQuyetDinh = value;
                OnPropertyChanged();
            }
        }

        private string _soQuyetDinh;
        public string SoQuyetDinh
        {
            get { return _soQuyetDinh; }
            set
            {
                _soQuyetDinh = value;
                OnPropertyChanged();
            }
        }
        private void LoadComboBoxData()
        {
            CapKhenThuongData = new ObservableCollection<string>();
            CapKhenThuongData.Add("Cấp 1");
            CapKhenThuongData.Add("Cấp 2");
            CapKhenThuongData.Add("Cấp 2");
        }
        private readonly IUserStore _userStore;
        public RemunerativeViewModel(IUserStore userStore, IUnitOfWork unitOfWork, IRepository<KhenThuongKyLuat> KhenThuongRepository)
        {
            _unitOfWork = unitOfWork;
            _khenThuongRespository = KhenThuongRepository;
            _userStore = userStore;

            LoadComboBoxData();
            Load();

            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
                return true;
            }, async (p) =>
            {
                var KhenThuong = new KhenThuongKyLuat()
                {
                    CapKhenThuong = CapKhenThuong,
                    TenHinhThucKhenThuong = TenHinhThucKhenThuong,
                    CapKyLuat = CapKyLuat,
                    TenHinhThucKyLuat = TenHinhThucKyLuat,
                    NgayQuyetDinh = NgayQuyetDinh,
                    SoQuyetDinh = SoQuyetDinh,
                    //NhanSuId = _userStore.CurrentNhanSu.NhanSuId
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    KhenThuong = await _khenThuongRespository.AddAsync(KhenThuong);
                    if (KhenThuong != null)
                    {
                        MessageBox.Show("Thêm thành công");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi hệ thống");
                    }
                    await _unitOfWork.CommitAsync();

                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackAsync();
                }
                Load();
                LoadComboBoxData();
            });
        }
        public void Load()
        {
            List = new ObservableCollection<KhenThuongKyLuat>(_khenThuongRespository.AsQueryable().Where(x=> x.NhanSuId.ToString() == _userStore.CurrentNhanSu.MaNhanVien).ToList());
        }
    }
}
