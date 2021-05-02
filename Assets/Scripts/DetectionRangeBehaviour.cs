using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRangeBehaviour : MonoBehaviour
{

    [Header("Object References")]
    [Tooltip("Enemy that will chase the player")]
    [SerializeField] EnemyBehaviour enemy;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && enemy != null)
        {
            enemy.SetChasePlayer(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && enemy != null)
        {
            enemy.SetChasePlayer(false);
        }
    }

    public void SetEnemy(EnemyBehaviour newEnemy)
    {
        enemy = newEnemy;
    }

}
