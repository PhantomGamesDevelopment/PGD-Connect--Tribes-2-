//Patchloader.cs
//Phantom139

//For TWM2 3.03 and Beyond
//Loads all scripts and packages in the /scripts/TWM2/patch/ folder
function loadPatches() {
   %search = "scripts/TWM2/patch/*.cs";
   for(%file = findFirstFile(%search); %file !$= ""; %file = findNextFile(%search)) {
      %type = fileBase(%file); // get the name of the script
      exec("scripts/TWM2/patch/" @ %type @ ".cs");
   }
}
loadPatches();
