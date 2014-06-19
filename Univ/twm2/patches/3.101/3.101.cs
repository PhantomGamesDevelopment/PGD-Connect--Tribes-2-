//Patch 3.101
// Fixes a little annoyance bug upong reahing the last rank

//function located in AdvancedRankSystem.cs
function UpdateClientRank(%client) {
    if(%client.donotupdate) {
       echo("Stopped rank up check on "@%client@", server denies access (probably loading univ rank)");
       return;
    }
    if($Rank::Prestige[%client.guid] $= "") {
       $Rank::Prestige[%client.guid] = 0;
    }
    if($Rank::XP[%client.guid] >= 3000001) {
       //stop here, over the max EXP
       return;
    }
    %name = %client.namebase;
    //anti-Hack system.
    %maxPossibleGain = getMaxGainedEXP(%client);
    if(%client.XP > (%maxPossibleGain+100)) {
       messageclient(%client, 'Msgclient', "\c5TWM2: Hack Alert: You have set off the TWM2 anti-hack system, congratulations");
       messageAll('msgAdminForce', "\c5"@%client.namebase@" has been banned for hacking the mod.");
       schedule(1500, 0, "Ban", %client, "HackBan");
       //WE obviously don't care about hackers in satellites
       PushEmailToPhantom($TWM2::ServerKey, "EXPHacking", %client);
    }
    $Rank::XP[%client.guid] += %client.XP;
    %client.XP = 0;
	%stat = $Rank::XP[%client.guid];
	for(%j = $Rank::RankCount; %j > 0; %j--){        //check all ranks
	   if($Rank::XP[%client.guid] >= $Ranks::MinPoints[%j]){
		  if($Rank::Rank[%client.guid] !$= $Ranks::NewRank[%j]){
             //
             $TWM2::RankNumber[%client.guid] = %j;
             if($TWM2::UseRankTags) {
                DoNameChangeChecks(%client);
             }
             //
		     $Rank::Rank[%client.guid] = $Ranks::NewRank[%j];
             //
             if($Prestige::Name[$Rank::Prestige[%client.guid]] >= 1) {
                $Prestige::Name[$Rank::Prestige[%client.guid]] = "";
             }
		     messageAll('msgclient',"\c2"@%name@" has become a "@$Prestige::Name[$Rank::Prestige[%client.guid]]@""@$Ranks::NewRank[%j]@" with a XP of "@$Rank::XP[%client.guid]@"!");
	         messageclient(%client, 'Msgclient', "~wfx/Bonuses/Nouns/General.wav");
		     bottomPrint(%client, "Excelent work "@%name@", you have been promoted to the rank of: "@$Prestige::Name[$Rank::Prestige[%client.guid]]@""@$Ranks::NewRank[%j]@"!", 5, 2 );
             echo("Promotion: "@%name@" to Rank "@$Ranks::NewRank[%j]@", XP: "@$Rank::XP[%client.guid]@".");
             UpdateRankFile(%client);
             PrepareUpload(%client);
		  }
		  %j = 1;
	   }
	}
    checkForXPAwards(%client, %stat);
}
