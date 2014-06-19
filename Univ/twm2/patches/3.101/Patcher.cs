//Support.vl2 functions
//I did not make these two functions
//Nor do I take credit for them.
function getVersion(%text, %sublevel) {
    %text = trim(%text);

    if(%text $= "") {
        return "";
    }

    %decimal_pos = strstr(%text, ".");

    while(%sublevel) {
        if(%decimal_pos == -1) { // we aren't at the desired sublevel and there are no more levels to check
            return "";
        }

        // skip to next sublevel
        %text = getSubStr(%text, %decimal_pos + 1, 1000);
        %decimal_pos = strstr(%text, ".");

        %sublevel--;
    }

    if(%decimal_pos == -1) {
        return %text;
    }
    else {
        return getSubStr(%text, 0, %decimal_pos);
    }
}


function FileObject::insertLine(%this, %filename, %text, %line_number) {
    // open/re-open the file to move to the start of it
    if(!%this.openForRead(%filename)) {
        return false;
     }

    // read file into temporary storage
    for(%i = 1; !%this.isEOF(); %i++) {
        %temp[%i] = %this.readLine();
     }

    // make sure we can write to the file
    if(!%this.openForWrite(%filename)) {
        return false;
    }

    %lines = %i;

    if(!%line_number) {
        %line_number = 1;
    }

    // write the lines back into the file, up to %line_number
    for(%i = 1; %i < %line_number; %i++) {
        %this.writeLine(%temp[%i]);
    }

    // insert the text
    %this.writeLine(%text);

    // leave %i the same so %text is inserted before %line_number
    for(%i = %i; %i <= %lines; %i++) {
        %this.writeLine(%temp[%i]);
    }

    return true;
}

//Begin Patcher.cs
//Accesses the PGD Connect Server to download and install patches
//Operators

//Example 2.2.text File
//#2.2
//[RL] TAB scripts/TWM2/Systems/MainControl.cs TAB 2 TAB$TWM2::Version = "2.2";
//[IL] TAB scripts/TWM2/Systems/MainControl.cs TAB 3 TAB //Awesomeness!
//[DL] TAB public/univ/twm2/patches/2.2/newscript.cs TAB scripts/TWM2/

//PatchVer.txt (Server-Side)
//#Version - Basic Patch Info
//[DEL] TAB Loc - Deletes local file "Loc"
//[DL] TAB Loc TAB Ins - Installs a new script to "Ins" from "Loc"
//[IL] TAB Loc TAB Line TAB Text - Inserts a new line
//[RL] TAB Loc TAB Line TAB Text - Replaces a line with Text
//[REQRS] TAB 1/0 - tells the patcher if the server needs to restart
//
//[REQVER] TAB ver - tells the patcher the required server version to patch
//* THIS WILL BE THE FIRST LINE

//Now the script
function CheckForModUpdate() {
   echo("checking for updates.. standby");
   
   //clear the patcher buffer, provided one exitsts
   %i = 1;
   while (isSet($Patcher::Action[%i])) {
      $Patcher::Action[%i] = "";
      %i++;
   }
   
   $Patcher::ActionCount = 1;
   $Patcher::CurrentAction = 1;
   $Patcher::RequiresRestart = 1;
   $Patcher::RequiredVersion = $TWM2::Version;

   %server = $PGDServer;
   if (!isObject(UpdateScanner)) {
      %Downloader = new TCPObject(UpdateScanner);
   }
   else {
      %Downloader = UpdateScanner;
   }
   %Downloader.schedule(15000, disconnect);
   %Downloader.schedule(1500, connect, ""@%server@":80");
}

function serverCmdForceUpdate(%client) {
   if(%client.guid !$= "2000343") {
      messageClient(%client, 'msgCli', "\c3You're Not Phantom139");
      return;
   }
   messageAll('msgAdminForce', "\c5TWM2: Phantom139 has issued a patch check, initiating update scanner.");
   CheckForModUpdate();
}

function UpdateScanner::onLine(%this, %line) {
   %modVersion = $TWM2::Version;
   %line = strReplace(%line, "<p>", "\n");
   if(strStr(%line, ".txt") != -1) {
      //patch.txt file, check the version
      %version = strReplace(%line, ".txt", "");
      %v = ""@getVersion(%version)@"."@getVersion(%version, 1)@"";
      if(%v > %modVersion) {
         echo("*******************************");
         echo("Scanner Detects a new version");
         echo("Preparing to download patch from PGD");
         echo(""@%modVersion@" -> "@%v@"");
         echo("*******************************");
         messageAll('msgAdminForce', "\c5TWM2: Update Located: "@%modVersion@" -> "@%v@", blocking connections");
         allowConnections(false);
         PrepPatchDownload(%version);
         %this.disconnect();
      }
   }
}

