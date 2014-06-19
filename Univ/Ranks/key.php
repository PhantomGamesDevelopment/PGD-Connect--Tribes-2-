<?php
   $agent = $_SERVER['HTTP_USER_AGENT'];
   if ($agent != 'Tribes 2') 
   {
      $time = date('m-d-Y, H:i:s');
      echo "<h1>WUT?</h1>";
      echo "<font face=\"verdana\">Allow me to ask. On the ".$time." you accessed this page trying to accomplish something. ";
      echo "I don't think you've got very far on that, as at this stage I have used the die(); command ";
      echo "to end the execution of the rest of this script, involving the upload of a file that is to be ";
      echo "stored in a directory for later access by TWM2 servers. Even if the die(); command didn't work ";
      echo "there is an if else block that will prevent anything else from happening if the statement is TRUE ";
      echo "In fact, why the hell is this file even relevant to what you're doing? this is only key.php ";
      echo "it will only return <b>yes</b> or <b>no</b>. if you want to TRY and do something, i suggest you ";
      echo "try and \"h4xor\" upload.php, which will do most of the described above. ";
      die("Thank you for wasting a few seconds of your life by reading the above, good bye!</font>");
   }
   else
   {
     require("functions.php"); // functions
  
     $HASH = $_POST['hash'];
     $USER = $_POST['user'];
     $GAMENAME = $_POST['gname'];
  
     if(!$USER || !preg_match('/^[a-zA-Z0-9]+$/', $USER))
     {
       die('no');
     }
     else if(!$HASH || !preg_match('/^[a-zA-Z0-9]+$/', $HASH)) 
     {
       die('no');
     }
     else if(!compareHash($USER, $GAMENAME, $HASH)) 
     {
       die('no');
     }
     else
     {
       die('yes');
     }
   }
?>
