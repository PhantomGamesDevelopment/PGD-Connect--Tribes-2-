//--------------------------------------------------------------------------
// SWs
// System Core now moved to: scripts/TWM2/Systems/Killstreak.cs
// Handles the Killstreak Datablocks
//--------------------------------------------------------------------------

//--------------------------------------
// UAV
//--------------------------------------
datablock ItemData(UAVCaller)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = UAVCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";
 
   isKSSW = 1;

   computeCRC = true;

};

datablock ShapeBaseImageData(UAVCallerImage)
{
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = UAVCaller;
   offset = "0 0 0";
   
   isKSSW = 1;

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function UAVCallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasUAV) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   %obj.client.HasUAV = 0;
   %obj.client.xp += 20;
   UpdateClientRank(%obj.client);
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: UAV Called In, +20XP.");
   
   $TWM2::UAVCalls[%obj.client.guid]++;
   UpdateSWBeaconFile(%obj.client, "UAV");

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(UAVCaller, 0, true);
   %count = 0;
   if(!$TWM2::FFAMode) {
      %obj.team.UAVLoop = UAVLoop(%obj, %obj.client.team, %count);
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         if(%cl.team == %obj.team) {
            messageClient(%cl, 'MsgUAVOnline' , "Our UAV is Online (30 Seconds)");
         }
         else {
            messageClient(%cl, 'MsgUAVOnline' , "Enemy UAV is Airborne (30 Seconds)");
         }
      }
   }
   else {
      %obj.client.UAVLoop = UAVLoop(%obj, "", 0);
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         if(%cl == %obj.client) {
            messageClient(%cl, 'MsgUAVOnline' , "Your UAV is Online (30 Seconds)");
         }
         else {
            messageClient(%cl, 'MsgUAVOnline' , ""@%obj.client.namebase@"'s UAV is Airborne (30 Seconds)");
         }
      }
   }
}

function UAVLoop(%obj, %team, %ct) {
   if(!isObject(Game)) {
      return;
   }
   if(!$TWM2::FFAMode || %team !$= "") {
      %ct++;
      if(%ct > 30) {
         MessageAll('msgOver', "Team "@%team@"'s UAV has expired.");
         return;
      }
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         if(%cl.team != %team && !%cl.IsActivePerk("UAV Disabler")) {
            if(isObject(%cl.player)) {
               %cl.UAVMarkerWp = new WayPoint() {
                   position = %cl.player.getPosition();
                   dataBlock = "WayPointMarker";
                   team = %cl.team;
                   name = ""@%cl.namebase@"";
               };
               MissionCleanup.add(%cl.UAVMarkerWp);
               %cl.UAVMarkerWp.schedule(1000, "Delete");
            }
         }
      }
      %obj.team.UAVLoop = schedule(1000, 0, "UAVLoop", %obj, %team, %ct);
   }
   else {
      %ct++;
      if(%ct > 30) {
         MessageAll('msgOver', ""@%obj.client.namebase@"'s UAV has expired.");
         return;
      }
      for(%i = 0; %i < ClientGroup.getCount(); %i++) {
         %cl = ClientGroup.getObject(%i);
         if(%cl != %obj.client && !%cl.IsActivePerk("UAV Disabler")) {
            if(isObject(%cl.player)) {
               %cl.UAVMarkerWp = new WayPoint() {
                   position = %cl.player.getPosition();
                   dataBlock = "WayPointMarker";
                   team = 31; //some rediculous number to make them all enemies
                   name = ""@%cl.namebase@"";
               };
               MissionCleanup.add(%cl.UAVMarkerWp);
               %cl.UAVMarkerWp.schedule(1000, "Delete");
            }
         }
      }
      %obj.client.UAVLoop = schedule(1000, 0, "UAVLoop", %obj, "", %ct);
   }
}

//--------------------------------------
// AIRSTRIKE
//--------------------------------------
datablock ItemData(AirstrikeCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = AirstrikeCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(AirstrikeCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = AirstrikeCaller;
   offset = "0 0 0";
   
   isKSSW = 1;

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function AirstrikeCallerImage::OnFire(%data, %obj, %slot) {
   %ASCam = new Camera() {
      dataBlock = TWM2ControlCamera;
   };
   if(!%obj.client.UnlimitedAS) {
      %obj.use(AirstrikeCaller);
      %obj.throwWeapon(1);
      %obj.throwWeapon(0);
      %obj.setInventory(AirstrikeCaller, 0, true);
   }

   MissionCleanup.add(%ASCam);
   %ASCam.setTransform(%obj.getTransform());
   %ASCam.setFlyMode();
   %ASCam.mode = "AirstrikeCall";
   %obj.client.setControlObject(%ASCam);
   CameraMessageLoop(%obj.client, %ASCam, %ASCam.mode);
}

function SpawnBomber(%CallerClient, %callPos, %strikePos, %add) {
   %strikePos = getWords(%strikePos, 0, 1) SPC (getWord(%strikePos, 2) + 150);
   %Bomber = new FlyingVehicle() {
      dataBlock = BomberFlyer;
      position = VectorAdd(%callPos, %add);
      team = %CallerClient.team;
   };
   MissionCleanup.add(%Bomber);
   BomberImpulse(%Bomber, %strikePos, %CallerClient, 0);
   %Bomber.schedule(30000, "Delete");
   //Rot
   %Bomber.ReachedDest = 0;
   ConstantBomberTurningLoop(%bomber, %strikePos);
   return %Bomber;
}

function ConstantBomberTurningLoop(%obj, %TPos) {
   //keeps us in line with out target
   if(!isObject(%obj)) {
      return;
   }
   %BPos = %obj.getPosition();
   //
   %Target = vectorSub(%TPos, %BPos);
   %SwapA = -1 * getWord(%target, 0);
   %TVector = getWord(%target, 1)@" "@%SwapA@" 0";
   %obj.setRotation(fullrot("0 0 0",%TVector));
   
   %dist = vectorDist(%TPos, %BPos);
   if(%dist < 75) {
      %obj.ReachedDest = 1;
      return;
   }
   else {
      schedule(100, 0, "ConstantBomberTurningLoop", %obj, %tpos);
   }
}

function Airstrike(%CallerClient, %position, %dirFrom) {

   if(!%CallerClient.UnlimitedAS) {
      $TWM2::AirstrikeCalls[%CallerClient.guid]++;
      UpdateSWBeaconFile(%CallerClient, "AirStrike");
   }
   
   //new stuff TWM2 2.6
   //%dirFrom = Spawn Position of Aircraft
   %THeight = getTerrainHeight(%dirFrom);
   %THeightCons = %THeight + 150;
   //Consider wartower
   if(!$TWM::PlayingWarTower) {
      if((%THeightCons) <= 5 && (%THeightCons) > -200) {
         //baaaaaaad
         %NewZ = %THeight + 150; //give us the perfect height
      }
      else {
         //fine
         %NewZ = getWord(%dirFrom, 2) + 150;
      }
   }
   %CallPos = getWords(%dirFrom, 0, 1) SPC %NewZ;
   //
   //echo(%callPos);

   if(getWord(%callPos, 0) < getWord(%callPos, 1)) {
      %b1Add = "-10 10 -20";
      %Bomber1 = SpawnBomber(%CallerClient, %callPos, %position, %b1add);
      %b2Add = "20 20 10";
      %Bomber2 = schedule(2000, 0, "SpawnBomber", %CallerClient, %callPos, %position, %b2add);
      %b3Add = "-10 -10 20";
      %Bomber3 = schedule(4000, 0, "SpawnBomber", %CallerClient, %callPos, %position, %b3add);
      %b4Add = "20 -20 -10";
      %Bomber4 = schedule(6000, 0, "SpawnBomber", %CallerClient, %callPos, %position, %b4add);
   }
   else {
      %b1Add = "-10 10 -20";
      %Bomber1 = SpawnBomber(%CallerClient, %callPos, %position, %b1add);
      %b2Add = "20 20 10";
      %Bomber2 = schedule(2000, 0, "SpawnBomber", %CallerClient, %callPos, %position, %b2add);
      %b3Add = "-10 -10 20";
      %Bomber3 = schedule(4000, 0, "SpawnBomber", %CallerClient, %callPos, %position, %b3add);
      %b4Add = "20 -20 -10";
      %Bomber4 = schedule(6000, 0, "SpawnBomber", %CallerClient, %callPos, %position, %b4add);
   }
}

function BomberImpulse(%obj, %pos, %cl, %count) {
   if(!isObject(%obj)) {
      return;
   }
   //not there yet
   %count++;
   if(vectorDist(%obj.getPosition(), %pos) > 345) {
      if(%count == 2) {
         %count = 0;
      }
      if(vectorLen(%obj.getVelocity()) < 400) {
         %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(), 1535 * 1.3));
      }
   }
   //in range.. BOMB EM
   else {
      if(vectorLen(%obj.getVelocity()) < 400) {
         %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(), 800 * 1.3));
      }
      if(%count == 2) {
         AirstrikeDropBombs(%obj, %pos, %cl);
         %count = 0;
      }
   }
   schedule(500, 0, "BomberImpulse", %obj, %pos, %cl, %count);
}

