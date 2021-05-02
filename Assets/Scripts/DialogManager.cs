using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{

    public static DialogManager inst;

    [Header("Object References")]
    [Tooltip("Place holder for the speaker name")]
    [SerializeField] TMP_Text nameText;
    [Tooltip("Place holder for the dialog text")]
    [SerializeField] TMP_Text dialogText;
    [Tooltip("Dialog Panel to switch on/off")]
    [SerializeField] GameObject dialogPanel;
    [Tooltip("Animator of the NPC that the Player is talking to")]
    [SerializeField] Animator npcAnim;

    private string[] dialogLines, nameLines;

    private int currentLine;

    bool hasQuest;
    string questDisplayName;

    // Start is called before the first frame update
    void Start()
    {
        if(inst == null)
        {
            inst = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (dialogPanel.activeInHierarchy)
        {
            PlayerBehaviour.inst.SetTalking(true);

            npcAnim.SetBool("talking", true);

            if (Input.GetButtonDown("Fire1"))
            {
                currentLine++;

                if (currentLine >= dialogLines.GetLength(0))
                {
                    dialogPanel.SetActive(false);

                    PlayerBehaviour.inst.SetTalking(false);

                    npcAnim.SetBool("talking", false);
                    if (hasQuest)
                    {
                        QuestManager.inst.NotifyNewQuest(questDisplayName);
                    }
                }
                else
                {
                    dialogText.text = dialogLines[currentLine];
                    nameText.text = nameLines[currentLine];
                    
                }
            }
        }

    }

    public void SetNPCAnim(Animator newNPC)
    {
        npcAnim = newNPC;
    }

    public void ActivateDialog(string[] newDialogLines, string[] newNameLines, bool isQuest, string questName)
    {
        dialogLines = newDialogLines;
        nameLines = newNameLines;

        currentLine = 0;

        dialogText.text = dialogLines[currentLine];
        nameText.text = nameLines[currentLine];

        dialogPanel.SetActive(true);

        hasQuest = isQuest;
        questDisplayName = questName;
    }

    public void ActivateDialogSingleLine(string message, string name)
    {
        dialogLines = new string[1];
        nameLines = new string[1];

        currentLine = 0;

        dialogText.text = message;
        nameText.text = name;

        dialogPanel.SetActive(true);
    }


}
