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
-- Table structure for table `cqrs_demo_account`
--

DROP TABLE IF EXISTS `cqrs_demo_account`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cqrs_demo_account` (
  `AccountId` int(11) NOT NULL,
  `Amount` decimal(20,2) NOT NULL,
  PRIMARY KEY (`AccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cqrs_demo_account`
--

LOCK TABLES `cqrs_demo_account` WRITE;
/*!40000 ALTER TABLE `cqrs_demo_account` DISABLE KEYS */;
INSERT INTO `cqrs_demo_account` VALUES (0,0.00),(1,1100.00),(2,8900.00);
/*!40000 ALTER TABLE `cqrs_demo_account` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cqrs_demo_account_activity`
--

DROP TABLE IF EXISTS `cqrs_demo_account_activity`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cqrs_demo_account_activity` (
  `ActivityId` int(11) NOT NULL AUTO_INCREMENT,
  `FromAccountId` int(11) DEFAULT NULL,
  `ToAccountId` int(11) DEFAULT NULL,
  `Amount` decimal(20,2) NOT NULL,
  PRIMARY KEY (`ActivityId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cqrs_demo_account_activity`
--

LOCK TABLES `cqrs_demo_account_activity` WRITE;
/*!40000 ALTER TABLE `cqrs_demo_account_activity` DISABLE KEYS */;
INSERT INTO `cqrs_demo_account_activity` VALUES (1,2,1,100.00),(2,2,1,100.00),(3,2,1,100.00),(4,2,1,100.00),(5,2,1,100.00),(6,2,1,100.00),(7,2,1,100.00),(8,2,1,100.00),(9,2,1,100.00),(10,2,1,100.00);
/*!40000 ALTER TABLE `cqrs_demo_account_activity` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cqrs_demo_investment`
--

DROP TABLE IF EXISTS `cqrs_demo_investment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cqrs_demo_investment` (
  `InvestmentId` int(11) NOT NULL,
  `ProjectId` int(11) NOT NULL,
  `AccountId` int(11) NOT NULL,
  `Amount` decimal(20,2) NOT NULL,
  `Status` int(11) NOT NULL,
  PRIMARY KEY (`InvestmentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cqrs_demo_investment`
--

LOCK TABLES `cqrs_demo_investment` WRITE;
/*!40000 ALTER TABLE `cqrs_demo_investment` DISABLE KEYS */;
INSERT INTO `cqrs_demo_investment` VALUES (65,1,2,100.00,1),(67,1,2,100.00,1),(69,1,2,100.00,1),(71,1,2,100.00,1),(73,1,2,100.00,1),(75,1,2,100.00,1),(76,1,2,100.00,1),(77,1,2,100.00,1),(78,1,2,100.00,1),(79,1,2,100.00,1),(80,1,2,100.00,0),(81,1,2,100.00,0),(82,1,2,100.00,0),(83,1,2,100.00,0),(84,1,2,100.00,1);
/*!40000 ALTER TABLE `cqrs_demo_investment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cqrs_demo_message`
--

DROP TABLE IF EXISTS `cqrs_demo_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cqrs_demo_message` (
  `MessageId` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` int(11) NOT NULL,
  `Content` varchar(1024) NOT NULL,
  PRIMARY KEY (`MessageId`)
) ENGINE=InnoDB AUTO_INCREMENT=77 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cqrs_demo_message`
--

LOCK TABLES `cqrs_demo_message` WRITE;
/*!40000 ALTER TABLE `cqrs_demo_message` DISABLE KEYS */;
INSERT INTO `cqrs_demo_message` VALUES (36,2,'投资成功，投资金额100.00元。'),(37,2,'账户变动，变动金额-100.00元。'),(38,1,'账户变动，变动金额100.00元。'),(39,2,'投资成功，投资金额100.00元。'),(40,2,'账户变动，变动金额-100.00元。'),(41,1,'账户变动，变动金额100.00元。'),(42,1,'账户变动，变动金额100.00元。'),(43,2,'账户变动，变动金额-100.00元。'),(44,2,'投资成功，投资金额100.00元。'),(45,2,'账户变动，变动金额-100.00元。'),(46,1,'账户变动，变动金额100.00元。'),(47,2,'投资成功，投资金额100.00元。'),(48,2,'账户变动，变动金额-100.00元。'),(49,1,'账户变动，变动金额100.00元。'),(50,2,'投资成功，投资金额100.00元。'),(51,2,'账户变动，变动金额-100.00元。'),(52,1,'账户变动，变动金额100.00元。'),(53,2,'投资成功，投资金额100.00元。'),(54,1,'账户变动，变动金额100.00元。'),(55,2,'账户变动，变动金额-100.00元。'),(56,2,'投资成功，投资金额100.00元。'),(57,2,'账户变动，变动金额-100.00元。'),(58,1,'账户变动，变动金额100.00元。'),(59,2,'投资成功，投资金额100.00元。'),(60,2,'账户变动，变动金额-100.00元。'),(61,1,'账户变动，变动金额100.00元。'),(62,2,'投资成功，投资金额100.00元。'),(63,2,'账户变动，变动金额-100.00元。'),(64,1,'账户变动，变动金额100.00元。'),(65,2,'投资成功，投资金额100.00元。'),(66,2,'账户变动，变动金额-100.00元。'),(67,1,'账户变动，变动金额100.00元。'),(68,2,'投资成功，投资金额100.00元。'),(69,2,'账户变动，变动金额-100.00元。'),(70,2,'投资成功，投资金额100.00元。'),(71,2,'投资成功，投资金额100.00元。'),(72,2,'账户变动，变动金额-100.00元。'),(73,2,'投资成功，投资金额100.00元。'),(74,2,'投资成功，投资金额100.00元。'),(75,2,'账户变动，变动金额-100.00元。'),(76,1,'账户变动，变动金额100.00元。');
/*!40000 ALTER TABLE `cqrs_demo_message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cqrs_demo_project`
--

DROP TABLE IF EXISTS `cqrs_demo_project`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cqrs_demo_project` (
  `ProjectId` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Amount` decimal(20,2) NOT NULL,
  `BorrowerId` int(11) NOT NULL,
  PRIMARY KEY (`ProjectId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cqrs_demo_project`
--

LOCK TABLES `cqrs_demo_project` WRITE;
/*!40000 ALTER TABLE `cqrs_demo_project` DISABLE KEYS */;
INSERT INTO `cqrs_demo_project` VALUES (1,'Test Project',8900.00,1);
/*!40000 ALTER TABLE `cqrs_demo_project` ENABLE KEYS */;
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
INSERT INTO `rbac_permission` VALUES (1,'p1'),(2,'p2'),(3,'p3'),(10,'p4'),(20,'p5');
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
INSERT INTO `rbac_role_permission` VALUES (1,1),(1,2),(1,20),(2,2),(3,1),(3,2),(3,3);
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
INSERT INTO `sequence` VALUES (1,4155,'Test'),(100,3,'CQRS_Account'),(101,2,'CQRS_Project'),(102,85,'CQRS_Investment');
/*!40000 ALTER TABLE `sequence` ENABLE KEYS */;
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

-- Dump completed on 2014-07-01 16:49:55
