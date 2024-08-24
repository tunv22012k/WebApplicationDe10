INSERT INTO QuanTriVien (TenQuanTriVien, MatKhauQuanTriVien, Email, NgayTao, NgayCapNhat)
VALUES 
('Admin 1', 'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', 'admin@gmail.com', GETDATE(), GETDATE()),
('Admin 2', 'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', 'admin2@gmail.com', GETDATE(), GETDATE()),
('Admin 3', 'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', 'admin3@gmail.com', GETDATE(), GETDATE()),
('Admin 4', 'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', 'admin4@gmail.com', GETDATE(), GETDATE());

------------------------------------------

INSERT INTO ThuongHieu (TenThuongHieu, QuocGia, NgayTao, NgayCapNhat)
VALUES 
(N'Apple', N'Mỹ', GETDATE(), GETDATE()),
(N'Samsung', N'Hàn Quốc', GETDATE(), GETDATE()),
(N'Sony', N'Nhật Bản', GETDATE(), GETDATE()),
(N'Xiaomi', N'Trung Quốc', GETDATE(), GETDATE()),
(N'Huawei', N'Trung Quốc', GETDATE(), GETDATE());

--------------------------------------------

INSERT INTO DanhMucSanPham (TenDanhMuc, MoTa, NgayTao, NgayCapNhat)
VALUES 
('Điện thoại', 'Các sản phẩm điện thoại di động thông minh.', GETDATE(), GETDATE()),
('Máy tính bảng', 'Các loại máy tính bảng và phụ kiện liên quan.', GETDATE(), GETDATE()),
('Laptop', 'Các loại laptop từ các thương hiệu nổi tiếng.', GETDATE(), GETDATE()),
('Phụ kiện điện thoại', 'Phụ kiện cho điện thoại như ốp lưng, sạc cáp.', GETDATE(), GETDATE()),
('Thiết bị gia dụng', 'Các thiết bị gia dụng thông minh như robot hút bụi, máy giặt.', GETDATE(), GETDATE());
