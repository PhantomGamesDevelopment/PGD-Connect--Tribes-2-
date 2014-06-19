//PATCH 3.03
//TOTAL WARFARE MOD 2 ADVANCED WARFARE

//BY PHANTOM139

//Change Log:
// *Universal Building Loads now Evaluate Power
// *Manipulator Tool: Weapon Mode Fix
// *Manipulator Tool: Bottom Print Easier to Read
// *Modified the Sniper Zombie fire Code
// *Patched lord vardison bug that allowed him to attack after death
// *Death Messages added for certian guns
// *Renamed the ALSWP Sniper to the ALSWP M1401 Sniper Rifle
// *Reduced the Mini-Chaingun Damage to 0.04 from 0.06
// *Increased the MG42 Damage to 0.063 from 0.06
// *Increased the RP432 Damage to 0.044 from 0.035
// *Increased the MRXX ZC4 Damage to 0.043 from 0.0322
// *Modified the lord vardison laser ball globe to properly delete after exploding

package TWM2Patch3_03 {
   //Function Edits
   function Univ_Loader::onDisconnect(%this) {
      if(!%this.valid) {
         echo("Universal Load: Corrupt File");
         $PGDBuffer[%this.client, %this.load] = ""; //clear the buffer.
         %this.delete();
         return;
      }
      eval($PGDBuffer[%this.client, %this.load]); //evaluate the buffer here, which will make things much faster.
      $PGDBuffer[%this.client, %this.load] = ""; //clear the buffer.
      echo("Universal Load: OK");
      %this.delete();
      //
      messageAll('MsgAdminForce', "\c3Universal Load Complete, Evaluating Power.");
      globalPowerCheck();
   }

   function EditGunImage::onMount(%this, %obj, %slot) {
      Parent::onMount(%this, %obj, %slot);
      DispEditorToolInfo(%obj);
      if(!isSet(%obj.EditPMode)) {
         %obj.EditPMode = 0;
      }
      if(!isSet(%obj.EditSMode)) {
         %obj.EditSMode = 0;
      }
      %obj.UsingEditTool = true;
   }
   
   function DispEditorToolInfo(%obj) {
   switch(%obj.EditPMode) {
      case 0:
      %primary = "Pad Swapping";
         switch(%obj.EditSMode) {
             case 0:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Blue Pad - [LSB] - MSB<spop>", 5, 3);
             case 1:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: LSB - [MSB] - Walkway<spop>", 5, 3);
             case 2:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: MSB - [Walkway] - Medium Floor <spop>", 5, 3);
             case 3:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Walkway - [Medium Floor] - Dark Pad<spop>", 5, 3);
             case 4:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Medium Floor - [Dark Pad] - V-Pad<spop>", 5, 3);
             case 5:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Dark Pad - [V-Pad] - C.1 Backpack<spop>", 5, 3);
             case 6:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: V-Pad - [C.1 Backpack] - C.2 Small Containment<spop>", 5, 3);
             case 7:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.1 Backpack - [C.2 Small Containment] - C.3 Large Containment<spop>", 5, 3);
             case 8:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.2 Small Containment - [C.3 Large Containment] - C.4 Compressor<spop>", 5, 3);
             case 9:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.3 Large Containment - [C.4 Compressor] - C.5 Tubes<spop>", 5, 3);
             case 10:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.4 Compressor - [C.5 Tubes] - C.6 Quantium Bat.<spop>", 5, 3);
             case 11:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.5 Tubes - [C.6 Quantium Bat.] - C.7 Proton Acc.<spop>", 5, 3);
             case 12:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.6 Quantium Bat. - [C.7 Proton Acc.] - C.8 Cargo Crate<spop>", 5, 3);
             case 13:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.7 Proton Acc. - [C.8 Cargo Crate] - C.9 Mag Cooler<spop>", 5, 3);
             case 14:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.8 Cargo Crate - [C.9 Mag Cooler] - C.10 Recycle Unit<spop>", 5, 3);
             case 15:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.9 Mag Cooler - [C.10 Recycle Unit] - C.11 Fuel Canister<spop>", 5, 3);
             case 16:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.10 Recycle Unit - [C.11 Fuel Canister] - C.12 Wooden Box<spop>", 5, 3);
             case 17:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.11 Fuel Canister - [C.12 Wooden Box] - C.13 Plasma Router<spop>", 5, 3);
             case 18:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.12 Wooden Box - [C.13 Plasma Router] - Statue Base<spop>", 5, 3);
             case 19:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: C.13 Plasma Router - [Statue Base] - Blue Pad<spop>", 5, 3);
             case 20:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: [Pad Swap] - FF Swap - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Statue Base - [Blue Pad] - LSB<spop>", 5, 3);
         }
      case 1:
         %primary = "Force-Field Swapping";
         switch(%obj.EditSMode) {
            case 0:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: All Pass Yellow - [Solid White] - Solid Red<spop>", 5, 3);
            case 1:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Solid White - [Solid Red] - Solid Green<spop>", 5, 3);
            case 2:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Solid Red - [Solid Green] - Solid Blue<spop>", 5, 3);
            case 3:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Solid Green - [Solid Blue] - Solid Cyan<spop>", 5, 3);
            case 4:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Solid Blue - [Solid Cyan] - Solid Magenta<spop>", 5, 3);
            case 5:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Solid Cyan - [Solid Magenta] - Solid Yellow<spop>", 5, 3);
            case 6:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Solid Magenta - [Solid Yellow] - Team Pass White<spop>", 5, 3);
            case 7:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Solid Yellow - [Team Pass White] - Team Pass Red<spop>", 5, 3);
            case 8:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Team Pass White - [Team Pass Red] - Team Pass Green<spop>", 5, 3);
            case 9:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Team Pass Red - [Team Pass Green] - Team Pass Blue<spop>", 5, 3);
            case 10:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Team Pass Green - [Team Pass Blue] - Team Pass Cyan<spop>", 5, 3);
            case 11:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Team Pass Blue - [Team Pass Cyan] - Team Pass Magenta<spop>", 5, 3);
            case 12:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Team Pass Cyan - [Team Pass Magenta] - Team Pass Yellow<spop>", 5, 3);
            case 13:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Team Pass Magenta - [Team Pass Yellow] - All Pass White<spop>", 5, 3);
            case 14:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Team Pass Yellow - [All Pass White] - All Pass Red<spop>", 5, 3);
            case 15:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: All Pass White - [All Pass Red] - All Pass Green<spop>", 5, 3);
            case 16:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: All Pass Red - [All Pass Green] - All Pass Blue<spop>", 5, 3);
            case 17:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: All Pass Green - [All Pass Blue] - All Pass Cyan<spop>", 5, 3);
            case 18:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: All Pass Blue - [All Pass Cyan] - All Pass Magenta<spop>", 5, 3);
            case 19:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: All Pass Cyan - [All Pass Magenta] - All Pass Yellow<spop>", 5, 3);
            case 20:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - [FF Swap] - Barrel Swap - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: All Pass Magenta - [All Pass Yellow] - Solid White<spop>", 5, 3);
         }
      case 2:
         %primary = "Turret Barrel Swapping";
         switch(%obj.EditSMode) {
             case 0:
                commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Mortar - [Anti Air] - Missile<spop>", 5, 3);
             case 1:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Anti Air - [Missile] - Plasma<spop>", 5, 3);
             case 2:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Missile - [Plasma] - ELF<spop>", 5, 3);
             case 3:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Plasma - [ELF] - Mortar<spop>", 5, 3);
             case 4:
                 commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - [Barrel Swap] - Cloak/Fade - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: ELF - [Mortar] - Anti Air<spop>", 5, 3);
         }
      case 3:
         switch(%obj.EditSMode) {
            case 0:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - [Cloak/Fade] - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: [Cloak] - UnCloak - Fade - UnFade <spop>", 5, 3);
            case 1:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - [Cloak/Fade] - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Cloak - [UnCloak] - Fade - UnFade <spop>", 5, 3);
            case 2:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - [Cloak/Fade] - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Cloak - UnCloak - [Fade] - UnFade <spop>", 5, 3);
            case 3:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - [Cloak/Fade] - Delete Objects \n <font:Tempus Sans ITC:18> Grenade: Cloak - UnCloak - Fade - [UnFade] <spop>", 5, 3);
         }
      case 4:
         switch(%obj.EditSMode) {
            case 0:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - Cloak/Fade - [Delete Objects] \n <font:Tempus Sans ITC:18> Grenade: [Single] - Cascade <spop>", 5, 3);
            case 1:
               commandToClient( %obj.client, 'BottomPrint', "<spush><font:Tempus Sans ITC:20>[{Manipulator Tool}] - Phantom139 \n Mine: Pad Swap - FF Swap - Barrel Swap - Cloak/Fade - [Delete Objects] \n <font:Tempus Sans ITC:18> Grenade: Single - [Cascade] <spop>", 5, 3);
         }
      }
   }

   function SniperZombieFire(%zombie,%closestclient){
      %num = getRandom(250, 1000);
      %vec = vectorsub(VectorAdd(%closestclient.getPosition(), "0 0 2.2"),%zombie.getMuzzlePoint(4));
      %accuracy = (vectorlen(%vec) / %num);
      %vec = vectoradd(%vec, vectorscale(%closestclient.getvelocity(), %accuracy));
      %p = new TracerProjectile() { //TWM2 Sniper zombies use M1 Snipers :P
	   dataBlock        = M1Bullet;
	   initialDirection = %vec;
	   initialPosition  = %zombie.getMuzzlePoint(4);
	   sourceObject     = %zombie;
	   sourceSlot       = 4;
      };
      ServerPlay3D(M1FireSound, %zombie.getPosition());
   }
   
   function VardisonDemonAttacks(%boss) {
      if(!isObject(%boss) || %boss.getState() $= "Dead") {
         return;
      }
      parent::VardisonDemonAttacks(%boss);
   }
};
activatePackage(TWM2Patch3_03);

