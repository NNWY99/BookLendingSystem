# 图书管理系统

## 项目概述

基于 **C# WinForms + .NET 8** 的图书管理系统，采用经典三层架构设计，使用 MySQL 数据库和 SunnyUI 组件库构建现代化界面。

### 核心功能

| 模块 | 功能描述 |
|------|----------|
| **登录系统** | 账号密码验证、账户锁定（5次失败锁定15分钟）、注册新账户 |
| **图书管理** | 添加、修改、删除、搜索图书信息，库存管理 |
| **借阅人管理** | 添加、修改、删除、搜索借阅人信息 |
| **借阅操作** | 选择图书和借阅人进行借阅，支持多本同时借阅 |
| **借阅历史** | 查看借阅记录，支持图书归还操作 |
| **逾期管理** | 查看逾期图书列表，显示逾期天数，支持归还 |

---

## 技术栈

| 分类 | 技术 | 版本 |
|------|------|------|
| 语言 | C# | 12.0 |
| 框架 | .NET | 8.0 |
| UI框架 | Windows Forms | - |
| UI组件库 | SunnyUI | 3.9.7 |
| 数据库 | MySQL | 5.7+ / 8.0+ |
| 数据库驱动 | MySqlConnector | 2.3.5 |

---

## 快速开始

### 环境要求

