<html>
<head>
<title>PGD Connect Account</title>
</head>
<body>
<?php

//lets start by checking the data they provided us
$email = $_POST[email];
$emailzor = str_replace('@', 'at', $email); 

$pass = $_POST["password"];

if($email == '' || $pass == '') {
   die('You forgot to enter an email address or the keycode.');
}

echo "PGD Connect 1.7<p>";

$con = mysql_connect("localhost","","");
if (!$con) {
  die('Could not connect: ' . mysql_error());
}
mysql_select_db("phantom7_PGDConnect", $con);

$result = mysql_query("SELECT * FROM Data WHERE email='$emailzor' AND password=$pass");
if (!$result)
echo "Query failed! " . mysql_error();

while($row1 = mysql_fetch_array($result)) {
  $match1 = $row1[email];
}

if($match1) {
   echo "Successful login.<p>";
   fullaccount($emailzor);
}
else {
   die('Invalid email or keycode given');
}

function fullaccount($email) {
echo "Loading Account...<p>";
$con = mysql_connect("localhost","","");
if (!$con) {
  die('Could not connect: ' . mysql_error());
}
mysql_select_db("phantom7_PGDConnect", $con);

$result = mysql_query("SELECT * FROM Data WHERE email='$email'");
if (!$result)
echo "Query failed! " . mysql_error();

while($row1 = mysql_fetch_array($result)) {
   $result2 = mysql_query("SELECT * FROM Data WHERE guid='$row1[guid]'");
   if (!$result2)
      echo "Query failed! " . mysql_error();

   while($row2 = mysql_fetch_array($result2)) {
      $match2 = $row2[Authenticated];
   }
   if($match2) {
      $authStr = "Authenticated: Full Access<p>";
   }
   else {
      $authStr = "Authenticated: Limited Access, Please Authenticate In Game<p>";
   }
   echo "Tribes 2 GUID ".$row1[guid]." - <a href=delinkmenu.php?guid=".$row1[guid].">[Unlink]</a> - <a href=Data/".$row1[guid]."/Buildings/>[Buildings]</a> - <a href=Data/".$row1[guid]."/Ranks/>[Ranks]</a>. ".$authStr."<p>";
}

}

?>
</body>
</html>
