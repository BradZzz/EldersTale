﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CthulhuPBearerClass : ClassNode
{
  public CthulhuPBearerClass(){
    whenToUpgrade = StaticClassRef.LEVEL4;
  }

  public override string ClassDesc()
  {
    return "EnfeebleEnemiesWait\nEnfeebleEnemiesWait";
  }

  public override string ClassName()
  {
      return "Plague Bearer";
  }

  public override ClassNode GetParent(){
      return new CthulhuHTotemClass();
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ };
  }
 
  public override Unit UpgradeCharacter(Unit unit)
  {
      List<string> skills = new List<string>(unit.GetSkills());
      skills.Add("EnfeebleEnemiesWait");
      skills.Add("EnfeebleEnemiesWait");
      unit.SetSkills(skills.ToArray());
      return unit;
  }
}
