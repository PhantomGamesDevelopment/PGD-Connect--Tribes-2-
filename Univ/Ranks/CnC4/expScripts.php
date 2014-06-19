<?php

   //GDI
   function GatherTopGDIRanks($doCount, $withlink) {
      $dir = "/home/phantom7/public_html/public/Univ/Data/";
   
      $count = 1;
      $highest = 0;
      $noabove = 100000000000;   //Highest Rank Level XP
   
      // Open a known directory, and proceed to read its contents
      if (is_dir($dir)) {
         if ($dh = opendir($dir)) {
            while (($file = readdir($dh)) !== false) {
               if(is_file("".$dir."/".$file."/Ranks/CnC4/Saved.Rank")) {
                  $exp1[$file] = ReadEXPFor15("".$dir."/".$file."/Ranks/CnC4/Saved.Rank", $file, 1);        
                  $guid[$count] = $file;   
                  $count++;
               }
            }
            closedir($dh);
            if($doCount == -1) {
               $doCount = $count;
            }
            echo "*TOP GDI RANKS*<p>\n";
            //do eet
            for($i = 1; $i <= $doCount-1; $i++){
	       for($j = 1; $j <= $count-1; $j++) {     	                  
                  $XPA = $exp1[$guid[$j]];                            
	          if(($XPA >= $highest) && ($XPA < $noabove)){              
	             $highest = $XPA;     	         	         
                     $player = $guid[$j];
                     //echo "DEBUG XPA NEW ".$player.": P".$highestPres." E".$highest."<p>";	
                  }                                            
               }
               $Top[$i] = GetNameFromFile("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player);
               if($Top[$i] == "") {
                  $Top[$i] = "NNF - ".$player."";
               }
               $TopXP[$i] = $exp1[$player];
	       $noabove = $highest;
	       //echo "No Above: ".$noAboveP."<p>";
	       $highest = 0;
	       if($withlink) {
	          echo "".$i.". <a href=http://www.phantomdev.net/public/Univ/Ranks/CnC4/RankPage.php?guid=".$player.">".$Top[$i]."</a>: ".GetRankImage("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player, 1).", ".number_format($TopXP[$i])." EXP<p>\n";
               }
               else {
	          echo "".$i.". ".$Top[$i].": ".GetRankImage("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player, 1).", ".number_format($TopXP[$i])." EXP<p>\n";               
               }
            }
         }
      }
   }   
   
   //NOD
   function GatherTopNODRanks($doCount, $withlink) {
      $dir = "/home/phantom7/public_html/public/Univ/Data/";
   
      $count = 1;
      $highest = 0;
      $noabove = 100000000000;   //Highest Rank Level XP
   
      // Open a known directory, and proceed to read its contents
      if (is_dir($dir)) {
         if ($dh = opendir($dir)) {
            while (($file = readdir($dh)) !== false) {
               if(is_file("".$dir."/".$file."/Ranks/CnC4/Saved.Rank")) {
                  $exp1[$file] = ReadEXPFor15("".$dir."/".$file."/Ranks/CnC4/Saved.Rank", $file, 2);        
                  $guid[$count] = $file;   
                  $count++;
               }
            }
            closedir($dh);
            if($doCount == -1) {
               $doCount = $count;
            }
            echo "*TOP NOD RANKS*<p>\n";
            //do eet
            for($i = 1; $i <= $doCount-1; $i++){
	       for($j = 1; $j <= $count-1; $j++) {     	                  
                  $XPA = $exp1[$guid[$j]];                            
	          if(($XPA >= $highest) && ($XPA < $noabove)){              
	             $highest = $XPA;     	         	         
                     $player = $guid[$j];
                     //echo "DEBUG XPA NEW ".$player.": P".$highestPres." E".$highest."<p>";	
                  }                                            
               }
               $Top[$i] = GetNameFromFile("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player);
               if($Top[$i] == "") {
                  $Top[$i] = "NNF - ".$player."";
               }
               $TopXP[$i] = $exp1[$player];
	       $noabove = $highest;
	       //echo "No Above: ".$noAboveP."<p>";
	       $highest = 0;
	       if($withlink) {
	          echo "".$i.". <a href=http://www.phantomdev.net/public/Univ/Ranks/CnC4/RankPage.php?guid=".$player.">".$Top[$i]."</a>: ".GetRankImage("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player, 2).", ".number_format($TopXP[$i])." EXP<p>\n";
               }
               else {
	          echo "".$i.". ".$Top[$i].": ".GetRankImage("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player, 2).", ".number_format($TopXP[$i])." EXP<p>\n";               
               }
            }
         }
      }
   } 

   //All
   function GatherTopRanks($doCount, $withlink) {
      $dir = "/home/phantom7/public_html/public/Univ/Data/";
   
      $count = 1;
      $highest = 0;
      $noabove = 100000000000;   //Highest Rank Level XP
   
      // Open a known directory, and proceed to read its contents
      if (is_dir($dir)) {
         if ($dh = opendir($dir)) {
            while (($file = readdir($dh)) !== false) {
               if(is_file("".$dir."/".$file."/Ranks/CnC4/Saved.Rank")) {
                  $exp1[$file] = ReadEXPFor15("".$dir."/".$file."/Ranks/CnC4/Saved.Rank", $file, 1);        
                  $exp2[$file] = ReadEXPFor15("".$dir."/".$file."/Ranks/CnC4/Saved.Rank", $file, 2);  
                  $guid[$count] = $file;   
                  $count++;
               }
            }
            closedir($dh);
            if($doCount == -1) {
               $doCount = $count;
            }
            echo "*".($doCount-1)." Registered Players*<p>\n";
            //do eet
            for($i = 1; $i <= $doCount-1; $i++){
	       for($j = 1; $j <= $count-1; $j++) {     	                  
                  $XPA = $exp1[$guid[$j]] + $exp2[$guid[$j]];                            
	          if(($XPA >= $highest) && ($XPA < $noabove)){              
	             $highest = $XPA;     	         	         
                     $player = $guid[$j];
                     //echo "DEBUG XPA NEW ".$player.": P".$highestPres." E".$highest."<p>";	
                  }                                            
               }
               $Top[$i] = GetNameFromFile("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player);
               if($Top[$i] == "") {
                  $Top[$i] = "NNF - ".$player."";
               }
               $TopXP[$i] = $exp1[$player] + $exp2[$player];
	       $noabove = $highest;
	       //echo "No Above: ".$noAboveP."<p>";
	       $highest = 0;
	       if($withlink) {
	          echo "".$i.". <a href=http://www.phantomdev.net/public/Univ/Ranks/CnC4/RankPage.php?guid=".$player.">".$Top[$i]."</a>: GDI: ".GetRankImage("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player, 1)." | NOD: ".GetRankImage("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player, 2)." | ".number_format($TopXP[$i])." Total EXP<p>\n";
               }
               else {
	          echo "".$i.". ".$Top[$i].": GDI: ".GetRankImage("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player, 1)." | NOD: ".GetRankImage("".$dir."/".$player."/Ranks/CnC4/Saved.Rank", $player, 2)." | Total EXP ".number_format($TopXP[$i])."<p>\n";               
               }
            }
         }
      }
   }
   
   function DisplayTop15($link) {
      GatherTopRanks(15, $link);     
   }
   
   function DisplayAll($link) {
      GatherTopRanks(-1, $link);   
   }
   
   function GetNameFromFile($loc, $guid) {       
      $fh = fopen($loc, 'r');
      $theData = fread($fh, filesize($loc));
      fclose($fh);
      //exp
      $nameStart = strpos($theData, "Name[".$guid."] =") + 16;
      $nameEnd = strpos($theData,";",$nameStart); 
      $nameLen = $nameEnd - $nameStart;
      $name = substr($theData, $nameStart, $nameLen);     
      $namef = str_replace("\"","",$name);      
      return $namef; 
   }

   function ReadEXPFor15($loc, $guid, $t) {
      $fh = fopen($loc, 'r');
      $theData = fread($fh, filesize($loc));
      fclose($fh);
      //exp
      $expStart = strpos($theData, "XP[".$guid.", ".$t."] =") + 17;
      $expEnd = strpos($theData,";",$expStart); 
      $expLen = $expEnd - $expStart;
      $exp = substr($theData, $expStart, $expLen); 
      return $exp; 
   }
   
   function GetRankImage($loc, $guid, $t) {
      $fh = fopen($loc, 'r');
      $theData = fread($fh, filesize($loc));
      fclose($fh);   
      //rank
      $rankStart = strpos($theData, "Rank[".$guid.", ".$t."] =") + 19;
      $rankEnd = strpos($theData,";",$rankStart); 
      $rankLen = $rankEnd - $rankStart;
      $rank = substr($theData, $rankStart, $rankLen); 
      $rankf = str_replace("\"","",$rank);
      //
      //
      return $rankf;        
   }
?>   