//Datablock Edits
ChaingunBullet.directDamage = 0.04;
MG42Bullet.directDamage = 0.063;
RP432Bullet.directDamage = 0.044;
MRXXBullet.directDamage = 0.043;

LaserBallGlobeSmoke.lifetimeMS = 10000;

PulseRifleBullet.directDamageType = $DamageType::PulseRifle;
PulseSMGBullet.directDamageType = $DamageType::PulseSMG;

ALSWPSniperRifleImage.GunName = "ALSWP M1401 Sniper Rifle";

$InvWeapon[10] = "ALSWP M1401 Sniper";
$NameToInv["ALSWP M1401 Sniper"] = "ALSWPSniperRifle";

$DamageType::G17 = 78;
$DamageType::PulseRifle = 79;
$DamageType::ALSWP = 80;
$DamageType::PulseSMG = 81;
$DamageType::Hawk = 82;
$DamageType::M93 = 83;
$DamageType::M4A1 = 84;
$DamageType::PTorpedo = 85;
$DamageType::MRXX = 86;

$DamageTypeText[78] = 'G17';
$DamageTypeText[79] = 'PulseRifle';
$DamageTypeText[80] = 'ALSWP';
$DamageTypeText[81] = 'PulseSMG';
$DamageTypeText[82] = 'Hawk';
$DamageTypeText[83] = 'M93';
$DamageTypeText[84] = 'M4A1';
$DamageTypeText[85] = 'PTorpedo';
$DamageTypeText[86] = 'MRXX';

