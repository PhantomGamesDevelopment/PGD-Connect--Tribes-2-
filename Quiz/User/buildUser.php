<?php

   require("../Lib/question.php");

   //get the fields... and store them accordingly
   $name = $_POST["name"];
   $mail = $_POST["email"];
   $age = $_POST["age"];
   $cell = $_POST["cell"];
   $t2n = $_POST["t2n"];
   $guid = $_POST["guid"];
   $afor = $_POST["afor"];
   //
   $frq1 = $_POST["frq1"];
   $frq2 = $_POST["frq2"];
   $frq3 = $_POST["frq3"];         
   //
   if(!isSet($name)) {
      die("You did not enter your name");
   }
   if(!isSet($mail)) {
      die("You did not enter your email address");
   }   
   if(!isSet($age)) {
      die("You did not enter your age");
   }
   if(!isSet($cell)) {
      die("You did not enter your cell phone #, please enter N/A if you do not have one");
   }
   if(!isSet($t2n)) {
      die("You did not enter your tribes 2 name");
   }
   if(!isSet($guid)) {
      die("You did not enter your guid, you can obtain this in game, also, see <a href=\"http://www.public.phantomgamesdevelopment.com/SMF/index.php/topic,160.0.html\">This PGD Topic</a> on how to do so");
   }
   if(!isSet($afor)) {
      die("Please select admin or super-admin");
   }  
   //
   if(!isSet($frq1) || !isSet($frq2) || !isSet($frq3)) {
      die("Please complete all free response questions");
   }       
   
   //compare existing field
   if(guidExistsInUserDB($guid) == 1) {
      die("Conflicting GUID Error: Please Contact Phantom139");
   }      
   //generate a 256 bit cookie for security
   //open a mysql connection, add the data, and then proceed
   $con = mysql_connect("localhost","","");
   if(!$con) {
      die('Could not connect: ' . mysql_error());
   }
   mysql_select_db("phantom7_Quiz", $con);
   $q = "INSERT INTO User(Name, email, Age, Cell, T2User, GUID, ApplyingFor, QuestionsUsed, CompleteCT, Score, FRQ1, FRQ2, FRQ3) VALUES ('$name', '$mail', '$age', '$cell', '$t2n', '$guid', '$afor', '', '-1', '0', '$frq1', '$frq2', '$frq3')";   
   mysql_query($q) or die("ERROR: ".mysql_error()); 
   $user = $guid;
   //with the FRQ's answered, and all of the other data completed, proceed to the quizzing!
   $qIndex = grabNonSelectedIndex($user);
   outputQuestion($user, $qIndex);
?>
