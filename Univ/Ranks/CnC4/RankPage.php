<html>
<head>
<title>C&C Tiberium Uprising: Online Rank Info</title>

<link rel="stylesheet" type="text/css" href="PGDRank.css" />

</head>
<body>
<?php

   require("RankList.php");
   $guid = $_GET["guid"];
   $myFile = "/home/phantom7/public_html/public/Univ/Data/".$guid."/Ranks/CnC4/Saved.Rank"; //sweet jesus :D
   if(is_file($myFile)) {
      $fh = fopen($myFile, 'r');
      $theData = fread($fh, filesize($myFile));
      fclose($fh);
      //name
      $nameStart = strpos($theData, "Name[".$guid."] =") + 16;
      $nameEnd = strpos($theData,";",$nameStart); 
      $nameLen = $nameEnd - $nameStart;
      $name = substr($theData, $nameStart, $nameLen);     
      $namef = str_replace("\"","",$name);        
      echo "<p align=\"center\">".$namef."'s Data</p>";
      if($guid == 2000343) {
         echo "<p align=\"center\">C&C Tiberium Uprising - LEAD DEVELOPER</p>";
      }    
      //
      if($guid == 2001729) {
         echo "<p align=\"center\">C&C Tiberium Uprising - CO-DEVELOPER</p>";
      } 
      //stuff 
      echo "<hr style=\"width: 50%\" />";
      echo "<table style=\"border-collapse: collapse\" summary=\"\" bgcolor=\"#3300CC\"  border=\"1\" bordercolor=\"#000000\" cellpadding=\"3\" cellspacing=\"0\" width=\"1239\" height=\"453\">";
      echo "<tbody>"; 
      echo "<tr valign=\"top\">";
      echo "<td width=\"20%\">";
      //exp
      $exp1Start = strpos($theData, "XP[".$guid.", 1] =") + 17;
      $exp1End = strpos($theData,";",$exp1Start); 
      $exp1Len = $exp1End - $exp1Start;
      $exp1 = substr($theData, $exp1Start, $exp1Len); 
      
      //rank
      $rank1Start = strpos($theData, "Rank[".$guid.", 1] =") + 19;
      $rank1End = strpos($theData,";",$rank1Start); 
      $rank1Len = $rank1End - $rank1Start;
      $rank1 = substr($theData, $rank1Start, $rank1Len); 
      $rankf1 = str_replace("\"","",$rank1);    
      //Global arrays
      global $RanksMinPointsGDI; 
      global $RanksNewRankGDI;
      global $RanksMinPointsNOD; 
      global $RanksNewRankNOD;      
      global $RankRankCount;
      //
      $NextRankFound = 0;
      //GDI
      if($rankf1 != "General") {
         for($rnk = 0; $rnk < $RankRankCount; $rnk = $rnk + 1) { 
            if($NextRankFound == 0) {
               if($exp1 >= $RanksMinPointsGDI[$rnk+1]) {      
                  $rname = $RanksNewRankGDI[$rnk+1];    
               }
               else {
                  //ladies and gentlemen, we have reached our next rank
                  $NextRankFound = 1;
                  $NextRank = $RanksNewRankGDI[$rnk +1]; 
                  $rexp = $RanksMinPointsGDI[$rnk+1];
                  $pexp = $RanksMinPointsGDI[$rnk];
                  $NextEXP = $rexp - $exp1;
               }
            }   
         }     
      }
      else {
         $rexp = $exp1;
         $NextRank = "No Further Progression";
         $NextEXP = 0;
      }
      echo "Current GDI Rank: ".$rankf1."";           
      echo "<p>GDI EXP: ".number_format($exp1)."";
      echo "<div class=\"progressbar1\"><div id=\"test\" class=\"progressbar1-completed\" style=\"width:".((($exp1-$pexp)/($rexp-$pexp))*100)."%;\"></div></div>(".$exp1."/".$rexp."(".number_format((($exp1-$pexp)/($rexp-$pexp))*100)."%))";
      echo "<br>";   
      echo "<p>Next GDI Rank: ".$NextRank."<p>";
      echo "EXP Needed: ".$NextEXP."<p>";
      echo "<hr style=\"width: 90%\" />";
      //Player Stats
      $TkillsStart = strpos($theData, "TotalKills[".$guid."] =") + 22;
      $TkillsEnd = strpos($theData,";",$TkillsStart); 
      $TKillsLen = $TkillsEnd - $TkillsStart;
      if($TKillsLen > 10) {  //lets be realistic now...
         $kills1 = 0;
      }
      else {
         $kills1 = substr($theData, $TkillsStart, $TKillsLen);    
      }
      //
      $UkillsStart = strpos($theData, "UKills[".$guid."] =") + 18;
      $UkillsEnd = strpos($theData,";",$UkillsStart); 
      $UkillsLen = $UkillsEnd - $UkillsStart;
      if($UkillsLen > 10) {  //lets be realistic now...
         $Ukills1 = 0;
      }
      else {
         $Ukills1 = substr($theData, $UkillsStart, $UkillsLen);    
      }          
      //    
      echo "Kills: ".$kills1." <p> Units Killed: ".$Ukills1." <p> Players Killed: ".($kills1-$Ukills1)."<p>";
      //
      $deathsstrt = strpos($theData, "Deaths[".$guid."] =") + 18;
      $deathsend = strpos($theData,";",$deathsstrt); 
      $deathslen = $deathsend - $deathsstrt;
      $deaths = substr($theData, $deathsstrt, $deathslen);       
      echo "Deaths: ".$deaths."<p>";      
      //
      echo "K:D Ratio: ".number_format($kills1/$deaths, 2)."<p>";
      //NOD
      echo "</td>";
      echo "<td width=\"20%\">";
      //exp
      $exp1Start = strpos($theData, "XP[".$guid.", 2] =") + 17;
      $exp1End = strpos($theData,";",$exp1Start); 
      $exp1Len = $exp1End - $exp1Start;
      $exp1 = substr($theData, $exp1Start, $exp1Len); 
      //rank
      $rank1Start = strpos($theData, "Rank[".$guid.", 2] =") + 19;
      $rank1End = strpos($theData,";",$rank1Start); 
      $rank1Len = $rank1End - $rank1Start;
      $rank1 = substr($theData, $rank1Start, $rank1Len); 
      $rankf1 = str_replace("\"","",$rank1);
      $NextRankFound = 0;
      //GDI
      if($rankf1 != "Hand Of Kane") {
         for($rnk = 0; $rnk < $RankRankCount; $rnk = $rnk + 1) { 
            if($NextRankFound == 0) {
               if($exp1 >= $RanksMinPointsNOD[$rnk+1]) {      
                  $rname = $RanksNewRankNOD[$rnk+1];    
               }
               else {
                  //ladies and gentlemen, we have reached our next rank
                  $NextRankFound = 1;
                  $NextRank = $RanksNewRankNOD[$rnk +1]; 
                  $rexp = $RanksMinPointsNOD[$rnk+1];
                  $pexp = $RanksMinPointsNOD[$rnk];
                  $NextEXP = $rexp - $exp1;
               }
            }   
         }     
      }
      else {
         $rexp = $exp1;
         $NextRank = "No Further Progression";
         $NextEXP = 0;
      }
      echo "Current NOD Rank: ".$rankf1."";           
      echo "<p>NOD EXP: ".number_format($exp1)."";
      echo "<div class=\"progressbar2\"><div id=\"test\" class=\"progressbar2-completed\" style=\"width:".((($exp1-$pexp)/($rexp-$pexp))*100)."%;\"></div></div>(".$exp1."/".$rexp."(".number_format((($exp1-$pexp)/($rexp-$pexp))*100)."%))";
      echo "<br>";   
      echo "<p>Next NOD Rank: ".$NextRank."<p>";
      echo "EXP Needed: ".$NextEXP."<p>";
      echo "<hr style=\"width: 90%\" />";  
      //Building stats
      $bldstrt = strpos($theData, "TotalBuilds[".$guid."] =") + 22;
      $bldend = strpos($theData,";",$bldstrt); 
      $bldlen = $bldend - $bldstrt;
      $blds = substr($theData, $bldstrt, $bldlen);       
      echo "Buildings Constructed: ".$blds."<p>";    
      //        
      $bldlstrt = strpos($theData, "TotalBuildingLosses[".$guid."] =") + 30;
      $bldlend = strpos($theData,";",$bldlstrt); 
      $bldllen = $bldlend - $bldlstrt;
      $bldl = substr($theData, $bldlstrt, $bldllen);       
      echo "Buildings Lost: ".$bldl."<p>"; 
      //
      echo "</td></tr></tbody></table>";      
   }
   else {
      echo "GUID: ".$guid." Does not have any universally saved data.<p>";
   }   
?>
</body>
</html>