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
-- Dumping data for table `settings_tree`
--

LOCK TABLES `settings_tree` WRITE;
/*!40000 ALTER TABLE `settings_tree` DISABLE KEYS */;
INSERT INTO `settings_tree` VALUES (1,0,NULL,NULL),(1,1,0,4),(1,2,1,11),(1,3,1,9),(1,4,3,13),(1,5,0,6),(1,6,5,5),(1,7,0,10),(1,8,7,12);
/*!40000 ALTER TABLE `settings_tree` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sequence`
--

DROP TABLE IF EXISTS `sequence`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sequence` (
  `Id` int(11) NOT NULL,
  `Value` int(11) NOT NULL DEFAULT '0',
  `Comment` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sequence`
--

LOCK TABLES `sequence` WRITE;
/*!40000 ALTER TABLE `sequence` DISABLE KEYS */;
INSERT INTO `sequence` VALUES (1,4155,'Test'),(100,3,'CQRS_Account'),(101,2,'CQRS_Project'),(102,64625,'CQRS_Investment'),(1000,14,'Settings Node'),(1001,7,'Settings Client');
/*!40000 ALTER TABLE `sequence` ENABLE KEYS */;
UNLOCK TABLES;

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
-- Dumping data for table `settings_client`
--

LOCK TABLES `settings_client` WRITE;
/*!40000 ALTER TABLE `settings_client` DISABLE KEYS */;
INSERT INTO `settings_client` VALUES (3,'www','<RSAKeyValue><Modulus>ouKLFb+t9OD9gSfw5ZnqwQPi48zevzZqb1d5wvgjjNaqLqNdJNm3MfQK90BmuMN+oahWe14Pu6hsX0Z0QJriVc+CLVas2dwy31Om63pVcNMQDA1gqyx4SWxwXyD+E1okIlljKzxzNQCXveee3CT08BobwdYvYllGYplE0N+H4pE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>',3,0,'2015-01-15 11:26:07','2015-01-16 22:59:27',NULL),(4,'app','fdsafdas',1,0,'2015-01-15 13:35:38','2015-01-15 18:15:40',NULL),(5,'www_test','fdsafdas',1,0,'2015-01-15 15:28:35','2015-01-15 18:16:03',NULL),(6,'fdsa','fdsafda',0,1,'2015-01-16 23:03:06','2015-01-16 23:03:06','2015-01-16 23:13:44');
/*!40000 ALTER TABLE `settings_client` ENABLE KEYS */;
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
INSERT INTO `settings_entry` VALUES (5,'ConnectionString','fdsafda'),(5,'TimeOutSeconds','60'),(9,'ConnectionString','fdsakjfdlsajfda'),(9,'TimeOutSeconds','60'),(11,'ConnectionString','fdkslafda'),(12,'ConnectionString','fdsafdas'),(12,'fdsa','fdsa'),(13,'ConnectionString','faf'),(13,'fdsa','fdas');
/*!40000 ALTER TABLE `settings_entry` ENABLE KEYS */;
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
INSERT INTO `settings_client_node` VALUES (3,4),(4,4),(3,5),(5,5),(5,6),(3,9),(4,9),(3,11),(4,11),(4,12),(3,13);
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
  `Deleted` int(11) NOT NULL DEFAULT '0',
  `DeleteAt` datetime DEFAULT NULL,
  PRIMARY KEY (`NodeId`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `settings_node`
--

LOCK TABLES `settings_node` WRITE;
/*!40000 ALTER TABLE `settings_node` DISABLE KEYS */;
INSERT INTO `settings_node` VALUES (4,'Product',1,'0001-01-01 00:00:00','2015-01-15 18:12:00',0,NULL),(5,'MySql',1,'2015-01-14 16:03:55','2015-01-15 18:14:51',0,NULL),(6,'Develop',1,'2015-01-14 16:04:22','2015-01-15 18:12:17',0,NULL),(9,'MySql',1,'2015-01-14 16:40:24','2015-01-15 18:13:49',0,NULL),(10,'QA',8,'2015-01-14 16:54:38','2015-01-15 18:12:41',0,NULL),(11,'MongoDB',2,'2015-01-15 18:14:12','2015-01-16 22:30:58',0,NULL),(12,'Redis',0,'2015-01-15 19:04:58','2015-01-15 19:04:58',0,NULL),(13,'fdafda',0,'2015-01-15 19:10:54','2015-01-15 19:10:54',1,'2015-01-16 23:20:26');
/*!40000 ALTER TABLE `settings_node` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2015-01-16 23:23:59
