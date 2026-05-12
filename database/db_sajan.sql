-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: localhost
-- Generation Time: May 12, 2026 at 08:22 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_sajan`
--

-- --------------------------------------------------------

--
-- Table structure for table `detail_pesanan`
--

CREATE TABLE `detail_pesanan` (
  `id_detail` int(11) NOT NULL,
  `id_pesanan` int(11) NOT NULL,
  `id_produk` int(11) NOT NULL,
  `id_toko` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `subtotal` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `detail_pesanan`
--

INSERT INTO `detail_pesanan` (`id_detail`, `id_pesanan`, `id_produk`, `id_toko`, `quantity`, `subtotal`) VALUES
(1, 1, 5, 101, 1, 5000.00),
(2, 2, 2, 101, 2, 20000.00),
(3, 2, 24, 105, 1, 6000.00),
(4, 3, 20, 104, 1, 15000.00),
(5, 4, 17, 104, 1, 6000.00),
(6, 5, 11, 102, 1, 10000.00),
(7, 6, 3, 101, 1, 10000.00),
(8, 7, 15, 103, 7, 7000.00),
(9, 8, 18, 104, 1, 10000.00),
(10, 9, 19, 104, 2, 24000.00),
(11, 10, 2, 101, 2, 20000.00),
(12, 11, 24, 105, 1, 6000.00),
(13, 11, 6, 101, 1, 3000.00),
(14, 12, 27, 105, 2, 10000.00),
(15, 13, 12, 103, 2, 6000.00),
(16, 14, 28, 105, 1, 13000.00),
(17, 15, 4, 101, 2, 8000.00),
(18, 16, 2, 101, 1, 10000.00),
(19, 16, 5, 101, 1, 5000.00),
(20, 17, 15, 103, 1, 1000.00),
(21, 21, 29, 106, 3, 36000.00),
(22, 22, 15, 103, 3, 3000.00),
(23, 23, 16, 103, 3, 3000.00),
(24, 24, 27, 105, 1, 5000.00),
(25, 25, 27, 105, 1, 5000.00),
(26, 26, 27, 105, 1, 5000.00);

-- --------------------------------------------------------

--
-- Table structure for table `tb_pembayaran`
--

CREATE TABLE `tb_pembayaran` (
  `id_pembayaran` int(11) NOT NULL,
  `id_pesanan` int(11) NOT NULL,
  `id_user` int(11) NOT NULL,
  `id_toko` int(11) NOT NULL,
  `kode_kwitansi` varchar(50) NOT NULL,
  `metode_pembayaran` enum('Saldo') NOT NULL DEFAULT 'Saldo',
  `jumlah_bayar` decimal(10,2) NOT NULL,
  `status_pembayaran` enum('Berhasil') NOT NULL DEFAULT 'Berhasil',
  `waktu_bayar` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tb_pesanan`
--

