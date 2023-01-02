CREATE DATABASE IF NOT EXISTS `MoonV` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `MoonV`;


DROP TABLE IF EXISTS `accounts`;
CREATE TABLE IF NOT EXISTS `accounts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(64) COLLATE utf8mb3_bin NOT NULL,
  `password` varchar(256) CHARACTER SET utf8mb3 NOT NULL,
  `socialId` bigint(64) unsigned NOT NULL,
  `isFirstLogin` tinyint(1) NOT NULL DEFAULT 1,
  `adminlevel` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_bin;

DROP TABLE IF EXISTS `accounts_characters`;
CREATE TABLE IF NOT EXISTS `accounts_characters` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accountId` int(11) NOT NULL,
  `firstname` varchar(64) NOT NULL,
  `lastname` varchar(64) NOT NULL,
  `gender` int(11) NOT NULL,
  `birthday` varchar(64) NOT NULL,
  `cash` int(11) NOT NULL,
  `bank` int(11) NOT NULL,
  `health` int(11) NOT NULL,
  `armor` int(11) NOT NULL,
  `jailtime` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4;

DROP TABLE IF EXISTS `accounts_position`;
CREATE TABLE IF NOT EXISTS `accounts_position` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accId` int(11) NOT NULL,
  `position` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_bin NOT NULL CHECK (json_valid(`position`)),
  `dimension` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_bin;


DROP TABLE IF EXISTS `accounts_skin`;
CREATE TABLE IF NOT EXISTS `accounts_skin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accId` int(11) NOT NULL,
  `facefeatures` varchar(256) COLLATE utf8mb3_bin NOT NULL,
  `headblendsdata` varchar(256) COLLATE utf8mb3_bin NOT NULL,
  `headoverlays` varchar(256) COLLATE utf8mb3_bin NOT NULL,
  `clothesTop` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesTorso` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesLeg` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesFeet` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesHat` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesGlass` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesEarring` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesNecklace` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesMask` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesArmor` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesUndershirt` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesBracelet` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesWatch` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesBag` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  `clothesDecal` varchar(128) COLLATE utf8mb3_bin NOT NULL DEFAULT 'None',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_bin;


DROP TABLE IF EXISTS `alphakeys`;
CREATE TABLE IF NOT EXISTS `alphakeys` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `alphakey` varchar(64) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4;
