<?php
$con = mysql_connect("localhost","","");
if (!$con) {
  die('Could not connect: ' . mysql_error());
}
mysql_select_db("phantom7_PGDConnect", $con);
//check these first
if(!isSet($_POST[email])) {
   die('Error: you need to enter an email address');
}
if(!isSet($_POST[guid])) {
   die('Error: you need to enter a tribes 2 GUID');
}

if(strlen($_POST[guid]) != 7) {
   die("Invalid Entry, Please Try Again.");
}

//
$ip = $_SERVER['REMOTE_ADDR'];
$ipBans = mysql_query("SELECT * FROM IPBan");
while($row3 = mysql_fetch_array($ipBans)) {
   if($row3[ip] == $ip && $_SERVER['HTTP_USER_AGENT'] != "Tribes 2")
      die("You are not permitted to do this action, please contact the PGD staff for assistance if needed.");
}

$result1 = mysql_query("SELECT * FROM Data");
while($row1 = mysql_fetch_array($result1)) {
  //bar them access to this page again.
  if($row1[IP] == $ip && $_SERVER['HTTP_USER_AGENT'] != "Tribes 2") {
     $sql="INSERT INTO IPBan (ip)
         VALUES
     ('$ip')";
     if (!mysql_query($sql,$con)) {
        die('Error: ' . mysql_error());
     }
     //
     die("Error: You are forbidden from creating another acount, goodbye \n If you require a second account, please contact the PGD Staff");
  }
  else if($row1[GUID] == $_POST[guid]) {
     die("This account is already registered.");
  }
}

$email = $_POST[email];
$emailzor = str_replace('@', 'at', $email); 

mysql_select_db("phantom7_PGDConnect", $con);

   //generate their keycode (password)
   $keycode = rand(10000000, 99999999);
   //
   $sql="INSERT INTO Data (guid, email, password, Authenticated, IP)
   VALUES
   ('$_POST[guid]','$emailzor','$keycode',0, '$ip')";

   if (!mysql_query($sql,$con)) {
     die('Error: ' . mysql_error());
   }
   echo "Data added to PGD Connect<p>";
   echo "Congratulations, your account is now connected!<p>";
   echo "This is your keycode (Password): ".$keycode."<p>";
   echo "we recommend writing it down somewhere.";

mysql_close($con);
?> 
