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
-- Database: `phantom7_Quiz`
--

-- --------------------------------------------------------

--
-- Table structure for table `Admin`
--

DROP TABLE IF EXISTS `Admin`;
CREATE TABLE IF NOT EXISTS `Admin` (
  `ScriptEnabled` int(11) NOT NULL,
  `VersionString` text NOT NULL,
  `AdminOpen` int(11) NOT NULL,
  `SAOpen` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `Admin`
--

INSERT INTO `Admin` (`ScriptEnabled`, `VersionString`, `AdminOpen`, `SAOpen`) VALUES
(1, 'Version Alpha 1A', 1, 0);

-- --------------------------------------------------------

--
-- Table structure for table `QDB_Admin`
--

DROP TABLE IF EXISTS `QDB_Admin`;
CREATE TABLE IF NOT EXISTS `QDB_Admin` (
  `QID` text NOT NULL,
  `Question` text NOT NULL,
  `A` text NOT NULL,
  `B` text NOT NULL,
  `C` text NOT NULL,
  `D` text NOT NULL,
  `ANS` text NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `QDB_Admin`
--

INSERT INTO `QDB_Admin` (`QID`, `Question`, `A`, `B`, `C`, `D`, `ANS`) VALUES
('00001', 'Which of these is PGD Supported', 'C&C Tiberium Uprising', 'Dark Mod', 'TWM2', 'All Of The Above', 'd'),
('00002', 'Identify the following: PGD Website Designer', 'Phantom139', 'DoL', 'Signal30', 'Dark Dragon DX', 'b'),
('00003', 'Identify the Following: PGD Creator', 'Phantom139', 'DoL', 'Arctic_Winter', 'Signal360', 'a'),
('00004', 'Which is the most efficient way to deal with a piece spammer?', 'kick them', 'restrict their piece ability', 'remove their pieces', 'warn them', 'c'),
('00005', 'Which of the following symbolizes the general rule on kicking in PGD Servers', 'based on personal choice', 'based on if the host is there or not', 'based on the public decision', 'B and C apply', 'b'),
('00006', 'Solve the following:\r\n\r\n X² + 4x + 10', '(x - 4)(x + 6)', '-2 +/- i(6)½', '2 +/- i(4)½', '(x + 4)(x - 6)', 'b'),
('00007', 'Reduce the square root of 776', '2 * sqrt(194)', '4 * sqrt(194)', '194 * sqrt(2)', 'cannot be reduced', 'a'),
('00008', 'Reduce the square root of 637', '7 * sqrt(3)', '7 * sqrt(13)', '14 * sqrt(7)', 'cannot be reduced', 'b'),
('00009', 'Reduce the square root of 22', '2 * sqrt(6)', '4 * sqrt(6)', '11', 'cannot be reduced', 'd'),
('00010', 'Which of the following is NOT a TWM2 Boss', 'Lord Yvex', 'Lord Rog', 'Major Vegenor', 'Major Insignia', 'c'),
('00011', 'Solve: \r\n\r\n<img src="http://www.phantomdev.net/public/Quiz/Lib/Images/int1.png"></img>', '(X³/3) + 4x² + 5x + C', '(X³/3) + 2x² + 10x + C', '(X³) + 2x² + 5x + C', 'wut? I no kno calculus.', 'b'),
('00012', 'What best symbolizes the process of integration.', 'Anti-Differentiation (The Opposite of finding a differential)', 'Anti-Derivative (The Reversed Process of Differentiation)', 'The Inverse of taking an integral', 'Calculus.', 'b'),
('00013', 'In Integral Calculus, there is integration by parts, which of the following rules is this considered the anti-derivative for?', 'Chain Rule', 'Quotient Rule', 'Product Rule', 'Constant-Multiple Rule', 'c'),
('00014', 'Who is the authority on TWM2 Core Keys?', 'Thyth', 'Phantom139', 'Signal360', 'Dark Dragon DX', 'b'),
('00015', 'Nighthawk Construction is built on a second scripting language, identify it', 'Package Based', 'Definition Based', 'Torque Based', 'Modular Based', 'd'),
('00016', 'Who is NOT a developer of TWM2', 'Phantom139', 'Signal360', 'DarknessOfLight (DoL)', 'Castiger', 'd'),
('00017', 'Solve the following:\r\n\r\n X² + 2x -8', '(x + 2)(x + 4)', '(x + 2)(x - 4)', '(x - 2)(x - 4)', '(x - 2)(x + 4)', 'd'),
('00018', 'Find the Derivative:\r\n\r\n(2x + 5) ^ 20', '40(2x + 5) ^ 19', '20(2x + 5) ^ 19', '19(2x + 5) ^ 20', '20(2) ^ 19', 'a'),
('00019', 'Find the Derivative:\r\n\r\nsqrt( - x)', '-sqrt(x)', '-(1 / 2sqrt(x))', '2sqrt(x)', '(1/2)sqrt(x)', 'b'),
('00020', 'In C&C Tiberium Conflict, there are 3 factions, list them', 'GDI, NOD, Civilians', 'Allies, Soviets, Yuri', 'GDI, NOD, NPA', 'Allies, Soviets, NPA', 'c'),
('00021', 'Tribes 2''s color tag scheme for [tag]name is?', '\\c6[tag]\\c7[name]', '\\c8[tag]\\c4[name]', '\\c4[tag]\\c6[name]', '\\c7[tag]\\c6[name]', 'd'),
('00022', 'One of PGD''s upcoming projects, is tactical uprising, which mirrors which of these PGD Mods?', 'Powers', 'Defcon', 'TWM', 'Nighthawk', 'c'),
('00023', 'PGD''s future project, galactic wars is preluded by which of these other PGD Projects?', 'Battlelord Armor Divisions', 'Battlelord Armor Divisions: Rising Frontiers', 'Tactical Uprising', 'Defcon Mod', 'c'),
('00024', 'PGD stands mainly against the views of what?', 'Democratic Party', 'Republican Party', 'Occupation Movement (Liberal Bias)', 'PGD Opposes All of These', 'd'),
('00025', 'How many Tactical Uprising Projects are there?', '2', '3', '4', '5', 'c'),
('00026', 'Of the initial 3 pilot projects, which saw it''s first removal from the list of fullyactive Projects?', 'Battlelord: Armor Divisions', 'Nightstorm RPG', 'Tactical Uprising: Beginnings', 'All of these are still active', 'a'),
('00027', 'How many officer ranks was TWM2 ORIGINALLY going to have?', '9', '15', '20', 'Unlimited', 'c'),
('00028', 'Tatical Uprising: Beginnings has a short and simple rank system consisting of ''x'' ranks and ''y'' officer promotions, fill in x and y using the following.', '15, 18', '18, 15', '20, 15', '15, 20', 'b');

-- --------------------------------------------------------

--
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
CREATE TABLE IF NOT EXISTS `User` (
  `Name` text NOT NULL,
  `email` text NOT NULL,
  `Age` text NOT NULL,
  `Cell` text NOT NULL,
  `T2User` text NOT NULL,
  `GUID` int(11) NOT NULL,
  `ApplyingFor` text NOT NULL,
  `QuestionsUsed` text NOT NULL,
  `AnswerSet` text NOT NULL,
  `CompleteCT` int(11) NOT NULL,
  `Score` decimal(10,0) NOT NULL,
  `FRQ1` text NOT NULL,
  `FRQ2` text NOT NULL,
  `FRQ3` text NOT NULL,
  `PassFail` int(1) NOT NULL COMMENT '1 - Pass, 0 - Fail'
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `User`
--

INSERT INTO `User` (`Name`, `email`, `Age`, `Cell`, `T2User`, `GUID`, `ApplyingFor`, `QuestionsUsed`, `AnswerSet`, `CompleteCT`, `Score`, `FRQ1`, `FRQ2`, `FRQ3`, `PassFail`) VALUES
('Robert MacGregor', 'darkdragondx@hotmail.com', '17', '', 'DarkDragonDX', 2003098, 'admin', ' 00019 00014  00012', ' a b', 3, 1, 'I already am an admin.', 'How everything can break easily.', 'Do what I already do.', 0),
('Alexander Featherston', 'F9Alejandro@FeatherDev.net', '15', '9782354620', 'F9Alejandro', 0, 'admin', ' 00005 00002 00012 00014 00028 00011 00013 00006 00023 00026 00007 00001 00010 00020 00017 00024', ' d a a b c d d c c b b d a a d d', 15, 33, 'I have had experience as being an admin i have never caused another admin to get upset with me and i have never asked for admin every since i started playing Tribes 2 and any other game.', 'I find that all of the Mods are unique in their own ways. the one that i find the most appealing is the Powers mod because of the fact that you can choose your own class and fight and gain actual experience from it.', 'I Would only host and moderate the main and other servers to make sure that there are no trolls and also that people are not exploiting glitches in the mods. an example is in TWM2 there is a glitch with the levels for some reason it says you beat a certain level multiple times, even though they have never really beat them.', 0),
('WoofusDoofus(t2name)', 'justdoofus@gmail.com', '16', 'N/A', 'WoofusDoofus', 2590996, 'admin', ' 00013 00023 00015 00022 00007 00014 00006 00005 00010 00003 00018 00020  00012 00001 00021', '', 15, 47, 'I am a very respectful admin that would take things seriously, I am very trustworthy, and listen two an issue very carefully before resolving.\r\n\r\nI am non-abusive, and I take orders fairly good, and I would keep the server up to it''s maximum operation while also keeping a fair reputation.\r\n', 'I find interesting about the mods, is how it''s all smashed into one, big work of art. (More like fun.) I also like the automatic bot and rank up systems when it comes to zombie fighting, and the idea sends me a ton of motivation to keep playing this mod.', 'I would make sure there are no spammers, un-fair builders/fighters, and will ask, and question the situation if at all possible.\r\n\r\nNot be an abusive admin. That''s all I have to say. Thanks! ', 0),
('Calvin McClain', 'mcclain_1995@live.com', '16', 'N/A (Till 1/1/2012)', 'RazeX', 3663538, 'admin', ' 00023 00026 00008 00011 00009 00021 00025 00002 00013 00010 00005 00020  00004 00017 00018', ' c b b d a c b b d c d d c a c c', 15, 227, 'Well, I was around when Castiger (old TWM Supporter) had his Server Up he WAS gonna put me as Admin but he quit and Went to Killing Floor Then i Lost Contact With him.\r\n\r\n(ive been playing T2 for idk... 2 yrs now?\r\n\r\nAlso im in Highschool (10th grade)\r\nAlso I Work at Four horsemen (Morgantown mall)\r\nAlso I Hosted Clans for all Sorts of MMOFPS & MMORPG\r\n\r\nSo i am Experianced in Leadership, Drama, And working', 'The Factor Of Horde and Construction You Guys have Basicly Put my Fav. 2 Mod''s together in 1 big mod. Witch Got my Hooked INSTANTLY', 'Well, I Would Enforce the Rules like i do at my Own Job, (Yes i Do Work in Real Life)[Also I go to School]\r\n\r\nBut Basicly i Would Enforce Rules to Some Extent. i am a Fair Person, (From everybody that knows me Point of View) \r\n\r\numm.. Yea... idk what else to say.\r\n\r\nIf You Do not Accecpt me i completly understand.\r\n\r\nIf You Do, alsome', 0),
('Alexander Featherston', 'F9Alejandro@FeatherDev.net', '15', '982354620', 'F9Alejandro', 2886178, 'admin', ' 00022 00020 00027 00028 00013 00026  00015 00025 00005 00016 00012 00006 00011 00014  00002  00008 00023 00024  00001  00007 00017 00003  00010 00021 00019 00004 00009 00018', ' c a a c c d a c d d b b d b a d a d d b b a c d b d d', 33, 14, 'I am willing to learn the ways and be more responsible as an admin and to be able to give help and gain help when needed. i also have past experience with being an admin and have never spammed or misused any commands. i will only use certain commands under certain circumstances like example /slap or /gag.', 'I always found the PGD connect database to be quite a sight as in it is perfectly made and i have been dumbfounded by how interactive it is with the servers and how it is almost perfected to it''s full potential.', 'i will only use commands when the problem is severe enough and i will give warnings to those who cause problems. also i will ask the host/owner for permission before kicking or any other means. if the player persists i will bring it up with the owner/host to have them temp banned or perm banned if they persist on another PGD server i will bring it to phantom139''s attention and i am sure that other admins will too.', 0);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
