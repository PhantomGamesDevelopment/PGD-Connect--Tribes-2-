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

//Now the script
function CheckForModUpdate() {
   echo("checking for updates.. standby");

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
   %this.disconnect();
   //
   schedule(30000, 0, "CheckForModUpdate");
}

function UpdateScanner::onDisconnect(%this) {
   error("-- UpdateScanner Disconnect, Deleteing Object");
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
   //the operators
   if(strStr(%line, "#") != -1) {
      echo(%line);
   }
   //[DEL] TAB Loc - Deletes a file
   if(strStr(%line, "[DEL]") != -1) {
      %fileLocation = getField(%line, 1);
      echo("Deleting "@%fileLocation@".");
      DeleteFile(%fileLocation);
   }
   //[DL] TAB Loc TAB Ins - Installs a new script to "Ins" from "Loc"
   if(strStr(%line, "[DL]") != -1) {
      %fileLocation = getField(%line, 1);
      %installTo = getField(%line, 2);
      echo("Downloading "@%fileLocation@" from PGD");
      RunDownloadOperator(%fileLocation, %installTo);
   }
   //[IL] TAB Loc TAB Line TAB Text - Inserts a new line
   if(strStr(%line, "[IL]") != -1) {
      %File = getField(%line, 1);
      %lineNumber = getField(%line, 2);
      %text = getField(%line, 3);
      echo("Insert Line: "@%text@"("@%lineNumber@"), "@%file@"");
      RunInsertLineOperator(%File, %lineNumber, %text);
   }
   //[RL] TAB Loc TAB Line TAB Text - Replaces a line with Text
   if(strStr(%line, "[RL]") != -1) {
      %File = getField(%line, 1);
      %lineNumber = getField(%line, 2);
      %text = getField(%line, 3);
      echo("Replace Line: "@%text@"("@%lineNumber@"), "@%file@"");
      RunReplaceLineOperator(%File, %lineNumber, %text);
   }
   if(strStr(%line, "*END") != -1) {
      echo("Patching complete...");
      echo("Restart to finish applying, closing in 10 sec.");
      schedule(10000, 0, "Quit");
      %this.disconnect();
   }
}

function PatchScanner::onConnectFailed(%this) {
   error("-- Could not connect to PatchScanner, disconnecting");
   %this.disconnect();
   schedule(2500, 0, CheckForModUpdate);
   //
}

function PatchScanner::onDisconnect(%this) {
   error("-- PatchScanner Disconnect, Deleteing Object");
   %this.schedule(1000, delete);
}

//OPERATORS
//Download
function RunDownloadOperator(%fileLocation, %installTo) {
   %server = "www.phantomgamesdevelopment.com:80";
   if (!isObject(PatchDownload)) {
      %Downloader = new HTTPObject(PatchDownload){};
   }
   else {
      %Downloader = PatchDownload;
   }
   %file = %fileLocation;
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
   %this.delete();
}

//NOTE:
//we do not delete the file objects

//InsertLine
function RunInsertLineOperator(%File, %lineNumber, %text) {
   InsertLine.insertLine(%File, %text, %lineNumber);
}

//ReplaceLine
function RunReplaceLineOperator(%File, %lineNumber, %text) {
   ReplaceLine.replaceLine(%File, %text, %lineNumber);
}
