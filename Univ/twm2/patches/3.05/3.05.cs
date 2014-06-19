//PATCH 3.05
//TOTAL WARFARE MOD 2 ADVANCED WARFARE

//BY PHANTOM139

//Change Log:
// *Patched a glitch in the killstreak saver
// *Suicide now properly kills you in wartower
// *You can now control the centaur artillery's turret, by pressing your grenade key while driving

package TWM2Patch3_05 {
   function GameConnection::GatherActiveStreaks(%client) {
      %count = 0;
      %str = "";
      //first part gathers the active streaks
      for(%i = 1; %i <= $KillstreakCount; %i++) {
         if(%client.isActiveStreak(%i)) {
            %count++;
            %streak[%count] = %i;
         }
      }
      //second part outputs the proper amount of string values
      for(%x = 1; %x <= %count; %x++) {
         %str = ""@%str@""@%streak[%x]@"\t";
      }
      return %str;
   }
   
   function serverCmdSuicide(%client) {
      // -------------------------------------
      // z0dd - ZOD, 5/8/02. Addition. Console spam fix.
      if(!isObject(%client.player))
         return;

      if ( $MatchStarted && !%client.isJailed) {
         %client.player.setInvinc(false);
         %client.player.scriptKill($DamageType::Suicide);
      }
   }
   
   function CentaurVehicle::deleteAllMounted(%data, %obj) {
      %turret = %obj.getMountNodeObject(10);
      if (!%turret)
         return;

      if(%turret.getControllingClient()) {
         %turret.getControllingClient().setControlObject(%turret.getControllingClient().player);
      }

      %turret.schedule(2000, delete);
   }

   function CentaurVehicle::onTrigger(%data, %obj, %trigger, %state) {
      %plyr = %obj.getMountNodeObject(0);
      if(%state == 1 && %trigger == 5) {
          if(%obj.barrel $= "Chain") {
             %obj.barrel = "Collider";
             %obj.turretObject.schedule(3500, "mountImage", CentaurColliderBarrel, 0);
             BottomPrint(%plyr.client, "Centaur: Switching to collider artillery", 3, 1);
             %obj.setfrozenstate(true);      //lock the artillery into place
             %obj.turretObject.source = %obj;
          }
          else {
             %obj.barrel = "Chain";
             %obj.turretObject.schedule(3500, "mountImage", Cent50CalBarrel, 0);
             BottomPrint(%plyr.client, "Centaur: Switching to 50Cal Chainguns", 3, 1);
             %obj.setfrozenstate(false);
          }
      }
      if(%state == 1 && %trigger == 4) {
         %plyr.client.setControlObject(%obj.turretObject);
      }
   }

   function CentaurTurret::onTrigger(%data, %obj, %trigger, %state) {
      %cli = %obj.getControllingClient();
      if(%state == 1 && %trigger == 5) {
          if(%obj.source.barrel $= "Chain") {
             %obj.source.barrel = "Collider";
             %obj.schedule(3500, "mountImage", CentaurColliderBarrel, 0);
             BottomPrint(%cli, "Centaur: Switching to collider artillery", 3, 1);
             %obj.source.setfrozenstate(true);      //lock the artillery into place
          }
          else {
             %obj.source.barrel = "Chain";
             %obj.schedule(3500, "mountImage", Cent50CalBarrel, 0);
             BottomPrint(%cli, "Centaur: Switching to 50Cal Chainguns", 3, 1);
             %obj.source.setfrozenstate(false);
          }
      }
      if(%state == 1 && %trigger == 4) {
         %cli.setControlObject(%obj.source);
      }
   }
};
ActivatePackage(TWM2Patch3_05);
