﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanInquisitorClass : ClassNode
{
  public HumanInquisitorClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
      return "+2 hp\n+1 atk\nHealTurn";
  }

  public override string ClassName()
  {
      return "Inquisitor";
  }

  public override ClassNode GetParent(){
      return new HumanIPaladinClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 2);
      unit.SetAttack(unit.GetAttack() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealTurn");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "+1 atk range battle";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetAttackRngBuff(unit.GetAttackRngBuff() +  1);
      return unit;
  }
}
