using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationMessages : MonoBehaviour
{
    EnemyBehaviour enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();   
    }

    public void StartHitting()
    {
        enemy.StartHitting();
    }

    public void StopHitting()
    {
        enemy.StopHitting();
    }

    public void SetNotAttacking()
    {
        enemy.SetNotAttacking();
    }

}
