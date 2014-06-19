// C.S.S CONSTRUCTION MOD SAVE FILE
// Saved by "henry"
// Created in mission "Pretty"
// Construction v0.70 Development Version 1

%building = new (StaticShape) () {datablock = "Deployedfloor";position = "-130.484 -427.804 128.438";rotation = "0 0 -1 82.6285";scale = "7.5 9.99999 3";team = "1";ownerGUID = "2884678";needsfit = "1";grounded = "1";powerFreq = "1";};addToDeployGroup(%building);checkPowerObject(%building);
%building = new (StaticShape) () {datablock = "GeneratorLarge";position = "-132.639 -436.948 129.938";rotation = "0 0 -1 80.8759";scale = "1 1 1";team = "1";ownerGUID = "2884678";deployed = "1";powerFreq = "1";};setTargetSensorGroup(%building.getTarget(),1);addToDeployGroup(%building);checkPowerObject(%building);%building.setSelfPowered();setTargetName(%building.target,addTaggedString("[ON]  Frequency" SPC %obj.powerFreq));%building.playThread($AmbientThread,"ambient");
%building = new (ForceFieldBare) () {datablock = "DeployedGravityField1";position = "-129.633 -429.343 129.928";rotation = "0 0 -1 82.6285";scale = "4.27 4.27 159.083";team = "1";ownerGUID = "2884678";needsfit = "1";velocityMod = "1";gravityMod = "0";appliedForce = "0 0 1000";powerFreq = "1";};setTargetSensorGroup(%building.getTarget(),1);addToDeployGroup(%building);checkPowerObject(%building);
%building = new (StaticShape) () {datablock = "DeployedSpine";position = "-129.011 -430.723 129.938";rotation = "0 0 -1 82.6285";scale = "0.25 0.333333 320";team = "1";ownerGUID = "2884678";needsfit = "1";powerFreq = "1";};addToDeployGroup(%building);checkPowerObject(%building);