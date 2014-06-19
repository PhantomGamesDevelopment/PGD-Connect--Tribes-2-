//The Perks
//LIST
$PerkRequire[1] = $Ranks::MinPoints[32];
$PerkRequire[2] = $Ranks::MinPoints[40];
$PerkRequire[7] = $Ranks::MinPoints[54];
$PerkRequire[9] = $Ranks::MinPoints[43];
$PerkRequire[10] = $Ranks::MinPoints[18];
$PerkRequire[11] = $Ranks::MinPoints[36];
$PerkRequire[15] = $Ranks::MinPoints[60];
$PerkRequire[16] = $Ranks::MinPoints[46];
$PerkRequire[21] = $Ranks::MinPoints[4];
$PerkRequire[22] = $Ranks::MinPoints[45];

//PRIMARY - Weapon Modifications
// AP Bullets [c] - Increase Damage to 150%
// Advanced Grip [c] - Increase Accuracy On All Guns with Spread
// Wind Brake Beacon [c] - Beacon Key Stops Your Fall Instantly
// 3 Second C4 [c] - C4 Detonates in 3 Seconds, not 10
// Blade Sweep [c] - BoV Affects a small area instead of a single attack
// Martydom [c] - Your armor explodes 4 seconds after death
// Pistol God [c] - Removes All Spread On Certain Pistols
// Double Down [c] - Gain double EXP for non boss Kills.

//SECONDARY - Armor And Defense
// OverKill [b] - Replaces your Pistol Slot with an Additional Weapon Slot
// Kevlar Armor [c] - Increase Shielding to 150%
// Head Guard [c] - Blocks Headshot Kills
// Storm Barrier [c] - Reduce Electrical Damage, blocks lightning
// Lim Zombie Shield [c] - 20% Chance that Zombies Will Be Repeled upon attack
// No-Infect Armor [c] - Zombie Attacks will not infect you
// Radar Phantom [c] - Jam enemy sensors

//TERTIARY - Assets
// Clip Boxes [c] - Cuts Reload Time By 33%
// UAV Disabler [c] - You will not show on enemy UAVs
// Team Gain [c] - All Teammates within 20M Of the Killer Gain XP
// Double Time [c] - Cuts Reload Time By 50%
// Ammo Vet [c] - Cuts Reload Time By 75%
// Bandolier [c] - Increases Clips To 200%
// Hardline [c] - Killstreaks require 1 less kill
// Bomb Shadower [c] - You do not show a Sabotage Waypoint
// Second Chance [c] - Spend a team revive to respawn in horde

$Perk::PerkCount[1] = 8;
$Perk::PerkCount[2] = 7;
$Perk::PerkCount[3] = 9;

$Perk::TotalPerks = $Perk::PerkCount[1] + $Perk::PerkCount[2] + $Perk::PerkCount[3];

$Perk::Perk[1] = "AP Bullets";
$Perk::Perk[2] = "Advanced Grip";
$Perk::Perk[3] = "Wind Brake Beacon";
$Perk::Perk[4] = "3 Second C4";
$Perk::Perk[5] = "Blade Sweep";
$Perk::Perk[6] = "Martydom";
$Perk::Perk[7] = "Pistol God";
$Perk::Perk[8] = "Double Down";
$Perk::Perk[9] = "OverKill";
$Perk::Perk[10] = "Kevlar Armor";
$Perk::Perk[11] = "Head Guard";
$Perk::Perk[12] = "Storm Barrier";
$Perk::Perk[13] = "Lim Zombie Shield";
$Perk::Perk[14] = "No-Infect Armor";
$Perk::Perk[15] = "Radar Phantom";
$Perk::Perk[16] = "Clip Boxes";
$Perk::Perk[17] = "UAV Disabler";
$Perk::Perk[18] = "Team Gain";
$Perk::Perk[19] = "Double Time";
$Perk::Perk[20] = "Ammo Vet";
$Perk::Perk[21] = "Bandolier";
$Perk::Perk[22] = "Hardline";
$Perk::Perk[23] = "Bomb Shadower";
$Perk::Perk[24] = "Second Chance";

