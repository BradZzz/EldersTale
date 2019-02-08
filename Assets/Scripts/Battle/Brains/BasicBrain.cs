﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicBrain : MonoBehaviour
{
    public static BasicBrain instance;

    void Awake()
    {
        instance = this;
    }
  
    public static void StartThinking()
    {
        instance.StartCoroutine(instance.BeginProcess());
    }
  
    public IEnumerator BeginProcess()
    {
        yield return new WaitForSeconds(.5f); 
        List<UnitProxy> aiUnits = new List<UnitProxy>(BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.ENEMY_TEAM));
        List<UnitProxy> opposingUnits = new List<UnitProxy>(BoardProxy.instance.GetUnits().Where(unit => unit.GetData().GetTeam() == BoardProxy.PLAYER_TEAM));

        Debug.Log("Beginning AI Turn");
        Debug.Log("aiUnits: " + aiUnits.Count.ToString());
        Debug.Log("playerUnits: " + opposingUnits.Count.ToString());

        foreach (UnitProxy unit in aiUnits)
        {
            bool didSomething = true;
            while (didSomething && opposingUnits.Count > 0)
            {
                didSomething = false;
                /*
                If an enemy is within range, attack
                If an enemy is not within range, go towards nearest one
                */
                List<TileProxy> visitableTiles = BoardProxy.instance.GetAllVisitableNodes(unit,true);
                List<TileProxy> opposingTeamTiles = new List<TileProxy>(visitableTiles.Where(tile => tile.HasUnit() 
                    && tile.GetUnit().GetData().GetTeam() != BoardProxy.ENEMY_TEAM));

                Debug.Log("Within range");
                Debug.Log("visitableTiles: " + visitableTiles.Count.ToString());
                Debug.Log("opposingTeamTiles: " + opposingTeamTiles.Count.ToString());

                if (opposingTeamTiles.Count == 0 && unit.GetData().GetTurnActions().CanMove())
                {
                    Debug.Log("Trying to Move");
                    //No opp units within range
                    //Get the pathfinding to first unit in list (to be optimized to return the closest)
                    Path<TileProxy> path = BoardProxy.instance.GetPath(BoardProxy.instance.GetTileAtPosition(unit.GetPosition()), 
                      BoardProxy.instance.GetTileAtPosition(opposingUnits[0].GetPosition()), unit);
                    //foreach (TileProxy step in path)
                    //{
                    //    Debug.Log("path Step: " + step.name);
                    //}
                    //From that list find the furthest tile that is also in the visitable tile list
                    TileProxy dest = path.Where(tile => visitableTiles.Contains(tile) && !tile.HasUnit()).First();
                    Debug.Log("start/end");
                    Debug.Log(BoardProxy.instance.GetTileAtPosition(unit.GetPosition()));
                    Debug.Log(dest.name);
                    //Move ai from current tile to tile mentioned above
                    unit.OnSelected();
                    yield return new WaitForSeconds(.25f);
                    InteractivityManager.instance.OnTileSelected(dest);
                    //StartCoroutine(unit.CreatePathToTileAndLerpToPosition(dest,
                    //  () =>
                    //  {
                    //      dest.ReceiveGridObjectProxy(unit);
                    //      BoardProxy.instance.GetTileAtPosition(unit.GetPosition()).RemoveGridObjectProxy(unit);
                    //  }));
                    //unit.GetData().GetTurnActions().Move();
                    didSomething = true;
                }
                else if (opposingTeamTiles.Count > 0 && unit.GetData().GetTurnActions().CanAttack())
                {
                  Debug.Log("Trying to Attack");
                  opposingTeamTiles[0].GetUnit().OnSelected();
                  didSomething = true;
                }
                yield return new WaitForSeconds(.25f); 
            }
        }
        //No more actions, end turn 
        TurnController.instance.EndTurn();
        yield return null;
    }
}
