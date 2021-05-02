using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestManager : MonoBehaviour
{

    public static NestManager inst;

    // Start is called before the first frame update
    void Start()
    {
        if(inst == null)
        {
            inst = this;
        }
    }

    public void RegisterKill(int nestID)
    {        
        foreach(NestBehaviour nest in FindObjectsOfType<NestBehaviour>())
        {
            if(nest.GetNestID() == nestID)
            {
                nest.RegisterKill();
            }
        }

        
    }

}
