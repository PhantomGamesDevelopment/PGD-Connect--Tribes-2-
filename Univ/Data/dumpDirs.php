<?php
$dir = "/home/phantom7/public_html/public/Univ/Data/";

// Open a known directory, and proceed to read its contents
if (is_dir($dir)) {
    if ($dh = opendir($dir)) {
        while (($file = readdir($dh)) !== false) {
            echo "filename: $file : filetype: " . filetype($dir . $file) . "<p>";
        }
        closedir($dh);
    }
}
?>