$Perk::Descrip[1] = "Bullets do 50% More Damage";
$Perk::Descrip[2] = "Improves Weapon Accuracy by 250%";
$Perk::Descrip[3] = "Instantly Stop a fall with your beacon key";
$Perk::Descrip[4] = "Your C4 Detonates in 3 Seconds, not 10";
$Perk::Descrip[5] = "The BoV Affects a small area, not 1 target";
$Perk::Descrip[6] = "Your armor explodes 4 seconds after death";
$Perk::Descrip[7] = "Makes certain pistols 100% accurate";
$Perk::Descrip[8] = "Gain Double EXP for non boss kills";
$Perk::Descrip[9] = "Adds an additional weapon slot to your armor";
$Perk::Descrip[10] = "Increases your armor by 50%";
$Perk::Descrip[11] = "Prevents you from being Headshot Killed";
$Perk::Descrip[12] = "Protects you from electrical attacks";
$Perk::Descrip[13] = "20% chance to deflect zombie attackers";
$Perk::Descrip[14] = "Prevents you from being infected";
$Perk::Descrip[15] = "Jam enemy sensors";
$Perk::Descrip[16] = "Reduces reload time by 33%";
$Perk::Descrip[17] = "You will not show on enemy UAVs";
$Perk::Descrip[18] = "Allies near you will gain XP for your kills";
$Perk::Descrip[19] = "Reduces reload time by 50%";
$Perk::Descrip[20] = "Reduces reload time by 75%";
$Perk::Descrip[21] = "Doubles your initial clip count";
$Perk::Descrip[22] = "Killstreaks require 1 less kill to earn";
$Perk::Descrip[23] = "You do not show a Sabotage Waypoint";
$Perk::Descrip[24] = "Spend a team revive to respawn in horde";

$Perk::PerkToGroup["AP Bullets"] = 1;
$Perk::PerkToGroup["Advanced Grip"] = 1;
$Perk::PerkToGroup["Wind Brake Beacon"] = 1;
$Perk::PerkToGroup["3 Second C4"] = 1;
$Perk::PerkToGroup["Blade Sweep"] = 1;
$Perk::PerkToGroup["Martydom"] = 1;
$Perk::PerkToGroup["Pistol God"] = 1;
$Perk::PerkToGroup["Double Down"] = 1;
$Perk::PerkToGroup["OverKill"] = 2;
$Perk::PerkToGroup["Kevlar Armor"] = 2;
$Perk::PerkToGroup["Head Guard"] = 2;
$Perk::PerkToGroup["Storm Barrier"] = 2;
$Perk::PerkToGroup["Lim Zombie Shield"] = 2;
$Perk::PerkToGroup["No-Infect Armor"] = 2;
$Perk::PerkToGroup["Radar Phantom"] = 2;
$Perk::PerkToGroup["Clip Boxes"] = 3;
$Perk::PerkToGroup["UAV Disabler"] = 3;
$Perk::PerkToGroup["Team Gain"] = 3;
$Perk::PerkToGroup["Double Time"] = 3;
$Perk::PerkToGroup["Ammo Vet"] = 3;
$Perk::PerkToGroup["Bandolier"] = 3;
$Perk::PerkToGroup["Hardline"] = 3;
$Perk::PerkToGroup["Bomb Shadower"] = 3;
$Perk::PerkToGroup["Second Chance"] = 3;

//Asset Function
function GameConnection::IsActivePerk(%client, %perk) {
   if(%client.ActivePerk[""@%perk@""] == 1) {
      return true;
   }
   else {
      return false;
   }
}

function DisableAllPerkGroup(%client, %PerkGroup) {
   for(%i = 1; %i <= $Perk::TotalPerks; %i++) {
      if(GetPerkGroup(%i) == %PerkGroup) {
         %client.ActivePerk[$Perk::Perk[%i]] = 0;
      }
   }
}

