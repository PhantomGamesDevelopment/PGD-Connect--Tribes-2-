<?php
   require("../Connected.php");
   $agent = $_SERVER['HTTP_USER_AGENT'];
   //echo "Connect From: ".$agent."<p>";
   if($agent != "Tribes 2")  {
      header("location: http://www.chodecircus.com/area51/");
      die ("What were you trying to achieve by accessing this file on this server?");
   }

   $GUID = $_POST["guid"];
   $filebase = basename( $_FILES['uploadedfile']['name']);

   if(!$GUID) {
      die("no_guid_input");
   }
   else if (!preg_match('/^[0-9]+$/', $GUID)) {
      die("badly_formatted_guid");
   }
   else if (!CheckValid($GUID)) {
      die("not_registered");
   }
   else {
     //echo "GUID ACCESS: ".$GUID." <p>";
     //echo "FILE ACCESS: ".$_FILES['uploadedfile']['name']." <p>";

     if(!strstr($filebase, ".cs")) {
        die("bad_extension");
     }   
     
     $target_path = "/home/phantom7/public_html/public/Univ/Data/".$GUID."/Buildings/".$filebase."";
     
     //echo "File base: ".$filebase." <p>";
     if(!is_dir("/home/phantom7/public_html/public/Univ/Data/".$GUID."/")) {
        mkdir("/home/phantom7/public_html/public/Univ/Data/".$GUID."/", 0755);
     }
     if(!is_dir("/home/phantom7/public_html/public/Univ/Data/".$GUID."/Buildings/")) {
        mkdir("/home/phantom7/public_html/public/Univ/Data/".$GUID."/Buildings/", 0755);
     }   
  
     if(move_uploaded_file($_FILES['uploadedfile']['tmp_name'], $target_path)) {
         //echo "The file ".  basename( $_FILES['uploadedfile']['name']).
         //" has been uploaded";
         echo "file_upload_ok";
     } 
     else  {
         echo "upload_error";
     }
   }
?>