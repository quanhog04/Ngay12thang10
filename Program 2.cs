using System;
using System.Collections.Generic;

// Lớp trừu tượng PhuongTien
abstract class PhuongTien
{
    public string TenPhuongTien { get; set; }
    public string LoaiNhienLieu { get; set; }

    public PhuongTien(string tenPhuongTien, string loaiNhienLieu)
    {
        TenPhuongTien = tenPhuongTien;
        LoaiNhienLieu = loaiNhienLieu;
    }

    // Phương thức trừu tượng
    public abstract void DiChuyen();
}

// Giao diện IThongTinThem
interface IThongTinThem
{
    double TocDoToiDa();
    // Phương thức này chỉ dành cho phương tiện có nhiên liệu
    double MucTieuThuNhienLieu();
}

// Lớp XeHoi kế thừa PhuongTien và hiện thực IThongTinThem
class XeHoi : PhuongTien, IThongTinThem
{
    public XeHoi(string tenPhuongTien, string loaiNhienLieu) : base(tenPhuongTien, loaiNhienLieu) { }

    // Hiện thực phương thức DiChuyen cho XeHoi
    public override void DiChuyen()
    {
        Console.WriteLine($"{TenPhuongTien} đang di chuyển bằng động cơ.");
    }

    // Hiện thực giao diện IThongTinThem
    public double TocDoToiDa()
    {
        return 180; // km/h
    }

    public double MucTieuThuNhienLieu()
    {
        return 8.5; // lít/100km
    }
}

// Lớp XeDap kế thừa PhuongTien và hiện thực phương thức TocDoToiDa
class XeDap : PhuongTien, IThongTinThem
{
    public XeDap(string tenPhuongTien) : base(tenPhuongTien, "Không sử dụng nhiên liệu") { }

    // Hiện thực phương thức DiChuyen cho XeDap
    public override void DiChuyen()
    {
        Console.WriteLine($"{TenPhuongTien} đang di chuyển bằng sức đạp.");
    }

    // Hiện thực phương thức TocDoToiDa cho XeDap
    public double TocDoToiDa()
    {
        return 25; // km/h
    }

    // Xe đạp không có mức tiêu thụ nhiên liệu nên không cần hiện thực MucTieuThuNhienLieu
    public double MucTieuThuNhienLieu()
    {
        throw new NotImplementedException("Xe đạp không sử dụng nhiên liệu.");
    }
}

// Chương trình chính
class Program
{
    static void Main(string[] args)
    {
        // Tạo danh sách các phương tiện
        List<PhuongTien> danhSachPhuongTien = new List<PhuongTien>
        {
            new XeHoi("Xe Hơi Toyota", "Xăng"),
            new XeDap("Xe Đạp Martin")
        };

        // Duyệt qua danh sách và in thông tin chi tiết
        foreach (var phuongTien in danhSachPhuongTien)
        {
            Console.WriteLine($"Tên phương tiện: {phuongTien.TenPhuongTien}");
            Console.WriteLine($"Loại nhiên liệu: {phuongTien.LoaiNhienLieu}");
            phuongTien.DiChuyen();

            // Kiểm tra nếu phương tiện hiện thực IThongTinThem
            if (phuongTien is XeHoi xeHoi)
            {
                Console.WriteLine($"Tốc độ tối đa: {xeHoi.TocDoToiDa()} km/h");
                Console.WriteLine($"Mức tiêu thụ nhiên liệu: {xeHoi.MucTieuThuNhienLieu()} lít/100km");
            }
            else if (phuongTien is XeDap xeDap)
            {
                Console.WriteLine($"Tốc độ tối đa: {xeDap.TocDoToiDa()} km/h");
            }

            Console.WriteLine(); // Dòng trắng để phân cách
        }
    }
}
