<?php

   require("question.php");

   $user = $_POST["USER"];
   // grab our answer
   $qid = $_POST["QID"];
   $con = mysql_connect("localhost","","");
   if(!$con) {
      die('Could not connect: ' . mysql_error());
   }
   mysql_select_db("phantom7_Quiz", $con);   
   $result = mysql_query("SELECT * FROM QDB_Admin WHERE QID = '$qid'");
   $row = mysql_fetch_array($result);   
   
   $ans = $row["ANS"];
   
   $oldStr = obtainField($user, "AnswerSet");
   $newStr = $oldStr ." ".$_POST["ans"];
   applyField($user, "AnswerSet", $newStr);
   
   if(strcmp($_POST["ans"], $ans) == 0) {
      $score = obtainField($user, "Score");
      applyField($user, "Score", $score + 1);   
   }
   if(obtainField($user, "CompleteCT") == 15) {
   
      $score = obtainField($user, "Score");
      
      $grd = ($score / 15)*100;
      
      applyField($user, "Score", ($grd));     
         
      pushResults($user);
   }
   else {
      //proceed... proceed.... proceed :P
      $index = grabNonSelectedIndex($user);
      outputQuestion($user, $index);
   }
?>
