<?php
   $count = 0;
   $dir = "/home/phantom7/public_html/public/Univ/twm2/patches/";
   if (is_dir($dir)) {
      if ($dh = opendir($dir)) {
         while (($file = readdir($dh)) !== false) {
            $count++;      
            $filed[$count] = $file;
         }
         closedir($dh); 
      }
   }        
   //
   for($i = 1; $i <= $count; $i++) {
      echo "$filed[$i]\n";
   }
?>