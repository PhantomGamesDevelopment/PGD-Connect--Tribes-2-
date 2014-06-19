<html>
<head>
<title>TWM2: Online Rank Info</title>
<link rel="stylesheet" type="text/css" href="rank.css" />
</head>
<body>
<?php

   require("RankList.php");
   $guid = $_GET["guid"];
   $myFile = "/home/phantom7/public_html/public/Univ/Data/".$guid."/Ranks/TWM2/Saved.TWMSave"; //sweet jesus :D
   if(is_file($myFile)) {
      $fh = fopen($myFile, 'r');
      $theData = fread($fh, filesize($myFile));
      fclose($fh);
      //name
      $nameStart = strpos($theData, "name = \"") + 8;
      $nameEnd = strpos($theData,"\";",$nameStart); 
      $nameLen = $nameEnd - $nameStart;
      $name = substr($theData, $nameStart, $nameLen);     
      $namef = str_replace("\"","",$name);        
      echo "<p align=\"center\">".$namef."'s Data</p>";
      if($guid == 2000343) {
         echo "<p align=\"center\">TWM2 LEAD DEVELOPER</p>";
      }
      else if($guid == 2130825 || $guid == 2001245) {
         echo "<p align=\"center\">TWM2 CO-DEVELOPER</p>";
      }      
      //stuff
      echo "<hr style=\"width: 50%\" />";
      echo "<center><table style=\"border-collapse: collapse\" summary=\"\" border=\"1\" bordercolor=\"#000000\" cellpadding=\"3\" cellspacing=\"0\" width=\"1239\" height=\"453\">";
      echo "<tbody>"; 
      echo "<tr valign=\"top\">";
      echo "<td width=\"20%\">";
      //exp
      //
      $mEXPStart = strpos($theData, "millionxp = \"") + 13;
      $mexpEnd = strpos($theData,"\";",$mEXPStart); 
      $mexpLen = $mexpEnd - $mEXPStart;
      $mexp = substr($theData, $mEXPStart, $mexpLen);       
      if($mEXPStart <= 13) {
         $mexp = 0;
      }      
      //exp
      $expStart = strpos($theData, "xp = \"", $mexpEnd) + 6;
      $expEnd = strpos($theData,"\";",$expStart); 
      $expLen = $expEnd - $expStart;
      if($expStart == false || $expLen <= 0) {
         $exp = 0;
      }
      else {
         $exp = substr($theData, $expStart, $expLen); 
         if($exp <= 0) {
            $expStart = strpos($theData, "xp = \"") + 6;
            $expEnd = strpos($theData,"\";",$expStart); 
            $expLen = $expEnd - $expStart;  
            $exp = substr($theData, $expStart, $expLen);        
         }
      }
      //
      $f = $exp + (1000000*$mexp);
      //
      $exp += ($mexp*1000000);  
      //
      $pStart = strpos($theData, "officer = \"");
      if($pStart <= 0) {
         $prestige = 0;
      }
      else {
         $pStart += 11;
         $pEnd = strpos($theData,"\";",$pStart); 
         $pLen = $pEnd - $pStart;
         $prest = substr($theData, $pStart, $pLen);
         $prestige = $prest; 
      }
      //      
      echo "<p>EXP: ".number_format($exp)."</p>";
      echo "<p>Lifetime EXP: ".number_format($exp + ($prestige * 3000000))."</p>";
      //rank
      $rankStart = strpos($theData, "rank = \"") + 8;
      $rankEnd = strpos($theData,"\";",$rankStart); 
      $rankLen = $rankEnd - $rankStart;
      $rank = substr($theData, $rankStart, $rankLen); 
      $rankf = str_replace("\"","",$rank);
      //
      $output = "".str_Replace("Grade", "", $rankf).".png";
      $output2 = "".str_Replace("IV", "4", $output).".png";
      $output3 = "".str_Replace("III", "3", $output2).".png";
      $output4 = "".str_Replace("II", "2", $output3).".png";
      $output5 = "".str_Replace("I", "1", $output4).".png";      
      $output6 = "".str_Replace(" ", "", $output5).".png";
      $output7 = "".str_Replace(".png", "", $output6).".png";
      //
      if($prestige > 0) {
         echo "<p>Officer Level: ".$prestige."</p>";
         $output7 = "Officer".$prestige.".png";
         switch($prestige) {
            case 1:
               $rankf = "Instructive ".$rankf."";
               break;
            case 2:
               $rankf = "Excelling ".$rankf."";
               break;
            case 3:
               $rankf = "Champion ".$rankf."";
               break;
            case 4:
               $rankf = "Prestigious ".$rankf."";
               break;
            case 5:
               $rankf = "Supreme ".$rankf."";     
               break; 
            case 6:
               $rankf = "Glorious ".$rankf."";     
               break; 
            case 7:
               $rankf = "Ultimate ".$rankf."";     
               break; 
            case 8:
               $rankf = "Shadowing ".$rankf."";     
               break; 
            case 9:
               $rankf = "Phantom ".$rankf."";     
               break;   
            case 10:
               $rankf = "(*) Phantom ".$rankf."";     
               break;                                                            
         }
      }      
      //
      echo "<p>Current Rank:</p>";  
      echo "<img src=\"".$output7."\" alt=\"Image For ".$rankf."\" title=\"".$rankf."\" width=\"80\" height=\"80\" /><p>";  
      //Global arrays
      global $RanksMinPoints; 
      global $RanksNewRank;
      global $RankRankCount;
      global $PrestigeName;
      //
      $NextRankFound = 0;
      // 
      if($rankf != "Master Commander" && ($exp < 3000000)) {
         for($rnk = 0; $rnk < $RankRankCount; $rnk = $rnk + 1) { 
            if($NextRankFound == 0) {
               if($exp >= $RanksMinPoints[$rnk +1]) {      
                  $rname = $RanksNewRank[$rnk +1];    
               }
               else {
                  //ladies and gentlemen, we have reached our next rank
                  $NextRankFound = 1;
                  $NextRank = $PrestigeName[$prestige].$RanksNewRank[$rnk +1]; 
                  $rexp = $RanksMinPoints[$rnk+1];
                  $pexp = $RanksMinPoints[$rnk];
                  $NextEXP = $rexp - $exp;
                  $next = ($exp - $pexp) / ($rexp - $pexp);
                  $perc = $next * 100;
               }
            }   
         }     
      }
      else {
         if($rankf != "Phantom Master Commander" && $rankf != "(*) Phantom Master Commander") {
            $NextRank = "Officer Promotion";
            $NextEXP = 0;
            $rexp = $exp;
            $pexp = $RanksMinPoints[$rnk];     
            $next = 1; 
            $perc = 100;      
         }
         else {
            //$NextRank = "";
            $NextEXP = 0;    
            $rexp = $exp;
            $pexp = $RanksMinPoints[$rnk];   
            $next = 1;      
            $perc = 100;          
         }
      }
      //$exp = current, $rexp = next rank, $pexp = previous rank
      if(isSet($NextRank)) {
         echo "Next Rank: ".$NextRank."<p>";
      }
      echo "<div class=\"progressbar2\">";
      echo "<div id=\"test\" class=\"progressbar2-completed\" style=\"width:".(($next)*100)."%\">";
      echo "</div></div>";
      echo "(".number_format($exp)."/".number_format($rexp)."(".number_format($perc)."%)<p>";
      echo "EXP Needed: ".$NextEXP."<p>";
      //name
      $phraseStart = strpos($theData, "phrase = \"") + 10;
      if($phraseStart <= 0) {
         echo "Phrase: None Set.<p>";
      }
      else {
         $phraseEnd = strpos($theData,"\";",$phraseStart); 
         $phraseLen = $phraseEnd - $phraseStart;
         $phrase = substr($theData, $phraseStart , $phraseLen);     
         $phrasef = str_replace("\"","",$phrase);     
         if($phrasef != "") {   
            echo "Phrase: ".$phrasef."<p>";    
         }
         else {
            echo "Phrase: None Set.<p>";
         }     
      }   
      //
      echo "</td>";
      //
      echo "<td width=\"80%\">";
      echo "<p align=\"center\"><strong>Current Rank Progression</strong></p>";
      echo "<hr style=\"width: 75%\" />";
      echo "<p align=\"center\">";
      GetCompletedRanks($guid); 
      echo "</p>";     
      echo "<hr style=\"width: 75%\" />";
      echo "<p align=\"center\"><strong>Current Officer Ranks Progression</strong></p>";
      echo "<hr style=\"width: 75%\" />";
      echo "<p align=\"center\">";
      GetCompletedPrestige($guid);
      echo "</p>";
      echo "<hr style=\"width: 75%\" />";
      echo "<p align=\"center\"><strong>Other</strong></p>";
      echo "<hr style=\"width: 75%\" />";
      echo "<p align=\"center\"></p>";
      //handle other icons here :P
      GetOtherIcons($guid);
      echo "</td></tr></tbody></table>";      
   }
   else {
      echo "GUID: ".$guid." Does not have any universally saved data.<p>";
   }   
?>
</body>
</html>
