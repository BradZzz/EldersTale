﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    /*
      TODO:
      Start at team zero
        (In unit settings, decrement movement/attack on use)
      When turn is clicked switch sides
      If the ai flag is active, start ai movement
      at end of ai movement, end turn
    */

    [HideInInspector]
    public int currentTeam;

    public static TurnController instance;
    private bool hard;

    private void Awake()
    {
        instance = this;
        currentTeam = BoardProxy.PLAYER_TEAM;
        hard = false;
        PlayerMeta player = BaseSaver.GetPlayer();
        if (player.world == GameMeta.World.candy || player.world == GameMeta.World.pyramid){
            hard = true;
        }   
    }

    public int GetTeam()
    {
        return currentTeam;
    }

    public bool PlayersTurn(){
        return GetTeam() == BoardProxy.PLAYER_TEAM;
    }

    void SwitchTeams()
    {
        currentTeam = GetTeam() == BoardProxy.ENEMY_TEAM ? BoardProxy.PLAYER_TEAM : BoardProxy.ENEMY_TEAM;
    }

    public void DisplayStartGameText(){
        PanelControllerNew.DisplayTT("Begin Battle!");
    }

    public void DisplayTurnText(){
        PanelControllerNew.DisplayTT(currentTeam == BoardProxy.PLAYER_TEAM ? "Player Turn" : "Enemy Turn");
    }

    public void StartTurn(bool firstTurn)
    {
        if (firstTurn) {
            DisplayStartGameText();
        } else {
            DisplayTurnText();
        }
        //Turn off all the units not on the team.
        //Turn on all the units with the current team.
        foreach(UnitProxy unit in BoardProxy.instance.GetUnits())
        {
            if (unit.GetData().GetTeam() == currentTeam)
            {
                unit.GetData().BeginTurn();
                //unit.AcceptAction(Skill.Actions.BeginGame,null);
            }
            else
            {
                unit.GetData().GetTurnActions().EndTurn();
            }
        }
        if (PlayersTurn()) {
          Camera.main.GetComponent<PinchZoom>().enabled = true;
          UnitProxy[] uArr = BoardProxy.instance.GetUnits().Where(unt => unt.IsAlive() && unt.GetData().GetTeam() == BoardProxy.PLAYER_TEAM).ToArray();
          if (uArr.Length > 0) {
            uArr[0].FocusOnUnit();
          }
        } else {
          Camera.main.GetComponent<PinchZoom>().enabled = false;
        }
        //Set the turn panel to the current turn
        //PanelController.instance.SetTurnPanel(currentTeam.ToString());
    }

    public void EndTurnActions()
    {
        //Turn off all the units not on the team.
        //Turn on all the units with the current team.
        foreach(UnitProxy unit in BoardProxy.instance.GetUnits())
        {
            if (unit.GetData().GetTeam() == currentTeam)
            {
                unit.AcceptAction(Skill.Actions.EndedTurn,null);
                if (unit.GetData().GetTurnActions().CanAttack() 
                  && unit.GetData().GetTurnActions().CanMove()) {
                  Debug.Log("DidWait Turn End Actions");
                  unit.AcceptAction(Skill.Actions.DidWait,null);
                }
            }
        }
        //Set the turn panel to the current turn
        //PanelController.instance.SetTurnPanel(currentTeam.ToString());
    }
  
    public void EndTurn()
    {
        //If your team ended the turn, unnullify them for next turn
        foreach(UnitProxy unit in BoardProxy.instance.GetUnits())
        {
            if (unit.GetData().GetTeam() == currentTeam ){
              unit.GetData().SetNullified(false);
            }
        }
        StartCoroutine(ASyncTurnEnd());
    }

    IEnumerator ASyncTurnEnd(){
        if (!AnimationInteractionController.AllAnimationsFinished()) { 
            yield return new WaitForSeconds(AnimationInteractionController.AFTER_KILL);
        }
        Debug.Log("EndTurn");
        //Perform end turn actions
        EndTurnActions();
        //Deselect selected tiles
        BoardProxy.instance.FlushTiles();
        //Play the turn cutscene

        PanelControllerNew.ClearPanels();

        //Switch controller teams
        SwitchTeams();
        //Perform start turn actions
        StartTurn(false);

        //Run AI (if applicable)
        if (currentTeam == BoardProxy.ENEMY_TEAM && !BoardProxy.HUMAN_PLAYER)
        {
            if (hard) {
                AdvancedBrain.StartThinking();
            } else {
                BasicBrain.StartThinking();
            }
        }
    }
}
