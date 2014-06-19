<?php
	//Functions.php
	//Phantom139 For Phantom Games Development
	//Copyright 2010  
   function applyField($user, $field, $set) {
      $con = mysql_connect("localhost","","");
      if(!$con) {
         die('Could not connect: ' . mysql_error());
      }
      mysql_select_db("phantom7_Quiz", $con);   

      mysql_query("UPDATE User SET ".$field." = '$set' WHERE GUID = '$user'");
      
   }   
   
   function obtainField($user, $field) {
      $con = mysql_connect("localhost","","");
      if(!$con) {
         die('Could not connect: ' . mysql_error());
      }
      mysql_select_db("phantom7_Quiz", $con);   
      $result = mysql_query("SELECT * FROM User WHERE GUID = '$user'");  
      $row = mysql_fetch_array($result);

      return $row[$field];
   }      
   
   function guidExistsInUserDB($field) {
      $con = mysql_connect("localhost","","");
      if(!$con) {
         die('Could not connect: ' . mysql_error());
      }
      mysql_select_db("phantom7_Quiz", $con);   
      $result = mysql_query("SELECT * FROM User WHERE GUID = '$field'");  
      $row = mysql_fetch_array($result);
      if(isSet($row["GUID"])) {
         return 1;     
      }
      return 0;
   }

?>
