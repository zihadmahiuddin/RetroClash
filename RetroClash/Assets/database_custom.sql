SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

CREATE DATABASE IF NOT EXISTS `u679954513_rcdb` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `u679954513_rcdb`;

CREATE TABLE IF NOT EXISTS `clan` (
  `ClanId` bigint(20) NOT NULL,
  `Name` text CHARACTER SET utf8mb4 NOT NULL,
  `Score` bigint(20) NOT NULL,
  `Data` text CHARACTER SET utf8mb4 NOT NULL,
  PRIMARY KEY (`ClanId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `player` (
  `PlayerId` bigint(20) NOT NULL,
  `Score` bigint(20) NOT NULL,
  `Language` text CHARACTER SET utf8mb4 NOT NULL,
  `Avatar` text CHARACTER SET utf8mb4 NOT NULL,
  `GameObjects` text CHARACTER SET utf8mb4 NOT NULL,
  PRIMARY KEY (`PlayerId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

CREATE TABLE IF NOT EXISTS `api` (
  `Id` bigint(20) NOT NULL,
  `Info` text CHARACTER SET utf8mb4 NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;