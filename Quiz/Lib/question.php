<?php
	//question.php
	//Phantom139 for Phantom Games Development
	//Copyright 2010
	require("functions.php");
	
   function grabNonSelectedIndex($user) {
      $usedString = obtainField($user, "QuestionsUsed");
   
      $con = mysql_connect("localhost","","");
      if(!$con) {
         die('Could not connect: ' . mysql_error());
      }
      mysql_select_db("phantom7_Quiz", $con);   
      $result = mysql_query("SELECT * FROM QDB_Admin");
      $questions = array();
      $i = 1;
      while($row = mysql_fetch_array($result)) {
         $questions[$i] = $row["QID"];
         $i++;
      }
      //loop one complete
      
      //Loop 2: keep looping until we generate a non-selected index
      $rand = rand(1, $i);
      while(strpos($usedString, $questions[$rand]) != 0) {
         $rand = rand(1, $i);   
      }
      $newStr = "".$usedString." ".$questions[$rand];
      //
      applyField($user, "QuestionsUsed", $newStr);
      applyField($user, "CompleteCT", (obtainField($user, "CompleteCT")+1));
      // 
      return $questions[$rand];
   }
   
   function outputQuestion($user, $QID) {
      $con = mysql_connect("localhost","","");
      if(!$con) {
         die('Could not connect: ' . mysql_error());
      }
      mysql_select_db("phantom7_Quiz", $con);   
      $result = mysql_query("SELECT * FROM QDB_Admin WHERE QID = '$QID'");
      $row = mysql_fetch_array($result);
      if(!isSet($row["Question"])) {
         //non-defined?
         //generate a new one
         $Q = grabNonSelectedIndex($user);
         outputQuestion($user, $Q);
      }
      //Echo the question
      echo $row["Question"];  
      echo "<br/>";
      echo "<form action=\"http://www.phantomdev.net/public/Quiz/Lib/answerScan.php\" method=\"post\">
  	A. ".$row["A"]." <input type=\"radio\" name=\"ans\" value=\"a\" /><br />
  	B. ".$row["B"]." <input type=\"radio\" name=\"ans\" value=\"b\"  /><br />
  	C. ".$row["C"]." <input type=\"radio\" name=\"ans\" value=\"c\"  /><br />
  	D. ".$row["D"]." <input type=\"radio\" name=\"ans\" value=\"d\"  /><br />
  	<input type=\"hidden\" name=\"USER\" value=".$user.">
  	<input type=\"hidden\" name=\"QID\" value=".$QID.">
      <input type=\"submit\" value=\"submit\" />
      </form>";
   }
   
   function pushResults($user) {
      echo "Congratulations, You have Completed the Exam";
      echo "<br/> <br/>";
      echo "The Following Are Your Results of The Examination:";   
      echo "<br/>FORM ".rand(50000, 100000)."";
      echo "<p><b>Final GRADE: ".obtainField($user, "Score")."";
      echo "<p><br/><br/>";
      echo "<br/>The following is a general table of consideration:";
      echo "<br/>100 - 90: Highly Considered";
      echo "<br/>89 - 75: Considered";
      echo "<br/>74 - 50: Low Consideration";
      echo "<br/>49 - 0: Try Again At Another Time";
      echo "<br/>Please Remember, just because you did well on this exam, does not guarentee you a position.";
      echo "<br/>Your information given will remain private only known to Phantom139.";
      echo "<br/>Final Results will be given to you after free response analysis and score determination";
      echo "<br/>Thank you for using the PGD Quizzer, you may now close this page.";
   }

?>
