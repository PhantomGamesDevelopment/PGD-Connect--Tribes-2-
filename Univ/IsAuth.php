<?php

$GUID = $_GET[guid];

$con = mysql_connect("localhost","","");
if (!$con) {
  die('Could not connect: ' . mysql_error());
}
mysql_select_db("phantom7_PGDConnect", $con);

$result = mysql_query("SELECT * FROM Data WHERE guid='$GUID' AND Authenticated=1");
if (!$result)
echo "Query failed! " . mysql_error();

while($row1 = mysql_fetch_array($result)) {
  $match1 = $row1[guid];
}

if($match1) {
   echo "Authenticated";
}
else {
   echo "Not";
}
?>
