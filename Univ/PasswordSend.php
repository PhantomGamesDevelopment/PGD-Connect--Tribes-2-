<?php
$get = $_POST["email"];
if(!$get) {
   die("Whoops! you forgot to enter the email address!");
}
$goto= str_replace('@', 'at', $get); 

$con = mysql_connect("localhost","","");
if (!$con) {
  die('Could not connect: ' . mysql_error());
}
mysql_select_db("phantom7_PGDConnect", $con);

$result = mysql_query("SELECT * FROM Data WHERE email='$goto'");
if (!$result)
echo "Query failed! " . mysql_error();

while($row1 = mysql_fetch_array($result)) {
  $match1 = $row1[email];
  $code = $row1[password];
}

if(!$match1) {
   die("I'm afraid that email address is not in our database, please register!");
}

$to = $get;
$from = "no-reply@phantomdev.net";
$subject = "PGD Connect Passcode";
$body = "Hello \n We have recieved a passcode delivery request \n This is your PGD Code: ".$code.".";
if (mail($to, $subject, $body,"From:".$from."")) {
  echo("<p>Message successfully sent!</p>");
} 
else {
  echo("<p>Message delivery failed...</p>");
}
?>
