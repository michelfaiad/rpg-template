using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [Header("The Item rewarded by this Chest")]
    [SerializeField] GameObject item;    
    
    [Header("Object References")]
    [Tooltip("Chest Animator")]
    [SerializeField] Animator anim;

    bool canActivate = false, opened = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!opened && canActivate && Input.GetButtonUp("Fire1"))
        {
            
            canActivate = false;
            opened = true;

            anim.SetTrigger("open");

            WeaponBehaviour weapon = item.GetComponent<WeaponBehaviour>();
            if (weapon != null)
            {
                //Reward item is a weapon
                ChestManager.inst.ActivateDialog(weapon.GetName());
                ItemManager.inst.SetSwordOwnership(weapon.GetID());
            }
            else
            {
                ArmorBehaviour armor = item.GetComponent<ArmorBehaviour>();
                if(armor != null)
                {
                    ChestManager.inst.ActivateDialog(armor.GetArmorName());
                    ItemManager.inst.SetArmorOwnership(armor.GetArmorID());
                }
            }

            
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
        }
    }
}
