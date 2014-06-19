<?php

function currentDate() {
   return date('Ymd');
}

function lastDay() {
   return date('Ymd', strtotime('-1 second',strtotime('+1 month',strtotime(date('m').'/01/'.date('Y').' 00:00:00'))));
}

function firstDay() {
   return date("Ymd", strtotime(date('m').'/01/'.date('Y').' 00:00:00'));
}

function lastWeek() {
   return date('Ymd', strtotime('Next Sunday'));
}

function firstWeek() {
   return date('Ymd', strtotime('Last Monday'));
}

function outLastTimes() {
   //date('Y-m-d',strtotime('-1 second',strtotime('+1 month',strtotime(date('m').'/01/'.date('Y').' 00:00:00'))));
   echo "#TIME ". lastDay() ."\t". firstDay() ."\t". lastWeek() ."\t". firstWeek();
}

function outTWM2Challenges() {
   //establish SQL connection
    $con = mysql_connect("localhost","","");
    if (!$con) {
      die('Could not connect: ' . mysql_error());
    }
    // Create table
    mysql_select_db("phantom7_PGDConnect", $con);
    $result1 = mysql_query("SELECT * FROM TWM2DC");
    while($row1 = mysql_fetch_array($result1)) {
       if($row1[Expire] >= currentDate() && currentDate() >= $row1[Active]) {
          echo "#CHLG ". $row1[ID]." \t ".$row1[Name]." \t ".$row1[Description]." \t ". $row1[Condition] ." \t ".$row1[Reward]." \t ".$row1[Expire]."\n";
       }  
    }
    mysql_close($con);   
}

?>
