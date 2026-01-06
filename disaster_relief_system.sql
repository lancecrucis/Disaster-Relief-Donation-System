-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: disaster_relief_system
-- ------------------------------------------------------
-- Server version	8.0.41

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
-- Table structure for table `fund_transfers`
--

DROP TABLE IF EXISTS `fund_transfers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fund_transfers` (
  `id` int NOT NULL AUTO_INCREMENT,
  `request_id` int NOT NULL,
  `amount` decimal(15,2) NOT NULL,
  `transferred_by` int NOT NULL,
  `transferred_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `notes` text,
  PRIMARY KEY (`id`),
  KEY `request_id` (`request_id`),
  KEY `transferred_by` (`transferred_by`),
  CONSTRAINT `fund_transfers_ibfk_1` FOREIGN KEY (`request_id`) REFERENCES `relief_requests` (`id`) ON DELETE CASCADE,
  CONSTRAINT `fund_transfers_ibfk_2` FOREIGN KEY (`transferred_by`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fund_transfers`
--

LOCK TABLES `fund_transfers` WRITE;
/*!40000 ALTER TABLE `fund_transfers` DISABLE KEYS */;
INSERT INTO `fund_transfers` VALUES (8,26,2000.00,1,'2026-01-06 19:17:51',NULL),(9,27,30000.00,1,'2026-01-06 19:36:16',NULL);
/*!40000 ALTER TABLE `fund_transfers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `institutional_donations`
--

DROP TABLE IF EXISTS `institutional_donations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `institutional_donations` (
  `id` int NOT NULL AUTO_INCREMENT,
  `organization_name` varchar(200) NOT NULL,
  `organization_type` enum('LGU','National Government Agency','School/University','Barangay','Private Sector/Corporation') NOT NULL,
  `representative_name` varchar(150) NOT NULL,
  `position_title` varchar(100) DEFAULT NULL,
  `email` varchar(150) NOT NULL,
  `contact_number` varchar(20) NOT NULL,
  `amount` decimal(15,2) NOT NULL,
  `transfer_method` enum('Bank Transfer','Government Check','Wire Transfer') NOT NULL,
  `status` enum('Processing','Completed') DEFAULT 'Processing',
  `donation_date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `processed_at` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `institutional_donations`
--

LOCK TABLES `institutional_donations` WRITE;
/*!40000 ALTER TABLE `institutional_donations` DISABLE KEYS */;
INSERT INTO `institutional_donations` VALUES (11,'Cavite State University','School/University','Danel Dave Barbuco','Teacher','cvsusilang@gmail.com','09687647484',5000.00,'Bank Transfer','Completed','2026-01-06 18:57:56',NULL),(12,'SM Foundation','Private Sector/Corporation','Jesus Cruz','CEO','smfoundation@gmail.com','0994353588',50000.00,'Wire Transfer','Completed','2026-01-06 19:03:19',NULL),(13,'University of the Philippines','School/University','Chancellor Anna Gomez','Chancellor','upph@gmail.com','09987278282',150000.00,'Bank Transfer','Completed','2026-01-06 19:23:05',NULL);
/*!40000 ALTER TABLE `institutional_donations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `relief_requests`
--

DROP TABLE IF EXISTS `relief_requests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `relief_requests` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `full_name` varchar(255) DEFAULT NULL,
  `contact_number` varchar(20) NOT NULL,
  `affected_location` varchar(200) NOT NULL,
  `priority` enum('Low','Medium','High','Critical') NOT NULL,
  `people_affected` int NOT NULL,
  `needed_by_date` date DEFAULT NULL,
  `situation_description` text NOT NULL,
  `additional_info` text,
  `status` enum('Pending','Approved','Rejected','Fulfilled') DEFAULT 'Pending',
  `requested_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `funds_needed` decimal(15,2) NOT NULL DEFAULT '0.00',
  `funds_received` decimal(15,2) NOT NULL DEFAULT '0.00',
  `approved_at` timestamp NULL DEFAULT NULL,
  `funds_transferred` decimal(15,2) NOT NULL DEFAULT '0.00',
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `relief_requests_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `relief_requests`
--

LOCK TABLES `relief_requests` WRITE;
/*!40000 ALTER TABLE `relief_requests` DISABLE KEYS */;
INSERT INTO `relief_requests` VALUES (25,4,'Danel Dave Barbuco','09945634425','Silang Bayan','Low',100,'2026-01-07','Food and water for the poor.','','Pending','2026-01-06 18:51:46',10000.00,0.00,NULL,0.00),(26,6,'Matthew Ferer','097662537','Savemore Market - Central Mall Dasmariñas','High',100,'2026-01-07','Due to a strong typhoon that recently struck the area, many families were forced to evacuate their homes because of severe flooding and damaged structures.','Basic necessities such as food, clean drinking water, clothing, hygiene kits, blankets, and basic medical supplies.','Approved','2026-01-06 19:10:04',7000.00,2000.00,'2026-01-06 19:16:01',0.00),(27,7,'Alex Binan','099289234','Sitio Riverside, Barangay Sta. Cruz, Marikina City','High',100,'2026-01-10','Following the aftermath of a typhoon, residents from low-lying areas were evacuated due to flooding and structural damage to homes.','','Approved','2026-01-06 19:35:27',100000.00,30000.00,'2026-01-06 19:35:51',0.00);
/*!40000 ALTER TABLE `relief_requests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_page`
--

DROP TABLE IF EXISTS `request_page`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `request_page` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `contact_person` varchar(100) NOT NULL,
  `contact_number` varchar(20) NOT NULL,
  `affected_location` varchar(100) NOT NULL,
  `priority_level` enum('Low','Medium','High','Critical') NOT NULL,
  `number_of_people_affected` int NOT NULL,
  `needed_by_date` date NOT NULL,
  `situation_description` text NOT NULL,
  `additional_information` text,
  `status` enum('Pending','Approved','Declined') DEFAULT 'Pending',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `reviewed_by` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `reviewed_by` (`reviewed_by`),
  CONSTRAINT `request_page_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  CONSTRAINT `request_page_ibfk_2` FOREIGN KEY (`reviewed_by`) REFERENCES `users` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_page`
--

LOCK TABLES `request_page` WRITE;
/*!40000 ALTER TABLE `request_page` DISABLE KEYS */;
/*!40000 ALTER TABLE `request_page` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `requests`
--

DROP TABLE IF EXISTS `requests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `requests` (
  `requester_id` int NOT NULL AUTO_INCREMENT,
  `requester_name` varchar(100) NOT NULL,
  `request_date` date NOT NULL,
  `location` varchar(255) NOT NULL,
  `request_note` text,
  `request_status` enum('Pending','Approved','Rejected') DEFAULT 'Pending',
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`requester_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `requests`
--

LOCK TABLES `requests` WRITE;
/*!40000 ALTER TABLE `requests` DISABLE KEYS */;
/*!40000 ALTER TABLE `requests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(255) NOT NULL,
  `role` enum('user','admin') NOT NULL,
  `created_at` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'lance','lance101','admin','2026-01-01 03:16:28'),(2,'kath','kath101','user','2026-01-01 03:18:20'),(3,'lanah','lanah101','user','2026-01-04 19:33:04'),(4,'Cavite State University','cvsu101','user','2026-01-06 18:44:45'),(5,'SM Foundation','sm101','user','2026-01-06 19:00:50'),(6,'Bantay Bata','bata101','user','2026-01-06 19:04:52'),(7,'philippine red cross','red101','user','2026-01-06 19:33:07');
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

-- Dump completed on 2026-01-07  3:52:01
