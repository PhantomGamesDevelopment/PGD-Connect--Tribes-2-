<html>
<head>
<title>CnC Tiberium Uprising : Official Top Ranks</title>
</head>
<body>
<?php
   require("expScripts.php");
   echo "<b>CnC Tiberium Uprising : Official Universal Ranks</b><p>\n";
   
   DisplayAll(1);
   
   echo "***************";
   echo "<p><p>";
   GatherTopGDIRanks(-1, 1);
   echo "***************";
   echo "<p><p>";
   GatherTopNODRanks(-1, 1);  
   echo "***************"; 
?>
</body>
</html>