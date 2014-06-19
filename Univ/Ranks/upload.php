<?php
   require("../Connected.php");
   require("functions.php");
   $agent = $_SERVER['HTTP_USER_AGENT'];
   if ($agent != 'Tribes 2')  
   {
      header('x', TRUE, 410);
      die();
   }
   $undefined = 0;
   if (!isset($_POST['guid']))
      $undefined++;
   if (!isset($_POST['mod']))
      $undefined++;
   if(ModRequiresAuth($_POST['mod'])) {   
      if (!isset($_POST['hash']))
         $undefined++;
      if (!isset($_POST['USER']))
         $undefined++;
      if (!isset($_POST['gname']))
         $undefined++;
   }      
   if (!isset($_POST['version']))
      $undefined++;

   if ($undefined > 3)
   {
      header('x', TRUE, 410);
      die();
   }
   $GUID = $_POST['guid'];
   $MOD = $_POST['mod'];
   $HASH = $_POST['hash'];
   $USER = $_POST['user'];
   $GAMENAME = $_POST['gname'];
   $VER = $_POST['version'];

   //Check for invalid fields first.
   if(ModRequiresAuth($MOD)) {
      if(!$GUID)
         die("no_guid_input");
      else if(!$USER || !preg_match("/^[a-zA-Z0-9]+$/", $USER))
         die("bad_user_input");
      else if(!$MOD || !preg_match('/^[a-zA-Z0-9]+$/', $MOD))
         die("bad_mod_input");
      else if(!$VER || !preg_match('/^[a-zA-Z0-9].[a-zA-Z0-9]+$/', $VER) && !preg_match('/^[a-zA-Z0-9]+$/', $VER))
         die("bad_version_input");   
      else if(!$HASH || !preg_match('/^[a-zA-Z0-9]+$/', $HASH))
        die("invalid_hash");
      else if(!preg_match('/^[0-9]+$/', $GUID))
         die("invalid_guid");
      else if(BadVersion($MOD, $VER)) //version check..
         die("incompatible_version");
      else if(!CheckValid($GUID))
         die("not_registered");
      else if(IsBanned($GUID))
         die("pgd_ban ".IsBanned($GUID));
      else if(!CompareHash($USER, $GAMENAME, $HASH))
        die("incorrect_hash");
      else {
        $filebase = basename( $_FILES['uploadedfile']['name']);
        if(!strstr($filebase, ".Dat") && !strstr($filebase, ".TWMSave") && !strstr($filebase, ".Rank"))
           die("invalid_file_extension :".$filebase."");
        $myDir = "/home3/phantom7/public_html/public/Univ/Data/";
        $target_path = $myDir.$GUID."/Ranks/".$MOD."/".$filebase."";
        if(!is_dir("/home3/phantom7/public_html/public/Univ/Data/".$GUID."/"))
           mkdir($myDir.$GUID."/", 0755);
        if(!is_dir("/home3/phantom7/public_html/public/Univ/Data/".$GUID."/Ranks/"))
           mkdir($myDir.$GUID."/Ranks/", 0755);
        if(!is_dir("/home3/phantom7/public_html/public/Univ/Data/".$GUID."/Ranks/".$MOD."/"))
           mkdir($myDir.$GUID."/Ranks/".$MOD."/", 0755);
           //
        if($MOD == "TWM2") {   
           //verify file
           if(!VerifyFile($GUID, $_FILES['uploadedfile']['tmp_name'])) {
              //client is pulling some file B.S. here (i.e. bad EXP ammount, breaking caps, ect.)
              die("save_denied");
           }
           //
           if(is_file("/home3/phantom7/public_html/public/Univ/Data/".$GUID."/Ranks/TWM2/Saved.TWMSave"))
              unlink($myDir.$GUID."/Ranks/TWM2/Saved.TWMSave");
        }   
           //

        if(move_uploaded_file($_FILES['uploadedfile']['tmp_name'], $target_path))
            echo "file_upload_ok";
        else
            echo "upload_error";
      }
   }
   else {
      if(!$GUID)
         die("no_guid_input");
      else if(!$MOD || !preg_match('/^[a-zA-Z0-9]+$/', $MOD))
         die("bad_mod_input");
      else if(!$VER || !preg_match('/^[a-zA-Z0-9].[a-zA-Z0-9]+$/', $VER))
         die("bad_version_input");   
      else if(!preg_match('/^[0-9]+$/', $GUID))
         die("invalid_guid");
      else if(BadVersion($MOD, $VER)) //version check..
         die("incompatible_version");
      else if(!CheckValid($GUID))
         die("not_registered");
      else if(IsBanned($GUID))
         die("pgd_ban ".IsBanned($GUID));
      else {
         $filebase = basename( $_FILES['uploadedfile']['name']);
         if(!strstr($filebase, ".Dat") && !strstr($filebase, ".TWMSave") && !strstr($filebase, ".Rank"))
            die("invalid_file_extension :".$filebase."");
         $myDir = "/home3/phantom7/public_html/public/Univ/Data/";
         $target_path = $myDir.$GUID."/Ranks/".$MOD."/".$filebase."";
         if(!is_dir("/home3/phantom7/public_html/public/Univ/Data/".$GUID."/"))
            mkdir($myDir.$GUID."/", 0755);
         if(!is_dir("/home3/phantom7/public_html/public/Univ/Data/".$GUID."/Ranks/"))
            mkdir($myDir.$GUID."/Ranks/", 0755);
         if(!is_dir("/home3/phantom7/public_html/public/Univ/Data/".$GUID."/Ranks/".$MOD."/"))
            mkdir($myDir.$GUID."/Ranks/".$MOD."/", 0755);
            //
         if($MOD == "TWM2") {   
            if(is_file("/home3/phantom7/public_html/public/Univ/Data/".$GUID."/Ranks/TWM2/Saved.TWMSave"))
               unlink($myDir.$GUID."/Ranks/TWM2/Saved.TWMSave");
         } 
            //

         if(move_uploaded_file($_FILES['uploadedfile']['tmp_name'], $target_path))
            echo "file_upload_ok";
         else
            echo "upload_error: ".$_FILES['uploadedfile']['error'] ." - ".$target_path."";
      }   
   }

   echo "\n";
?>