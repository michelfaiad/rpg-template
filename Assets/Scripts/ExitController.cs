using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitController : MonoBehaviour
{
    [Header("Exit stage configurations")]
    [SerializeField] private Vector3 nextSceneStartPoint;
    [SerializeField] private string nextScene;
    [Header("Quest blocker")]
    [SerializeField] bool hasBlockingQuest;
    [SerializeField] int questID;

    private float wait = .3f;

    private bool waitAndLoad;

    private void Update()
    {
        if (waitAndLoad)
        {
            wait -= Time.deltaTime;
            if(wait <= 0)
            {
                waitAndLoad = false;
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") == true)
        {
            if (CheckQuestBlock())
            {
                PlayerBehaviour.inst.SetStartPoint(nextSceneStartPoint);

                waitAndLoad = true;

                FadeController.inst.FadeToBlack();
            }
        }
    }

    private bool CheckQuestBlock()
    {
        if (hasBlockingQuest)
        {
            Quest quest = QuestManager.inst.GetQuestByID(questID);
            if (quest.Active)
            {
                return true;
            }
            else
            {
                DialogManager.inst.ActivateDialogSingleLine(quest.BlockMessage, "Phil");
                return false;
            }
        }
        else
        {
            return true;
        }
    }
}
