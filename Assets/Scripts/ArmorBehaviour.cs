using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBehaviour : MonoBehaviour
{
    [Header("Armor Parameters")]
    [Tooltip("Does player owns it?")]
    [SerializeField] bool playerOwns;
    [Tooltip("Armor unique ID")]
    [SerializeField] int armorID;
    [Tooltip("Armor Life Bonus")]
    [SerializeField] int lifeBonus;
    [Tooltip("Armor Display Name")]
    [SerializeField] string armorName;
    [Tooltip("Armor Description")]
    [SerializeField] string armorDescription;

    public void SetPlayerOwns(bool ownership)
    {
        playerOwns = ownership;
    }

    public bool GetPlayerOwns()
    {
        return playerOwns;
    }

    public int GetArmorID()
    {
        return armorID;
    }

    public int GetLifeBonus()
    {
        return lifeBonus;
    }

    public string GetArmorName()
    {
        return armorName;
    }

    public string GetArmorDescription()
    {
        return armorDescription;
    }

}