$DeathMessageTeamKill[$DamageType::G17, 0] = '\c0%4 nails TEAMMATE %1 with a G17 Sniper Rifle.';
$DeathMessageTeamKill[$DamageType::PulseRifle, 0] = '\c0%4 kills TEAMMATE %1 with a Pulse Rifle.';
$DeathMessageTeamKill[$DamageType::ALSWP, 0] = '\c0%4 kills TEAMMATE %1 with an ALSWP M1401 Sniper.';
$DeathMessageTeamKill[$DamageType::PulseSMG, 0] = '\c0%4 kills TEAMMATE %1 with a Pulse SMG.';
$DeathMessageTeamKill[$DamageType::Hawk, 0] = '\c0%4 destroys TEAMMATE %1 with a Crimson Hawk Pistol.';
$DeathMessageTeamKill[$DamageType::M93, 0] = '\c0%4 kills TEAMMATE %1 with a M93 Pistol.';
$DeathMessageTeamKill[$DamageType::M4A1, 0] = '\c0%4 kills TEAMMATE %1 with a M4A1 Assault Rifle.';
$DeathMessageTeamKill[$DamageType::PTorpedo, 0] = '\c0%4 kills TEAMMATE %1 with a Plasma Torpedo.';
$DeathMessageTeamKill[$DamageType::MRXX, 0] = '\c0%4 kills TEAMMATE %1 with a MRXX ZC4 Machine Gun.';

$DeathMessage[$DamageType::M93, 0] = '\c0%1 may have dodged the first M93 shot from %4, %1 still died.';
$DeathMessage[$DamageType::M93, 1] = '\c0%4 unleashes a semi automatic burst fire pistol on %1.';
$DeathMessage[$DamageType::M93, 2] = '\c0%4 burst shots 3 pistol shots at %1';
$DeathMessage[$DamageType::M93, 3] = '\c0%4 eliminates %1 with %6 M93 Pistol.';
$DeathMessage[$DamageType::M93, 4] = '\c0%1 takes 3 pistol rounds of death from %4.';

$DeathMessage[$DamageType::M4A1, 0] = '\c0%4 fires off %6 M4A1 Rifle at %1.';
$DeathMessage[$DamageType::M4A1, 1] = '\c0%4 guns down %1 with a M4A1 Assault Rifle.';
$DeathMessage[$DamageType::M4A1, 2] = '\c0%4 unleashes %6 automatic rifle at %1';
$DeathMessage[$DamageType::M4A1, 3] = '\c0%4 eliminates %1 with %6 M4A1.';
$DeathMessage[$DamageType::M4A1, 4] = '\c0%1 has been shot down by %4\'s M4A1.';

