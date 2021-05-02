using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestAnimationMessages : MonoBehaviour
{

    NestBehaviour nest;

    // Start is called before the first frame update
    void Start()
    {
        nest = GetComponentInParent<NestBehaviour>();
    }

    public void SpawnEnemyObject()
    {
        nest.SpawnEnemyObject();
    }
}
