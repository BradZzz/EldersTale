﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  Pushes attacked enemy away
*/

[Serializable]
public class ForceAtk : Skill
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
      Vector3Int posAtk = attacker.GetPosition();
      Vector3Int posDef = defender.GetPosition();

      Vector3Int diff = new Vector3Int(posDef.x,posDef.y,posDef.z);

      if (diff.x > 0) {
        //Defender to the right of the attacker
        if (diff.y > 0) {
          //Defender is above attacker
          //Move the defender up and to the right
          diff.x+=1;
          diff.y+=1;
        } else if (diff.y < 0) {
          //Defender is below attacker
          //Move the defender down and to the right
          diff.x+=1;
          diff.y-=1;
        } else {
          diff.x+=1;
        }
      } else if (diff.x < 0) {
        //Defender to the left of the attacker
        if (diff.y > 0) {
          //Defender is above attacker
          //Move the defender up and to the left
          diff.x-=1;
          diff.y+=1;
        } else if (diff.y < 0) {
          //Defender is below attacker
          //Move the defender down and to the left
          diff.x-=1;
          diff.y-=1;
        } else {
          diff.x-=1;
        }
      } else {
        //Defender is right below or above attacker
        if (diff.y > 0) {
          //Defender is above attacker
          //Move defender up
          diff.y+=1;
        } else if (diff.y < 0) {
          //Defender is below attacker
          //Move defender down
          diff.y-=1;
        }
      }
      TileProxy nwDefTile = BoardProxy.instance.GetTileAtPosition(diff);
      if (nwDefTile != null && !nwDefTile.HasObstruction()) {
          TileProxy oldTile = BoardProxy.instance.GetTileAtPosition(diff);
          nwDefTile.ReceiveGridObjectProxy(defender);
          oldTile.RemoveGridObjectProxy(defender);
          defender.SnapToCurrentPosition();   
      } else if (nwDefTile != null) {
          Debug.Log("Character was slammed into obstacle! Might want to do something here.");
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

  public override string PrintDetails(){
      return "ForceAtk";
  }
}
