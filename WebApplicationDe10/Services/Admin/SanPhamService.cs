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

        public List<SanPham> GetAllSanPham(SanPham paramsSanPham)
        {
            List<SanPham> danhMuc = new List<SanPham>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                string query = @"
                    SELECT 
                        sp.MaSanPham,
                        sp.TenSanPham,
                        sp.MaDanhMuc,
                        sp.MaThuongHieu,
                        sp.GiaSanPham,
                        sp.MoTa,
                        sp.URLHinhAnh,
                        sp.SoLuongTonKho,
                        sp.ThoiGianBaoHanh,
                        sp.NgayRaMat,
                        sp.NgayTao,
                        sp.NgayCapNhat,
                        dm.TenDanhMuc,
                        th.TenThuongHieu
                    FROM 
                        SanPham sp
                    INNER JOIN DanhMucSanPham dm 
                        ON sp.MaDanhMuc = dm.MaDanhMuc
                    INNER JOIN ThuongHieu th 
                        ON sp.MaThuongHieu = th.MaThuongHieu
                ";

                if (!string.IsNullOrEmpty(paramsSanPham.TenSanPham) || paramsSanPham.MaDanhMuc > 0 || paramsSanPham.MaThuongHieu > 0)
                {
                    bool isAnd = false;
                    query += " WHERE";

                    if (!string.IsNullOrEmpty(paramsSanPham.TenSanPham))
                    {
                        query += " sp.TenSanPham LIKE @TenSanPham";
                        isAnd = true;
                    }

                    if (paramsSanPham.MaDanhMuc > 0)
                    {
                        if (isAnd)
                        {
                            query += " AND";
                        }
                        query += " sp.MaDanhMuc = @MaDanhMuc";
                        isAnd = true;
                    }

                    if (paramsSanPham.MaThuongHieu > 0)
                    {
                        if (isAnd)
                        {
                            query += " AND";
                        }
                        query += " sp.MaThuongHieu = @MaThuongHieu";
                        isAnd = true;
                    }
                }

                SqlCommand cmd = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(paramsSanPham.TenSanPham))
                {
                    cmd.Parameters.AddWithValue("@TenSanPham", "%" + paramsSanPham.TenSanPham + "%");
                }
                if (paramsSanPham.MaDanhMuc > 0)
                {
                    cmd.Parameters.AddWithValue("@MaDanhMuc", paramsSanPham.MaDanhMuc);
                }
                if (paramsSanPham.MaThuongHieu > 0)
                {
                    cmd.Parameters.AddWithValue("@MaThuongHieu", paramsSanPham.MaThuongHieu);
                }

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhMuc.Add(new SanPham
                        {
                            MaSanPham           = (int)reader["MaSanPham"],
                            TenSanPham          = reader["TenSanPham"].ToString(),
                            MaDanhMuc           = (int)reader["MaDanhMuc"],
                            MaThuongHieu        = (int)reader["MaThuongHieu"],
                            GiaSanPham          = Convert.ToDecimal(reader["GiaSanPham"]),
                            MoTa                = reader["MoTa"].ToString(),
                            URLHinhAnh          = reader["URLHinhAnh"].ToString(),
                            SoLuongTonKho       = (int)reader["SoLuongTonKho"],
                            ThoiGianBaoHanh     = (int)reader["ThoiGianBaoHanh"],
                            NgayRaMat           = Convert.ToDateTime(reader["NgayRaMat"]),
                            NgayTao             = Convert.ToDateTime(reader["NgayTao"]),
                            NgayCapNhat         = reader["NgayCapNhat"] != DBNull.Value ? Convert.ToDateTime(reader["NgayCapNhat"]) : (DateTime?)null,
                            TenDanhMuc          = reader["TenDanhMuc"].ToString(),
                            TenThuongHieu       = reader["TenThuongHieu"].ToString(),
                            ThongSoKyThuatSPs   = GetThongSoKyThuatSPByMaSanPham((int)reader["MaSanPham"])
                        });
                    }
                }
            }

            return danhMuc;
        }

        private List<ThongSoKyThuatSP> GetThongSoKyThuatSPByMaSanPham(int maSanPham)
        {
            List<ThongSoKyThuatSP> thongSoList = new List<ThongSoKyThuatSP>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                connection.Open();

                string query = @"
                    SELECT 
                        MaTSKTSP, 
                        TenThongSo, 
                        GiaTriThongSo 
                    FROM 
                        ThongSoKyThuatSP 
                    WHERE 
                        MaSanPham = @MaSanPham
                ";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var thongSo = new ThongSoKyThuatSP
                            {
                                MaTSKTSP        = (int)reader["MaTSKTSP"],
                                TenThongSo      = reader["TenThongSo"].ToString(),
                                GiaTriThongSo   = reader["GiaTriThongSo"].ToString(),
                            };

                            thongSoList.Add(thongSo);
                        }
                    }
                }
            }

            return thongSoList;
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

        public bool DeleteSanPham(string MaSanPham)
        {
            // Tạo kết nối với cơ sở dữ liệu và lưu thông tin người dùng
            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                bool isSuccess = false;
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string query2 = "DELETE FROM ThongSoKyThuatSP WHERE MaSanPham = @MaSanPham";

                    SqlCommand cmd2 = new SqlCommand(query2, connection, transaction);
                    cmd2.Parameters.AddWithValue("@MaSanPham", int.Parse(MaSanPham));

                    cmd2.ExecuteNonQuery();

                    string query = "DELETE FROM SanPham WHERE MaSanPham = @MaSanPham";

                    SqlCommand cmd = new SqlCommand(query, connection, transaction);
                    cmd.Parameters.AddWithValue("@MaSanPham", int.Parse(MaSanPham));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Nếu số hàng bị ảnh hưởng lớn hơn 0, quá trình chèn đã thành công
                    isSuccess = rowsAffected > 0;

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