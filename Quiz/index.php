<?php

echo "Phantom Games Development, Admin Application";
//Connect to the database, see if apps are open
$con = mysql_connect("localhost","","");
if(!$con) {
   die('Could not connect: ' . mysql_error());
}
mysql_select_db("phantom7_Quiz", $con);   
$result = mysql_query("SELECT * FROM Admin");
$row = mysql_fetch_array($result);
echo "<br/>".$row["VersionString"]."";
echo "<br/>Created by Phantom139";

echo "<br/><br/><br/>";
if($row["ScriptEnabled"] == 0) {
   die("Applications are currently closed, please come back at some other time");
}

if($row["AdminOpen"] == 0 && $row["SAOpen"] == 0) {
   die("We are currently not hiring any new admins/SAs");
}
echo "Welcome, Please Complete The Following Fields";
echo "<br/>You will be tested after completing this form.";
echo "<form action=\"../Quiz/User/buildUser.php\" method=\"post\">
Your Real Name: <input type=\"text\" name=\"name\" />
<br/>E-Mail: <input type=\"text\" name=\"email\" />
<br/>Age: <input type=\"text\" name=\"age\" />
<br/>Cell Phone # (N/A if none): <input type=\"text\" name=\"cell\" />
<br/>Tribes 2 Name: <input type=\"text\" name=\"t2n\" />
<br/>Tribes 2 GUID: <input type=\"text\" name=\"guid\" />
<br/>Please Select a Position to apply For: <br/>";
if($row["AdminOpen"] == 1) {
echo "Admin <input type=\"radio\" name=\"afor\" value=\"admin\" /> ";
}
if($row["SAOpen"] == 1) {
echo "Super-Admin <input type=\"radio\" name=\"afor\" value=\"sa\" /> ";
}
echo "
<br/><br/><br/>
Free Response Questions: <br/>
<p>
1. Why should I consider you for this position? <br/>
<textarea cols=\"45\" rows=\"15\" name=\"frq1\">Response</textarea>
<p>
2. What do you find interesting about PGD Mods? <br/>
<textarea cols=\"45\" rows=\"15\" name=\"frq2\">Response</textarea>
<p>
3. What would you do if hired (how would you use your job)? <br/>
<textarea cols=\"45\" rows=\"15\" name=\"frq3\">Response</textarea>
<input type=\"submit\" />
</form>";
?>
