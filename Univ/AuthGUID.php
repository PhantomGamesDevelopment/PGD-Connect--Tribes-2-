<?php

   $agent = $_SERVER['HTTP_USER_AGENT'];//In-Game Auths only
   echo "Connect From: ".$agent."<p>";
   if($agent != "Tribes 2") {
      die ("Access Denied.");
   }

$GUID = $_GET[guid];

$con = mysql_connect("localhost","","");
if (!$con) {
  die('Could not connect: ' . mysql_error());
}

// Create table
mysql_select_db("phantom7_PGDConnect", $con);

$result1 = mysql_query("UPDATE Data SET Authenticated = '1' WHERE guid ='$GUID'");
if($result1) {
   echo "Authentication Complete.";
}
mysql_close($con);

?>
