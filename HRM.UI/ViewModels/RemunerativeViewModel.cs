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
        private ObservableCollection<KhenThuong> _list = new ObservableCollection<KhenThuong>();
        public ObservableCollection<KhenThuong> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }
        private IUnitOfWork _unitOfWork;

        private IRepository<KhenThuong> _khenThuongRespository;
        public ICommand AddCommand { get; set; }
        public ObservableCollection<string> CapKhenThuongData { get; set; }

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

        private string _thamQuyenKhenThuong;
        public string ThamQuyenKhenThuong
        {
            get { return _thamQuyenKhenThuong; }
            set
            {
                _thamQuyenKhenThuong = value;
                OnPropertyChanged();
            }
        }

        private DateOnly _thoiGianBanHanh;
        public DateOnly ThoiGianBanHanh
        {
            get { return _thoiGianBanHanh; }
            set
            {
                _thoiGianBanHanh = value;
                OnPropertyChanged();
            }
        }

        private string _noiDung;
        public string NoiDung
        {
            get { return _noiDung; }
            set
            {
                _noiDung = value;
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
        public RemunerativeViewModel(IUserStore userStore, IUnitOfWork unitOfWork, IRepository<KhenThuong> KhenThuongRepository)
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
                var KhenThuong = new KhenThuong()
                {
                    CapKhenThuong = CapKhenThuong,
                    ThamQuyen = ThamQuyenKhenThuong,
                    ThoiGianBanHanh = ThoiGianBanHanh,
                    NoiDung = NoiDung,
                    SoQuyetDinh = SoQuyetDinh,
                    MaNhanVien = _userStore.CurrentNhanSu.MaNhanVien
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
            List = new ObservableCollection<KhenThuong>(_khenThuongRespository.AsQueryable().Where(x=> x.MaNhanVien == _userStore.CurrentNhanSu.MaNhanVien).ToList());
        }
    }
}