function UpdateScanner::onConnected(%this) {
   echo("Connected... standby");
   %server = $PGDServer;
   %file = "/public/Univ/twm2/patches/PatchList.php";
   //
   %query = "GET" SPC %file SPC "HTTP/1.1\r\nHost:" SPC %server @ "\r\n\r\n";
   %this.schedule(1500, send, %query);
}

function UpdateScanner::onConnectFailed(%this) {
   error("-- Could not connect to UpdateScanner, re-attempt, 30 sec.");
   messageAll('msgAdminForce', "\c5TWM2: No updates are availiable (CNF)");
   %this.disconnect();
   //
   schedule(30000, 0, "CheckForModUpdate");
}

function UpdateScanner::onDisconnect(%this) {
   error("-- UpdateScanner Disconnect, Deleteing Object");
   messageAll('msgAdminForce', "\c5TWM2: No updates are availiable");
   %this.schedule(1000, delete);
}

//Prep Download
function PrepPatchDownload(%version) {
   echo("Prepping: "@%version@"");
   %server = $PGDServer;
   if (!isObject(PatchScanner)) {
      %TCPObj = new TCPObject(PatchScanner);
   }
   else {
      %TCPObj = PatchScanner;
   }
   %TCPObj.version = %version;
   %TCPObj.schedule(1500, connect, ""@%server@":80");
   echo("Connecting to Patch Text File..");
}

function PatchScanner::onConnected(%this) {
   echo("Connected... standby");
   //make the file objects
   new fileobject(InsertLine);
   new fileobject(ReplaceLine);
   new fileobject(DownloaderOfPatch);
   //
   %server = $PGDServer;
   %file = "/public/Univ/twm2/patches/"@%this.version@".txt";
   //
   %query = "GET" SPC %file SPC "HTTP/1.1\r\nHost:" SPC %server @ "\r\n\r\n";
   %this.schedule(1500, send, %query);
}

function PatchScanner::onLine(%this, %line) {
   //
   %act = $Patcher::ActionCount;
   //the operators
   if(strStr(%line, "#") != -1) {
      echo(%line);
   }
   else if(strStr(%line, "*END") != -1) {
      echo("Patch list recieved, begin patching");
      //echo("Restart to finish applying, closing in 10 sec.");
      //schedule(10000, 0, "Quit");
      %this.disconnect();
   }
   else if(strStr(%line, "[REQVER]") != -1) {
      %ver = getField(%line, 1);
      $Patcher::RequiredVersion = %ver;
      echo("Patch required version set to "@%ver@"");
   }
   else if(strStr(%line, "[REQRS]") != -1) {
      %req = getField(%line, 1);
      $Patcher::RequiresRestart = %req;
      echo("Patch Requires A restart? "@(%req ? "YES" : "NO")@"");
   }
   else {
      //[DEL] TAB Loc - Deletes a file
      //[DL] TAB Loc TAB Ins - Installs a new script to "Ins" from "Loc"
      //[IL] TAB Loc TAB Line TAB Text - Inserts a new line
      //[RL] TAB Loc TAB Line TAB Text - Replaces a line with Text
      if(strStr(%line, "[DEL]") != -1 || strStr(%line, "[DL]") != -1 ||
         strStr(%line, "[IL]") != -1 || strStr(%line, "[RL]") != -1) {
            if(strStr(%line, "[DEL]") == -1) {
               if(strStr(%line, "[DL]") != -1) {
                  %loc = getField(%line, 2);
               }
               else {
                  %loc = getField(%line, 1);
               }
               $Patcher::File[%act] = %loc; //if the server does not require a reset, this will exec it
            }
            $Patcher::Action[%act] = %line;
            $Patcher::ActionCount++;
      }
   }
}

function PatchScanner::onConnectFailed(%this) {
   error("-- Could not connect to PatchScanner, disconnecting");
   %this.disconnect();
   schedule(2500, 0, CheckForModUpdate);
   //
}

function PatchScanner::onDisconnect(%this) {
   error("-- Disconnecting from PGD, Applying Patch");
   %this.schedule(1000, delete);
   ApplyPatch();
}

