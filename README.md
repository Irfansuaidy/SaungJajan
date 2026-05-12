# SAUNGJAJAN

SAUNGJAJAN adalah aplikasi web berbasis ASP.NET Core Razor Pages untuk pemesanan makanan dan minuman pada warung/kantin. Aplikasi ini memiliki fitur utama untuk user, toko, katalog produk, pemesanan, log pesanan, pembayaran, kwitansi, dan laporan transaksi penjualan.

## 1. Teknologi yang Digunakan

Aplikasi ini menggunakan teknologi berikut:

- ASP.NET Core Razor Pages
- Entity Framework Core
- MySQL Database
- Bootstrap
- C#
- HTML, CSS, dan JavaScript

## 2. Kebutuhan Sistem

Sebelum menjalankan project, pastikan perangkat sudah memiliki:

- .NET 8 SDK
- MySQL Server
- Visual Studio Code atau Visual Studio
- MySQL client seperti phpMyAdmin, DBeaver, atau MySQL Workbench

Cek instalasi .NET dengan perintah:

```bash
dotnet --version

kalau belum ada .NET 8 SDK, download .NET resmi dari Microsoft
Pilih .NET 8 SDK
Windows x64 Installer


Cek daftar SDK yang terpasang:
```bash
dotnet --list-sdks

Cek daftar runtime yang terpasang:
```bash
dotnet --list-runtimes

3. Clone atau Buka Project

Jika project dari GitHub, jalankan:
```bash
git clone https://github.com/username/nama-repository.git
cd nama-repository

Jika project sudah ada di komputer, buka folder project melalui terminal:
```bash
cd path/ke/folder/project

Contoh:
```bash
cd D:\Project\SAUNGJAJAN

4. Restore Package

Jalankan perintah berikut untuk mengunduh semua dependency project:
```bash
dotnet restore

Perintah ini wajib dijalankan setelah project pertama kali dibuka atau setelah ada perubahan package.

5. Konfigurasi Database

buat database dengan nama:
db_sajan, lalu ke tab import dan masukkan file yang ada di database, db_sajan.sql

6. Konfigurasi Koneksi Database

Buka file:

appsettings.json

Tambahkan atau sesuaikan connection string berikut:

{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=db_sajan;user=root;password=;"
  }
}

7. Menjalankan Project

Jalankan project dengan perintah:
```bash
dotnet run

lalu masuk ke url http://localhost:xxxx

8. Build Project

Untuk memastikan project bisa dikompilasi tanpa error, jalankan:
```bash
dotnet build

Jika berhasil, akan muncul keterangan:
```bash
Build succeeded.

9. Clean Project

Untuk membersihkan hasil build sebelumnya, jalankan:
```bash
dotnet clean

Perintah ini akan menghapus file hasil kompilasi pada folder bin dan obj.

Setelah clean, jalankan kembali:
```bash
dotnet restore
dotnet build
dotnet run

10. Struktur Folder Utama

Struktur umum project:

SAUNGJAJAN
├── Data
│   └── AppDbContext.cs
├── Models
│   ├── TbUser.cs
│   ├── TbToko.cs
│   ├── TbProduk.cs
│   ├── TbPesanan.cs
│   ├── DetailPesanan.cs
│   └── TbPembayaran.cs
├── Pages
│   ├── Auth
│   │   ├── Login.cshtml
│   │   ├── Login.cshtml.cs
│   │   ├── LoginToko.cshtml
│   │   ├── LoginToko.cshtml.cs
│   │   ├── Logout.cshtml
│   │   └── LogoutToko.cshtml
│   ├── User
│   │   ├── Menu.cshtml
│   │   ├── Menu.cshtml.cs
│   │   ├── LogPesanan.cshtml
│   │   ├── LogPesanan.cshtml.cs
│   │   ├── Kwitansi.cshtml
│   │   └── Kwitansi.cshtml.cs
│   └── User_Toko
│       ├── Dashboard.cshtml
│       ├── Dashboard.cshtml.cs
│       ├── LaporanPenjualan.cshtml
│       └── LaporanPenjualan.cshtml.cs
├── appsettings.json
├── Program.cs
└── README.md