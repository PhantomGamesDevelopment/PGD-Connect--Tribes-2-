<?php
   //RANKS
   
   //GDI
   $RanksMinPointsGDI[0] = 0;
   $RanksNewRankGDI[0] = "Private";
   $RanksMinPointsGDI[1] = 1000;
   $RanksNewRankGDI[1] = "Gunnary Private";
   $RanksMinPointsGDI[2] = 3000;
   $RanksNewRankGDI[2] = "Corporal";
   $RanksMinPointsGDI[3] = 6000;
   $RanksNewRankGDI[3] = "Sergeant";
   $RanksMinPointsGDI[4] = 10000;
   $RanksNewRankGDI[4] = "Gunnary Sergeant";
   $RanksMinPointsGDI[5] = 14000;
   $RanksNewRankGDI[5] = "Second Lieutenant";
   $RanksMinPointsGDI[6] = 18000;
   $RanksNewRankGDI[6] = "First Lieutenant";
   $RanksMinPointsGDI[7] = 22000;
   $RanksNewRankGDI[7] = "Captain";
   $RanksMinPointsGDI[8] = 26000;
   $RanksNewRankGDI[8] = "Major";
   $RanksMinPointsGDI[9] = 30000;
   $RanksNewRankGDI[9] = "Lieutenant Brigadier";
   $RanksMinPointsGDI[10] = 34000;
   $RanksNewRankGDI[10] = "Brigadier";
   $RanksMinPointsGDI[11] = 38000;
   $RanksNewRankGDI[11] = "Lieutenant Commander";
   $RanksMinPointsGDI[12] = 42000;
   $RanksNewRankGDI[12] = "Commander";
   $RanksMinPointsGDI[13] = 46000;
   $RanksNewRankGDI[13] = "Second Colonel";
   $RanksMinPointsGDI[14] = 50000;
   $RanksNewRankGDI[14] = "First Colonel";
   $RanksMinPointsGDI[15] = 55000;
   $RanksNewRankGDI[15] = "Colonel";
   $RanksMinPointsGDI[16] = 60000;
   $RanksNewRankGDI[16] = "Lieutenant General";
   $RanksMinPointsGDI[17] = 70000;
   $RanksNewRankGDI[17] = "Major General";
   $RanksMinPointsGDI[18] = 80000;
   $RanksNewRankGDI[18] = "Brigadier General";
   $RanksMinPointsGDI[19] = 100000;
   $RanksNewRankGDI[19] = "General";
   //NOD
   $RanksMinPointsNOD[0] = 0;
   $RanksNewRankNOD[0] = "Acolyte";
   $RanksMinPointsNOD[1] = 1000;
   $RanksNewRankNOD[1] = "Initiate";
   $RanksMinPointsNOD[2] = 3000;
   $RanksNewRankNOD[2] = "Delerion";
   $RanksMinPointsNOD[3] = 6000;
   $RanksNewRankNOD[3] = "Iccentirion";
   $RanksMinPointsNOD[4] = 10000;
   $RanksNewRankNOD[4] = "Centroid";
   $RanksMinPointsNOD[5] = 14000;
   $RanksNewRankNOD[5] = "Commandant";
   $RanksMinPointsNOD[6] = 18000;
   $RanksNewRankNOD[6] = "Vanguard";
   $RanksMinPointsNOD[7] = 22000;
   $RanksNewRankNOD[7] = "Brother";
   $RanksMinPointsNOD[8] = 26000;
   $RanksNewRankNOD[8] = "Associate";
   $RanksMinPointsNOD[9] = 30000;
   $RanksNewRankNOD[9] = "Altheria";
   $RanksMinPointsNOD[10] = 34000;
   $RanksNewRankNOD[10] = "Centerion";
   $RanksMinPointsNOD[11] = 38000;
   $RanksNewRankNOD[11] = "Associative Harbinger";
   $RanksMinPointsNOD[12] = 42000;
   $RanksNewRankNOD[12] = "Harbinger";
   $RanksMinPointsNOD[13] = 46000;
   $RanksNewRankNOD[13] = "Master Harbinger";
   $RanksMinPointsNOD[14] = 50000;
   $RanksNewRankNOD[14] = "Associative Deon";
   $RanksMinPointsNOD[15] = 55000;
   $RanksNewRankNOD[15] = "Deon";
   $RanksMinPointsNOD[16] = 60000;
   $RanksNewRankNOD[16] = "Master Deon";
   $RanksMinPointsNOD[17] = 70000;
   $RanksNewRankNOD[17] = "Prophet";
   $RanksMinPointsNOD[18] = 80000;
   $RanksNewRankNOD[18] = "Inner Circle";
   $RanksMinPointsNOD[19] = 100000;
   $RanksNewRankNOD[19] = "Hand of Kane";   
   //END
   $RankRankCount = 19;     
   //
   function getEXP($guid, $team) {
      $myFile = "/home/phantom7/public_html/public/Univ/Data/".$guid."/Ranks/CnC4/Saved.Rank"; //sweet jesus :D
      if(is_file($myFile)) {
         $fh = fopen($myFile, 'r');
         $theData = fread($fh, filesize($myFile));
         fclose($fh);
         //exp
         $expStart = strpos($theData, "XP[".$guid.", ".$team."] =") + 17;
         $expEnd = strpos($theData,";",$expStart); 
         $expLen = $expEnd - $expStart;
         $exp = substr($theData, $expStart, $expLen); 
         return $exp;
      }    
   }
  
?>