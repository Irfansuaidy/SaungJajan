# SAUNGJAJAN

SAUNGJAJAN adalah aplikasi web berbasis **ASP.NET Core Razor Pages** untuk pemesanan makanan dan minuman pada warung atau kantin.  
Aplikasi ini memiliki fitur utama seperti manajemen user, toko, katalog produk, pemesanan, log pesanan, pembayaran, kwitansi, dan laporan transaksi penjualan.

---

# 1. Teknologi yang Digunakan

Aplikasi ini dibangun menggunakan teknologi berikut:

- ASP.NET Core Razor Pages
- Entity Framework Core
- MySQL Database
- Bootstrap
- C#
- HTML, CSS, dan JavaScript

---

# 2. Kebutuhan Sistem

Sebelum menjalankan project, pastikan perangkat sudah memiliki:

- .NET 8 SDK
- MySQL Server
- Visual Studio Code atau Visual Studio
- MySQL Client:
  - phpMyAdmin
  - DBeaver
  - MySQL Workbench

---

# 3. Instalasi .NET 8 SDK

## Cek Versi .NET

```bash
dotnet --version
```

## Cek Daftar SDK

```bash
dotnet --list-sdks
```

## Cek Daftar Runtime

```bash
dotnet --list-runtimes
```

Jika .NET 8 SDK belum terpasang:

1. Download .NET SDK dari website resmi Microsoft
2. Pilih:
   - .NET 8 SDK
   - Windows x64 Installer
3. Install hingga selesai

---

# 4. Clone atau Buka Project

## Jika Project Berasal dari GitHub

```bash
git clone https://github.com/username/nama-repository.git
cd nama-repository
```

## Jika Project Sudah Ada di Komputer

Buka folder project melalui terminal:

```bash
cd path/ke/folder/project
```

Contoh:

```bash
cd D:\Project\SAUNGJAJAN
```

---

# 5. Restore Package

Jalankan perintah berikut untuk mengunduh seluruh dependency project:

```bash
dotnet restore
```

Perintah ini wajib dijalankan saat pertama kali membuka project atau setelah ada perubahan package.

---

# 6. Konfigurasi Database

## Membuat Database

Buat database baru dengan nama:

```sql
db_sajan
```

## Import Database

1. Buka MySQL Client (phpMyAdmin / DBeaver / MySQL Workbench)
2. Pilih menu **Import**
3. Import file:

```text
db_sajan.sql
```

---

# 7. Konfigurasi Connection String

Buka file:

```text
appsettings.json
```

Tambahkan atau sesuaikan konfigurasi berikut:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=db_sajan;user=root;password=;"
  }
}
```

Sesuaikan username dan password MySQL sesuai konfigurasi perangkat masing-masing.

---

# 8. Menjalankan Project

Jalankan project menggunakan perintah:

```bash
dotnet run
```

Jika berhasil, buka browser dan akses:

```text
http://localhost:xxxx
```

Port akan muncul otomatis pada terminal saat project dijalankan.

---

# 9. Build Project

Untuk memastikan project dapat dikompilasi tanpa error:

```bash
dotnet build
```

Jika berhasil, akan muncul output:

```bash
Build succeeded.
```

---

# 10. Clean Project

Untuk membersihkan file hasil build sebelumnya:

```bash
dotnet clean
```

Perintah ini akan menghapus folder:

- bin
- obj

Setelah melakukan clean, jalankan kembali:

```bash
dotnet restore
dotnet build
dotnet run
```

---

# 11. Struktur Folder Project

```text
SAUNGJAJAN
├── Data
│   └── AppDbContext.cs
│
├── Models
│   ├── TbUser.cs
│   ├── TbToko.cs
│   ├── TbProduk.cs
│   ├── TbPesanan.cs
│   ├── DetailPesanan.cs
│   └── TbPembayaran.cs
│
├── Pages
│   ├── Auth
│   │   ├── Login.cshtml
│   │   ├── Login.cshtml.cs
│   │   ├── LoginToko.cshtml
│   │   ├── LoginToko.cshtml.cs
│   │   ├── Logout.cshtml
│   │   └── LogoutToko.cshtml
│   │
│   ├── User
│   │   ├── Menu.cshtml
│   │   ├── Menu.cshtml.cs
│   │   ├── LogPesanan.cshtml
│   │   ├── LogPesanan.cshtml.cs
│   │   ├── Kwitansi.cshtml
│   │   └── Kwitansi.cshtml.cs
│   │
│   └── User_Toko
│       ├── Dashboard.cshtml
│       ├── Dashboard.cshtml.cs
│       ├── LaporanPenjualan.cshtml
│       └── LaporanPenjualan.cshtml.cs
│
├── appsettings.json
├── Program.cs
└── README.md
```

---