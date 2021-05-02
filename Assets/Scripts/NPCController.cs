using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("NPC Configuration")]
    [Tooltip("Check if this NPC has a quest to give")]
    [SerializeField] bool hasQuest;
    [Header("Quest ID to give to the Player")]
    [SerializeField] int questID;

    [Header("Object References")]
    [Tooltip("Default dialog lines, if has no active quest")]
    [SerializeField] string[] defaultLines;
    [Tooltip("Default dialog names, must be same size as dialog lines")]
    [SerializeField] string[] defaultName;
    
    string[] dialogLines, nameLines;
    bool canActivate;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canActivate && Input.GetButtonUp("Fire1"))
        {
            if (hasQuest)
            {
                LoadQuestDialog();               
            }                    
            
            canActivate = false;
        }

    }

    private void LoadQuestDialog()
    {
        Quest quest = QuestManager.inst.GetQuestByID(questID);

        if(!quest.Active)
        {
            quest.Active = true;
            dialogLines = quest.StartDialogLines;
            nameLines = quest.StartDialogNames;
            DialogManager.inst.ActivateDialog(dialogLines, nameLines, true, quest.Name);
        }
        else if (quest.Done && !quest.Rewarded)
        {
            quest.Rewarded = true;
            dialogLines = quest.RewardDialogLines;
            nameLines = quest.RewardDialogNames;
            StatsManager.inst.GiveGoldReward(quest.GoldReward);
            StatsManager.inst.GiveXPReward(quest.XpReward);
            DialogManager.inst.ActivateDialog(dialogLines, nameLines, false, "");
        }
        else
        {
            dialogLines = defaultLines;
            nameLines = defaultName;
            DialogManager.inst.ActivateDialog(dialogLines, nameLines, false, "");
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
            DialogManager.inst.SetNPCAnim(GetComponentInChildren<Animator>());
        }
    }


}
