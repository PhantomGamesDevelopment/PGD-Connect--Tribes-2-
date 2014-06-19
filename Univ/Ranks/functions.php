<?php
   function getUserPass($user) {
      if($user == "")
         return 0;
      $con = mysql_connect("localhost","","");
      if (!$con)
         die("sql_error");
      mysql_select_db("phantom7_PGDConnect", $con);
      $result = mysql_query("SELECT * FROM TWM2Core WHERE Username='$user'");
      if (!$result)
         die("sql_error");
      while($row1 = mysql_fetch_array($result))
         $match1 = $row1['Password'];
      if($match1)
         return $match1;
      else
         return 0;
   }

   function CompareHash($user, $nameasHash, $providedhash)
   {
      $time = time();
      $time = "$time";
      //echo "pgd_debug Epoch ".substr($time, 0, -2)."\n";
      $salt  = base64_encode(substr($time, 0, -3));
      //echo "pgd_debug Salt ".$salt."\n";
      $nonce = sha1($user.$nameasHash);
      //echo "pgd_debug nonce ".$nonce."\n";

      $pass  = getUserPass($user);

      if ($pass)
      {
         $hash = sha1($user . ":" . $pass);
         $compare = sha1($nonce . $hash . $salt);
         //echo "pgd_debug hash ".$compare."\n";
         if (strcmp($providedhash, $compare) == 0)
           return 1;
         else
           return 0;
      }
      else
        return 0;
   }

   function IsBanned($guid) {
      $con = mysql_connect("localhost","","");
      if (!$con) {
         die('sql_error');
      }
      mysql_select_db("phantom7_PGDConnect", $con);
      $result = mysql_query("SELECT * FROM PermBans WHERE guid=$guid");
      while($row = mysql_fetch_array($result)) 
      {
         $expire = $row['Expre'];
         //in yyyymmdd
         $cur = gmdate("Ymd");
         if($expire > $cur) 
         {
            return $row['reason'];
         }
         else 
         {
            return 0;
         }
      }
   } 
   
   function ModRequiresAuth($MOD) {
      if($MOD == "TWM2") {
         return 1;
      }
      else if($MOD == "CnC4") {
         return 0;
      }
      else if($MOD == "Powers") {
         return 0;
      }
      else {
         return 0;
      }
   }
   
   function BadVersion($MOD, $VER) {
      if($MOD == "TWM2") {
         if($VER < 3.4) {
            return 1;
         }
         else {
            return 0;
         }
      }   
      return 0;
   }
   
   function isDev($guid) {
      if(strcmp($guid, "2000343") == 0) {
         return true;
      }
      else {
         return false;
      }
   }
   
   function getEXPCap() {
      $con = mysql_connect("localhost","","");
      if (!$con) {
        return -1;
      }
      // Create table
      mysql_select_db("phantom7_PGDConnect", $con);
      $result1 = mysql_query("SELECT * FROM TWM2Admin");
      $row1 = mysql_fetch_array($result1);  //1 row, lotsa data
      return $row1[RankCap];
   }
   
   function verifyFile($guid, $temp) {
      if(isDev($guid)) {
         //always good, no matter what
         return true;
      }
      //
      $current = date('Ymd');
      $hash = sha1($current);
      $len = strlen($hash) + 4;
      if(getEXPCap() <= 0) {
         return true; //no cap today, moving along....
      }
      //check if we are illegal
      $fh = fopen($temp, 'r');
      $theData = fread($fh, filesize($temp));
      fclose($fh);      
      $field = "xpGain".$hash;
      $expStart = strpos($theData , "".$field." = \"") + $len;
      $expEnd = strpos($theData ,"\";",$expStart); 
      $expLen = $expEnd - $expStart;  
      $expGain = substr(theData, $expStart, $expLen);    
      //
      if($expGain > getEXPCap()) {
         return false;
      }       
      // looks like we passed all checks, moving on
      return true;
   }
?>