CREATE TABLE `tb_pesanan` (
  `id_pesanan` int(11) NOT NULL,
  `id_user` int(11) NOT NULL,
  `status` enum('Diproses','Siap','Dibatalkan','Lunas') NOT NULL,
  `waktu_pesan` datetime NOT NULL,
  `total_harga` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tb_pesanan`
--

INSERT INTO `tb_pesanan` (`id_pesanan`, `id_user`, `status`, `waktu_pesan`, `total_harga`) VALUES
(1, 14, 'Lunas', '2026-04-01 10:30:00', 5000.00),
(2, 3, 'Siap', '2026-04-02 11:15:00', 20000.00),
(3, 19, 'Diproses', '2026-04-03 12:45:00', 15000.00),
(4, 7, 'Lunas', '2026-04-03 14:00:00', 6000.00),
(5, 12, 'Siap', '2026-04-04 09:20:00', 10000.00),
(6, 1, 'Lunas', '2026-04-04 15:10:00', 10000.00),
(7, 20, 'Diproses', '2026-04-05 08:05:00', 7000.00),
(8, 5, 'Lunas', '2026-04-05 13:30:00', 10000.00),
(9, 18, 'Siap', '2026-04-06 10:00:00', 24000.00),
(10, 9, 'Lunas', '2026-04-06 12:25:00', 20000.00),
(11, 2, 'Lunas', '2026-04-07 07:45:00', 9000.00),
(12, 16, 'Lunas', '2026-04-07 10:15:00', 10000.00),
(13, 11, 'Siap', '2026-04-07 11:00:00', 6000.00),
(14, 6, 'Lunas', '2026-04-07 11:20:00', 13000.00),
(15, 15, 'Lunas', '2026-04-07 11:30:00', 8000.00),
(16, 8, 'Lunas', '2026-04-07 11:40:00', 15000.00),
(17, 4, 'Siap', '2026-04-07 11:45:00', 24000.00),
(18, 10, 'Diproses', '2026-04-07 11:50:00', 12500.00),
(19, 13, 'Lunas', '2026-04-07 11:55:00', 30000.00),
(20, 17, 'Siap', '2026-04-07 12:00:00', 21000.00),
(21, 23, 'Lunas', '2026-05-12 11:36:11', 36000.00),
(22, 23, 'Lunas', '2026-05-12 12:11:37', 3000.00),
(23, 23, 'Siap', '2026-05-12 12:51:08', 3000.00),
(24, 23, 'Diproses', '2026-05-12 13:04:11', 5000.00),
(25, 23, 'Diproses', '2026-05-12 13:04:19', 5000.00),
(26, 23, 'Diproses', '2026-05-12 13:04:45', 5000.00);

-- --------------------------------------------------------

--
-- Table structure for table `tb_produk`
--

CREATE TABLE `tb_produk` (
  `id_produk` int(11) NOT NULL,
  `id_toko` int(11) NOT NULL,
  `nama_makanan` varchar(40) NOT NULL,
  `jenis` enum('makanan','minuman') NOT NULL DEFAULT 'makanan',
  `harga` decimal(10,2) NOT NULL,
  `stok` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tb_produk`
--

INSERT INTO `tb_produk` (`id_produk`, `id_toko`, `nama_makanan`, `jenis`, `harga`, `stok`) VALUES
(1, 101, 'Mie', 'makanan', 7000.00, 60),
(2, 101, 'Mie Telur', 'makanan', 10000.00, 120),
(3, 101, 'Kentang Goreng', 'makanan', 10000.00, 30),
(4, 101, 'Floridina', 'minuman', 4000.00, 25),
(5, 101, 'Teh Botol', 'minuman', 5000.00, 60),
(6, 101, 'Le Minerale', 'minuman', 3000.00, 80),
(7, 102, 'Dimsum', 'makanan', 12000.00, 40),
(8, 102, 'Dimsum Satuan', 'makanan', 3000.00, 200),
(9, 102, 'Siomay isi 7', 'makanan', 10000.00, 55),
(10, 102, 'Siomay Satuan', 'makanan', 1500.00, 70),
(11, 102, 'Batagor', 'makanan', 10000.00, 35),
(12, 103, 'Risol Mayo', 'makanan', 3000.00, 20),
(13, 103, 'Gorengan (Bakwan, Tempe, Tahu isi, Marta', 'makanan', 5000.00, 90),
(14, 103, 'Pisang Goreng', 'makanan', 5000.00, 50),
(15, 103, 'Bakpau Mini', 'makanan', 1000.00, 37),
(16, 103, 'Cireng Mini', 'makanan', 1000.00, 97),
(17, 104, 'Jasuke Kecil', 'makanan', 6000.00, 65),
(18, 104, 'Jasuke Besar', 'makanan', 10000.00, 48),
(19, 104, 'Nasi Katsu', 'makanan', 12000.00, 30),
(20, 104, 'Nasi Katsu Telur Ceplok', 'makanan', 15000.00, 12),
(21, 105, 'Rogut Asin', 'makanan', 3000.00, 12),
(22, 105, 'Rogut Manis', 'makanan', 2500.00, 11),
(23, 105, 'Rogut Manis Combo', 'makanan', 5000.00, 33),
(24, 105, 'Rogut Asin Combo', 'makanan', 6000.00, 33),
(25, 105, 'Rogut Mix Manis Asin', 'makanan', 5000.00, 32),
(26, 105, 'Cimol Ori', 'makanan', 5000.00, 21),
(27, 105, 'Donat isi 3', 'makanan', 5000.00, 10),
(28, 105, 'Bakso', 'makanan', 13000.00, 33),
(29, 106, 'ayam', 'makanan', 12000.00, 120);

-- --------------------------------------------------------

--
-- Table structure for table `tb_toko`
--

CREATE TABLE `tb_toko` (
  `id_toko` int(11) NOT NULL,
  `nama_toko` varchar(255) NOT NULL,
  `pemasukan` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tb_toko`
--

INSERT INTO `tb_toko` (`id_toko`, `nama_toko`, `pemasukan`) VALUES
(101, 'Warung Pak Jaya', 0.00),
(102, 'Warung Bu Yuli', 0.00),
(103, 'Warung Bu Nur', 6000.00),
(104, 'Warung Bu Wulan', 0.00),
(105, 'Warung Bu Ida', 15000.00),
(106, 'Warung Bu Dewi', 36000.00);

-- --------------------------------------------------------

--
-- Table structure for table `tb_user`
--

CREATE TABLE `tb_user` (
  `id_user` int(11) NOT NULL,
  `nama` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `saldo` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tb_user`
--

INSERT INTO `tb_user` (`id_user`, `nama`, `email`, `password`, `saldo`) VALUES
(1, 'Zaki Al-Farabi', 'zaki.alfarabi@ic.sch.id', 'Zaki#2026!', 54000.00),
(2, 'Raihan Faeyza', 'raihan.faeyza@ic.sch.id', 'Raih@n99_', 82000.00),
(3, 'Fathir Muhammad', 'fathir.muhammad@ic.sch.id', 'Fathir*201', 67000.00),
(4, 'Ghazali Robbani', 'ghazali.robbani@ic.sch.id', 'Ghaz_44!', 91000.00),
(5, 'Ayman Hafizh', 'ayman.hafizh@ic.sch.id', 'Ayman#007', 75000.00),
(6, 'Dzaki Izzatul', 'dzaki.izzatul@ic.sch.id', 'Dzaki.Izza', 59000.00),
(7, 'Naufal Hamzah', 'naufal.hamzah@ic.sch.id', 'Naufal%22', 88000.00),
(8, 'Ihsan Kamil', 'ihsan.kamil@ic.sch.id', 'Ihsan_K88', 100000.00),
(9, 'Thariq Aziz', 'thariq.aziz@ic.sch.id', 'Thariq!12', 63000.00),
(10, 'Zaidan Khairy', 'zaidan.khairy@ic.sch.id', 'Zaidan^77', 72000.00),
(11, 'Alya Mukhbita', 'alya.mukhbita@ic.sch.id', 'Alya#2026!', 51000.00),
(12, 'Najla Syauqia', 'najla.syauqia@ic.sch.id', 'Najla.99*', 84000.00),
(13, 'Hana Humaira', 'hana.humaira@ic.sch.id', 'HanaH_01!', 96000.00),
(14, 'Kayla Az-Zahra', 'kayla.azzahra@ic.sch.id', 'Kayla%202', 60000.00),
(15, 'Yasmin Khairunnisa', 'yasmin.khair@ic.sch.id', 'Yasmin#X1', 78000.00),
(16, 'Fatimah Az-Zakiya', 'fatimah.azzakiya@ic.sch.id', 'Fatim_Zz7', 55000.00),
(17, 'Safira Nur Latifah', 'safira.latifah@ic.sch.id', 'Safira!88', 89000.00),
(18, 'Aisya Kamila', 'aisya.kamila@ic.sch.id', 'Aisya#909', 93000.00),
(19, 'Nabila Fawwaz', 'nabila.fawwaz@ic.sch.id', 'Nab_Faw!z', 66000.00),
(20, 'Zhafira Izzati', 'zhafira.izzati@ic.sch.id', 'Zhafi.Zha', 70000.00),
(21, 'Bobby Santoso', 'idjiqw@gmail.com', '19147a9b94aacf0f44574121abdd6ec7efda2b920dbccfc4cbf989ce5bc5bd79', 123000.00),
(22, '0 OR 1=1', 'qeqwe@gmail.com', '9592dd359ef049d457f42ec6303de84c0a4878e063afd7e708d0650bf72cc578', 0.00),
(23, 'test account', 'whnyah@gmail.com', '123456', 143000.00);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `detail_pesanan`
--
ALTER TABLE `detail_pesanan`
  ADD PRIMARY KEY (`id_detail`),
  ADD KEY `id_produk` (`id_produk`),
  ADD KEY `id_pesanan` (`id_pesanan`),
  ADD KEY `fk_detail_toko` (`id_toko`);

--
-- Indexes for table `tb_pembayaran`
--
ALTER TABLE `tb_pembayaran`
  ADD PRIMARY KEY (`id_pembayaran`),
  ADD UNIQUE KEY `uk_pembayaran_pesanan` (`id_pesanan`),
  ADD UNIQUE KEY `uk_kode_kwitansi` (`kode_kwitansi`),
  ADD KEY `fk_pembayaran_user` (`id_user`),
  ADD KEY `fk_pembayaran_toko` (`id_toko`);

--
-- Indexes for table `tb_pesanan`
--
ALTER TABLE `tb_pesanan`
  ADD PRIMARY KEY (`id_pesanan`),
  ADD KEY `id_user` (`id_user`);

--
-- Indexes for table `tb_produk`
--
ALTER TABLE `tb_produk`
  ADD PRIMARY KEY (`id_produk`),
  ADD KEY `id_toko` (`id_toko`);

--
-- Indexes for table `tb_toko`
--
ALTER TABLE `tb_toko`
  ADD PRIMARY KEY (`id_toko`),
  ADD UNIQUE KEY `nama_toko` (`nama_toko`);

--
-- Indexes for table `tb_user`
--
ALTER TABLE `tb_user`
  ADD PRIMARY KEY (`id_user`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `detail_pesanan`
--
ALTER TABLE `detail_pesanan`
  MODIFY `id_detail` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `tb_pembayaran`
--
ALTER TABLE `tb_pembayaran`
  MODIFY `id_pembayaran` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tb_pesanan`
--
ALTER TABLE `tb_pesanan`
  MODIFY `id_pesanan` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT for table `tb_produk`
--
ALTER TABLE `tb_produk`
  MODIFY `id_produk` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `tb_toko`
--
ALTER TABLE `tb_toko`
  MODIFY `id_toko` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=107;

--
-- AUTO_INCREMENT for table `tb_user`
--
ALTER TABLE `tb_user`
  MODIFY `id_user` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `detail_pesanan`
--
ALTER TABLE `detail_pesanan`
  ADD CONSTRAINT `fk_detail_produk` FOREIGN KEY (`id_produk`) REFERENCES `tb_produk` (`id_produk`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_detail_toko` FOREIGN KEY (`id_toko`) REFERENCES `tb_toko` (`id_toko`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `tb_pembayaran`
--
ALTER TABLE `tb_pembayaran`
  ADD CONSTRAINT `fk_pembayaran_pesanan` FOREIGN KEY (`id_pesanan`) REFERENCES `tb_pesanan` (`id_pesanan`),
  ADD CONSTRAINT `fk_pembayaran_toko` FOREIGN KEY (`id_toko`) REFERENCES `tb_toko` (`id_toko`),
  ADD CONSTRAINT `fk_pembayaran_user` FOREIGN KEY (`id_user`) REFERENCES `tb_user` (`id_user`);

--
-- Constraints for table `tb_pesanan`
--
ALTER TABLE `tb_pesanan`
  ADD CONSTRAINT `tb_pesanan_user` FOREIGN KEY (`id_user`) REFERENCES `tb_user` (`id_user`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `tb_produk`
--
ALTER TABLE `tb_produk`
  ADD CONSTRAINT `fk_produk_toko` FOREIGN KEY (`id_toko`) REFERENCES `tb_toko` (`id_toko`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