1. **.NET 8.0 SDK** - [下载地址](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **MySQL Server** - [下载地址](https://dev.mysql.com/downloads/mysql/)
3. **Visual Studio 2022**（可选）- 用于开发调试

### 数据库配置

#### 1. 创建数据库

执行 SQL 脚本创建数据库和表结构：

```sql
CREATE DATABASE IF NOT EXISTS mybook CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE mybook;

CREATE TABLE IF NOT EXISTS Admin (
    id INT PRIMARY KEY AUTO_INCREMENT,
    admin_name VARCHAR(32) NOT NULL,
    admin_account VARCHAR(32) NOT NULL UNIQUE,
    admin_password VARCHAR(32) NOT NULL,
    create_time DATETIME DEFAULT CURRENT_TIMESTAMP,
    last_login_time DATETIME NULL,
    fail_count INT DEFAULT 0,
    last_fail_time DATETIME NULL,
    is_locked TINYINT DEFAULT 0
);

CREATE TABLE IF NOT EXISTS Borrowers (
    id INT PRIMARY KEY AUTO_INCREMENT,
    borrowers_name VARCHAR(20) NOT NULL,
    IDCard VARCHAR(20) NOT NULL UNIQUE,
    sex CHAR(3) DEFAULT '男',
    tel VARCHAR(11) NOT NULL,
    borrowing_code INT UNIQUE,
    price INT DEFAULT 0,
    Order_number VARCHAR(50) UNIQUE,
    remark INT DEFAULT 1
);

CREATE TABLE IF NOT EXISTS Books (
    id INT PRIMARY KEY AUTO_INCREMENT,
    barCode INT NOT NULL,
    bookName VARCHAR(50) NOT NULL,
    category VARCHAR(10) NOT NULL,
    author VARCHAR(50) NOT NULL,
    publishingHouse VARCHAR(50) NOT NULL,
    publicationDate DATETIME NOT NULL,
    loansNumber INT NOT NULL DEFAULT 0,
    TotalNumber INT NOT NULL DEFAULT 0,
    remark INT DEFAULT 1
);

CREATE TABLE IF NOT EXISTS Borrow (
    id INT PRIMARY KEY AUTO_INCREMENT,
    admin_id INT,
    borrowers_id INT,
    num INT NOT NULL DEFAULT 0,
    FOREIGN KEY (admin_id) REFERENCES Admin(id),
    FOREIGN KEY (borrowers_id) REFERENCES Borrowers(id)
);

CREATE TABLE IF NOT EXISTS Borrowing_details (
    id INT PRIMARY KEY AUTO_INCREMENT,
    book_id INT,
    borrow_id INT,
    loanTime DATETIME DEFAULT CURRENT_TIMESTAMP,
    cut_off_time DATETIME NOT NULL,
    return_time DATETIME NULL,
    FOREIGN KEY (book_id) REFERENCES Books(id),
    FOREIGN KEY (borrow_id) REFERENCES Borrow(id)
);

INSERT INTO Admin (admin_name, admin_account, admin_password) VALUES ('李白', 'libai', '123');
INSERT INTO Admin (admin_name, admin_account, admin_password) VALUES ('杜甫', 'dufu', '111');
```

#### 2. 修改连接字符串

编辑 `DAL/DBHelper.cs` 文件：

```csharp
private static readonly string connectionString = "server=localhost;database=mybook;uid=root;pwd=yourpassword;port=3306;charset=utf8;";
```

### 构建运行

```powershell
# 进入项目目录
cd "~\BookLendingSystem"

# 还原依赖
dotnet restore

# 构建项目
dotnet build

# 运行项目
dotnet run
```

---

## 项目结构

```
BookLendingSystem/
├── Model/                    # 实体层
│   ├── Admin.cs              # 管理员实体
│   ├── Borrowers.cs          # 借阅人实体
│   ├── Books.cs              # 图书实体
│   ├── Borrow.cs             # 借阅记录实体
│   └── Borrowing_details.cs  # 借阅详情实体
├── DAL/                      # 数据访问层
│   ├── DBHelper.cs           # 数据库连接辅助类
│   ├── AdminDAL.cs           # 管理员数据访问
│   ├── BooksDAL.cs           # 图书数据访问
│   ├── BorrowersDAL.cs       # 借阅人数据访问
│   ├── BorrowDAL.cs          # 借阅数据访问
│   └── BorrowingDetailsDAL.cs# 借阅详情数据访问
├── BLL/                      # 业务逻辑层
│   ├── AdminBLL.cs           # 管理员业务逻辑
│   ├── BooksBLL.cs           # 图书业务逻辑
│   ├── BorrowersBLL.cs       # 借阅人业务逻辑
│   └── BorrowBLL.cs          # 借阅业务逻辑
├── Views/                    # 视图层
│   ├── LoginForm.cs          # 登录窗体
│   ├── RegisterForm.cs       # 注册窗体
│   ├── MainForm.cs           # 主窗体
│   ├── BookManageForm.cs     # 图书管理
│   ├── BookEditDialog.cs     # 图书编辑对话框
│   ├── BorrowerManageForm.cs # 借阅人管理
│   ├── BorrowerEditDialog.cs # 借阅人编辑对话框
│   ├── BorrowForm.cs         # 借阅操作
│   ├── BorrowHistoryForm.cs  # 借阅历史
│   └── OverdueForm.cs        # 逾期管理
├── Resources/                # 资源文件
├── BookLendingSystem.csproj  # 项目配置
├── BookLendingSystem.sln     # 解决方案文件
├── Program.cs                # 程序入口
└── README.md                 # 项目说明
```

---

## 使用说明

### 登录与注册

**默认账号：**
| 账号 | 密码 |
|------|------|
| libai | 123 |
| dufu | 111 |

**安全机制：**
- 连续5次登录失败，账户将被锁定15分钟
- 锁定期间无法登录，超时自动解锁

### 图书管理

- 搜索：支持按书名、作者、条码号模糊搜索
- 添加/编辑：填写图书信息，注意库存数量
- 删除：需要先确认该图书没有借阅记录

### 借阅人管理

- 搜索：支持按姓名、身份证号、电话搜索
- 添加：身份证号必须唯一
- 删除：需要先确认该借阅人没有未归还的图书

### 借阅操作

1. 在左侧图书列表选择要借阅的图书
2. 点击「添加图书」加入借阅列表
3. 查询并选择借阅人
4. 设置借阅天数（默认30天）
5. 点击「确认借阅」完成操作

### 归还图书

在「借阅历史」或「逾期管理」页面：
1. 选择要归还的记录
2. 点击「归还图书」按钮
3. 确认后完成归还，库存自动恢复

---

## 架构设计

### 三层架构

```
Views（视图层）
    ↓ 调用业务方法
BLL（业务逻辑层）
    ↓ 调用数据访问方法
DAL（数据访问层）
    ↓ 执行SQL
MySQL 数据库
```

### 界面布局

系统采用 **Panel 容器布局**，替代传统 MDI 模式：

```
MainForm
├── panelLeft（左侧导航）
│   ├── 系统标题
│   ├── 用户信息
│   └── 导航按钮
└── panelMain（主内容区）
    └── 嵌入的子窗体（TopLevel=false, Dock=Fill）
```

---

## 常见问题

**Q: 连接数据库失败？**
- 检查 MySQL 服务是否启动
- 确认连接字符串中的密码和端口正确
- 验证数据库 `mybook` 是否已创建

**Q: 借阅时提示库存不足？**
- 在图书管理中增加可借数量
- 或等待其他借阅人归还

**Q: 账号被锁定？**
- 等待15分钟自动解锁
- 或联系管理员重置锁定状态

---

## 开发规范

### 命名规范

- 类名：`PascalCase`（如 `BookManageForm`）
- 方法名：`PascalCase`（如 `GetAllBooks`）
- 私有字段：`camelCase`（如 `booksBLL`）

### 安全规范

- 使用参数化查询防止 SQL 注入
- 敏感信息（如密码）不应明文存储
- 异常信息不应暴露数据库细节

---

## 许可证

本项目仅供学习参考使用。
