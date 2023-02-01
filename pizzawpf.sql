-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Feb 01, 2023 at 10:02 AM
-- Server version: 5.7.36
-- PHP Version: 8.0.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `pizzawpf`
--

-- --------------------------------------------------------

--
-- Table structure for table `ingredienten`
--

DROP TABLE IF EXISTS `ingredienten`;
CREATE TABLE IF NOT EXISTS `ingredienten` (
  `ingredientId` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(50) NOT NULL,
  PRIMARY KEY (`ingredientId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `ingredienten`
--

INSERT INTO `ingredienten` (`ingredientId`, `name`) VALUES
(1, 'salami'),
(2, 'Tomato'),
(3, 'Cheese'),
(4, 'mushrooms'),
(5, 'onions'),
(6, 'olives'),
(7, 'beans'),
(8, 'peppers'),
(9, 'spinach'),
(10, 'carrot'),
(11, 'almonds'),
(12, 'peanuts');

-- --------------------------------------------------------

--
-- Table structure for table `pizza`
--

DROP TABLE IF EXISTS `pizza`;
CREATE TABLE IF NOT EXISTS `pizza` (
  `PizzaID` int(11) NOT NULL AUTO_INCREMENT,
  `PizzaName` varchar(50) NOT NULL,
  `Price` decimal(11,0) NOT NULL,
  `Photo` text NOT NULL,
  `Description` text,
  PRIMARY KEY (`PizzaID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pizza`
--

INSERT INTO `pizza` (`PizzaID`, `PizzaName`, `Price`, `Photo`, `Description`) VALUES
(1, 'Neapolitan Pizza', '6', 'http://www.sweetphi.com/wp-content/uploads/2014/03/Kens-Pizza-Making-25.jpg', 'Description'),
(2, 'Sicilian Pizza', '5', 'https://bloximages.newyork1.vip.townnews.com/postandcourier.com/content/tncms/assets/v3/editorial/3/25/32569e5e-d878-11e6-881b-23c6e7e89e3d/5876f9ab0aaf0.image.jpg?resize=1200%2C800', 'Description'),
(3, 'Greek Pizza', '8', 'https://nikosroastbeefandseafood.com/wp-content/uploads/2020/05/greek-pizza.jpg', 'Description'),
(4, 'Detroit Pizza', '5', 'http://toolsandtoys.net/wp-content/uploads/2017/03/IMG_3662-1920x1440.jpg', 'Description'),
(5, 'Chicago Pizza', '6', 'https://pizzaneed.com/wp-content/uploads/2019/06/chicago-style-pizza.jpg', 'Description'),
(6, 'StLouis Pizza', '6', 'http://1.bp.blogspot.com/-7cX4hvbbis4/Vp5R40hcKqI/AAAAAAABOYQ/gsHbGyndCF0/s1600/IMG_6347.JPG', 'Description'),
(7, 'California Pizza', '8', 'https://pizzaneed.com/wp-content/uploads/2019/09/california-style-pizza.jpg', 'Description'),
(8, 'Types Of Pizza Crust', '5', '0', 'Description'),
(9, 'New York-Style Pizza', '8', 'https://www.biggerbolderbaking.com/wp-content/uploads/2021/02/New-York-Style-Pizza-Thumbnail1-scaled.jpg', 'Description');

-- --------------------------------------------------------

--
-- Table structure for table `pizzaingredienten`
--

DROP TABLE IF EXISTS `pizzaingredienten`;
CREATE TABLE IF NOT EXISTS `pizzaingredienten` (
  `PizzaID` int(11) NOT NULL,
  `ingredientId` int(11) NOT NULL,
  UNIQUE KEY `PizzaID_2` (`PizzaID`),
  UNIQUE KEY `ingredientId_2` (`ingredientId`),
  KEY `ingredientId` (`ingredientId`),
  KEY `PizzaID` (`PizzaID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `pizzaingredienten`
--
ALTER TABLE `pizzaingredienten`
  ADD CONSTRAINT `pizzaingredienten_ibfk_1` FOREIGN KEY (`PizzaID`) REFERENCES `pizza` (`PizzaID`),
  ADD CONSTRAINT `pizzaingredienten_ibfk_2` FOREIGN KEY (`ingredientId`) REFERENCES `ingredienten` (`ingredientId`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