function AirstrikeDropBombs(%obj, %pos, %cl) {
   if(!isObject(%obj)) {
      return;
   }
   %p = new (BombProjectile)() {
      dataBlock        = BomberBomb;
      initialDirection = "0 0 -5";
      initialPosition  = %obj.getPosition();
      sourceObject     = %obj;
      sourceSlot       = 0;
   };
   MissionCleanup.add(%p);
   //set the projectile to be owned by the caller
   // adds moar kills =-D
   if(isObject(%cl.player)) {
      %p.sourceObject = %cl.player;
   }
}

//--------------------------------------
// HELICOPTER
//--------------------------------------
datablock ItemData(HeliCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = HeliCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(HeliCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = HeliCaller;
   offset = "0 0 0";
   
   isKSSW = 1;

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function HeliCallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasHeli) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   if(Game.CheckModifier("Scrambler") == 1) {
      for(%i = 0; %i < MissionCleanup.getCount(); %i++) {
         %obj = MissionCleanup.getObject(%i);
         if(%obj.isZombie) {
            if(%obj.isAlive()) {
               if(%obj.getDatablock().getName() $= "LordZombieArmor") {
                  messageClient(%obj.client, 'msgHeliComing', "\c5HELLJUMP: A Zombie Lord Is Scrambling the Signal, Helicopters/Harriers cannot be called in at the time.");
                  return;
               }
            }
         }
      }
   }
   %obj.client.xp += 50;
   UpdateClientRank(%obj.client);
   
   $TWM2::HeliCalls[%obj.client.guid]++;
   UpdateSWBeaconFile(%obj.client, "Heli");
   
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.team == %obj.client.team) {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Friendly Helicopter Approaching");
      }
      else {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Enemy Helicopter Inbound");
      }
   }
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Combat Helicopter Called In, +50XP.");
   %obj.client.HasHeli = 0;

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(HeliCaller, 0, true);
   //
   MakeTheHeli(%obj.client);
}

function MakeTheHeli(%cl, %gunner) {
   if(%gunner $= "") {
      %gunner = 0;
   }
   
   if(%gunner) {
      %Heli = new FlyingVehicle() {
         dataBlock = ApacheHelicopter;
         position = VectorAdd(VectorAdd(getRandomPosition(250, 1), "500 0 150"), %cl.player.getPosition());
         team = %cl.team;
      };
      MissionCleanup.add(%Heli);
      %Heli.doneAttack = 0;
      //
      %Heli.team = %cl.team;

      %heli.Targeting = HeliScan(%heli);
      schedule(60000, 0, "EndHeli", %Heli);

      %cl.setControlObject(%Heli.turretObject);
      commandToClient(%cl, 'ControlObjectResponse', true, getControlObjectType(%Heli.turretObject,%cl.player));
      %cl.schedule(500, "setControlObject", %Heli.turretObject);
      HeliControlLoop(%cl, %Heli);
      %cl.player.lastTransformStuff = %cl.player.getTransform();
      %cl.player.setPosition(VectorAdd(%x SPC %y SPC 0,$Prison::JailPos));
   }
   else {
      %Heli = new FlyingVehicle() {
         dataBlock = CombatHelicopter;
         position = VectorAdd(VectorAdd(getRandomPosition(250, 1), "500 0 150"), %cl.player.getPosition());
         team = %cl.team;
      };
      MissionCleanup.add(%Heli);
      %Heli.doneAttack = 0;
      //
      %Heli.team = %cl.team;

      %heli.Targeting = HeliScan(%heli);
      schedule(60000, 0, "EndHeli", %Heli);
   }
}

function HeliControlLoop(%client, %gunship) {
   if(!isObject(%gunship)) {
      if(isObject(%client.player)) {
         ReMoveClientSW(%client);
      }
      return;
   }
   //Remember, we're controlling the turret
   if(%client.getControlObject() != %gunship.turretObject) {
      if(isObject(%client.player)) {
         ReMoveClientSW(%client);
      }
      EndHeli(%gunship);
      return;
   }
   schedule(100, 0, "HeliControlLoop", %client, %gunship);
}


function HeliImpulse(%obj) {
   if(!isObject(%obj)) {
      return;
   }
   if(vectorLen(%obj.getVelocity()) < 500) {
      %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(), 1535 * 1.3));
   }
   schedule(500, 0, "HeliImpulse", %obj);
}

function EndHeli(%heli) {
   if(!isObject(%heli)) {
      return;
   }
   %heli.doneAttack = 1;
   cancel(%heli.Targeting);
   HeliImpulse(%heli);
   %heli.schedule(10000, "delete");
}

function HeliScan(%heli) {
   //echo("scan begin");
   if(!isObject(%heli)) {
      //echo("no heli");
      return;
   }
   if(%heli.doneAttack == 1) {
      //echo("done attacking");
      return;
   }
   InitContainerRadiusSearch(%heli.getposition(), 9999, $TypeMasks::PlayerObjectType);
   while ((%target = containerSearchNext()) != 0) {
      //echo("target "@%target@"");
      if(%target.team != %heli.team) {
         //echo("lock");
         HeliBeginAttack(%heli, %target);
         return;
      }
   }
   //echo("no targs");
   %heli.Targeting = schedule(500, 0, "HeliScan", %heli);
}

function HeliBeginAttack(%heli, %target) {
   %pos = %heli.getworldboxcenter();

   if(!isobject(%heli)) {
      return;
   }
   if(!isObject(%target) || %target.getState() $= "Dead") {
      //echo("dead target");
      %heli.Targeting = schedule(500, 0, "HeliScan", %heli);
      return;
   }
   
   schedule(500, 0, "HeliBeginAttack", %heli, %target);
	%clpos = %target.getPosition();
    if(vectorDist(%clpos, %pos) < 125) {
       return; //no movement needed...
    }
      %vector = vectorNormalize(vectorAdd(vectorSub(%clpos, %pos), "50 0 100"));
	%v1 = getword(%vector, 0);
	%v2 = getword(%vector, 1);
	%nv1 = %v2;
	%nv2 = (%v1 * -1);
	%none = 0;
	%vector2 = %nv1@" "@%nv2@" 10";
	%heli.setRotation(fullrot("0 0 0",%vector2));

	%moveMult = 1535;
	%vector = vectorscale(%vector, %moveMult);

	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);

	%vector = %x@" "@%y@" "@%z@"";
	%heli.applyImpulse(%pos, %vector);

}




