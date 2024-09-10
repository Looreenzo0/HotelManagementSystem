-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: localhost    Database: hoteldb
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20240907125203_initialCreate','8.0.8'),('20240907125549_addUserAndContactTable','8.0.8'),('20240910022405_updateStatusToString','8.0.8'),('20240910042700_updateRoomStatusToString','8.0.8'),('20240910044542_addAvailabilityCol','8.0.8'),('20240910074653_addNewEntity','8.0.8'),('20240910075059_add-WalkinCol','8.0.8');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contacts`
--

DROP TABLE IF EXISTS `contacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `contacts` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FirstName` longtext NOT NULL,
  `LastName` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `PhoneNumber` longtext NOT NULL,
  `CreatedBy` longtext,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedBy` longtext,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contacts`
--

LOCK TABLES `contacts` WRITE;
/*!40000 ALTER TABLE `contacts` DISABLE KEYS */;
/*!40000 ALTER TABLE `contacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hotelrooms`
--

DROP TABLE IF EXISTS `hotelrooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hotelrooms` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(75) DEFAULT NULL,
  `RoomType` varchar(45) DEFAULT NULL,
  `RoomRate` decimal(18,2) DEFAULT NULL,
  `Status` varchar(25) DEFAULT NULL,
  `CreatedBy` varchar(75) DEFAULT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedBy` varchar(75) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  `Availability` int DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hotelrooms`
--

LOCK TABLES `hotelrooms` WRITE;
/*!40000 ALTER TABLE `hotelrooms` DISABLE KEYS */;
INSERT INTO `hotelrooms` VALUES (1,'Executive Room','Executive Room',7000.00,'Available','John Resando','2024-09-03 13:52:17.556622',NULL,NULL,6),(2,'Deluxe Room','Deluxe Room',4500.00,'Available','John Resando','2024-09-03 13:52:17.556622',NULL,NULL,2),(3,'Suite Room','Suite Room',3500.00,'Available','John Resando','2024-09-03 13:52:17.556622',NULL,NULL,3);
/*!40000 ALTER TABLE `hotelrooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reservations`
--

DROP TABLE IF EXISTS `reservations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reservations` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RefNum` varchar(30) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Address` varchar(120) DEFAULT NULL,
  `ContactNumber` varchar(30) DEFAULT NULL,
  `Pax` int DEFAULT NULL,
  `CheckIn` datetime(6) DEFAULT NULL,
  `CheckOut` datetime(6) DEFAULT NULL,
  `RoomId` int DEFAULT NULL,
  `Status` varchar(25) DEFAULT NULL,
  `CreatedBy` varchar(45) DEFAULT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedBy` varchar(45) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  `IsWalkIn` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RefNum_UNIQUE` (`RefNum`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reservations`
--

LOCK TABLES `reservations` WRITE;
/*!40000 ALTER TABLE `reservations` DISABLE KEYS */;
/*!40000 ALTER TABLE `reservations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `ContactId` int NOT NULL,
  `Username` longtext NOT NULL,
  `PasswordHash` longtext NOT NULL,
  `Role` longtext NOT NULL,
  `CreatedBy` longtext,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedBy` longtext,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Users_ContactId` (`ContactId`),
  CONSTRAINT `FK_Users_Contacts_ContactId` FOREIGN KEY (`ContactId`) REFERENCES `contacts` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-09-10 23:37:00
