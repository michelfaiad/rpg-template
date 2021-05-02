using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{

    [Header("Weapon Parameters")]
    [Tooltip("Does player owns it?")]
    [SerializeField] bool playerOwns;
    [Tooltip("Weapon Unique ID")]
    [SerializeField] int weaponID;
    [Tooltip("Weapon Damage")]
    [SerializeField] int damage;
    [Tooltip("Weapon Display Name")]
    [SerializeField] string swordName;
    [Tooltip("Weapon Description")]
    [SerializeField] string description;
    [Tooltip("Weapon Trail effect")]
    [SerializeField] TrailRenderer trail;

    CapsuleCollider swordHit;

    

    void Awake()
    {
        swordHit = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        
    }

    public void SetPlayerOwns(bool ownership)
    {
        playerOwns = ownership;
    }

    public bool GetPlayerOwns()
    {
        return playerOwns;
    }

    public int GetID()
    {
        return weaponID;
    }

    public int GetDamage()
    {
        return damage;
    }

    public CapsuleCollider GetCollider()
    {
        return swordHit;
    }

    public TrailRenderer GetTrail()
    {
        return trail;
    }

    public string GetName()
    {
        return swordName;
    }

    public string GetDescription()
    {
        return description;
    }

}
