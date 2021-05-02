using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager inst;

    [Header("Configuration")]
    [Tooltip("Canvas of the Quest Log.")]
    [SerializeField] GameObject questCanvas;
    [Header("Text Fields to fill with quests parameters")]
    [SerializeField] TMP_Text activeQuests;
    [SerializeField] TMP_Text summary;
    [SerializeField] TMP_Text rewards;
    [SerializeField] TMP_Text location;
    [SerializeField] TMP_Text dialog;
    [SerializeField] TMP_Text progress;
    
    [Header("Quest Completed Configuration")]
    [Tooltip("Canvas of the Complete Quest Dialog.")]
    [SerializeField] GameObject completeQuestCanvas;
    [Tooltip("Field to put the name of the completed quest.")]
    [SerializeField] TMP_Text questDialogTitle;
    [Tooltip("Field to put the name of the completed quest.")]
    [SerializeField] TMP_Text questName;

    Quest[] questList;

    // Start is called before the first frame update
    void Start()
    {
        if (inst == null)
        {
            inst = this;
        }

        LoadAllQuests();
        ClearQuestFields();
    }

    // Update is called once per frame
    void Update()
    {
        CheckOpenQuestLog();
    }

    private void CheckOpenQuestLog()
    {
        if (Input.GetKeyDown(KeyCode.L))
            SwitchQuestCanvas();
    }

    public void SwitchQuestCanvas()
    {
        if (!questCanvas.activeInHierarchy)
        {
            ClearQuestFields();

            foreach (Quest quest in questList)
            {
                if (quest.Active)
                {
                    activeQuests.text = quest.Name;
                    summary.text = quest.Summary;
                    rewards.text = "Gold: " + quest.GoldReward + "\n XP: " + quest.XpReward;
                    location.text = quest.Location;

                    string[] dialogLines = quest.StartDialogLines;
                    string[] dialogNames = quest.StartDialogNames;

                    int i = 0;
                    foreach (string line in dialogLines)
                    {
                        dialog.text += dialogNames[i++] + "\n-'" + line + "'\n\n";
                    }

                    progress.text = "" + quest.CurrentProgress + " / " + quest.TargetProgress;
                }
            }

            questCanvas.SetActive(true);
        }
        else
        {
            questCanvas.SetActive(false);
        }
    }

    void ClearQuestFields()
    {
        activeQuests.text = "";
        summary.text = "";
        rewards.text = "";
        location.text = "";
        dialog.text = "";
        progress.text = "";
    }

    public Quest GetQuestByID(int id)
    {
        foreach(Quest quest in questList)
        {
            if(quest.QuestID == id)
            {
                return quest;
            }
        }
        return null;
    }

    public Quest[] GetQuests()
    {
        return questList;
    }

    public void UpdateQuestProgress(int targetUniqueID)
    {
        foreach (Quest quest in questList)
        {
            if(quest.TargetUniqueID == targetUniqueID)
            {
                if (quest.TargetProgress > quest.CurrentProgress)
                {
                    quest.CurrentProgress += 1;

                    if(quest.TargetProgress == quest.CurrentProgress)
                    {
                        quest.Done = true;
                        StartCoroutine(OpenQuestNoticeDialog(quest.Name, "Quest completed!"));
                    }

                }
            }
        }
        
    }

    public void NotifyNewQuest(string questName)
    {
        StartCoroutine(OpenQuestNoticeDialog(questName, "Quest received!"));
    }


    private void LoadAllQuests()
    {
        string[] dialogLines = new string[6];
        
        dialogLines[0] = "Help Me!";
        dialogLines[1] = "What happened?";
        dialogLines[2] = "Those monsters...";
        dialogLines[3] = "... destroyed my fence!";
        dialogLines[4] = "There is a nest of them near the well! \nPlease destroy it!";
        dialogLines[5] = "On my way!";

        string[] dialogNames = new string[6];

        dialogNames[0] = "Old Man";
        dialogNames[1] = "Phil";
        dialogNames[2] = "Old Man";
        dialogNames[3] = "Old Man";
        dialogNames[4] = "Old Man";
        dialogNames[5] = "Phil";

        string[] rewardLines = new string[3];

        rewardLines[0] = "The monsters are gone!";
        rewardLines[1] = "Thank you so much!";
        rewardLines[2] = "Glad I could help!";

        string[] rewardNames = new string[3];

        rewardNames[0] = "Phil";        
        rewardNames[1] = "Old Man";
        rewardNames[2] = "Phil";
        
        Quest quest = new Quest(1, false, false, false,
                                "Source of Evil", "Some monsters destroyed the old man's fence!", "Farm",
                                100, 20, 
                                dialogLines, dialogNames, 
                                rewardLines, rewardNames,
                                1,0, 300,
                                "Wait! The Old man over there is calling me!");

        questList = new Quest[1];
        questList[0] = quest;
    }

    IEnumerator OpenQuestNoticeDialog(string newQuestName, string dialogTitle)
    {
        completeQuestCanvas.SetActive(true);
        questName.text = newQuestName;
        questDialogTitle.text = dialogTitle;


        yield return new WaitForSecondsRealtime(6f);

        completeQuestCanvas.SetActive(false);
    }

}
