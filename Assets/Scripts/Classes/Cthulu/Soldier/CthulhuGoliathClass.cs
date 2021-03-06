﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuGoliathClass : ClassNode
{
  public CthulhuGoliathClass(){
    whenToUpgrade = StaticClassRef.LEVEL2;
  }

  public override string ClassDesc()
  {
      return "-1 mv\n+1 hp\n+2 atk";
  }

  public override string ClassName()
  {
      return "Goliath";
  }

  public override ClassNode GetParent(){
      return new CthulhuBaseSoldier();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new CthulhuRGiantClass(), new CthulhuSBehemothClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() - 1);
      unit.SetAttack(unit.GetAttack() + 2);
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 hp battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetHpBuffInactive(unit.GetHpBuff() + 1);
      return unit;
  }
}
