using System;
using System.Data.SqlClient;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;

namespace WebApplicationDe10.Services
{
    public class DatHangService
    {
        public bool Create(DatHangViewModel datHangViewModel)
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
                        INSERT INTO KhachHang 
                        (
                            TenKhachHang,
                            Email,
                            DienThoai,
                            DiaChi,
                            NgayTao,
                            NgayCapNhat
                        ) 
                        VALUES 
                        (
                            @TenKhachHang,
                            @Email,
                            @DienThoai,
                            @DiaChi,
                            @NgayTao,
                            @NgayCapNhat
                        )
                        SELECT SCOPE_IDENTITY();
                    ";

                    SqlCommand cmd = new SqlCommand(query, connection, transaction);
                    cmd.Parameters.AddWithValue("@TenKhachHang", datHangViewModel.TenKhachHang);
                    cmd.Parameters.AddWithValue("@Email", datHangViewModel.Email);
                    cmd.Parameters.AddWithValue("@DienThoai", datHangViewModel.DienThoai);
                    cmd.Parameters.AddWithValue("@DiaChi", datHangViewModel.DiaChi);
                    cmd.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                    cmd.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);

                    var maKhachHang = Convert.ToInt32(cmd.ExecuteScalar());

                    // Nếu số hàng bị ảnh hưởng lớn hơn 0, quá trình chèn đã thành công
                    isSuccess = maKhachHang > 0;

                    // tạo thông số
                    foreach (var sanPham in datHangViewModel.SanPhamDatHangViewModel)
                    {
                        string query2 = @"
                            INSERT INTO DonHang 
                            (
                                MaKhachHang,
                                MaSanPham,
                                SoLuong,
                                GiaDonVi,
                                DiaChiGiaoHang,
                                NgayDatHang,
                                TongTien,
                                TrangThaiDonHang,
                                PhuongThucThanhToan,
                                PhuongThucGiaoHang,
                                NgayTao,
                                NgayCapNhat
                            ) 
                            VALUES 
                            (
                                @MaKhachHang,
                                @MaSanPham,
                                @SoLuong,
                                @GiaDonVi,
                                @DiaChiGiaoHang,
                                @NgayDatHang,
                                @TongTien,
                                @TrangThaiDonHang,
                                @PhuongThucThanhToan,
                                @PhuongThucGiaoHang,
                                @NgayTao,
                                @NgayCapNhat
                            )
                        ";

                        SqlCommand cmd2 = new SqlCommand(query2, connection, transaction);
                        cmd2.Parameters.AddWithValue("@MaKhachHang", maKhachHang);
                        cmd2.Parameters.AddWithValue("@MaSanPham", sanPham.MaSanPham);
                        cmd2.Parameters.AddWithValue("@SoLuong", sanPham.SoLuong);
                        cmd2.Parameters.AddWithValue("@GiaDonVi", sanPham.GiaSanPham);
                        cmd2.Parameters.AddWithValue("@DiaChiGiaoHang", datHangViewModel.DiaChiGiaoHang);
                        cmd2.Parameters.AddWithValue("@NgayDatHang", DateTime.Now);
                        cmd2.Parameters.AddWithValue("@TongTien", sanPham.TongTien);
                        cmd2.Parameters.AddWithValue("@TrangThaiDonHang", "Đặt hàng");
                        cmd2.Parameters.AddWithValue("@PhuongThucThanhToan", "Trực tiếp");
                        cmd2.Parameters.AddWithValue("@PhuongThucGiaoHang", "Giao tận nhà");
                        cmd2.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                        cmd2.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);

                        cmd2.ExecuteNonQuery();
                    }

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