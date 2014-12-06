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
-- Table structure for table `ace_demo_account`
--

DROP TABLE IF EXISTS `ace_demo_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ace_demo_account` (
  `AccountId` int(11) NOT NULL,
  `Amount` decimal(20,2) NOT NULL,
  PRIMARY KEY (`AccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ace_demo_account`
--

LOCK TABLES `ace_demo_account` WRITE;
/*!40000 ALTER TABLE `ace_demo_account` DISABLE KEYS */;
INSERT INTO `ace_demo_account` VALUES (1,6673.00),(2,99993327.00);
/*!40000 ALTER TABLE `ace_demo_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ace_demo_account_activity`
--

DROP TABLE IF EXISTS `ace_demo_account_activity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ace_demo_account_activity` (
  `ActivityId` int(11) NOT NULL AUTO_INCREMENT,
  `FromAccountId` int(11) DEFAULT NULL,
  `ToAccountId` int(11) DEFAULT NULL,
  `Amount` decimal(20,2) NOT NULL,
  `CreateAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ActivityId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ace_demo_account_activity`
--

LOCK TABLES `ace_demo_account_activity` WRITE;
/*!40000 ALTER TABLE `ace_demo_account_activity` DISABLE KEYS */;
INSERT INTO `ace_demo_account_activity` VALUES (1,2,1,1.00,'2014-11-19 17:43:15'),(2,2,1,1.00,'2014-11-19 18:05:28'),(3,2,1,1.00,'2014-11-19 18:07:20'),(4,2,1,1.00,'2014-11-19 18:07:41'),(5,2,1,1.00,'2014-11-19 18:07:46');
/*!40000 ALTER TABLE `ace_demo_account_activity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ace_demo_investment`
--

DROP TABLE IF EXISTS `ace_demo_investment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ace_demo_investment` (
  `InvestmentId` int(11) NOT NULL,
  `ProjectId` int(11) NOT NULL,
  `AccountId` int(11) NOT NULL,
  `Amount` decimal(20,2) NOT NULL,
  `Status` int(11) NOT NULL,
  `CreateAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`InvestmentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ace_demo_investment`
--

LOCK TABLES `ace_demo_investment` WRITE;
/*!40000 ALTER TABLE `ace_demo_investment` DISABLE KEYS */;
INSERT INTO `ace_demo_investment` VALUES (64525,1,2,1.00,0,'2014-11-07 14:57:12'),(64527,1,2,1.00,0,'2014-11-07 15:01:24'),(64529,1,2,1.00,1,'2014-11-19 17:42:27'),(64531,1,2,1.00,1,'2014-11-19 18:04:59'),(64532,1,2,1.00,1,'2014-11-19 18:07:13'),(64533,1,2,1.00,1,'2014-11-19 18:07:31'),(64534,1,2,1.00,1,'2014-11-19 18:07:44');
/*!40000 ALTER TABLE `ace_demo_investment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ace_demo_message`
--

DROP TABLE IF EXISTS `ace_demo_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ace_demo_message` (
  `MessageId` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` int(11) NOT NULL,
  `Content` varchar(1024) NOT NULL,
  PRIMARY KEY (`MessageId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ace_demo_message`
--

LOCK TABLES `ace_demo_message` WRITE;
/*!40000 ALTER TABLE `ace_demo_message` DISABLE KEYS */;
/*!40000 ALTER TABLE `ace_demo_message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ace_demo_project`
--

DROP TABLE IF EXISTS `ace_demo_project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ace_demo_project` (
  `ProjectId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Amount` decimal(20,2) NOT NULL,
  `BorrowerId` int(11) NOT NULL,
  PRIMARY KEY (`ProjectId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ace_demo_project`
--

LOCK TABLES `ace_demo_project` WRITE;
/*!40000 ALTER TABLE `ace_demo_project` DISABLE KEYS */;
INSERT INTO `ace_demo_project` VALUES (1,'Test Project',99993327.00,1);
/*!40000 ALTER TABLE `ace_demo_project` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rbac_permission`
--

DROP TABLE IF EXISTS `rbac_permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rbac_permission` (
  `PermissionId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  PRIMARY KEY (`PermissionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rbac_permission`
--

LOCK TABLES `rbac_permission` WRITE;
/*!40000 ALTER TABLE `rbac_permission` DISABLE KEYS */;
INSERT INTO `rbac_permission` VALUES (1,'p1'),(2,'p2'),(3,'p3'),(4,'Good job'),(10,'p4'),(20,'p5'),(21,'Great'),(22,'NewB');
/*!40000 ALTER TABLE `rbac_permission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rbac_role`
--

DROP TABLE IF EXISTS `rbac_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rbac_role` (
  `RoleId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  PRIMARY KEY (`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rbac_role`
--

LOCK TABLES `rbac_role` WRITE;
/*!40000 ALTER TABLE `rbac_role` DISABLE KEYS */;
INSERT INTO `rbac_role` VALUES (1,'r1'),(2,'r2'),(3,'r3');
/*!40000 ALTER TABLE `rbac_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rbac_role_permission`
--

DROP TABLE IF EXISTS `rbac_role_permission`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rbac_role_permission` (
  `RoleId` int(11) NOT NULL,
  `PermissionId` int(11) NOT NULL,
  PRIMARY KEY (`RoleId`,`PermissionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rbac_role_permission`
--

LOCK TABLES `rbac_role_permission` WRITE;
/*!40000 ALTER TABLE `rbac_role_permission` DISABLE KEYS */;
INSERT INTO `rbac_role_permission` VALUES (1,1),(1,2),(1,3),(1,10),(1,20),(2,2),(3,1),(3,2),(3,3);
/*!40000 ALTER TABLE `rbac_role_permission` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rbac_subject`
--

DROP TABLE IF EXISTS `rbac_subject`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rbac_subject` (
  `SubjectId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  PRIMARY KEY (`SubjectId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rbac_subject`
--

LOCK TABLES `rbac_subject` WRITE;
/*!40000 ALTER TABLE `rbac_subject` DISABLE KEYS */;
INSERT INTO `rbac_subject` VALUES (1,'s1'),(2,'s2'),(3,'s3');
/*!40000 ALTER TABLE `rbac_subject` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rbac_subject_role`
--

DROP TABLE IF EXISTS `rbac_subject_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `rbac_subject_role` (
  `SubjectId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  PRIMARY KEY (`SubjectId`,`RoleId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rbac_subject_role`
--

LOCK TABLES `rbac_subject_role` WRITE;
/*!40000 ALTER TABLE `rbac_subject_role` DISABLE KEYS */;
INSERT INTO `rbac_subject_role` VALUES (1,1),(2,1),(2,2),(3,1),(3,2),(3,3);
/*!40000 ALTER TABLE `rbac_subject_role` ENABLE KEYS */;
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
INSERT INTO `sequence` VALUES (1,4155,'Test'),(100,3,'CQRS_Account'),(101,2,'CQRS_Project'),(102,64535,'CQRS_Investment');
/*!40000 ALTER TABLE `sequence` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tree`
--

DROP TABLE IF EXISTS `tree`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tree` (
  `Tree` int(11) NOT NULL,
  `Id` int(11) NOT NULL,
  `Parent` int(11) DEFAULT NULL,
  `Data` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`,`Tree`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tree`
--

LOCK TABLES `tree` WRITE;
/*!40000 ALTER TABLE `tree` DISABLE KEYS */;
INSERT INTO `tree` VALUES (8,0,NULL,NULL),(8,1,0,2),(8,2,0,3),(8,3,0,10),(8,4,3,20),(8,5,4,21),(8,6,5,22),(8,7,4,4),(8,8,3,1);
/*!40000 ALTER TABLE `tree` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user` (
  `Id` int(11) NOT NULL,
  `Username` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'123'),(2,'234');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
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

-- Dump completed on 2014-12-06 19:53:03
