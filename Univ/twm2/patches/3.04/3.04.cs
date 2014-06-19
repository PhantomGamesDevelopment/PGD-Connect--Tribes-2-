//PATCH 3.04
//TOTAL WARFARE MOD 2 ADVANCED WARFARE

//BY PHANTOM139

//Change Log:
// *Added /depSec (Piece Security)
// *Settings are now kept on univ-rank load
// *Patched perk/killstreak selection to check for the requirement as well
// *Updated the settings saver
// *Patches an error with the medal saver, it didn't update the rank first

package TWM2Patch3_04 {
   function ccHelp(%sender, %admin) {
      messageclient(%sender, 'MsgClient', "\c5TWM2 Chat Commands.");
      messageclient(%sender, 'MsgClient', "\c3/cmdHelp, /nameSlot, /me, /me1, /me2, /me3");
      messageclient(%sender, 'MsgClient', "\c3/me4, /me5, /r, /giveCard, /TakeCard, /bf");
      messageclient(%sender, 'MsgClient', "\c3/getScale, /getObj, /pm, /OpenDoor, /setPass");
      messageclient(%sender, 'MsgClient', "\c3/setSpawn, /clearSpawn, /delMyPieces, /name");
      messageclient(%sender, 'MsgClient', "\c3/scale, /objmove, /del, /givePieces, /power");
      messageclient(%sender, 'MsgClient', "\c3/hover, /moveAll, /Radius, /admincmds, /sacmds");
      messageclient(%sender, 'MsgClient', "\c3/objPower, /idea, /Timer, /setRot, /setNudge");
      messageclient(%sender, 'MsgClient', "\c3/getGUID, /voteBoss, /myPhrase, /whois, /depSec");
      return 1;
   }

   function ccDepSec(%sender, %args) {
      %statString = %sender.pieceSecured ? "are no longer" : "are now";
      %sender.pieceSecured = !%sender.pieceSecured;
      messageClient(%sender, 'msgClient', "\c3Deploy rights on your pieces "@%statString@" secured.");
      return 1;
   }
   
   function ShapeBaseImageData::testDeploySecure(%item, %plyr) {
      %mask = $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType
       | $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType
       | $TypeMasks::ForceFieldObjectType | $TypeMasks::StaticObjectType;

      InitContainerRadiusSearch(%item.surfacePt, 4, %mask);
      while ((%hitObj = containerSearchNext()) != 0) {
         if(%hitObj.squareSize !$= "") {
            return false; //terrain!
         }
         %owner = %hitObj.getOwner();

         if(%owner != %plyr.client) {
            //echo(%hitObj.getOwner());
            if(%owner.pieceSecured) {
               return true;
            }
            else {
               return false;
            }
         }
         else {
            return false;
         }
      }
   }
   
   function ShapeBaseImageData::testInvalidDeployConditions(%item, %plyr, %slot) {
	  cancel(%plyr.deployCheckThread);
	  %disqualified = $NotDeployableReason::None;  //default
	  $MaxDeployDistance = %item.maxDeployDis;
	  $MinDeployDistance = %item.minDeployDis;

	  %pack = %plyr.getMountedImage($BackpackSlot);

	  if (%pack.deployed.className $= "decoration") {
         if (%plyr.packSet == 6) {
            $MinDeployDistance = 2;
         }
		 if (%plyr.packSet == 10) {
            $MinDeployDistance = 5;
         }
		 if (%plyr.packSet == 11) {
            $MinDeployDistance = 4;
         }
	  }

	  %surface = Deployables::searchView(%plyr, $MaxDeployDistance, ($TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::StaticShapeObjectType));
	  if (%surface) {
         %surfacePt  = posFromRaycast(%surface);
		 %surfaceNrm = normalFromRaycast(%surface);

		 // Check that point to see if anything is obstructing it...
		 %eyeTrans = %plyr.getEyeTransform();
		 %eyePos   = posFromTransform(%eyeTrans);

		 %searchResult = containerRayCast(%eyePos, %surfacePt, -1, %plyr);
		 if (!%searchResult) {
            %item.surface = %surface;
			%item.surfacePt = %surfacePt;
			%item.surfaceNrm = %surfaceNrm;
		 }
		 else {
            if (checkPositions(%surfacePT, posFromRaycast(%searchResult))) {
			   %item.surface = %surface;
			   %item.surfacePt = %surfacePt;
			   %item.surfaceNrm = %surfaceNrm;
            }
			else {
			   if (%searchResult.getType() & $TypeMasks::WaterObjectType) {
			      %item.surface = %surface;
			      %item.surfacePt = %surfacePt;
			      %item.surfaceNrm = %surfaceNrm;
			   }
			   else {
			      %disqualified = $NotDeployableReason::MaxDeployed;
               }
            }
		 }
		 if (!getTerrainAngle(%surfaceNrm) && %item.flatMaxDeployDis !$= "") {
            $MaxDeployDistance = %item.flatMaxDeployDis;
			$MinDeployDistance = %item.flatMinDeployDis;
 		 }
      }

	  %item.surfaceinher = 0;
	  if (%item.surface.needsFit == 1) {
         %item.surfaceinher = 1;
		%mask = invFace(%item.surfaceNrm);
                %narrower = vectorMultiply(%mask,%item.surface.getRealSize());
		%subject = vectorNormalize(topVec(%narrower));
                %item.surfaceNrm2 = realVec(%item.surface,%subject);
		%item.surfaceNrm = VectorNormalize(realVec(%item.surface,%surfaceNrm));
		%item.surfaceNrm2 = VectorNormalize(%item.surfaceNrm2);
		%mCenter = "0 0 -0.5";
		%className = %item.surface.getDataBlock().className;
		if (%className !$= "tree" && %className !$= "crate" && %className !$= "vpad") {
			%item.surfacePt = link(%item.surface,%surfaceNrm,%surfacePt,VectorScale(getjoint(%item),0.5),%mCenter);
        }
        if (%className $= "decoration") {
		   if (%item.surface.getDataBlock().getName() $= "DeployedDecoration6") {
		      %item.surfacePt = vectorAdd(%item.surfacePt,vectorScale(realVec(%item.surface,"0 0 1"),3.3));
			  %item.surfaceNrm = realVec(%item.surface,"0 0 1");
		   }
        }
	}

    if (%item.testMaxDeployed(%plyr)) {
      %disqualified = $NotDeployableReason::MaxDeployed;
    }
    else if (%item.testNoSurfaceInRange(%plyr)) {
      %disqualified = $NotDeployableReason::NoSurfaceFound;
    }
    else if (%item.testNoTerrainFound(%surface)) {
      %disqualified = $NotDeployableReason::NoTerrainFound;
    }
    else if (%item.testNoInteriorFound()) {
      %disqualified = $NotDeployableReason::NoInteriorFound;
    }
    else if (%item.testSurfaceTooNarrow(%surface)) {
      %disqualified = $NotDeployableReason::SurfaceTooNarrow;
    }
    else if (%item.testSlopeTooGreat(%surface, %surfaceNrm)) {
      %disqualified = $NotDeployableReason::SlopeTooGreat;
    }
    else if (%item.testSelfTooClose(%plyr, %surfacePt)) {
      %disqualified = $NotDeployableReason::SelfTooClose;
    }
    else if (%item.testObjectTooClose(%surfacePt,%plyr)) {
      %disqualified = $NotDeployableReason::ObjectTooClose;
    }
    else if (%item.testTurretTooClose(%plyr) && $Host::Purebuild != 1) {
      %disqualified = $NotDeployableReason::TurretTooClose;
    }
    else if (%item.testInventoryTooClose(%plyr)) {
      %disqualified = $NotDeployableReason::InventoryTooClose;
    }
    else if (%item.testTurretSaturation() && $Host::Purebuild != 1) {
      %disqualified = $NotDeployableReason::TurretSaturation;
    }
    else if (%item.testDeploySecure(%plyr)) {
      %disqualified = $NotDeployableReason::CantDeploySecure;
    }
    else if (%disqualified == $NotDeployableReason::None) {
      // Test that there are no obstructing objects that this object
      //  will intersect with
      //
      %rot = %item.getInitialRotation(%plyr);
      if (%item.deployed.className $= "DeployedTurret") {
         %xform = %item.deployed.getDeployTransform(%item.surfacePt, %item.surfaceNrm);
      }
      else {
         %xform = %surfacePt SPC %rot;
      }
    }
    if (%plyr.getMountedImage($BackpackSlot) == %item) {
       if (%disqualified)
          activateDeploySensorRed(%plyr);
       else
          activateDeploySensorGrn(%plyr);

	   if (%plyr.client.deployPack == true) {
          %item.attemptDeploy(%plyr, %slot, %disqualified);
	   }
       else {
          %plyr.deployCheckThread = %item.schedule(25, "testInvalidDeployConditions", %plyr, %slot); //update checks every 50 milliseconds
       }
    }
    else {
       deactivateDeploySensor(%plyr);
    }
   }
   
   function Deployables::displayErrorMsg(%item, %plyr, %slot, %error) {
      deactivateDeploySensor(%plyr);

      %errorSnd = '~wfx/misc/misc.error.wav';
      switch (%error) {
         case $NotDeployableReason::None:
		   if (isObject(%plyr.client)) {
			   if (!spamCheck(%plyr.client)) {
				   %deplObj = %item.onDeploy(%plyr, %slot);
				   %deplObj.depTime = getSimTime();
				   // TODO - temporary - remove
				   if ($TimedDisCenter !$= "") {
					   if (vectorLen(vectorDist($TimedDisCenter,%deplObj.getPosition())) < 150 && %plyr.client != nameToID(LocalClientConnection))
				   	    	%deplObj.timedDis = %deplObj.getDataBlock().schedule(1 * 5 * 1000,disassemble,0,%deplObj);
				   }
				   // ----
				   messageClient(%plyr.client, 'MsgTeamDeploySuccess', "");
				   return;
			   }
			   else if ($Host::Prison::DeploySpamRemoveRecentMS !$= "" && $Host::Prison::DeploySpamRemoveRecentMS > 0) {
				   %group = nameToID("MissionCleanup/Deployables");
				   %count = %group.getCount();
				   for(%i=0;%i<%count;%i++) {
					   %obj = %group.getObject(%i);
					   if (%obj.getOwner() == %plyr.client && getSimTime() - $Host::Prison::DeploySpamRemoveRecentMS < %obj.depTime) {
					    	%obj.getDataBlock().schedule(50 * %disCount++,disassemble,%plyr,%obj);
					   }
				   }
			   }
		   }
		   else {
			   %item.onDeploy(%plyr, %slot);
			   messageClient(%plyr.client, 'MsgTeamDeploySuccess', "");
			   return;
		   }
         case $NotDeployableReason::NoSurfaceFound:
            %msg = '\c2Item must be placed within reach.%1';

         case $NotDeployableReason::MaxDeployed:
            %msg = '\c2Your team\'s control network has reached its capacity for this item.%1';

         case $NotDeployableReason::SlopeTooGreat:
            %msg = '\c2Surface is too steep to place this item on.%1';

         case $NotDeployableReason::SelfTooClose:
            %msg = '\c2You are too close to the surface you are trying to place the item on.%1';

         case $NotDeployableReason::ObjectTooClose:
            %msg = '\c2You cannot place this item so close to another object.%1';

         case $NotDeployableReason::NoTerrainFound:
            %msg = '\c2You must place this on outdoor terrain.%1';

         case $NotDeployableReason::NoInteriorFound:
            %msg = '\c2You must place this on a solid surface.%1';

         case $NotDeployableReason::TurretTooClose:
            %msg = '\c2Interference from a nearby turret prevents placement here.%1';

         case $NotDeployableReason::TurretSaturation:
            %msg = '\c2There are too many turrets nearby.%1';

         case $NotDeployableReason::SurfaceTooNarrow:
            %msg = '\c2There is not adequate surface to clamp to here.%1';

         case $NotDeployableReason::InventoryTooClose:
            %msg = '\c2Interference from a nearby inventory prevents placement here.%1';

         case $NotDeployableReason::CantDeploySecure:
            %msg = '\c2Deploy rights on these pieces are secured.%1';

         default:
            %msg = '\c2Deploy failed.';
      }
      messageClient(%plyr.client, 'MsgDeployFailed', %msg, %errorSnd);
   }
   
   function LoadUniversalRank(%client) {
      //A Little PGD Connect Ad.
      %client.donotupdate = 1;
      if(!%client.IsPGDConnected()) {
         %client.donotupdate = 0;
         messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect account required to load universal ranks.");
         messageClient(%client, 'msgPGDRequired', "\c5PGD: Sign up for PGD Connect today, It's Fast, Easy, and FREE!");
         messageClient(%client, 'msgPGDRequired', "\c5See: www.public.phantomgamesdevelopment.com/SMF/ in the PGD Section");
         messageClient(%client, 'msgPGDRequired', "\c5For more details.");
         schedule(500, 0, "LoadClientRankfile", %client);
         return 1;
      }
      //IS FILE
      if(!PGD_IsFile("Data/"@%client.guid@"/Ranks/TWM2/Saved.TWMSave")) {
         %client.donotupdate = 0;
         messageClient(%client, 'msgPGDRequired', "\c5PGD: PGD Connect confirms you do not have a universal rank.");
         messageClient(%client, 'msgPGDRequired', "\c5PGD: Play on a main server to start progressing one today!");
         messageClient(%client, 'msgPGDRequired', "\c5PGD: Loading your local rank file for the time being...");
         schedule(500, 0, "LoadClientRankfile", %client);
         return 1;
      }
      //Passed, we have a universal rank, time to loady :)
      %server = "www.phantomgamesdevelopment.com:80";
      if (!isObject(RankGrabber)) {
         %Downloader = new HTTPObject(RankGrabber);
      }
      else {
         %Downloader = RankGrabber;
      }
      //scan for local rank file:  //sig- lotsa messages we don't need :)
      //MessageClient(%client, 'msgAccess', "\c5PGD: Scanning and Deleting Local Rank Settings");
      //deleteFile($TWM::RanksDirectory@"/"@%client.guid@"/Settings.TWMSave");
      deleteFile($TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave");
      //If the server crashes here, let everyone know why
      MessageClient(%client, 'msgAccess', "\c5PGD: Requesting Your Universal Rank File, Creating New Local Cache.");
      echo("Client:" SPC %client.namebase SPC "needs universal rank load. It will be stored locally.");
      //Cache Create
      %file = "/public/Univ/Data/"@%client.guid@"/Ranks/TWM2/Saved.TWMSave";

      //Downloader
      %Downloader.client = %client;
      %Downloader.get(%server, %file);
      %Downloader.schedule(15000, "disconnect");
   }
   
   function SetPerkStatus(%client, %perk, %status) {
      if(%client.CanUsePerk($Perk::PerkToID[%perk])) {
         %client.ActivePerk[%perk] = %status;
         if(%status == 1) {
            MessageClient(%client, 'MsgPerkOn', "TWM2: PERK "@%perk@" ACTIVE");
            //Perk details
            if(%perk $= "Radar Phantom") {
               setTargetSensorData(%client.target, JammerSensorObjectActive);
            }
         }
         else {
            //Perk details
            if(%perk $= "Radar Phantom") {
               setTargetSensorData(%client.target, PlayerSensor);
            }
         }
      }
      else {
         MessageClient(%client, 'MsgPerkOn', "TWM2: PERK "@%perk@" - You cannot use this perk");
      }
   }
   
   function GameConnection::setStreakStatus(%client, %val, %stat) {
      if(!%client.HasKillstreak(%val)) {
         messageClient(%client, 'msgTooMany', "\c5TWM2: You cannot use this kill streak.");
         return;
      }
      if(%stat == 1) {    //Activate streak
         if(%client.GetActiveStreakCount() == %client.getMaxActiveStreaks()) {
            messageClient(%client, 'msgTooMany', "\c5TWM2: You already have all "@%client.getMaxActiveStreaks()@" Killstreaks active.");
            return;
         }
         else {
            %client.KillstreakOn[%val] = 1;
            messageClient(%client, 'msgKSOn', "\c5TWM2: Killstreak "@StreakValToName(%val)@" activated ("@%client.GetActiveStreakCount()@"/"@%client.getMaxActiveStreaks()@").");
         }
      }
      else {              //De-activate streak
         %client.KillstreakOn[%val] = 0;
         messageClient(%client, 'msgKSOff', "\c5TWM2: Killstreak "@StreakValToName(%val)@" deactivated ("@%client.GetActiveStreakCount()@"/"@%client.getMaxActiveStreaks()@").");
      }
   }
   
   function UpdateSettings(%client) {
      %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Settings.TWMSave";
      if(isFile(%file)) {
         DeleteFile(%file);
      }
      //Gather Perk Info
      if(%client.Perk[1] $= "") {
         %toWrite1 = "\"\"";
      }
      else {
         %toWrite1 = "\""@%client.Perk[1]@"\"";
      }

      if(%client.Perk[2] $= "") {
         %toWrite2 = "\"\"";
      }
      else {
         %toWrite2 = "\""@%client.Perk[2]@"\"";
      }

      if(%client.Perk[3] $= "") {
         %toWrite3 = "\"\"";
      }
      else {
         %toWrite3 = "\""@%client.Perk[3]@"\"";
      }
      //

      //
      //
      new fileobject(CSSys22);
      CSSys22.openforWrite(""@$TWM::RanksDirectory@"/"@%client.guid@"/Settings.TWMSave");
      CSSys22.WriteLine("//Generated by CSS, Created By Phantom139");
      CSSys22.WriteLine("//Saved For "@%client.namebase@", GUID: "@%client.guid@"");
      CSSys22.WriteLine("");
      CSSys22.WriteLine("function LoadSavedSettings(%client) {");
      CSSys22.WriteLine("   //Let's Load the perks Up First");
      CSSys22.WriteLine("   schedule(1000, 0, \"SetPerkStatus\", %client, "@%toWrite1@", 1);");
      CSSys22.WriteLine("   schedule(1000, 0, \"SetPerkStatus\", %client, "@%toWrite2@", 1);");
      CSSys22.WriteLine("   schedule(1000, 0, \"SetPerkStatus\", %client, "@%toWrite3@", 1);");
      CSSys22.WriteLine("   //Now Let's Load the killstreaks, disable defaults first ");
      CSSys22.WriteLine("   %client.schedule(1000, \"setStreakStatus\", 1, 0);");
      CSSys22.WriteLine("   %client.schedule(1000, \"setStreakStatus\", 2, 0);");
      CSSys22.WriteLine("   %client.schedule(1000, \"setStreakStatus\", 4, 0);");
      CSSys22.WriteLine("   //Now Let's Load OUR killstreaks ");
      //Gather streak Info
      %streakStr = %client.GatherActiveStreaks();
      %count = getFieldCount(%streakStr);
      for(%i = 0; %i < %count; %i++) {
         %savedStreak = getField(%streakStr, %i);
         CSSys22.WriteLine("   %client.schedule(1000, \"setStreakStatus\", "@%savedStreak@", 1);");
      }
      //Gather Armor Flag
      CSSys22.WriteLine("   //Armor Flag? ");
      if(%client.useFlag) {
         CSSys22.WriteLine("   %client.useFlag = 1;");
         CSSys22.WriteLine("   %client.flagType = \""@%client.flagType@"\";");
      }
      //Gather ALL current weapon upgrades I have on, and apply
      CSSys22.WriteLine("   //Weapon Upgrades (Coming next version) ");
      CSSys22.WriteLine("   echo(\"Settings Successfully Loaded\");");
      CSSys22.WriteLine("   //All Loaded");
      CSSys22.WriteLine("}");
      CSSys22.close();
      CSSys22.delete();

      LoadSettings(%client);
   }
   
   function WriteHonorFile(%client,%GUID,%Type,%TrueFalse){
      UpdateClientRank(%client);
      UpdateRankFile(%client);
      if (!IsFile(""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave")){
         new fileobject(Medals);
         Medals.openforwrite(""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave");
         Medals.writeline("$Medals::"@%Type@"["@%GUID@"] = "@%Truefalse@";");
         Medals.close();
         exec(""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave");
         Medals.delete();
         return "Added Client To "@%Type@" List: "@%GUID@"";
      }
      else {
         new fileobject(Medals);
         Medals.openforappend(""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave");
         Medals.writeline("$Medals::"@%Type@"["@%GUID@"] = "@%Truefalse@";");
         Medals.close();
         exec(""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave");
         Medals.delete();
         return "Added Client To "@%Type@" List: "@%GUID@"";
      }
   }
};
activatePackage(TWM2Patch3_04);

addCMDToHelp("DepSec", "Usage: /DepSec: secure deploy rights on your pieces.");

$NotDeployableReason::CantDeploySecure          =  12;

$Perk::PerkToID["AP Bullets"] = 1;
$Perk::PerkToID["Advanced Grip"] = 2;
$Perk::PerkToID["Wind Brake Beacon"] = 3;
$Perk::PerkToID["3 Second C4"] = 4;
$Perk::PerkToID["Blade Sweep"] = 5;
$Perk::PerkToID["Martydom"] = 6;
$Perk::PerkToID["Pistol God"] = 7;
$Perk::PerkToID["Double Down"] = 8;
$Perk::PerkToID["OverKill"] = 9;
$Perk::PerkToID["Kevlar Armor"] = 10;
$Perk::PerkToID["Head Guard"] = 11;
$Perk::PerkToID["Storm Barrier"] = 12;
$Perk::PerkToID["Lim Zombie Shield"] = 13;
$Perk::PerkToID["No-Infect Armor"] = 14;
$Perk::PerkToID["Radar Phantom"] = 15;
$Perk::PerkToID["Clip Boxes"] = 16;
$Perk::PerkToID["UAV Disabler"] = 17;
$Perk::PerkToID["Team Gain"] = 18;
$Perk::PerkToID["Double Time"] = 19;
$Perk::PerkToID["Ammo Vet"] = 20;
$Perk::PerkToID["Bandolier"] = 21;
$Perk::PerkToID["Hardline"] = 22;
$Perk::PerkToID["Bomb Shadower"] = 23;
$Perk::PerkToID["Second Chance"] = 24;