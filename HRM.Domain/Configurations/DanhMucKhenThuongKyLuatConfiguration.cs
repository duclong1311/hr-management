using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HRM.Domain.Configurations
{
    public class DanhMucKhenThuongKyLuatConfiguration : IEntityTypeConfiguration<DanhMucKhenThuongKyLuat>
    {
        public void Configure(EntityTypeBuilder<DanhMucKhenThuongKyLuat> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new DanhMucKhenThuongKyLuat()
            {
                Id = 1,
                CapKhenThuongKyLuat = "Cá nhân",
                HinhThucKhenThuongKyLuat = "Thưởng tiền mặt"
            }, new DanhMucKhenThuongKyLuat()
            {
                Id = 2,
                CapKhenThuongKyLuat = "Đội nhóm",
                HinhThucKhenThuongKyLuat = "Thưởng tiền mặt"
            }, new DanhMucKhenThuongKyLuat()
            {
                Id = 3,
                CapKhenThuongKyLuat = "Bộ phận",
                HinhThucKhenThuongKyLuat = "Thưởng tiền mặt"
            }
            , new DanhMucKhenThuongKyLuat()
            {
                Id = 4,
                CapKhenThuongKyLuat = "Cá nhân",
                HinhThucKhenThuongKyLuat = "Trao giấy khen, bằng khen"
            }
            , new DanhMucKhenThuongKyLuat()
            {
                Id = 5,
                CapKhenThuongKyLuat = "Đội nhóm",
                HinhThucKhenThuongKyLuat = "Trao giấy khen, bằng khen"
            }
            , new DanhMucKhenThuongKyLuat()
            {
                Id = 6,
                CapKhenThuongKyLuat = "Bộ phận",
                HinhThucKhenThuongKyLuat = "Trao giấy khen, bằng khen"
            }
            , new DanhMucKhenThuongKyLuat()
            {
                Id = 7,
                CapKhenThuongKyLuat = "Cá nhân",
                HinhThucKhenThuongKyLuat = "Khiển trách bằng lời nói"
            }
            , new DanhMucKhenThuongKyLuat()
            {
                Id = 8,
                CapKhenThuongKyLuat = "Đội nhóm",
                HinhThucKhenThuongKyLuat = "Khiển trách bằng lời nói"
            }, new DanhMucKhenThuongKyLuat()
            {
                Id = 9,
                CapKhenThuongKyLuat = "Bộ phận",
                HinhThucKhenThuongKyLuat = "Khiển trách bằng lời nói"
            }
            , new DanhMucKhenThuongKyLuat()
            {
                Id = 10,
                CapKhenThuongKyLuat = "Cá nhân",
                HinhThucKhenThuongKyLuat = "Khiển trách bằng văn bản"
            }
            , new DanhMucKhenThuongKyLuat()
            {
                Id = 11,
                CapKhenThuongKyLuat = "Đội nhóm",
                HinhThucKhenThuongKyLuat = "Khiển trách bằng văn bản"
            }, new DanhMucKhenThuongKyLuat()
            {
                Id = 12,
                CapKhenThuongKyLuat = "Bộ phận",
                HinhThucKhenThuongKyLuat = "Khiển trách bằng văn bản"
            }, new DanhMucKhenThuongKyLuat()
            {
                Id = 13,
                CapKhenThuongKyLuat = "Cá nhân",
                HinhThucKhenThuongKyLuat = "Sa thải"
            }
            );
        }
    }
}
