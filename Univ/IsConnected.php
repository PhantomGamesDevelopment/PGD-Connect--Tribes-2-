<html>
<head>
</head>
<body>
<?php
require("Connected.php");
$GUID = $_GET['guid'];
if(isConnected($GUID)) {
   echo "yes";
}
else {
   echo "no";
}
?> 
</body>
</html>