//--------------------------------------
// STEALTH BOMBER AIRSTRIKE
//--------------------------------------
datablock ItemData(StealthAirstrikeCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = StealthAirstrikeCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(StealthAirstrikeCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = StealthAirstrikeCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function StealthAirstrikeCallerImage::OnFire(%data, %obj, %slot) {
   %ASCam = new Camera() {
      dataBlock = TWM2ControlCamera;
   };
  // if(!%obj.client.UnlimitedAS) {
   %obj.use(StealthAirstrikeCaller);
   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(StealthAirstrikeCaller, 0, true);
 //  }

   MissionCleanup.add(%ASCam);
   %ASCam.setTransform(%obj.getTransform());
   %ASCam.setFlyMode();
   %ASCam.mode = "StlhAirstrikeCall";
   %obj.client.setControlObject(%ASCam);
   CameraMessageLoop(%obj.client, %ASCam, %ASCam.mode);
}

function StealthAirstrike(%CallerClient, %position, %dirFrom) {
   $TWM2::StealthAirStrikeCalls[%CallerClient.guid]++;
   UpdateSWBeaconFile(%CallerClient, "StealthAirStrike");

   //new stuff TWM2 2.6
   //%dirFrom = Spawn Position of Aircraft
   %THeight = getTerrainHeight(%dirFrom);
   %THeightCons = %THeight + 150;
   //Consider wartower
   if(!$TWM::PlayingWarTower) {
      if((%THeightCons) <= 5 && (%THeightCons) > -200) {
         //baaaaaaad
         %NewZ = %THeight + 150; //give us the perfect height
      }
      else {
         //fine
         %NewZ = getWord(%dirFrom, 2) + 150;
      }
   }
   %CallPos = getWords(%dirFrom, 0, 1) SPC %NewZ;

   %Bomber1 = new FlyingVehicle() {
      dataBlock = BomberFlyer;
      position = %callPos;
      team = %CallerClient.team;
   };
   MissionCleanup.add(%Bomber1);
   //Impulse the bombers
   SlthBomberImpulse(%Bomber1, %position, %CallerClient);
   %Bomber1.setCloaked(true);
   %Bomber1.schedule(30000, "Delete");
   %strikePos = getWords(%position, 0, 1) SPC (getWord(%position, 2) + 150);
   ConstantBomberTurningLoop(%Bomber1, %strikePos);
}

function ccSuperStealth(%sender, %args) {
   if(!%sender.issuperadmin) {
      return 3;
   }
   if(!isObject(%sender.player) || %sender.player.getState() $= "dead") {
      messageClient(%sender, 'msgfail', "\c5Airstrike. youm ust be alivez");
      return;
   }
   SuperStealthAirstrike(%sender);
}

function SuperStealthAirstrike(%CallerClient) {
   %position = %callerClient.player.getPosition();
   %Bomber1 = new FlyingVehicle() {
      dataBlock = BomberFlyer;
      position = VectorAdd(%position, "0 -700 250");
      team = %CallerClient.team;
   };
   MissionCleanup.add(%Bomber1);
   //Impulse the bombers
   SlthBomberImpulse(%Bomber1, %position, %CallerClient);
   %Bomber1.setCloaked(true);
   %Bomber1.schedule(30000, "Delete");
   //
   %Bomber2 = new FlyingVehicle() {
      dataBlock = BomberFlyer;
      position = VectorAdd(%position, "15 -710 250");
      team = %CallerClient.team;
   };
   MissionCleanup.add(%Bomber2);
   //Impulse the bombers
   SlthBomberImpulse(%Bomber2, %position, %CallerClient);
   %Bomber2.setCloaked(true);
   %Bomber2.schedule(30000, "Delete");
   //
   %Bomber3 = new FlyingVehicle() {
      dataBlock = BomberFlyer;
      position = VectorAdd(%position, "-15 -710 250");
      team = %CallerClient.team;
   };
   MissionCleanup.add(%Bomber3);
   //Impulse the bombers
   SlthBomberImpulse(%Bomber3, %position, %CallerClient);
   %Bomber3.setCloaked(true);
   %Bomber3.schedule(30000, "Delete");
   //
   %Bomber4 = new FlyingVehicle() {
      dataBlock = BomberFlyer;
      position = VectorAdd(%position, "15 -725 250");
      team = %CallerClient.team;
   };
   MissionCleanup.add(%Bomber4);
   //Impulse the bombers
   SlthBomberImpulse(%Bomber4, %position, %CallerClient);
   %Bomber4.setCloaked(true);
   %Bomber4.schedule(30000, "Delete");
   //
   %Bomber5 = new FlyingVehicle() {
      dataBlock = BomberFlyer;
      position = VectorAdd(%position, "-15 -725 250");
      team = %CallerClient.team;
   };
   MissionCleanup.add(%Bomber5);
   //Impulse the bombers
   SlthBomberImpulse(%Bomber5, %position, %CallerClient);
   %Bomber5.setCloaked(true);
   %Bomber5.schedule(30000, "Delete");
}

function SlthBomberImpulse(%obj, %pos, %cl) {
   if(!isObject(%obj)) {
      return;
   }
   if(vectorDist(%obj.getPosition(), %pos) > 430) {
      if(vectorLen(%obj.getVelocity()) < 500) {
         %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(), 1200 * 1.3));
      }
   }
   //in range.. BOMB EM
   else {
      %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(), 950));
      AirstrikeDropBombs(%obj, %pos, %cl);
   }
   schedule(500, 0, "SlthBomberImpulse", %obj, %pos, %cl);
}








//--------------------------------------
// UAMS
//--------------------------------------
datablock ItemData(GMCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = GMCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(GMCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = GMCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function GMCallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasGM) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   %obj.client.xp += 50;
   UpdateClientRank(%obj.client);

   $TWM2::GMCalls[%obj.client.guid]++;
   UpdateSWBeaconFile(%obj.client, "GM");

   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.team == %obj.client.team) {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Friendly Missile Strike Approaching");
      }
      else {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Enemy UAMS Detected!!!");
      }
   }
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Missile Strike Called In, +50XP.");
   %obj.client.HasGM = 0;

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(GMCaller, 0, true);
   //
   CreateMissileSat(%obj.client);
}

//--------------------------------------
// Harbinger's Wrath
//--------------------------------------

datablock ItemData(HarbinsWrathCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = HarbinsWrathCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(HarbinsWrathCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = HarbinsWrathCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function HarbinsWrathCallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasHarbinsWrath) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   %obj.client.xp += 100;
   UpdateClientRank(%obj.client);
   
   if($CurrentMission $= "ChristmasMall09") {
      CompleteNWChallenge(%CallerClient, "GunshipMall");
   }

   $TWM2::HarbinsWrathCalls[%obj.client.guid]++;
   UpdateSWBeaconFile(%obj.client, "HarbinsWrath");

   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.team == %obj.client.team) {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Friendly Gunship Approaching");
      }
      else {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Enemy Gunship... INCOMING!!!");
      }
   }
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Harbinger's Wrath Called In, +100XP.");
   %obj.client.HasHarbinsWrath = 0;

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(HarbinsWrathCaller, 0, true);
   //
   if($TWM2::UnmannedGunship) {
      StartHarbingersWrath(%obj.client, 1);
   }
   else {
      StartHarbingersWrath(%obj.client, 0);
   }
}







