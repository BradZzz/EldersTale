﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit00AnimationHelper : MonoBehaviour
{
   public void Attack()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.Unit001AttackSound, this.gameObject);
    }

    public void CthuluAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(FMODPaths.CthuluAttackSound, this.gameObject);
    }
}
