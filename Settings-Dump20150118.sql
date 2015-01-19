CREATE DATABASE  IF NOT EXISTS `grit` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `grit`;
-- MySQL dump 10.13  Distrib 5.6.13, for Win32 (x86)
--
-- Host: 127.0.0.1    Database: grit
-- ------------------------------------------------------
-- Server version	5.6.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `settings_tree`
--

DROP TABLE IF EXISTS `settings_tree`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `settings_tree` (
  `Tree` int(11) NOT NULL,
  `Id` int(11) NOT NULL,
  `Parent` int(11) DEFAULT NULL,
  `Data` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`,`Tree`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `settings_client`
--

DROP TABLE IF EXISTS `settings_client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `settings_client` (
  `ClientId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `PublicKey` varchar(2000) NOT NULL,
  `Version` int(11) NOT NULL,
  `Deleted` int(11) NOT NULL DEFAULT '0',
  `CreateAt` datetime NOT NULL,
  `UpdateAt` datetime NOT NULL,
  `DeleteAt` datetime DEFAULT NULL,
  PRIMARY KEY (`ClientId`),
  UNIQUE KEY `ix_client_name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `settings_entry`
--

DROP TABLE IF EXISTS `settings_entry`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `settings_entry` (
  `NodeId` int(11) NOT NULL,
  `Key` varchar(100) NOT NULL,
  `Value` varchar(4000) NOT NULL,
  PRIMARY KEY (`NodeId`,`Key`),
  CONSTRAINT `fk_node_entry` FOREIGN KEY (`NodeId`) REFERENCES `settings_node` (`NodeId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `settings_client_node`
--

DROP TABLE IF EXISTS `settings_client_node`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `settings_client_node` (
  `ClientId` int(11) NOT NULL,
  `NodeId` int(11) NOT NULL,
  PRIMARY KEY (`ClientId`,`NodeId`),
  KEY `fk_node_idx` (`NodeId`),
  CONSTRAINT `fk_node` FOREIGN KEY (`NodeId`) REFERENCES `settings_node` (`NodeId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `settings_node`
--

DROP TABLE IF EXISTS `settings_node`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `settings_node` (
  `NodeId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Version` int(11) NOT NULL,
  `CreateAt` datetime NOT NULL,
  `UpdateAt` datetime NOT NULL,
  `Deleted` int(11) NOT NULL DEFAULT '0',
  `DeleteAt` datetime DEFAULT NULL,
  PRIMARY KEY (`NodeId`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `settings_user`
--

DROP TABLE IF EXISTS `settings_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `settings_user` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `PasswordHash` varchar(100) NOT NULL,
  `Version` int(11) NOT NULL,
  `Deleted` int(11) NOT NULL DEFAULT '0',
  `CreateAt` datetime NOT NULL,
  `UpdateAt` datetime NOT NULL,
  `DeleteAt` datetime DEFAULT NULL,
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `idx_username` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `settings_user`
--

LOCK TABLES `settings_user` WRITE;
/*!40000 ALTER TABLE `settings_user` DISABLE KEYS */;
INSERT INTO `settings_user` VALUES (1,'admin','1000:LBtm29kdYeO/yj627CzHnp4otNRBjyZ8:ElN/ssx1YsFjfaB/n+u5iEna/pTQGhdw',2,0,'2015-01-18 16:52:59','2015-01-18 16:52:59',NULL);
/*!40000 ALTER TABLE `settings_user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

--
-- Table structure for table `sequence`
--

/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE IF NOT EXISTS `sequence` (
  `Id` int(11) NOT NULL,
  `Value` int(11) NOT NULL DEFAULT '0',
  `Comment` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-01-18 19:35:24