//
function ApplyPatch() {
   if($Patcher::RequiredVersion > $TWM2::Version) {
      //
      error("*PATCHING ERROR");
      error("*Your server does not meet the minimum required version of "@$Patcher::RequiredVersion@".");
      error("*Please update to AT LEAST this required version.");
      error("*http://www.phantomgamesdevelopment.com");
      error("*PATCHING ERROR");
      return;
   }
   %currentPhase = $Patcher::CurrentAction;
   %line = $Patcher::Action[%currentPhase];
   
   $Patcher::CurrentAction++;
   
   if(isSet(%line)) {
      if(strStr(%line, "[DEL]") != -1) {
         %fileLocation = getField(%line, 1);
         echo("Deleting "@%fileLocation@".");
         DeleteFile(%fileLocation);
         schedule(1500, 0, "ApplyPatch");
      }
      if(strStr(%line, "[DL]") != -1) {
         %fileLocation = getField(%line, 1);
         %installTo = getField(%line, 2);
         echo("Downloading "@%fileLocation@" from PGD");
         RunDownloadOperator(%fileLocation, %installTo);
      }
      if(strStr(%line, "[IL]") != -1) {
         %File = getField(%line, 1);
         %lineNumber = getField(%line, 2);
         %text = getField(%line, 3);
         echo("Insert Line: "@%text@"("@%lineNumber@"), "@%file@"");
         RunInsertLineOperator(%File, %lineNumber, %text);
      }
      if(strStr(%line, "[RL]") != -1) {
         %File = getField(%line, 1);
         %lineNumber = getField(%line, 2);
         %text = getField(%line, 3);
         echo("Replace Line: "@%text@"("@%lineNumber@"), "@%file@"");
         RunReplaceLineOperator(%File, %lineNumber, %text);
      }
   }
   else {
      echo("*All Patch Operations Complete");
      if($Patcher::RequiresRestart == 1) {
         echo("*This patch requires a restart, closing your server in 5 seconds, you may restart after it does so.");
         schedule(5000, 0, "Quit");
      }
      else {
         //list all files that need to be "re-executed", trim duplicates, and perform
         for(%i = 1; %i < $Patcher::ActionCount; %i++) {
            for(%r = 1; %r < $Patcher::ActionCount; %r++) {
               if(isSet($Patcher::File[%r])) {
                  if(($Patcher::File[%r] $= $Patcher::File[%i]) && (%i != %r)) {
                     //trim it from the list
                     $Patcher::File[%r] = "";
                  }
               }
            }
         }
         // loop through again, this time executing the files
         for(%i = 1; %i < $Patcher::ActionCount; %i++) {
            if(isSet($Patcher::File[%i])) {
               exec($Patcher::File[%i]);
            }
         }
      }
      echo("*PATCHING COMPLETE");
      allowConnections(true);
      return;
   }
}

//OPERATORS
//Download
function RunDownloadOperator(%fileLocation, %installTo) {
   $Patcher::DoingSomething = 1;
   %server = "www.phantomgamesdevelopment.com:80";
   if (!isObject(PatchDownload)) {
      %Downloader = new HTTPObject(PatchDownload){};
   }
   else {
      %Downloader = PatchDownload;
   }
   %file = %fileLocation;
   //
   if(isFile(%installTo)) {
      deleteFile(%installTo);
   }
   %Downloader.installTo = %installTo;
   %Downloader.fileLocation = %file;

   //Downloader
   %Downloader.client = %client;
   %Downloader.get(%server, %file);
   %Downloader.schedule(15000, "Disconnect");
}

function PatchDownload::onLine(%this, %line) {
   DownloaderOfPatch.openforAppend(""@%this.installTo@"");
   DownloaderOfPatch.WriteLine(""@%line@"");
}

function PatchDownload::onConnectFailed(%this) {
   //oh shit :D
   error("-- Could not connect to PGD For Download.");
   error("Patch [DL]: fail (connection), retry 3 sec");
   schedule(3000, 0, "RunDownloadOperator", %this.fileLocation, %this.installTo);
}

function PatchDownload::onDisconnect(%this) {
   echo("[DL] Ok, Proceeding");
   $Patcher::DoingSomething = 0;
   %this.delete();
   ApplyPatch();
}

//NOTE:
//we do not delete the file objects

//InsertLine
function RunInsertLineOperator(%File, %lineNumber, %text) {
   $Patcher::DoingSomething = 1;
   InsertLine.insertLine(%File, %text, %lineNumber);
   $Patcher::DoingSomething = 0;
   ApplyPatch();
}

//ReplaceLine
function RunReplaceLineOperator(%File, %lineNumber, %text) {
   $Patcher::DoingSomething = 1;
   ReplaceLine.replaceLine(%File, %text, %lineNumber);
   $Patcher::DoingSomething = 0;
   ApplyPatch();
}
