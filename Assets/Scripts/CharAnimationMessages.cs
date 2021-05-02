using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimationMessages : MonoBehaviour
{
    PlayerBehaviour moverScript;

    private void Start()
    {
        moverScript = GetComponentInParent<PlayerBehaviour>();
    }

    public void SetAttackingFalse()
    {
        moverScript.SetNotAttacking();
    }

    public void SetNotHitting()
    {
        moverScript.SetNotHitting();
    }

    public void SetAttackingTrue()
    {
        moverScript.SetAttacking();
    }

    public void SetDamagedFalse()
    {
        moverScript.SetNotDamaged();
    }

    public void GameOver()
    {
        GameController.inst.GameOver();
    }

}
