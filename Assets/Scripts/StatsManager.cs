using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsManager : MonoBehaviour
{
    public static StatsManager inst;

    [Header("Object References")]
    [Tooltip("Player Level text field in the canvas")]
    [SerializeField] TMP_Text levelTXT;
    [Tooltip("Gold text field in the canvas")]
    [SerializeField] TMP_Text goldTXT;
    [Tooltip("XP max text field in the canvas")]
    [SerializeField] TMP_Text xpMaxTXT;
    [Tooltip("XP current text field in the canvas")]
    [SerializeField] TMP_Text xpCurrentTXT;
    [Tooltip("Gold reward canvas")]
    [SerializeField] GameObject goldCanvas;
    [Tooltip("Gold reward amount text field in the canvas")]
    [SerializeField] TMP_Text goldAmountTXT;
    [Tooltip("XP reward amount text field in the canvas")]
    [SerializeField] TMP_Text xpAmountTXT;
    [Tooltip("XP reward canvas")]
    [SerializeField] GameObject xpCanvas;

    int gold;
    int playerLevel, playerXP, nextLevelXP;

    // Start is called before the first frame update
    void Start()
    {
        if (inst != null && inst != this)
            Destroy(this);
        else
        {
            inst = this;
        }

        gold = 0;
        goldTXT.text = gold.ToString();

        playerLevel = 1;
        levelTXT.text = playerLevel.ToString();

        nextLevelXP = 1000;
        xpMaxTXT.text = nextLevelXP.ToString();

        playerXP = 0;
        xpCurrentTXT.text = playerXP.ToString();

    }

    public void GiveGoldReward(int amount)
    {
        GiveGold(amount);
        if (amount > 0)
            StartCoroutine(ShowGoldReward(amount));
    }

    public void GiveGold(int amount)
    {
        gold += amount;
        goldTXT.text = gold.ToString();
    }

    public void GiveXPReward(int amount)
    {
        GiveXP(amount);
        if (amount > 0)
            StartCoroutine(ShowXPReward(amount));
    }

    public void GiveXP(int amount)
    {
        playerXP += amount;
        xpCurrentTXT.text = playerXP.ToString();
        //Check level up
    }

    IEnumerator ShowGoldReward(int amount)
    {
        goldAmountTXT.text = amount.ToString();
        goldCanvas.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        goldCanvas.SetActive(false);
    }

    IEnumerator ShowXPReward(int amount)
    {
        xpAmountTXT.text = amount.ToString();
        xpCanvas.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        xpCanvas.SetActive(false);
    }
}
