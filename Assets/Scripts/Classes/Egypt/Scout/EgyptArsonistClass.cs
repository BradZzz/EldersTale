﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EgyptArsonistClass : ClassNode
{
  public EgyptArsonistClass(){
    whenToUpgrade = StaticClassRef.LEVEL3;
  }

  public override string ClassDesc()
  {
    return "+1 hp\nFireDef\nFireAtk";
  }

  public override string ClassName()
  {
      return "Arsonist";
  }

  public override ClassNode GetParent(){
      return new EgyptWhispererClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMaxHP(unit.GetMaxHP() + 1);
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("FireDef");
      skills.Add("FireAtk");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}