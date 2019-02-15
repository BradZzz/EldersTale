﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HumanBaseScout : ClassNode
{
  public HumanBaseScout(){
    whenToUpgrade = 1;
  }

  public override string ClassDesc()
  {
    return "+1 mv\n+1 mv trn";
  }

  public override string ClassName()
  {
      return "Scout";
  }

  public override ClassNode GetParent(){
      return null;
  }

  public override ClassNode[] GetChildren(){
      return new ClassNode[]{ new HumanCorporalClass(), new HumanFSergeantClass() };
  }

  public override Unit UpgradeCharacter(Unit unit)
  {
      unit.SetMoveSpeed(unit.GetMoveSpeed() + 1);
      //unit.GetTurnActions().mv += 1;
      unit.SetTurnMoves(unit.GetTurnMoves() + 1);
      return unit;
  }
}