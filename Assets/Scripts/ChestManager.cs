using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestManager : MonoBehaviour
{
    public static ChestManager inst;

    [Header("Text Fields to fill with chest parameters")]
    [SerializeField] TMP_Text itemText;
    [Header("The Panel to show chests rewards")]
    [SerializeField] GameObject dialogPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (inst == null)
        {
            inst = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (dialogPanel.activeInHierarchy)
        {

            if (Input.GetButtonDown("Fire1"))
            {                
                dialogPanel.SetActive(false);
                PlayerBehaviour.inst.SetTalking(false);
            }
        }

    }

    public void ActivateDialog(string item)
    {

        itemText.text = "You got " + item;

        PlayerBehaviour.inst.SetTalking(true);

        dialogPanel.SetActive(true);
    }
}