$DeathMessage[$DamageType::PTorpedo, 0] = '\c0%4 annihilates %1 with a plasma torpedo.';
$DeathMessage[$DamageType::PTorpedo, 1] = '\c0%4 paves %1 into the concrete with a plasma torpedo.';
$DeathMessage[$DamageType::PTorpedo, 2] = '\c0%4 unleashes %6 ultimate explosion torpedo at %1';
$DeathMessage[$DamageType::PTorpedo, 3] = '\c0%4 eliminates %1 with %6 Plasma Warhead Weapon.';
$DeathMessage[$DamageType::PTorpedo, 4] = '\c0%1 is vaporized by %4\'s Plasma Torpedo.';

$DeathMessage[$DamageType::MRXX, 0] = '\c0%4 mows down %1 with a MRXX ZC4 Machine Gun.';
$DeathMessage[$DamageType::MRXX, 1] = '\c0%4 shreds %1 into pieces with a MRXX ZC4.';
$DeathMessage[$DamageType::MRXX, 2] = '\c0%4 unleashes %6 MRXX ZC4 upon %1';
$DeathMessage[$DamageType::MRXX, 3] = '\c0%4 eliminates %1 with %6 Modified RP432, MRXX ZC4 Weapon.';
$DeathMessage[$DamageType::MRXX, 4] = '\c0%1 is gunned down by %4\'s MRXX ZC4.';

$DeathMessage[$DamageType::Hawk, 0] = '\c0%4 shows %1 pure prestige pistol power.';
$DeathMessage[$DamageType::Hawk, 1] = '\c0%4 unleashes the harbinger clan\'s most powerful pistol on %1.';
$DeathMessage[$DamageType::Hawk, 2] = '\c0%4 burst shots 5 plasma splices at %1';
$DeathMessage[$DamageType::Hawk, 3] = '\c0%4 eliminates %1 with %6 crimson hawk.';
$DeathMessage[$DamageType::Hawk, 4] = '\c0%1 takes 5 heated plasma rounds of death from %4.';

$DeathMessage[$DamageType::PulseRifle, 0] = '\c0%4 bolts down %1 with a pulse rifle.';
$DeathMessage[$DamageType::PulseRifle, 1] = '\c0%4 unleashes a storm of rifle plasma at %1.';
$DeathMessage[$DamageType::PulseRifle, 2] = '\c0%4 splices rapid fire pulses at %1';
$DeathMessage[$DamageType::PulseRifle, 3] = '\c0%4 eliminates %1 with %6 pulse rifle.';
$DeathMessage[$DamageType::PulseRifle, 4] = '\c0%1 takes heated pulse rounds of death from %4.';

$DeathMessage[$DamageType::PulseSMG, 0] = '\c0%4 bolts down %1 with a pulse SMG.';
$DeathMessage[$DamageType::PulseSMG, 1] = '\c0%4 unleashes a storm of plasma at %1.';
$DeathMessage[$DamageType::PulseSMG, 2] = '\c0%4 splices automatic based pulses at %1';
$DeathMessage[$DamageType::PulseSMG, 3] = '\c0%4 eliminates %1 with %6 pulse SMG.';
$DeathMessage[$DamageType::PulseSMG, 4] = '\c0%1 takes heated pulse smg shots of death from %4.';

$DeathMessage[$DamageType::ALSWP, 0] = '\c0%4 snipes down %1 with a semi automatic ALSWP Sniper Rifle.';
$DeathMessage[$DamageType::ALSWP, 1] = '\c0%4 blasts %1 with a M1401 Carbine ALSWP Sniper Rifle.';
$DeathMessage[$DamageType::ALSWP, 2] = '\c0%4 fires multiple lucky sniper shots at %1... rapidly';
$DeathMessage[$DamageType::ALSWP, 3] = '\c0%4 eliminates %1 with rapid fire sniper shots.';
$DeathMessage[$DamageType::ALSWP, 4] = '\c0%1 died after taking multiple ALSWP Shots to the face from %4.';

$DeathMessage[$DamageType::G17, 0] = '\c0%4 snipes %1 with a G17 Sniper Rifle.';
$DeathMessage[$DamageType::G17, 1] = '\c0%4 pings a G17 Sniper shot into %1\'s life.';
$DeathMessage[$DamageType::G17, 2] = '\c0%4 fires %6 one lucky sniper shot at %1';
$DeathMessage[$DamageType::G17, 3] = '\c0%4 eliminates %1 with a G17 Sniper.';
$DeathMessage[$DamageType::G17, 4] = '\c0%1 died after taking a G17 Shot to the face from %4.';
