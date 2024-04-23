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
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace HRM.UI.ViewModels
{
    public class DisciplineViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;

        private IRepository<KyLuat> _kyLuatRespository;
        public ICommand AddCommand { get; set; }
        public ObservableCollection<string> CapKyLuatData { get; set; }

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

        private string _thamQuyenKyLuat;
        public string ThamQuyenKyLuat
        {
            get { return _thamQuyenKyLuat; }
            set
            {
                _thamQuyenKyLuat = value;
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
            CapKyLuatData = new ObservableCollection<string>();
            CapKyLuatData.Add("Cấp 1");
            CapKyLuatData.Add("Cấp 2");
            CapKyLuatData.Add("Cấp 2");
        }

        public DisciplineViewModel(IUnitOfWork unitOfWork, IRepository<KyLuat> KyLuatRepository)
        {
            _unitOfWork = unitOfWork;
            _kyLuatRespository = KyLuatRepository;

            LoadComboBoxData();

            AddCommand = new Commands.RelayCommand<object>((p) =>
            {
                return true;
            }, async (p) =>
            {
                var KyLuat = new KyLuat()
                {
                    CapKyLuat = CapKyLuat,
                    ThamQuyen = ThamQuyenKyLuat,
                    ThoiGianBanHanh = ThoiGianBanHanh,
                    NoiDung = NoiDung,
                    SoQuyetDinh = SoQuyetDinh,
                };
                await _unitOfWork.BeginTransactionAsync();
                try
                {
                    KyLuat = await _kyLuatRespository.AddAsync(KyLuat);
                    if (KyLuat != null)
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
            });
        }
    }
}
