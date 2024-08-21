using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services.Admin
{
    public class SanPhamService
    {
        public List<DanhMucSanPham> GetAllDanhMuc()
        {
            string query = "SELECT * FROM DanhMucSanPham";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            List<DanhMucSanPham> danhMucSanPhams = new List<DanhMucSanPham>();

            foreach (DataRow row in dt.Rows)
            {
                DanhMucSanPham danhMucSanPham = new DanhMucSanPham();
                danhMucSanPham.MaDanhMuc    = (int)row["MaDanhMuc"];
                danhMucSanPham.TenDanhMuc   = row["TenDanhMuc"].ToString();
                danhMucSanPham.MoTa         = row["MoTa"].ToString();
                danhMucSanPham.NgayTao      = Convert.ToDateTime(row["NgayTao"]);
                danhMucSanPham.NgayCapNhat  = row["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(row["NgayCapNhat"]) : (DateTime?)null;
                danhMucSanPhams.Add(danhMucSanPham);
            }

            return danhMucSanPhams;
        }

        public List<ThuongHieu> GetAllThuongHieu()
        {
            string query = "SELECT * FROM ThuongHieu";
            DataTable dt = DatabaseHelper.ExecuteQuery(query);

            List<ThuongHieu> thuongHieus = new List<ThuongHieu>();

            foreach (DataRow row in dt.Rows)
            {
                ThuongHieu thuongHieu = new ThuongHieu();
                thuongHieu.MaThuongHieu     = (int)row["MaThuongHieu"];
                thuongHieu.TenThuongHieu    = row["TenThuongHieu"].ToString();
                thuongHieu.QuocGia          = row["QuocGia"].ToString();
                thuongHieu.NgayTao          = Convert.ToDateTime(row["NgayTao"]);
                thuongHieu.NgayCapNhat      = row["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(row["NgayCapNhat"]) : (DateTime?)null;
                thuongHieus.Add(thuongHieu);
            }

            return thuongHieus;
        }

        public bool CreateSanPham(SanPham sanPham)
        {
            // Tạo kết nối với cơ sở dữ liệu và lưu thông tin người dùng
            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                bool isSuccess = false;
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string query = @"
                        INSERT INTO SanPham 
                        (
                            TenSanPham,
                            MaDanhMuc,
                            MaThuongHieu,
                            GiaSanPham,
                            MoTa,
                            URLHinhAnh,
                            SoLuongTonKho,
                            ThoiGianBaoHanh,
                            NgayRaMat,
                            NgayTao,
                            NgayCapNhat
                        ) 
                        VALUES 
                        (
                            @TenSanPham,
                            @MaDanhMuc,
                            @MaThuongHieu,
                            @GiaSanPham,
                            @MoTa,
                            @URLHinhAnh,
                            @SoLuongTonKho,
                            @ThoiGianBaoHanh,
                            @NgayRaMat,
                            @NgayTao,
                            @NgayCapNhat
                        )
                        SELECT SCOPE_IDENTITY();
                    ";

                    SqlCommand cmd = new SqlCommand(query, connection, transaction);
                    cmd.Parameters.AddWithValue("@TenSanPham", sanPham.TenSanPham);
                    cmd.Parameters.AddWithValue("@MaDanhMuc", sanPham.MaDanhMuc);
                    cmd.Parameters.AddWithValue("@MaThuongHieu", sanPham.MaThuongHieu);
                    cmd.Parameters.AddWithValue("@GiaSanPham", sanPham.GiaSanPham);
                    cmd.Parameters.AddWithValue("@MoTa", sanPham.MoTa);
                    cmd.Parameters.AddWithValue("@URLHinhAnh", string.IsNullOrEmpty(sanPham.URLHinhAnh) ? (object)DBNull.Value : sanPham.URLHinhAnh);
                    cmd.Parameters.AddWithValue("@SoLuongTonKho", sanPham.SoLuongTonKho);
                    cmd.Parameters.AddWithValue("@ThoiGianBaoHanh", sanPham.ThoiGianBaoHanh);
                    cmd.Parameters.AddWithValue("@NgayRaMat", sanPham.NgayRaMat);
                    cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                    cmd.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);

                    var maSanPham = Convert.ToInt32(cmd.ExecuteScalar());

                    // tạo thông số
                    foreach (var thongSo in sanPham.ThongSoKyThuatSPs)
                    {
                        string query2 = @"
                            INSERT INTO ThongSoKyThuatSP 
                            (
                                MaSanPham,
                                TenThongSo,
                                GiaTriThongSo
                            ) 
                            VALUES 
                            (
                                @MaSanPham,
                                @TenThongSo,
                                @GiaTriThongSo
                            )
                        ";

                        SqlCommand cmd2 = new SqlCommand(query2, connection, transaction);
                        cmd2.Parameters.AddWithValue("@MaSanPham", maSanPham);
                        cmd2.Parameters.AddWithValue("@TenThongSo", thongSo.TenThongSo);
                        cmd2.Parameters.AddWithValue("@GiaTriThongSo", thongSo.GiaTriThongSo);

                        cmd2.ExecuteNonQuery();
                    }

                    // Nếu số hàng bị ảnh hưởng lớn hơn 0, quá trình chèn đã thành công
                    isSuccess = maSanPham > 0;

                    transaction.Commit();
                    return isSuccess;
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback(); // Hoàn tác các thay đổi nếu có lỗi
                    }

                    throw new Exception("Cannot open database connection: " + ex.Message);
                }
                finally
                {
                    connection.Close(); // Đảm bảo kết nối được đóng
                }
            }
        }
    }
}