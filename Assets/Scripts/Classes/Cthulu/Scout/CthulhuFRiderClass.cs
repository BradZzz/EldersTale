﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuFRiderClass : ClassNode
{
  public CthulhuFRiderClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 hp\n+1 atk trn";
  }

  public override string ClassName()
  {
      return "Famine Rider";
  }

  public override ClassNode GetParent(){
      return new CthulhuPBeastClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      unit.SetTurnAttacks(unit.GetTurnAttacks() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+3 hp battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetHpBuffInactive(unit.GetHpBuff() + 3);
      return unit;
  }
}
