<?php
  function CheckValid($G) {
    $con = mysql_connect("localhost","","");
    if (!$con) {
      die('Could not connect: ' . mysql_error());
    }
    // Create table
    mysql_select_db("phantom7_PGDConnect", $con);
    $result1 = mysql_query("SELECT * FROM Data WHERE GUID='$G'");
    while($row1 = mysql_fetch_array($result1)) {
      $match1 = $row1[GUID];
    }
    if($match1) {
       return true;
    }
    else {
       return false;
    }
    mysql_close($con);
  }

  function isConnected($GUID) {
     if($GUID) {
        if (CheckValid($GUID)) {
           return true;
        }
        else {
           return false;
        }
     }
     else {
        return false;
     }
  }
  //$GUID = $_GET['guid'];
?>
