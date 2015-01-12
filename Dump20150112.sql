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
-- Table structure for table `settings_client`
--

DROP TABLE IF EXISTS `settings_client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `settings_client` (
  `ClientId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `RequestPrivateKey` varchar(2000) NOT NULL,
  `ResponsePublicKey` varchar(2000) NOT NULL,
  PRIMARY KEY (`ClientId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `settings_client`
--

LOCK TABLES `settings_client` WRITE;
/*!40000 ALTER TABLE `settings_client` DISABLE KEYS */;
/*!40000 ALTER TABLE `settings_client` ENABLE KEYS */;
UNLOCK TABLES;

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
  CONSTRAINT `fk_client` FOREIGN KEY (`ClientId`) REFERENCES `settings_client` (`ClientId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_node` FOREIGN KEY (`NodeId`) REFERENCES `settings_node` (`NodeId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `settings_client_node`
--

LOCK TABLES `settings_client_node` WRITE;
/*!40000 ALTER TABLE `settings_client_node` DISABLE KEYS */;
/*!40000 ALTER TABLE `settings_client_node` ENABLE KEYS */;
UNLOCK TABLES;

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
  PRIMARY KEY (`NodeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `settings_node`
--

LOCK TABLES `settings_node` WRITE;
/*!40000 ALTER TABLE `settings_node` DISABLE KEYS */;
/*!40000 ALTER TABLE `settings_node` ENABLE KEYS */;
UNLOCK TABLES;

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
-- Dumping data for table `settings_entry`
--

LOCK TABLES `settings_entry` WRITE;
/*!40000 ALTER TABLE `settings_entry` DISABLE KEYS */;
/*!40000 ALTER TABLE `settings_entry` ENABLE KEYS */;
UNLOCK TABLES;

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
-- Dumping data for table `settings_tree`
--

LOCK TABLES `settings_tree` WRITE;
/*!40000 ALTER TABLE `settings_tree` DISABLE KEYS */;
/*!40000 ALTER TABLE `settings_tree` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'grit'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-01-12 18:03:02