//AC130
datablock ItemData(AC130Caller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = AC130CallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(AC130CallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = AC130Caller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function AC130CallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasAcGunner) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   %obj.client.xp += 100;
   UpdateClientRank(%obj.client);

   if($CurrentMission $= "ChristmasMall09") {
      CompleteNWChallenge(%CallerClient, "GunshipMall");
   }

   $TWM2::AC130Calls[%obj.client.guid]++;
   UpdateSWBeaconFile(%obj.client, "AC130");

   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.team == %obj.client.team) {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Friendly AC-130 Approaching");
      }
      else {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Enemy AC130 ABOVE!!!");
      }
   }
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: AC-130 Called In, +100XP.");
   %obj.client.HasAcGunner = 0;

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(AC130Caller, 0, true);
   //
   if($TWM2::UnmannedGunship) {
      StartAC130(%obj.client, 1);
   }
   else {
      StartAC130(%obj.client, 0);
   }
}




















datablock ItemData(ChopperGunnerCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = ChopperGunnerCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(ChopperGunnerCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = ChopperGunnerCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function ChopperGunnerCallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasChopperGunner) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   %obj.client.xp += 100;
   UpdateClientRank(%obj.client);

   $TWM2::ChopperGunnerCalls[%obj.client.guid]++;
   UpdateSWBeaconFile(%obj.client, "ChopperGunner");

   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.team == %obj.client.team) {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Friendly Apache Approaching");
      }
      else {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Enemy Apache... INCOMING!!!");
      }
   }
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Apache Gunner Called In, +100XP.");
   %obj.client.HasChopperGunner = 0;

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(ChopperGunnerCaller, 0, true);
   //
   MakeTheHeli(%obj.client, 1);
}

//--------------------------------------
// Artillery
//--------------------------------------
datablock GrenadeProjectileData(AStrikeColliderShell) {
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 3.6;
   damageRadius        = 25.0;
   radiusDamageType    = $DamageType::Bomb;
   kickBackStrength    = 500;

   explosion           = "MiniProtonColliderExplosion";
   velInheritFactor    = 0.0;
   splash              = GrenadeSplash;

   baseEmitter         = GrenadeBubbleEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;

   grenadeElasticity = 0.0;
   grenadeFriction   = 0.3;
   armingDelayMS     = -1;
   gravityMod        = 1.0;
   muzzleVelocity    = 225.0;
   drag              = 0.1;

   sound			 = MortarTurretProjectileSound;

   hasLight    = true;
   lightRadius = 3;
   lightColor  = "0.05 0.2 0.05";
};

datablock ItemData(ArtilleryCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = ArtilleryCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   
   isKSSW = 1;

};

datablock ShapeBaseImageData(ArtilleryCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = ArtilleryCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function ArtilleryCallerImage::OnFire(%data, %obj, %slot) {
   %ASCam = new Camera() {
      dataBlock = TWM2ControlCamera;
   };
   //if(!%obj.client.UnlimitedAS) {
      %obj.use(ArtilleryCaller);
      %obj.throwWeapon(1);
      %obj.throwWeapon(0);
      %obj.setInventory(ArtilleryCaller, 0, true);
   //}

   MissionCleanup.add(%ASCam);
   %ASCam.setTransform(%obj.getTransform());
   %ASCam.setFlyMode();
   %ASCam.mode = "ArtilleryCall";
   %obj.client.setControlObject(%ASCam);
   CameraMessageLoop(%obj.client, %ASCam, %ASCam.mode);
}

function Artillery(%CallerClient, %position) {

  // if(!%CallerClient.UnlimitedAS) {
      $TWM2::ArtilleryCalls[%CallerClient.guid]++;
      UpdateSWBeaconFile(%CallerClient, "Artillery");
  // }
   if(ServerReturnMonthDate() $= "1221") {
      CompleteNWChallenge(%CallerClient, "SoulsticeBombard");
   }
   %mainUpPos = vectoradd(%position, "0 0 400");// main pos
   for(%i=0;%i<25;%i++) {
      schedule(350*%i, 0, MessageAll, 'msgFiah', "~wfx/powered/turret_mortar_fire.wav");
      %mainUpPos = vectoradd(%mainUpPos, "0 0 "@(300+(%i*75))@"");   //increment by 100 each time
      %final = vectoradd(%mainUpPos,GetRandomPosition(30,1));
      %Shell1 = new GrenadeProjectile() {
         dataBlock        = AStrikeColliderShell;
         initialPosition  = %final;
         initialDirection = "0 0 -5";   // this will hit first
      };
      %Shell1.sourceObject = %CallerClient.player;
   }
}

//--------------------------------------
// Nuke
//--------------------------------------
datablock ItemData(NukeCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = NukeCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(NukeCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = NukeCaller;
   offset = "0 0 0";
   
   isKSSW = 1;

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function NukeCallerImage::OnFire(%data, %obj, %slot) {
   %ASCam = new Camera() {
      dataBlock = TWM2ControlCamera;
   };
   //if(!%obj.client.UnlimitedAS) {
      %obj.use(NukeCaller);
      %obj.throwWeapon(1);
      %obj.throwWeapon(0);
      %obj.setInventory(NukeCaller, 0, true);
   //}

   MissionCleanup.add(%ASCam);
   %ASCam.setTransform(%obj.getTransform());
   %ASCam.setFlyMode();
   %ASCam.mode = "NukeCall";
   %obj.client.setControlObject(%ASCam);
   CameraMessageLoop(%obj.client, %ASCam, %ASCam.mode);
}

function Nuke(%CallerClient, %position) {
   awardClient(%callerClient, 024);
   if(ServerReturnMonthDate() $= "0101") {
      CompleteNWChallenge(%CallerClient, "NewYears");
   }
  // if(!%CallerClient.UnlimitedAS) {
      $TWM2::NukeCalls[%CallerClient.guid]++;
      UpdateSWBeaconFile(%CallerClient, "Nuke");
  // }
   %mainUpPos = vectoradd(%position, "0 0 400");// main pos
      %Shell1 = new SeekerProjectile() {
         dataBlock        = ShoulderNuclear;
         initialPosition  = %mainUpPos;
         initialDirection = "0 0 -5";   // this will hit first
      };
      %Shell1.sourceObject = %CallerClient.player;
  // }
}















//--------------------------------------
// HELICOPTER
//--------------------------------------
datablock ItemData(GunshipHeliCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = GunshipHeliCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(GunshipHeliCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = GunshipHeliCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function GunshipHeliCallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasGunshipHeli) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   if(Game.CheckModifier("Scrambler") == 1) {
      for(%i = 0; %i < MissionCleanup.getCount(); %i++) {
         %obj = MissionCleanup.getObject(%i);
         if(%obj.isZombie) {
            if(%obj.isAlive()) {
               if(%obj.getDatablock().getName() $= "LordZombieArmor") {
                  messageClient(%obj.client, 'msgHeliComing', "\c5HELLJUMP: A Zombie Lord Is Scrambling the Signal, Helicopters/Harriers cannot be called in at the time.");
                  return;
               }
            }
         }
      }
   }
   %obj.client.xp += 250;
   UpdateClientRank(%obj.client);

   $TWM2::GunshipHeliCalls[%obj.client.guid]++;
   UpdateSWBeaconFile(%obj.client, "GunshipHeli");

   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.team == %obj.client.team) {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Friendly Assault Helicopter Approaching");
      }
      else {
         messageClient(%cl, 'msgHeliComing', "\c5TWM2: Enemy Gunship Helicopter Inbound!!!");
      }
   }
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Gunship Helicopter Called In, +250XP.");
   %obj.client.HasGunshipHeli = 0;

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(GunshipHeliCaller, 0, true);
   //
   MakeTheHeli2(%obj.client, 0);
}

//this one functions for gunship helicopters and harriers
function MakeTheHeli2(%cl, %harrier) {
   if(%harrier $= "") {
      %harrier = 0;
   }
   if(!%harrier) {
      %Heli = new FlyingVehicle() {
         dataBlock = GunshipHelicopter;
         position = VectorAdd(VectorAdd(getRandomPosition(250, 1), "500 0 150"), %cl.player.getPosition());
         team = %cl.team;
      };
      MissionCleanup.add(%Heli);
      %Heli.doneAttack = 0;
      //
      %Heli.team = %cl.team;
      
      %heli.canFireMissiles = 1;

      %heli.Targeting = GunshipHeliScan(%heli);
      schedule(60000, 0, "EndHeli", %Heli);
   }
   else {
      %Heli = new FlyingVehicle() {
         dataBlock = Harrier;
         position = VectorAdd(VectorAdd(getRandomPosition(250, 1), "500 0 150"), %cl.player.getPosition());
         team = %cl.team;
      };
      MissionCleanup.add(%Heli);
      %Heli.doneAttack = 0;
      //
      %Heli.team = %cl.team;

      %heli.Targeting = HeliScan(%heli);
      schedule(60000, 0, "EndHeli", %Heli);
   }
}

function GunshipHeliScan(%heli) {
   //echo("scan begin");
   if(!isObject(%heli)) {
      //echo("no heli");
      return;
   }
   if(%heli.doneAttack == 1) {
      //echo("done attacking");
      return;
   }
   InitContainerRadiusSearch(%heli.getposition(), 9999, $TypeMasks::PlayerObjectType);
   while ((%target = containerSearchNext()) != 0) {
      //echo("target "@%target@"");
      if(%target.team != %heli.team) {
         //echo("lock");
         GunshipHeliBeginAttack(%heli, %target);
         return;
      }
   }
   //echo("no targs");
   %heli.Targeting = schedule(500, 0, "GunshipHeliScan", %heli);
}

function HeliUseMissiles(%heli, %target) {
   if(!isobject(%heli)) {
      return;
   }
   if(!isObject(%target) || %target.getState() $= "Dead") {
      return;
   }
   %clpos = %target.getPosition();
   %num = getRandom(250, 1000);
   %vec = vectorsub(VectorAdd(%clpos, "0 0 2.2"), %heli.turretObject.getMuzzlePoint(0));
   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/%num));
   //
   %p = new SeekerProjectile() { //TWM2 Sniper zombies use M1 Snipers :P
	     dataBlock        = HornetStrikeMissile;
	     initialDirection = %vec;
	     initialPosition  = %heli.turretObject.getMuzzlePoint(0);
	     sourceObject     = %heli;
   };
   ServerPlay3d(EscapePodLaunchSound, %heli.getPosition());
   ServerPlay3d(EscapePodLaunchSound2, %heli.getPosition());
}

