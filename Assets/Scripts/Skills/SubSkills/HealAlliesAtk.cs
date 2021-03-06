﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealAlliesAtk : Skill
{
  public override void RouteBehavior(Actions action, UnitProxy u1, UnitProxy u2, List<TileProxy> path)
  {
      switch(action){
          case Actions.DidAttack: DidAttack(u1, u2); break;
          default: return;
      }
  }

  public override void BeginningGame(UnitProxy unit)
  {

  }

  public override void DidAttack(UnitProxy attacker, UnitProxy defender)
  {
      foreach(TileProxy tl in BoardProxy.instance.GetAllVisitableNodes(attacker, value + 1, true)){
          bool isAttacker = tl == BoardProxy.instance.GetTileAtPosition(attacker.GetPosition());
          if (!isAttacker) {
              //tl.FloatUp(Skill.Actions.DidAttack, "heal", Color.green, "Allies healed from another unit's attack");
              tl.CreateAnimation(Glossary.fx.healSmoke, AnimationInteractionController.ATK_WAIT);
              if (tl.HasUnit() && tl.GetUnit().GetData().GetTeam() == attacker.GetData().GetTeam()) {
                    Debug.Log("Attempting to heal unit");
                    tl.GetUnit().HealUnit(1, Skill.Actions.DidAttack);
              }
          }
      }
  }

  public override void DidKill(UnitProxy attacker, UnitProxy defender)
  {

  }

  public override void DidMove(UnitProxy unit, List<TileProxy> path){

  }

  public override void DidWait(UnitProxy unit)
  {

  }

  public override void EndTurn(UnitProxy unit)
  {

  }

  public override SkillTypes[] GetSkillTypes()
  {
      return new SkillTypes[]{ SkillTypes.Defense };
  }

  public override string PrintDetails(){
      return "Heal nearby allies on successful attack of enemy. " + ReturnBlurbByString(GetSkillGen());
  }

  public override string PrintStackDetails()
  {
      return ReturnStackTypeByString(Skill.SkillStack.rng);
  }

  public override SkillGen GetSkillGen()
  {
      return SkillGen.Heal;
  }
}
