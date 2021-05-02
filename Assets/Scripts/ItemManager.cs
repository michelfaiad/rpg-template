using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ItemManager : MonoBehaviour
{
    public static ItemManager inst;

    [Header("Item Lists Configuration")]    
    [Tooltip("List of Swords")]
    [SerializeField] WeaponBehaviour[] weaponBehaviours;
    [Tooltip("Swords UI Buttons")]
    [SerializeField] Button[] swordButtons;
    [Tooltip("List of Armors")]
    [SerializeField] ArmorBehaviour[] armorBehaviours;
    [Tooltip("Armors UI Buttons")]
    [SerializeField] Button[] armorButtons;

    [Header("Object References")]
    [Tooltip("Player Script")]
    [SerializeField] PlayerBehaviour player;
    [Tooltip("Swords Panel")]
    [SerializeField] GameObject swordsCanvas;
    [Tooltip("Armors Panel")]
    [SerializeField] GameObject armorsCanvas;

    int equipedWeaponID = 0, equipedArmorID = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (inst == null)
        {
            inst = this;
        }

        EquipWeapon(equipedWeaponID);
        EquipArmor(equipedArmorID);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchSwordCanvas();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            SwitchArmorCanvas();
        }
    }

    public void SetArmorOwnership(int newArmorID)
    {
        foreach (ArmorBehaviour armor in armorBehaviours)
        {
            if(armor.GetArmorID() == newArmorID)
            {
                armor.SetPlayerOwns(true);
            }
        }
    }

    public void SetSwordOwnership(int newSwordID)
    {
        foreach (WeaponBehaviour weapon in weaponBehaviours)
        {
            if (weapon.GetID() == newSwordID)
            {
                weapon.SetPlayerOwns(true);
            }
        }
    }

    public void SwitchSwordCanvas()
    {
        if (!swordsCanvas.activeInHierarchy)
        {
            LoadSwordsInfo();
        }

        swordsCanvas.SetActive(!swordsCanvas.activeInHierarchy);
    }

    public void SwitchArmorCanvas()
    {
        if (!armorsCanvas.activeInHierarchy)
        {
            LoadArmorsInfo();
        }

        armorsCanvas.SetActive(!armorsCanvas.activeInHierarchy);
    }

    private void LoadArmorsInfo()
    {
        for (int i = 0; i < armorBehaviours.Length; i++)
        {

            armorButtons[i].gameObject.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = armorBehaviours[i].GetArmorName();
            armorButtons[i].gameObject.transform.Find("Description").gameObject.GetComponent<TMP_Text>().text = armorBehaviours[i].GetArmorDescription();
            armorButtons[i].gameObject.transform.Find("LifeBonusValue").gameObject.GetComponent<TMP_Text>().text = armorBehaviours[i].GetLifeBonus().ToString();

            if (armorBehaviours[i].GetPlayerOwns())
            {
                armorButtons[i].gameObject.SetActive(true);

                if (armorBehaviours[i].GetArmorID() == equipedArmorID)
                {
                    armorButtons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    armorButtons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
                }

            }
            else
            {
                armorButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void LoadSwordsInfo()
    {
        for (int i = 0; i < weaponBehaviours.Length; i++)
        {

            swordButtons[i].gameObject.transform.Find("Name").gameObject.GetComponent<TMP_Text>().text = weaponBehaviours[i].GetName();
            swordButtons[i].gameObject.transform.Find("Description").gameObject.GetComponent<TMP_Text>().text = weaponBehaviours[i].GetDescription();
            swordButtons[i].gameObject.transform.Find("DamageValue").gameObject.GetComponent<TMP_Text>().text = weaponBehaviours[i].GetDamage().ToString();

            if (weaponBehaviours[i].GetPlayerOwns())
            {
                swordButtons[i].gameObject.SetActive(true);

                if (weaponBehaviours[i].GetID() == equipedWeaponID)
                {
                    swordButtons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    swordButtons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
                }

            }
            else
            {
                swordButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void EquipWeapon(int newWeaponID)
    {
        foreach(WeaponBehaviour weapon in weaponBehaviours)
        {
            //Get weapon in list
            if (weapon.GetID() == newWeaponID)
            {

                //Activate game object
                weapon.gameObject.SetActive(true);
                //Update player damage
                //Update hit Collider
                player.SetWeapon(weapon.GetDamage(), weapon.GetCollider(), weapon.GetTrail());
                //Store equiped weapon
                equipedWeaponID = weapon.GetID();               

            }
            else
            {
                //Disable others
                weapon.gameObject.SetActive(false);
            }
        }

        LoadSwordsInfo();
    }

    public void EquipArmor(int newArmorID)
    {
        foreach (ArmorBehaviour armor in armorBehaviours)
        {
            //Get weapon in list
            if (armor.GetArmorID() == newArmorID)
            {

                //Activate game object
                armor.gameObject.SetActive(true);
                //Update player damage
                //Update hit Collider
                player.SetArmor(armor.GetLifeBonus());
                //Store equiped armor
                equipedArmorID = armor.GetArmorID();

            }
            else
            {
                //Disable others
                armor.gameObject.SetActive(false);
            }

            LoadArmorsInfo();

        }


    }

}