function ResetHeliMissiles(%heli) {
   %heli.canFireMissiles = 1;
}

function GunshipHeliBeginAttack(%heli, %target) {
   %pos = %heli.getworldboxcenter();

   if(!isobject(%heli)) {
      return;
   }
   if(!isObject(%target) || %target.getState() $= "Dead") {
      //echo("dead target");
      %heli.Targeting = schedule(500, 0, "GunshipHeliScan", %heli);
      return;
   }
   %clpos = %target.getPosition();
   if(%heli.canFireMissiles) {
      HeliUseMissiles(%heli, %target);
      schedule(750, 0, "HeliUseMissiles", %heli, %target);
      schedule(1500, 0, "HeliUseMissiles", %heli, %target);
      %heli.canFireMissiles = 0;
      schedule(25000, 0, "ResetHeliMissiles", %heli);
   }

    schedule(500, 0, "GunshipHeliBeginAttack", %heli, %target);

    if(vectorDist(%clpos, %pos) < 125) {
       return; //no movement needed...
    }
    %vector = vectorNormalize(vectorAdd(vectorSub(%clpos, %pos), "50 0 100"));
	%v1 = getword(%vector, 0);
	%v2 = getword(%vector, 1);
	%nv1 = %v2;
	%nv2 = (%v1 * -1);
	%none = 0;
	%vector2 = %nv1@" "@%nv2@" 0";
	%heli.setRotation(fullrot("0 0 0",%vector2));

	%moveMult = 1535;
	%vector = vectorscale(%vector, %moveMult);

	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);

	%vector = %x@" "@%y@" "@%z@"";
	%heli.applyImpulse(%pos, %vector);

}












//--------------------------------------
// HARRIER AIRSTRIKE
//--------------------------------------
datablock ItemData(HarrierAirstrikeCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = HarrierAirstrikeCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(HarrierAirstrikeCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = HarrierAirstrikeCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function HarrierAirstrikeCallerImage::OnFire(%data, %obj, %slot) {
   %ASCam = new Camera() {
      dataBlock = TWM2ControlCamera;
   };
   //if(!%obj.client.UnlimitedAS) {
      %obj.use(AirstrikeCaller);
      %obj.throwWeapon(1);
      %obj.throwWeapon(0);
      %obj.setInventory(HarrierAirstrikeCaller, 0, true);
   //}

   MissionCleanup.add(%ASCam);
   %ASCam.setTransform(%obj.getTransform());
   %ASCam.setFlyMode();
   %ASCam.mode = "HarrierCall";
   %obj.client.setControlObject(%ASCam);
   CameraMessageLoop(%obj.client, %ASCam, %ASCam.mode);
}

function HarrierAirstrike(%CallerClient, %position, %dirFrom) {

   $TWM2::HarrierAirstrikeCalls[%CallerClient.guid]++;
   UpdateSWBeaconFile(%CallerClient, "HarrierAirStrike");

   //new stuff TWM2 2.6
   //%dirFrom = Spawn Position of Aircraft
   %THeight = getTerrainHeight(%dirFrom);
   %THeightCons = %THeight + 150;
   //Consider wartower
   if(!$TWM::PlayingWarTower) {
      if((%THeightCons) <= 5 && (%THeightCons) > -200) {
         //baaaaaaad
         %NewZ = %THeight + 150; //give us the perfect height
      }
      else {
         //fine
         %NewZ = getWord(%dirFrom, 2) + 150;
      }
   }
   %CallPos = getWords(%dirFrom, 0, 1) SPC %NewZ;

   %Bomber1 = new FlyingVehicle() {
      dataBlock = Harrier;
      position = VectorAdd(%CallPos, "17 -50 10");
      team = %CallerClient.team;
   };
   %Bomber2 = new FlyingVehicle() {
      dataBlock = Harrier;
      position = VectorAdd(%CallPos, "-17 50 -10");
      team = %CallerClient.team;
   };
   MissionCleanup.add(%Bomber1);
   MissionCleanup.add(%Bomber2);
   //Impulse the bombers
   HarrierBomberImpulse(%Bomber1, %position, %CallerClient, 3);
   HarrierBomberImpulse(%Bomber2, %position, %CallerClient, 3);
   //
   %Bomber1.schedule(30000, "Delete");
   %Bomber2.schedule(30000, "Delete");
   //
   %strikePos = getWords(%position, 0, 1) SPC (getWord(%position, 2) + 150);
   ConstantBomberTurningLoop(%Bomber1, %strikePos);
   ConstantBomberTurningLoop(%Bomber2, %strikePos);
   //
   //The Remaining one
   schedule(8000, 0, MakeTheHeli2, %CallerClient, 1);
}

function HarrierBomberImpulse(%obj, %pos, %cl, %ammoleft) {
   if(!isObject(%obj)) {
      return;
   }
   //not there yet
   if(vectorDist(%obj.getPosition(), %pos) > 530) {
      if(vectorLen(%obj.getVelocity()) < 500) {
         %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(), 1535 * 1.3));
      }
   }
   //in range.. BOMB EM
   else {
      %obj.applyImpulse(%obj.getPosition(),vectorScale(%obj.getForwardVector(), 800));
      if(%ammoleft > 0) {
         AirstrikeDropBombs(%obj, %pos, %cl);
         %ammoleft--;
      }
   }
   schedule(500, 0, "HarrierBomberImpulse", %obj, %pos, %cl, %ammoleft);
}

