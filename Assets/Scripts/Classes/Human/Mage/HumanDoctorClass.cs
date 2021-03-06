﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanDoctorClass : ClassNode
{
  public HumanDoctorClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "+1 mv\nHealAlliesAtk";
  }

  public override string ClassName()
  {
      return "Doctor";
  }

  public override ClassNode GetParent(){
      return new HumanGeniusClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("HealAlliesAtk");
      skills.Add("HealAlliesAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }

  public override string ClassInactiveDesc(){
      return "HealTurn";
  }

  public override Unit InactiveUpgradeCharacter(Unit unit)
  {
      unit.SetSkillsBuffs(new string[]{ "HealTurn" });
      return unit;
  }
}