function GetActivePerks(%client) {
   for(%i = 1; %i <= $Perk::TotalPerks; %i++) {
      if(%client.IsActivePerk($Perk::Perk[%i])) {
         if(GetPerkGroup(%i) == 1) {
            %client.Perk[1] = $Perk::Perk[%i];
         }
         else if(GetPerkGroup(%i) == 2) {
            %client.Perk[2] = $Perk::Perk[%i];
         }
         else if(GetPerkGroup(%i) == 3) {
            %client.Perk[3] = $Perk::Perk[%i];
         }
      }
   }
}

function GetPerkGroup(%perkVal) {
   if(%perkVal <= $Perk::PerkCount[1]) {
      return 1;
   }
   else if( %perkVal > $Perk::PerkCount[1] && %perkVal <= $Perk::PerkCount[1] + $Perk::PerkCount[2]) {
      return 2;
   }
   else {
      return 3;
   }
}

function GameConnection::CanUsePerk(%client, %perkVal) {
   %xp = $Rank::XP[%client.guid];
   switch(%perkVal) {
      case 1:
         if(%xp >= $PerkRequire[1]) {
            return true;
         }
         else {
            return false;
         }
      case 2:
         if(%xp >= $PerkRequire[2]) {
            return true;
         }
         else {
            return false;
         }
      case 3:
         if($Medals::Harbiend[%client.GUID]) {
            return true;
         }
         else {
            return false;
         }
      case 4 or 5:
         if($Medals::RevengeAvoidedAgain[%client.GUID]) {
            return true;
         }
         else {
            return false;
         }
      case 6:
         if($Medals::TheSourceOfAllEvil[%client.GUID]) {
            return true;
         }
         else {
            return false;
         }
      case 7:
         if(%xp >= $PerkRequire[7]) {
            return true;
         }
         else {
            return false;
         }
      case 8:
         if($Rank::Prestige[%client.guid] >= 1) {
            return true;
         }
         else {
            return false;
         }
      case 9:
         if(%xp >= $PerkRequire[9]) {
            return true;
         }
         else {
            return false;
         }
      case 10:
         if(%xp >= $PerkRequire[10]) {
            return true;
         }
         else {
            return false;
         }
      case 11:
         if(%xp >= $PerkRequire[11]) {
            return true;
         }
         else {
            return false;
         }
      case 12:
         if($Medals::ThundaStruk[%client.GUID]) {
            return true;
         }
         else {
            return false;
         }
      case 13:
         if($Medals::TheNewGeneral[%client.GUID]) {
            return true;
         }
         else {
            return false;
         }
      case 14:
         if($Medals::InsigniaDefeated[%client.GUID]) {
            return true;
         }
         else {
            return false;
         }
      case 15:
         if(%xp >= $PerkRequire[15]) {
            return true;
         }
         else {
            return false;
         }
      case 16:
         if(%xp >= $PerkRequire[16]) {
            return true;
         }
         else {
            return false;
         }
      case 17:
         if($Medals::SerialKiller[%client.GUID]) {
            return true;
         }
         else {
            return false;
         }
      case 18 or 20:
         if($Medals::AboutDamnTime[%client.GUID]) {
            return true;
         }
         else {
            return false;
         }
      case 19:
         if($Medals::InvatationAccepted[%client.GUID]) {
            return true;
         }
         else {
            return false;
         }
      case 21:
         if(%xp >= $PerkRequire[21]) {
            return true;
         }
         else {
            return false;
         }
      case 22:
         if(%xp >= $PerkRequire[22]) {
            return true;
         }
         else {
            return false;
         }
      case 23:
         if(%client.CheckNWChallengeCompletion("3For5Sabo")) {
            return true;
         }
         else {
            return false;
         }
      case 24:
         if(%client.CheckNWChallengeCompletion("ArmyOf50Stopped")) {
            return true;
         }
         else {
            return false;
         }
      default:
         error("TWM2: Perks.cs: Invalid Value Passed to CanUsePerk");
         return false;
   }
}