//--------------------------------------
// ZBomb
//--------------------------------------
datablock ItemData(ZBombCaller)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = ZBombCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(ZBombCallerImage)
{
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = ZBombCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function ZBombCallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasZBomb) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   %obj.client.HasZBomb = 0;
   %obj.client.xp += 1000;
   UpdateClientRank(%obj.client);
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Z-Bomb Activated, +1000XP.");
   MessageAll('msgWohoo', "\c5TWM2: "@%obj.client.namebase@" has activated a Z-Bomb, eliminating all zombies");

   $TWM2::ZBombCalls[%obj.client.guid]++;
   UpdateSWBeaconFile(%obj.client, "ZBomb");

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(ZBombCaller, 0, true);

   if($TWM::PlayingHorde) {
      CompleteNWChallenge(%obj.client, "ZBomber");
   }

   %count = MissionCleanup.getCount();
   for(%i = 0; %i < %count; %i++) {
      %tobj = MissionCleanup.getObject(%i);
      if(isObject(%tobj)) {
         if(%tobj.iszombie && !%tobj.isBoss) {
            %tobj.damage(%obj,%tobj.getWorldBoxCenter(), 100.0, $DamageType::ZBomb); //lotsa EXP for mah kills :D
         }
         else {
            continue;
         }
      }
   }
   //flashy and soundy
   %count2 = ClientGroup.getCount();
   for(%x = 0; %x < %count2; %x++) {
      %flcl = ClientGroup.getObject(%x);
      messageClient(%flcl, 'msgSound', "~wfx/weapons/mortar_explode.wav");
      if(isObject(%flcl.player)) {
         %flcl.player.setWhiteout(1.8);
      }
   }
}










datablock ItemData(FissionBombCaller)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = FissionBombCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(FissionBombCallerImage)
{
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = FissionBombCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function FissionBombCallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasFission) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   %obj.client.HasFission = 0;
   %obj.client.xp += 2500;
   UpdateClientRank(%obj.client);
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Activating fission bomb.. nice work :D +2500XP.");

   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(FissionBombCaller, 0, true);
   
   CompleteNWChallenge(%obj.client, "GameEnder");
   
   MessageAll('msgItsOva', "\c5COMMAND: FISSION BOMB!!! IT'S OVER!! RUN!!!!!! ~wfx/misc/red_alert_short.wav");
   FissionBombLoop(%obj.client, %obj, %obj.getPosition(), 10);
}

function FissionBombLoop(%client, %caller, %strikePos, %ticks) {
   if(%ticks == 10) {
      schedule(500, 0,spawnprojectile,VegenorFireMeteor,GrenadeProjectile, vectorAdd(%strikePos ,"0 0 700"), "0 0 -1");
   }
   if(%ticks > 0) {
      CenterPrintAll("FISSION BOMB IMPACT IN: "@%ticks@".", 1, 1);
      messageAll('msgSiren', "~wfx/misc/red_alert_short.wav");
      schedule(1000, 0, "FissionBombLoop", %client, %caller, %strikePos, %ticks--);
      return;
   }
   else {
      $TeamScore[%client.team] += 99999;
      %client.score += 999999;
      if($FissionEndsGame) {
         Game.schedule(5500, GameOver);
         schedule(5500, 0, cycleMissions);
      }
      Aidpulse(%strikePos, %caller, 5);
      return;
   }
}






























//
//EMIITAHS and PARTICLES
datablock ShockwaveData(HyperDevProj2Shockwave)
{
   width = 10;
   numSegments = 60;
   numVertSegments = 1;
   velocity = 0;
   acceleration = 0;
   lifetimeMS = 3000;
   height = 0.1;
   is2D = false;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 2.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "0.2 0.2 1 1";
   colors[1] = "0.2 0.4 1.0 0.50";
   colors[2] = "0.2 0.3 1 0.1";

   mapToTerrain = false;
   orientToNormal = true;
   renderBottom = true;
};

