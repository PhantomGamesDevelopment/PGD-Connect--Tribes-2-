-- phpMyAdmin SQL Dump
-- version 3.4.11.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Mar 11, 2014 at 09:06 AM
-- Server version: 5.5.36
-- PHP Version: 5.2.17

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `phantom7_PGDConnect`
--

-- --------------------------------------------------------

--
-- Table structure for table `Data`
--

DROP TABLE IF EXISTS `Data`;
CREATE TABLE IF NOT EXISTS `Data` (
  `GUID` int(11) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `password` int(11) NOT NULL,
  `Authenticated` int(11) NOT NULL,
  `IP` varchar(100) NOT NULL COMMENT 'Tracks spam bots by preventing dual-registers over one IP.'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `Data`
--

INSERT INTO `Data` (`GUID`, `Email`, `password`, `Authenticated`, `IP`) VALUES
(2000343, 'phantom139atphantomdev.net', 37928584, 0, '98.212.60.170'),
(2687806, 'JetBujasonatyahoo.com', 13854244, 0, '71.65.246.82'),
(2205246, 'castiger1234athotmail.com', 47255949, 0, '68.187.245.159'),
(2703588, 'world.builderathotmail.com', 82596155, 0, '173.67.249.202'),
(2604147, 'snowFire789athotmail.com', 53121595, 0, '72.224.193.95'),
(2714648, 'awesomevgameguyathotmail.com', 49462279, 0, '97.113.5.4'),
(2130825, 'justanotherkrakenextendedharahatgmail.com', 63224493, 0, '173.25.210.94'),
(3040953, 'jewlsmurrayatyahoo.com', 74539105, 0, '174.125.96.130'),
(2860882, 'spora2034atyahoo.com', 93396769, 0, '76.186.136.103'),
(2744981, 'leppala98athotmail.com', 43374221, 0, '69.14.106.128'),
(2710770, 'Jewlsmurrayatcenturytel.net', 12332130, 0, '174.125.97.122'),
(3152732, 'dream_miroratyahoo.com', 37802841, 0, '79.119.229.108'),
(2886178, 'fluffyfaataim.com', 87486033, 0, '72.72.111.199'),
(3226760, 'gogreen32ataim.com', 76024290, 0, '68.42.153.109'),
(2771857, 'xlthuathopecatgmail.com', 44163581, 0, '84.93.191.209'),
(2003098, 'darkdragondxathotmail.com', 31500492, 0, '66.233.149.94'),
(2118847, 'phantom139atphantomdev.net', 93726502, 0, '98.212.60.170'),
(2567733, 'phantom139atphantomdev.net', 12424674, 0, '98.212.60.170'),
(2466121, 'jakermitchll98atgmail.com', 24259589, 0, '74.196.233.239'),
(3232740, 'roobeallatyahoo.com', 21556570, 0, '67.235.171.202'),
(2763554, 'tlosoyaataustin.rr.com', 62137490, 0, '72.183.113.228'),
(2073419, 'bluemechaatearthlink.net', 35843596, 0, '173.145.229.157'),
(2212728, 'LittleTasatmsn.com', 69193603, 0, '70.177.105.102'),
(2752642, 'deibrossatyahoo.com', 87383449, 0, '71.236.249.166'),
(5000000, 'testatlolb.test', 28690033, 0, '128.194.28.212'),
(3336626, 'ittech101athotmail.com', 71128355, 0, '65.9.138.212'),
(3392146, 'jamesschlittatgmail.com', 43758785, 0, '98.213.144.112'),
(2217449, 'fragmaster4atgmail.com', 66480649, 0, '8.2.214.52'),
(3475269, 'peanutman2atlive.com', 49815410, 0, '174.58.81.220'),
(2320280, 'c', 64830434, 0, '67.81.123.30'),
(3569784, 'Wings1233atpublic.phantomdev.net', 92402887, 0, '98.184.167.13'),
(2513554, 'celerityhedgehogatyahoo.com', 35006620, 0, '70.20.24.57'),
(3584821, 'roach45636atgmail.com', 19809116, 0, '75.17.244.182'),
(2822979, 'Jeremi2011atgmail.com', 71512971, 0, '98.17.35.247'),
(3119044, 'hmsabre2003atyahoo.com--rIMe8q3xMOX1Wsq0--', 95843207, 0, '209.243.26.132'),
(2488783, 'jesusaddict93atyahoo.com', 24672351, 0, '209.243.26.132'),
(2166941, 'alexleroy509atgmail.com', 23599748, 0, '209.243.26.132'),
(2590996, 'Justdoofusatgmail.com', 28054263, 0, '209.243.26.132'),
(3632676, 'Jewlsmurrayatyahoo.com', 15952235, 0, '209.243.26.132'),
(2007255, 'deathbornathotmail.co.uk', 48417600, 0, '209.243.26.132'),
(3060368, 'bradybear1atgmail.com', 12404045, 0, '209.243.26.132'),
(2610528, 'underdog1a2b3cathotmail.com', 82816558, 0, '209.243.26.132'),
(3659303, 'aacevedo2580atyahoo.com', 17969722, 0, '209.243.26.132'),
(3498885, 'gow1097atdorilla.com', 57067347, 0, '74.110.220.95'),
(2884678, 'henry_tuoriathotmail.com', 88784655, 0, '209.243.26.132'),
(2786170, 'jddogluveratgmail.com', 10308902, 0, '209.243.26.132'),
(3322673, 'zombieapocolapsatgmail.com', 90753449, 0, '209.243.26.132'),
(3332624, 'zombieapocolapsatgmail.com', 79326773, 0, '209.243.26.132');

-- --------------------------------------------------------

--
-- Table structure for table `IPBan`
--

DROP TABLE IF EXISTS `IPBan`;
CREATE TABLE IF NOT EXISTS `IPBan` (
  `ip` varchar(100) NOT NULL COMMENT 'This table is only used to block out possible spam bots from spamming this service, this does not necessarily "Ban" them from the service, just from registering.'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `IPBan`
--

INSERT INTO `IPBan` (`ip`) VALUES
('69.14.106.128'),
('84.93.191.209'),
('72.72.111.199'),
('128.194.28.212'),
('98.213.144.112'),
('67.81.123.30'),
('98.17.35.247'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132'),
('209.243.26.132');

-- --------------------------------------------------------

--
-- Table structure for table `PermBans`
--

DROP TABLE IF EXISTS `PermBans`;
CREATE TABLE IF NOT EXISTS `PermBans` (
  `guid` int(7) NOT NULL COMMENT 'this is in case we play on multiple IP''s',
  `reason` text NOT NULL COMMENT 'Why they are banned',
  `Expre` int(8) NOT NULL COMMENT 'expire date of ban (YYYYMMDD)',
  `ip` text NOT NULL COMMENT 'ip to track multi-account users'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `PermBans`
--

INSERT INTO `PermBans` (`guid`, `reason`, `Expre`, `ip`) VALUES
(1234567, 'this is the testing of the ban system.', 20091225, '127.0.0.0');

-- --------------------------------------------------------

--
-- Table structure for table `PGDAdminList`
--

DROP TABLE IF EXISTS `PGDAdminList`;
CREATE TABLE IF NOT EXISTS `PGDAdminList` (
  `guid` int(11) NOT NULL COMMENT 'auto dev(SA) these guids'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `PGDAdminList`
--

INSERT INTO `PGDAdminList` (`guid`) VALUES
(2000343);

-- --------------------------------------------------------

--
-- Table structure for table `TWM2Admin`
--

DROP TABLE IF EXISTS `TWM2Admin`;
CREATE TABLE IF NOT EXISTS `TWM2Admin` (
  `RankCap` int(11) NOT NULL COMMENT 'EXP Rank Cap for the day',
  `DevList` text NOT NULL COMMENT 'Developer list (in case little noobie script editors delete it from the file.',
  `MaxRank` int(11) NOT NULL COMMENT 'Rank # goes here',
  `MaxOfficer` int(11) NOT NULL,
  `EXPMultiplier` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `TWM2Admin`
--

INSERT INTO `TWM2Admin` (`RankCap`, `DevList`, `MaxRank`, `MaxOfficer`, `EXPMultiplier`) VALUES
(3000000, '2000343:Dev TAB 2118847:Dev TAB 3070956:Dev TAB 2130825:CoDev TAB 2003098:CoDev', 1000, 11, 6);

-- --------------------------------------------------------

--
-- Table structure for table `TWM2Core`
--

DROP TABLE IF EXISTS `TWM2Core`;
CREATE TABLE IF NOT EXISTS `TWM2Core` (
  `Username` text NOT NULL COMMENT 'Username for servers',
  `Password` text NOT NULL COMMENT 'and their respective password',
  `DISP` text NOT NULL COMMENT '$TWM2::PGDCredentials = "username\\tpassword";'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `TWM2Core`
--

INSERT INTO `TWM2Core` (`Username`, `Password`, `DISP`) VALUES
('phantom139', 'TWM2Ub3rM0dd3r', ''),
('signal360', 'badgers1', ''),
('jetbu', 'qwertyuiop9894', ''),
('castiger', 'Arielle', ''),
('SicknTwisted', 'Condemned', ''),
('Exerpt', 'Thunder', ''),
('wigley5', 'jg1105', ''),
('darknessoflight', 'JustAnotherKrakenExtendedHarah', ''),
('Minehem', 'Chevy503', ''),
('FeatherDev', 'GLaD0S', '');

-- --------------------------------------------------------

--
-- Table structure for table `TWM2DC`
--

DROP TABLE IF EXISTS `TWM2DC`;
CREATE TABLE IF NOT EXISTS `TWM2DC` (
  `ID` int(11) NOT NULL COMMENT 'Identified Challenge Type: 1-Daily, 2-Weekly, 3-Monthly',
  `Name` text NOT NULL,
  `Description` text NOT NULL,
  `Condition` text NOT NULL,
  `Reward` int(11) NOT NULL,
  `Expire` int(11) NOT NULL,
  `Active` int(11) NOT NULL COMMENT 'Defines when the challenge becomes active.'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `TWM2DC`
--

INSERT INTO `TWM2DC` (`ID`, `Name`, `Description`, `Condition`, `Reward`, `Expire`, `Active`) VALUES
(1, 'Zombiecide', 'Kill 100 Regular Zombies Today', 'Z 100 1 A', 50000, 20120207, 20120207),
(1, 'Boom Headshot', 'Kill 5 enemy players with headshots', 'HS 5 E', 50000, 20120207, 20120207),
(1, 'Big n Bright', 'Call in a tactical nuke strike today', 'KS 14 1', 15000, 20110316, 20110316),
(2, 'Have a Hand', 'Promote to the first officer rank', 'Prestige 1', 25000, 20101107, 20101101),
(3, 'Millionaire', 'Kill 1000000 zombies over the course of this month.', 'Z 1000000 A A', 75000, 20101101, 20101001),
(1, 'Spectre', 'Kill a player with the shadow rifle today', 'E 1 ShadowRifleImage', 5000, 20101220, 20101220),
(1, 'Flash Of Death', 'Call in a Z-bomb today', 'KS 15 1', 10000, 20101023, 20101023),
(3, 'Supreme Has It Best', 'Reach The fifth Officer level', 'Prestige 5', 75000, 20110201, 20110101),
(1, 'Rapiers, Fast n Low', 'Kill 25 Air Rapiers', 'Z 25 5 A', 50000, 20120207, 20120207),
(1, '[MBC 2] Missile -> Face', 'Kill a player with the RPG-7', 'E 1 RPGImage', 650000, 20120507, 20120505),
(1, 'Bring Them Down', 'Kill 5 Enemies with the Stinger', 'E 5 StingerImage', 5000, 20110316, 20110316),
(2, 'Immortality', 'Earn an ''Immortal'' spree (25 in a row)', 'SKS 25 1', 25000, 20110918, 20110915),
(1, 'Serial Killer', 'Earn a Serial Killer Streak (10 in a row) today.', 'SKS 10 1', 5000, 20110315, 20110315),
(2, 'A Thousand Must Fall', 'Kill 1000 Zombies This Week', 'Z 1000 A A', 50000, 20111127, 20111125),
(1, 'Not The Face!', 'Kill 50 zombies with headshots', 'HS 50 Z ', 50000, 20110915, 20110915),
(3, 'Ten-Thousandare', 'Kill ten thousand zombies this month', 'Z 10000 A A', 75000, 20110331, 20110301),
(1, 'I Dream Of Green', 'Kill 25 Zombies with an Acid Cannon', 'Z 25 A AcidCannonImage', 15000, 20101220, 20101220),
(1, 'Impressive...', 'Earn a Killtrocity streak (7 successive kills) today, for a nice big bonus.', 'SK 7 1', 35000, 20101221, 20101221),
(1, 'I Came... I Saw... I Sniped.', 'Headshot one player today for 35K, compliments of Phantom139. Have a Nice Day!', 'HS 1 E', 35000, 20120207, 20120207),
(2, 'Holiday Tank Buster', 'Kill 150 Enemies With The Stinger This Week.', 'E 150 StingerImage', 75000, 20101226, 20101220),
(1, 'Neeen-Ja', 'Assassinate 10 Enemy Players Today', 'Back E 10', 35000, 20120207, 20120207),
(1, 'Du-Trople', 'Earn 2 Overkill Streaks Today', 'SK 4 2', 10000, 20110317, 20110317),
(1, 'It''s a Blast!', 'Call in a Satellite Strike Today', 'KS 6 1', 35000, 20120207, 20120207),
(1, '[MBC 1] Assasin Lord', 'Assassinate 1 zombie', 'Back Z 1', 500000, 20120507, 20120505),
(1, 'I Hate Spec-Ops!', 'Kill 3 Wraith Spec. Ops Zombies Today.', 'Z 3 15 A', 15000, 20110318, 20110318),
(1, 'Puny... But Deadly', 'Kill 25 players with a colt pistol', 'E 25 PistolImage', 15000, 20110317, 20110317),
(1, 'Collide This!', 'Kill 50 players with the PRTCL-995 MCC today', 'E 50 MiniColliderCannonImage', 30000, 20110318, 20110318),
(1, 'Hidden Death...', 'Call in a stealth bomber.', 'KS 8 1', 35000, 20110315, 20110315),
(1, 'Mastermind', 'Kill 250 Players Today', 'E 250 A', 50000, 20110915, 20110915),
(1, 'Kentucky Fried Zombie', 'Kill 50 Zombies With The Flamethrower Today.', 'Z 50 A flamerImage', 25000, 20110318, 20110318),
(1, 'Merry Christmas', 'You shouldn''t be playing TWM2 today :P, go celebrate, but eh, kill one guy for a bonus.', 'E 1 A', 50000, 20101225, 20101225),
(1, 'Holiday ''Fire'' works', 'In Phantom139''s Server, call in the new ''Napalm Strike'' kill streak.', 'KS 9 1', 25000, 20101224, 20101224),
(2, 'Spring Into Action', 'Kill 1000 Zombies This Week', 'Z 1000 A A', 75000, 20110320, 20110314),
(1, 'Lords-A-Fallin', 'Kill 100 Zombie Lords Today', 'Z 100 3 A', 50000, 20120207, 20120207),
(2, 'Nightmare This!', 'Defeat Lord Yvex 3 Times this week', 'Boss Yvex 3', 60000, 20120219, 20120209),
(2, 'What Platform???', 'Defeat Colonel Windshear 3 Times this week', 'Boss CnlWindshear 3', 60000, 20120219, 20120209),
(2, 'Broken Thunder', 'Defeat The Ghost of Lightning 3 Times this week', 'Boss GhostOfLightning 3', 60000, 20120219, 20120209),
(2, 'Flaming Zombies don''t Mix', 'Defeat General Vegenor 3 Times this week', 'Boss Vegenor 3', 60000, 20120219, 20120209),
(2, 'I am Dissapoint Son', 'Defeat Lord Rog 5 Times this week', 'Boss LordRog 5', 65000, 20120219, 20120209),
(2, 'Sorry! No Returns!', 'Defeat Major Insignia 5 Times this week', 'Boss Insignia 5', 65000, 20120219, 20120209),
(2, 'Dark Archmage', 'Defeat Lord Vardison (First Form) 3 Times this week', 'Boss Vardison1 3', 70000, 20120219, 20120209),
(2, 'Flying Demons must Die', 'Defeat Lord Vardison (Second Form) 4 Times this week', 'Boss Vardison2 4', 70000, 20120219, 20120209),
(2, 'The Devil Within', 'Defeat Lord Vardison (Final Form) 5 Times this week', 'Boss Vardison3 5', 70000, 20120219, 20120209),
(2, 'I Hate Tanks', 'Defeat Lordranius Trevor 7 Times this week', 'Boss Trebor 7', 70000, 20120219, 20120209),
(2, 'Oh Yeah, That Guy', 'Defeat Commander Stormrider 7 Times this week', 'Boss Stormrider 7', 70000, 20120219, 20120209),
(2, 'No Volcanoes Today!!!', 'Defeat The Ghost of Fire 7 Times this week', 'Boss GhostOfFire 7', 70000, 20120219, 20120209),
(2, '[MBC 3] Awakening Dawn', 'Defeat The Shade Lord 7 Times this week', 'Boss ShadeLord 7', 1000000, 20120507, 20120505);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
