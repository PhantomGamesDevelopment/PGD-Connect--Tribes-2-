<?php
   $debugMode = 0;
   function GatherTopRanks($doCount, $withlink) {
      global $debugMode;
      $dir = "/home/phantom7/public_html/public/Univ/Data/";
   
      $count = 1;
      $highest = 0;
      $noabove = 100000000000;   //Highest Rank Level XP
   
      if($debugMode == 1) {
         echo "<b>DEBUG MODE IS ON</b><br/>";
      }
   
      // Open a known directory, and proceed to read its contents
      if (is_dir($dir)) {
         if ($dh = opendir($dir)) {
            while (($file = readdir($dh)) !== false) {
               if(is_file("".$dir."/".$file."/Ranks/TWM2/Saved.TWMSave")) {
                  $exp[$file] = ReadEXPFor15("".$dir."/".$file."/Ranks/TWM2/Saved.TWMSave", $file);        
                  $pres[$file] = ReadPresFor15("".$dir."/".$file."/Ranks/TWM2/Saved.TWMSave", $file);  
                  //
                  if($debugMode) {
                     echo $file ." - ". $exp[$file] ." - ". $pres[$file] ." <br/>"; 
                  } 
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
	          $Prest = $pres[$guid[$j]];          	                  
                  $XPA = $exp[$guid[$j]] + (10000000*$Prest);                              
	          if(($XPA >= $highest) && ($XPA < $noabove)){              
	             $highest = $XPA;     	         	         
                     $player = $guid[$j];
                  }                                            
               }
               $Top[$i] = GetNameFromFile("".$dir."/".$player."/Ranks/TWM2/Saved.TWMSave", $player);
               if($Top[$i] == "") {
                  $Top[$i] = "NNF - ".$player."";
               }
               $TopXP[$i] = $exp[$player];
               $TotalEXP += ($TopXP[$i] + $pres[$guid[$i]]*3000000);
	       $noabove = $highest;
	       //echo "No Above: ".$noAboveP."<p>";
	       $highest = 0;
	       if($withlink) {
	          echo "".$i.". ".GetRankImage("".$dir."/".$player."/Ranks/TWM2/Saved.TWMSave", $player)."<a href=http://www.phantomdev.net/public/Univ/Ranks/TWM2/RankPage.php?guid=".$player.">".$Top[$i]."</a>: ".number_format($TopXP[$i])." EXP<p>\n";
               }
               else {
	          echo "".$i.". ".GetRankImage("".$dir."/".$player."/Ranks/TWM2/Saved.TWMSave", $player)."".$Top[$i].": ".number_format($TopXP[$i])." EXP<p>\n";               
               }
            }
         }
         echo "<p>";
         echo "TOTAL EXP BY ALL PLAYERS: ".number_format($TotalEXP)."";
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
      $nameStart = strpos($theData, "name = \"") + 8;
      $nameEnd = strpos($theData,"\";",$nameStart); 
      $nameLen = $nameEnd - $nameStart;
      $name = substr($theData, $nameStart, $nameLen);     
      $namef = str_replace("\"","",$name);      
      return $namef; 
   }

   function ReadEXPFor15($loc, $guid) {
      global $debugMode;
      if($debugMode == 1) {
         echo "Opening: ". $loc ." for ".$guid."<br/>";
      }
      $fh = fopen($loc, 'r');
      if(filesize($loc) <= 0) {
         //corrupt?
         fclose($fh);
         return 0;
      }
      $theXPData = fread($fh, filesize($loc));
      fclose($fh);
      //
      $mEXPStart = strpos($theXPData , "millionxp = \"") + 13;
      $mexpEnd = strpos($theXPData ,"\";",$mEXPStart); 
      $mexpLen = $mexpEnd - $mEXPStart;
      $mexp = substr($theXPData, $mEXPStart, $mexpLen);       
      if($mEXPStart <= 13) {
         $mexp = 0;
      }      
      //exp
      $expStart = strpos($theXPData, "xp = \"", $mexpEnd) + 6;
      $expEnd = strpos($theXPData ,"\";",$expStart); 
      $expLen = $expEnd - $expStart;
      if($expStart == false || $expLen <= 0) {
         if($debugMode == 1) {
            echo $guid ." 0 EXP by boolean <br/>";
         }
         $exp = 0;
      }
      else {
         $exp = substr($theXPData , $expStart, $expLen); 
         if($exp <= 0) {
            $expStart = strpos($theXPData, "xp = \"") + 6;
            $expEnd = strpos($theXPData ,"\";",$expStart); 
            $expLen = $expEnd - $expStart;  
            $exp = substr($theXPData , $expStart, $expLen);        
         }
         if($debugMode == 1) {
            echo $guid ." -> ". $exp ." | ".substr($theXPData , $expStart, $expLen)." from ". $expStart ." to ". $expLen ." <br/";
         }
      }
      //
      $f = $exp + (1000000*$mexp);
      return $f; 
   }
   
   function ReadPresFor15($loc, $guid) {
      $fh = fopen($loc, 'r');
      if(filesize($loc) <= 0) {
         //corrupt?
         fclose($fh);
         return 0;
      }      
      $theData = fread($fh, filesize($loc));
      fclose($fh);
      //exp
      $expStart = strpos($theData, "officer = \"");
      if($expStart <= 0) {
         return 0;
      }
      else {
         $expStart += 11;
      }
      $expEnd = strpos($theData,"\";",$expStart); 
      $expLen = $expEnd - $expStart;
      $prest = substr($theData, $expStart, $expLen); 
      return $prest; 
   }   
   
   function GetRankImage($loc, $guid) {
      $fh = fopen($loc, 'r');
      $theData = fread($fh, filesize($loc));
      fclose($fh);   
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
      //Prestige Override
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
      if($prestige > 0) {
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
      return "<img src=\"".$output7."\" alt=\"Image For ".$rankf."\" title=\"".$rankf."\" width=\"20\" height=\"20\" />";        
   }
?>   