function CreatePerkMenu(%client, %tag, %index) {
   messageClient( %client, 'SetLineHud', "", %tag, %index, "-* Primary Perks *-");
   %index++;
   for(%i = 1; %i <= $Perk::PerkCount[1]; %i++) {
      if(%client.CanUsePerk(%i)) {
         if(!%client.IsActivePerk($Perk::Perk[%i])) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPerkStatus\t"@$Perk::Perk[%i]@">"@$Perk::Perk[%i]@"</a> - "@$Perk::Descrip[%i]@"");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$Perk::Perk[%i]@" - ACTIVE - "@$Perk::Descrip[%i]@"");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "This Perk Is Not Availible For Use");
         %index++;
      }
   }
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "-* Secondary Perks *-");
   %index++;
   for(%i = $Perk::PerkCount[1] + 1; %i <= $Perk::PerkCount[1]+$Perk::PerkCount[2]; %i++) {
      if(%client.CanUsePerk(%i)) {
         if(!%client.IsActivePerk($Perk::Perk[%i])) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPerkStatus\t"@$Perk::Perk[%i]@">"@$Perk::Perk[%i]@"</a> - "@$Perk::Descrip[%i]@"");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$Perk::Perk[%i]@" - ACTIVE - "@$Perk::Descrip[%i]@"");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "This Perk Is Not Availible For Use");
         %index++;
      }
   }
   //
   messageClient( %client, 'SetLineHud', "", %tag, %index, "");
   %index++;
   messageClient( %client, 'SetLineHud', "", %tag, %index, "-* Tertiary Perks *-");
   %index++;
   for(%i = $Perk::PerkCount[1] + $Perk::PerkCount[2] + 1; %i <= $Perk::PerkCount[1] + $Perk::PerkCount[2] + $Perk::PerkCount[3]; %i++) {
      if(%client.CanUsePerk(%i)) {
         if(!%client.IsActivePerk($Perk::Perk[%i])) {
            messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tPerkStatus\t"@$Perk::Perk[%i]@">"@$Perk::Perk[%i]@"</a> - "@$Perk::Descrip[%i]@"");
            %index++;
         }
         else {
            messageClient( %client, 'SetLineHud', "", %tag, %index, ""@$Perk::Perk[%i]@" - ACTIVE - "@$Perk::Descrip[%i]@"");
            %index++;
         }
      }
      else {
         messageClient( %client, 'SetLineHud', "", %tag, %index, "This Perk Is Not Availible For Use");
         %index++;
      }
   }
   return %index;
}

function SetPerkStatus(%client, %perk, %status) {
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

function DoPerksStuff(%client, %player) {
   if(%client.IsActivePerk("Radar Phantom")) {
      setTargetSensorData(%client.target, JammerSensorObjectActive);
   }
   if(%client.IsActivePerk("Wind Brake Beacon")) {
      %client.player.AirBrakes = 3;
   }
}



//Perks
//For Lim Zombie
function RepelZombie(%zombie, %player) {
   %tgtPos = %zombie.getWorldBoxCenter();
   %distance2 = VectorDist(%tgtPos, %player.getPosition());
   %distance = mfloor(%distance2);
   %vec = VectorNormalize(VectorSub(%player.getPosition(), %tgtpos));
   %nForce = 2000;                              //buh bye!
   %forceVector = vectorScale(%vec, -1*%nForce);

   %zombie.applyImpulse(%zombie.getPosition(), %forceVector);
   %zombie.playShieldEffect("1 1 1");
}

function MartydomExplode(%position, %client) {
   ServerPlay3D("SatchelChargeExplosionSound", %position);
   %c4 = new Item() {
      datablock = C4Deployed;
      position = %position;
      scale = ".1 .1 .1";
      owner = %client;
   };
   %C4.sourceObject = %client.player;
   %C4.theClient = %client;
   MissionCleanup.add(%c4);
   schedule(770, 0, "C4GoBoom", %c4);
}
