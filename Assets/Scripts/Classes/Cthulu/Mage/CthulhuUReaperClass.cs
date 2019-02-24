﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuUReaperClass : ClassNode
{
  public CthulhuUReaperClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "SkeleKill\nSkeleKill\nSkeleKill";
  }

  public override string ClassName()
  {
      return "Under Reaper";
  }

  public override ClassNode GetParent(){
      return new CthulhuLichClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("SkeleKill");
      skills.Add("SkeleKill");
      skills.Add("SkeleKill");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
