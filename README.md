CREATE TABLE users (
    userId INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính tự động tăng
	email NVARCHAR(100) NOT NULL UNIQUE, -- Tên đăng nhập của người dùng, phải là duy nhất
    username NVARCHAR(100) NOT NULL, -- user name
    password NVARCHAR(255) NOT NULL, -- Mật khẩu đã được mã hóa
    role NVARCHAR(50) -- Vai trò của người dùng (ví dụ: Admin, User, etc.)
);

INSERT INTO Users (email, username, password, role)
VALUES ('admin@gmail.com', 'admin', 'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', 'Admin');

INSERT INTO Users (email, username, password, role)
VALUES ('user@gmail.com', 'user1', 'pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=', 'User');
