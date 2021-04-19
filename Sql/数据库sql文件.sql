-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: lydb
-- ------------------------------------------------------
-- Server version	8.0.19

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
-- Table structure for table `comments`
--

DROP TABLE IF EXISTS `comments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `comments` (
  `id` int NOT NULL AUTO_INCREMENT,
  `sub_id` int DEFAULT NULL,
  `sub2_id` int DEFAULT NULL,
  `reply_username` varchar(45) DEFAULT NULL,
  `time` varchar(45) DEFAULT NULL,
  `username` varchar(45) DEFAULT NULL,
  `txt` varchar(200) DEFAULT NULL,
  `img_path` varchar(80) DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `comments`
--

LOCK TABLES `comments` WRITE;
/*!40000 ALTER TABLE `comments` DISABLE KEYS */;
INSERT INTO `comments` VALUES (1,2,1,'*','2020/5/8 15:36:16','王伟华','我第一个发言哈哈','/Upload/1323339677502013571938.jpeg'),(2,2,1,'-','2020/5/8 15:36:25','王伟华','我第一个我回复我自己','/Upload/1323339677502013571938.jpeg'),(3,12,1,'*','2020/5/8 15:45:10','wang','其实西安不错','/Upload/1323339727895287905898.jpeg'),(4,12,1,'-','2020/5/8 15:45:24','伟哥','西安有什么玩的呢？','/Upload/1323339717712793081233.jpeg'),(5,12,1,'伟哥','2020/5/8 15:45:46','wang','西安有兵马俑还有华山','/Upload/1323339727895287905898.jpeg'),(6,12,1,'wang','2020/5/8 15:46:19','伟哥','你去过吗？感觉怎么样？','/Upload/1323339717712793081233.jpeg'),(7,12,1,'伟哥','2020/5/8 15:46:35','wang','去过感觉很好','/Upload/1323339727895287905898.jpeg'),(8,12,1,'伟哥','2020/5/8 15:47:11','王伟华','西安还有博物馆，那里真的棒','/Upload/1323339677502013571938.jpeg'),(9,12,2,'*','2020/5/8 15:47:31','王伟华','湖北也有很多景区可以玩呀，','/Upload/1323339677502013571938.jpeg'),(10,12,2,'-','2020/5/8 15:47:43','王伟华','黄鹤楼不错呀','/Upload/1323339677502013571938.jpeg'),(11,12,3,'*','2020/5/8 15:48:34','王哥','可以去沿海的城市，可以看大海','/Upload/1323339710265028235759.jpeg'),(12,12,4,'*','2020/5/8 15:49:11','王哥','湖北的武当山也很好','/Upload/1323339710265028235759.jpeg'),(13,12,5,'*','2020/5/8 15:49:32','王哥','你可以上网去查查一些资料','/Upload/1323339710265028235759.jpeg');
/*!40000 ALTER TABLE `comments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `title`
--

DROP TABLE IF EXISTS `title`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `title` (
  `id` int NOT NULL AUTO_INCREMENT,
  `text` varchar(200) DEFAULT NULL,
  `head_txt` varchar(200) DEFAULT NULL,
  `time` varchar(45) DEFAULT NULL,
  `userId` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `title`
--

LOCK TABLES `title` WRITE;
/*!40000 ALTER TABLE `title` DISABLE KEYS */;
INSERT INTO `title` VALUES (1,'随便说说吧哈哈','这是第一个帖子','2020/5/8 15:34:31',1),(2,'随便说说吧哈哈','这是第二个帖子','2020/5/8 15:34:45',1),(3,'我是一个新人哈哈','我是一个新人哈哈','2020/5/8 15:38:40',2),(4,'今天天气真的很好呀','今天天气真的很好呀','2020/5/8 15:38:56',2),(5,'今天天气真的很好呀','今天天气真的很好呀','2020/5/8 15:39:53',3),(6,'其实我感觉中国的很多地方旅行不错','你最想旅行的地方是哪里','2020/5/8 15:41:56',4),(7,'1年有多少秒你知道吗','1年有多少秒你知道吗','2020/5/8 15:42:19',4),(8,'这是一个测试贴','这是一个测试贴','2020/5/8 15:43:42',3),(9,'这是一个测试贴','这是一个测试贴','2020/5/8 15:43:51',3),(10,'这是一个测试贴','这是一个测试贴','2020/5/8 15:43:56',3),(11,'什么地方好玩呀','我好想出去玩有什么可以推荐的地方','2020/5/8 15:44:37',3),(12,'什么地方好玩呀','我好想出去玩有什么可以推荐的地方','2020/5/8 15:44:47',3);
/*!40000 ALTER TABLE `title` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `brith` varchar(45) NOT NULL,
  `sex` varchar(45) NOT NULL,
  `img_path` varchar(80) DEFAULT '',
  PRIMARY KEY (`id`),
  UNIQUE KEY `username_UNIQUE` (`username`),
  UNIQUE KEY `name_UNIQUE` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'111111','王伟华','111111','2020-05-13','男','/Upload/1323339677502013571938.jpeg'),(2,'222222','王哥','222222','2020-05-21','男','/Upload/1323339710265028235759.jpeg'),(3,'333333','伟哥','333333','2020-05-12','男','/Upload/1323339717712793081233.jpeg'),(4,'444444','wang','444444','2013-05-06','女','/Upload/1323339727895287905898.jpeg'),(5,'12345685','wang2','123456781','2021-03-25','男',''),(6,'33366678','wang11','33366678','2021-03-05','男','');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-04-19 19:58:07