datablock ExplosionData(HyperDev2SubExplosion1)
{
   explosionShape = "disc_explosion.dts";
   faceViewer           = true;

   delayMS = 100;

   offset = 5.0;

   playSpeed = 1.5;

   sizes[0] = "1 1 1";
   sizes[1] = "1 1 1";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock ExplosionData(HyperDev2SubExplosion2)
{
   explosionShape = "disc_explosion.dts";
   faceViewer           = true;

   delayMS = 50;

   offset = 5.0;

   playSpeed = 1.0;

   sizes[0] = "5.0 5.0 5.0";
   sizes[1] = "5.0 5.0 5.0";
   times[0] = 0.0;
   times[1] = 1.0;
};

datablock ExplosionData(HyperDev2SubExplosion3)
{
   explosionShape = "disc_explosion.dts";
   faceViewer           = true;
   delayMS = 0;
   offset = 0.0;
   playSpeed = 0.7;

   sizes[0] = "10.0 10.0 10.0";
   sizes[1] = "20.0 20.0 20.0";
   times[0] = 0.0;
   times[1] = 1.0;

};

datablock AudioProfile(HyperDevCannonExplosionSound2)
{
	filename = "fx/explosions/explosion.xpl23.wav";
	description = AudioBIGExplosion3d;
   preload = true;
};

datablock ExplosionData(HyperDevCannonExplosion2)
{
   soundProfile   = HyperDevCannonExplosionSound2;

   shockwave[0] = HyperDevCannonShockwave2;
   shockwaveOnTerrain[0] = true;

   subExplosion[0] = HyperDev2SubExplosion1;
   subExplosion[1] = HyperDev2SubExplosion2;
   subExplosion[2] = HyperDev2SubExplosion3;
   subExplosion[3] = SatchelSubExplosion;
   subExplosion[4] = SatchelSubExplosion2;
   subExplosion[5] = SatchelSubExplosion3;

   emitter[0] = PlasmaBarrelCrescentEmitter;
   emitter[1] = SatchelSparksEmitter;
   emitter[2] = SatchelExplosionSmokeEmitter;

   shakeCamera = true;
   camShakeFreq = "8.0 9.0 7.0";
   camShakeAmp = "100.0 100.0 100.0";
   camShakeDuration = 1.3;
   camShakeRadius = 25.0;
};

datablock ParticleData(HyperDevSmokeParticle) {
	dragCoeffiecient     = 0.0;
    windCoeffiecient     = 0.0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;

	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 3000;

	textureName          = "skins/mort012";

	useInvAlpha = false;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;

	colors[0]     = "0.0 1 1 1.0";
	colors[1]     = "0.0 0 1 1.0";
	colors[2]     = "0.0 1 0 1.0";
	sizes[0]      = 6.0;
	sizes[1]      = 6.0;
	sizes[2]      = 6.5;
	times[0]      = 0.0;
	times[1]      = 0.2;
	times[2]      = 1.0;
};

datablock ParticleEmitterData(HyperDevCannonBaseEmitter) {
	ejectionPeriodMS = 0.2;
	periodVarianceMS = 1;

	ejectionVelocity = 25;
	velocityVariance = 0.0;

	thetaMin         = 0.0;
	thetaMax         = 0.0;

	particles = "HyperDevSmokeParticle";
};

datablock ShockwaveData(HyperDevProjShockwave)
{
   width = 7.0;
   numSegments = 16;
   numVertSegments = 16;
   velocity = 5;
   acceleration = 4.0;
   lifetimeMS = 1570;
   height = 3;
   is2D = false;

   texture[0] = "special/shockwave5";
   texture[1] = "special/lightning1frame2";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "0.0 0.8 0.8 1.00";
   colors[1] = "0.0 0.5 0.7 0.20";
   colors[2] = "0.0 0.8 0.5 0.0";

   mapToTerrain = false;
   orientToNormal = true;
   renderBottom = true;
};

datablock LinearFlareProjectileData(HyperDevestatorBeam) {
   scale               = "15.0 15.0 15.0";
   faceViewer          = false;
   directDamage        = 1.0;
   hasDamageRadius     = true;
   indirectDamage      = 4.9;
   damageRadius        = 30.0;
   kickBackStrength    = 40000.0;
   radiusDamageType    = $DamageType::Explosion;

   explosion[0]           = "HyperDevCannonExplosion2";
   explosion[1]           = "SatchelMainExplosion";
   splash              = PlasmaSplash;
   baseEmitter         = HyperDevCannonBaseEmitter;


   dryVelocity       = 500.0;
   wetVelocity       = 200;
   velInheritFactor  = 0.5;
   fizzleTimeMS      = 20000;
   lifetimeMS        = 20000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   //activateDelayMS = 100;
   activateDelayMS = -1;

   size[0]           = 9;
   size[1]           = 10;
   size[2]           = 11;


   numFlares         = 400;
   flareColor        = "0.0 1.0 0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";

   sound        = MissileProjectileSound;
   fireSound    = PlasmaFireSound;
   wetFireSound = PlasmaFireWetSound;

   hasLight    = true;
   lightRadius = 3.0;
   lightColor  = "0 0.75 0.25";

};

//--------------------------------------
// AIRSTRIKE
//--------------------------------------
datablock ItemData(OLSCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = OLSCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(OLSCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = OLSCaller;
   offset = "0 0 0";

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;
   
   isKSSW = 1;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function OLSCallerImage::OnFire(%data, %obj, %slot) {
   %ASCam = new Camera() {
      dataBlock = TWM2ControlCamera;
   };
   %obj.use(OLSCaller);
   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(OLSCaller, 0, true);

   MissionCleanup.add(%ASCam);
   %ASCam.setTransform(%obj.getTransform());
   %ASCam.setFlyMode();
   %ASCam.mode = "OLSCall";
   %obj.client.setControlObject(%ASCam);
   CameraMessageLoop(%obj.client, %ASCam, %ASCam.mode);
}

function OrbitalLaserStrike(%CallerClient, %position) {
   %p = new LinearFlareProjectile() {
      dataBlock        = HyperDevestatorBeam;
      initialDirection = "0 0 -10";
      initialPosition  = vectoradd(%position, "0 0 1500");
	  sourceSlot       = 4;
   };
   %p.sourceObject = %CallerClient.player;
   MissionCleanup.add(%p);
}







//--------------------------------------
// MASS EMP
//--------------------------------------
datablock ItemData(MassEMPCaller) {
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_targeting.dts";
   image        = MassEMPCallerImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
	pickUpName = "a targeting laser rifle";

   computeCRC = true;
   isKSSW = 1;

};

datablock ShapeBaseImageData(MassEMPCallerImage) {
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   item = MassEMPCaller;
   offset = "0 0 0";
   
   isKSSW = 1;

   projectile = BasicTargeter;
   projectileType = TargetProjectile;
   deleteLastProjectile = true;

	usesEnergy = true;
	minEnergy = 3;

   stateName[0]                     = "Activate";
   stateSequence[0]                 = "Activate";
	stateSound[0]                    = TargetingLaserSwitchSound;
   stateTimeoutValue[0]             = 0.5;
   stateTransitionOnTimeout[0]      = "ActivateReady";

   stateName[1]                     = "ActivateReady";
   stateTransitionOnAmmo[1]         = "Ready";
   stateTransitionOnNoAmmo[1]       = "NoAmmo";

   stateName[2]                     = "Ready";
   stateTransitionOnNoAmmo[2]       = "NoAmmo";
   stateTransitionOnTriggerDown[2]  = "Fire";

   stateName[3]                     = "Fire";
	stateEnergyDrain[3]              = 3;
   stateFire[3]                     = true;
   stateAllowImageChange[3]         = false;
   stateScript[3]                   = "onFire";
   stateTransitionOnTriggerUp[3]    = "Deconstruction";
   stateTransitionOnNoAmmo[3]       = "Deconstruction";
   stateSound[3]                    = TargetingLaserPaintSound;

   stateName[4]                     = "NoAmmo";
   stateTransitionOnAmmo[4]         = "Ready";

   stateName[5]                     = "Deconstruction";
   stateScript[5]                   = "deconstruct";
   stateTransitionOnTimeout[5]      = "Ready";
};

function MassEMPCallerImage::OnFire(%data, %obj, %slot) {
   if(!%obj.client.HasMassEMP) {
      MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Nice Try.");
      return;
   }
   %obj.use(MassEMPCaller);
   %obj.throwWeapon(1);
   %obj.throwWeapon(0);
   %obj.setInventory(MassEMPCaller, 0, true);

   %obj.client.xp += 750;
   UpdateClientRank(%obj.client);
   MessageClient(%obj.client, 'MsgZKill', "\c5TWM2: Mass EMP Activated +750XP.");

   %obj.client.HasMassEMP = 0;
   for(%i = 0; %i < ClientGroup.getCount(); %i++) {
      %cl = ClientGroup.getObject(%i);
      if(%cl.team != %obj.client.team) {
         messageClient(%cl, 'msgAlert', "\c5Command: EMP! Electronic Weapons Offline!");
         ApplyEMP(%cl);
         schedule(180000, 0, "KillEMP", %cl);
      }
      messageClient(%cl, 'msgSound', "~wfx/weapons/mortar_explode.wav");
      if(isObject(%cl.player)) {
         %cl.player.setWhiteout(1.0);
      }
   }
   //make vehicles go boom.
   %count = MissionCleanup.getCount();
   for (%i=0;%i<%count;%i++) {
	  %obj = MissionCleanup.getObject(%i);
	  if (%obj) {
	     if ((%obj.getType() & $TypeMasks::VehicleObjectType)) {
	        %random = getRandom() * 100;
	        %obj.schedule(%random, setDamageState , Destroyed);
	     }
      }
   }
}





































































//FILE STUFF
function UpdateSWBeaconFile(%client, %SW) {
   %file = ""@$TWM::RanksDirectory@"/"@%client.guid@"/Saved.TWMSave";
   new fileobject(Kills2);
   switch$(%SW) {
      case "UAV":
         //awards
         if($TWM2::UAVCalls[%client.guid] >= 30) {
            CompleteNWChallenge(%client, "UAV1");
         }
         if($TWM2::UAVCalls[%client.guid] >= 75) {
            CompleteNWChallenge(%client, "UAV2");
         }
         if($TWM2::UAVCalls[%client.guid] >= 150) {
            CompleteNWChallenge(%client, "UAV3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::UAVCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::UAVCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::UAVCalls["@%client.guid@"] = "@$TWM2::UAVCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::UAVCalls["@%client.guid@"] = "@$TWM2::UAVCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "AirStrike":
         //awards
         if($TWM2::AirstrikeCalls[%client.guid] >= 25) {
            CompleteNWChallenge(%client, "Airstrike1");
         }
         if($TWM2::AirstrikeCalls[%client.guid] >= 65) {
            CompleteNWChallenge(%client, "Airstrike2");
         }
         if($TWM2::AirstrikeCalls[%client.guid] >= 125) {
            CompleteNWChallenge(%client, "Airstrike3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::AirstrikeCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::AirstrikeCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::AirstrikeCalls["@%client.guid@"] = "@$TWM2::AirstrikeCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::AirstrikeCalls["@%client.guid@"] = "@$TWM2::AirstrikeCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "GM":
         //awards
         if($TWM2::GMCalls[%client.guid] >= 25) {
            CompleteNWChallenge(%client, "UAMS1");
         }
         if($TWM2::GMCalls[%client.guid] >= 65) {
            CompleteNWChallenge(%client, "UAMS2");
         }
         if($TWM2::GMCalls[%client.guid] >= 125) {
            CompleteNWChallenge(%client, "UAMS3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::GMCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::GMCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::GMCalls["@%client.guid@"] = "@$TWM2::GMCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::GMCalls["@%client.guid@"] = "@$TWM2::GMCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "HarbinsWrath":
         //awards
         if($TWM2::HarbinsWrathCalls[%client.guid] >= 15) {
            CompleteNWChallenge(%client, "Gunship1");
         }
         if($TWM2::HarbinsWrathCalls[%client.guid] >= 35) {
            CompleteNWChallenge(%client, "Gunship2");
         }
         if($TWM2::HarbinsWrathCalls[%client.guid] >= 75) {
            CompleteNWChallenge(%client, "Gunship3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::HarbinsWrathCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::HarbinsWrathCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::HarbinsWrathCalls["@%client.guid@"] = "@$TWM2::HarbinsWrathCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::HarbinsWrathCalls["@%client.guid@"] = "@$TWM2::HarbinsWrathCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "AC130":
         //awards
         if($TWM2::AC130Calls[%client.guid] >= 15) {
            CompleteNWChallenge(%client, "ACGunship1");
         }
         if($TWM2::AC130Calls[%client.guid] >= 35) {
            CompleteNWChallenge(%client, "ACGunship2");
         }
         if($TWM2::AC130Calls[%client.guid] >= 75) {
            CompleteNWChallenge(%client, "ACGunship3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::AC130Calls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::AC130Calls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::AC130Calls["@%client.guid@"] = "@$TWM2::AC130Calls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::AC130Calls["@%client.guid@"] = "@$TWM2::AC130Calls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "Heli":
         //awards
         if($TWM2::HeliCalls[%client.guid] >= 25) {
            CompleteNWChallenge(%client, "Helicopter1");
         }
         if($TWM2::HeliCalls[%client.guid] >= 65) {
            CompleteNWChallenge(%client, "Helicopter2");
         }
         if($TWM2::HeliCalls[%client.guid] >= 125) {
            CompleteNWChallenge(%client, "Helicopter3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::HeliCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::HeliCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::HeliCalls["@%client.guid@"] = "@$TWM2::HeliCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::HeliCalls["@%client.guid@"] = "@$TWM2::HeliCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "HarrierAirStrike":
         //awards
         if($TWM2::HarrierAirstrikeCalls[%client.guid] >= 20) {
            CompleteNWChallenge(%client, "Harrier1");
         }
         if($TWM2::HarrierAirstrikeCalls[%client.guid] >= 55) {
            CompleteNWChallenge(%client, "Harrier2");
         }
         if($TWM2::HarrierAirstrikeCalls[%client.guid] >= 110) {
            CompleteNWChallenge(%client, "Harrier3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::HarrierAirstrikeCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::HarrierAirstrikeCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::HarrierAirstrikeCalls["@%client.guid@"] = "@$TWM2::HarrierAirstrikeCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::HarrierAirstrikeCalls["@%client.guid@"] = "@$TWM2::HarrierAirstrikeCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "GunshipHeli":
         //awards
         if($TWM2::GunshipHeliCalls[%client.guid] >= 20) {
            CompleteNWChallenge(%client, "GunHeli1");
         }
         if($TWM2::GunshipHeliCalls[%client.guid] >= 55) {
            CompleteNWChallenge(%client, "GunHeli2");
         }
         if($TWM2::GunshipHeliCalls[%client.guid] >= 110) {
            CompleteNWChallenge(%client, "GunHeli3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::GunshipHeliCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::GunshipHeliCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::GunshipHeliCalls["@%client.guid@"] = "@$TWM2::GunshipHeliCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::GunshipHeliCalls["@%client.guid@"] = "@$TWM2::GunshipHeliCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "ChopperGunner":
         //awards
         if($TWM2::ChopperGunnerCalls[%client.guid] >= 15) {
            CompleteNWChallenge(%client, "Apache1");
         }
         if($TWM2::ChopperGunnerCalls[%client.guid] >= 35) {
            CompleteNWChallenge(%client, "Apache2");
         }
         if($TWM2::ChopperGunnerCalls[%client.guid] >= 75) {
            CompleteNWChallenge(%client, "Apache3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::ChopperGunnerCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::ChopperGunnerCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::ChopperGunnerCalls["@%client.guid@"] = "@$TWM2::ChopperGunnerCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::ChopperGunnerCalls["@%client.guid@"] = "@$TWM2::ChopperGunnerCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "Artillery":
         //awards
         if($TWM2::ArtilleryCalls[%client.guid] >= 10) {
            CompleteNWChallenge(%client, "Centaur1");
         }
         if($TWM2::ArtilleryCalls[%client.guid] >= 25) {
            CompleteNWChallenge(%client, "Centaur2");
         }
         if($TWM2::ArtilleryCalls[%client.guid] >= 50) {
            CompleteNWChallenge(%client, "Centaur3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::ArtilleryCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::ArtilleryCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::ArtilleryCalls["@%client.guid@"] = "@$TWM2::ArtilleryCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::ArtilleryCalls["@%client.guid@"] = "@$TWM2::ArtilleryCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "StealthAirStrike":
         //awards
         if($TWM2::StealthAirStrikeCalls[%client.guid] >= 20) {
            CompleteNWChallenge(%client, "SBomber1");
         }
         if($TWM2::StealthAirStrikeCalls[%client.guid] >= 50) {
            CompleteNWChallenge(%client, "SBomber2");
         }
         if($TWM2::StealthAirStrikeCalls[%client.guid] >= 100) {
            CompleteNWChallenge(%client, "SBomber3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::StealthAirStrikeCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::StealthAirStrikeCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::StealthAirStrikeCalls["@%client.guid@"] = "@$TWM2::StealthAirStrikeCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::StealthAirStrikeCalls["@%client.guid@"] = "@$TWM2::StealthAirStrikeCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "Nuke":
         //awards
         if($TWM2::NukeCalls[%client.guid] >= 5) {
            CompleteNWChallenge(%client, "Nuke1");
         }
         if($TWM2::NukeCalls[%client.guid] >= 10) {
            CompleteNWChallenge(%client, "Nuke2");
         }
         if($TWM2::NukeCalls[%client.guid] >= 25) {
            CompleteNWChallenge(%client, "Nuke3");
         }
         //writing
         if(Kills2.findInFile(%file, "$TWM2::NukeCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::NukeCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::NukeCalls["@%client.guid@"] = "@$TWM2::NukeCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::NukeCalls["@%client.guid@"] = "@$TWM2::NukeCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
      case "ZBomb":
         if(Kills2.findInFile(%file, "$TWM2::ZBombCalls["@%client.guid@"]", "", "") != 0) {
            //the above line checks to see if we already have killed
            //If so, we need to replace that line with the new one
            //So, First Off, Lets get that line again
            %ln = Kills2.findInFile(%file, "$TWM2::ZBombCalls["@%client.guid@"]", "", ""); //start after the rank stuff
            //now we can replace it
            Kills2.replaceLine(%file, "$TWM2::ZBombCalls["@%client.guid@"] = "@$TWM2::ZBombCalls[%client.guid]@";", %ln);
            Kills2.Close();
            Kills2.Delete();
         }
         else {
            Kills2.OpenForAppend(%file);
            Kills2.WriteLine("$TWM2::ZBombCalls["@%client.guid@"] = "@$TWM2::ZBombCalls[%client.guid]@";");
            Kills2.close();
            Kills2.Delete();
         }
   }
}

