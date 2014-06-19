<?php
   $Check = $_GET["File"];
   $PathToCDir = "/home/phantom7/public_html/public/Univ/".$Check."";
   if(is_file($PathToCDir)) {
      echo 'Exists';
   }
   else {
      echo 'Does Not Exist';
   }
?>