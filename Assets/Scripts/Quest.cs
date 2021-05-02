public class Quest
{
    bool active, done, rewarded;
    string name, summary, location, blockMessage;
    int xpReward, goldReward, questID, targetProgress, currentProgress, targetUniqueID;
    string[] startDialogLines, startDialogNames, rewardDialogLines, rewardDialogNames;

    public Quest(int questID, bool active, bool done, bool rewarded, string name, string summary, string location, int xpReward, int goldReward, string[] startDialogLines, string[] startDialogNames, string[] rewardDialogLines, string[] rewardDialogNames, int targetProgress, int currentProgress, int targetUniqueID, string blockMessage)
    {
        QuestID = questID;
        Active = active;
        Done = done;
        Rewarded = rewarded;
        Name = name;
        Summary = summary;
        Location = location;
        XpReward = xpReward;
        GoldReward = goldReward;
        StartDialogLines = startDialogLines;
        StartDialogNames = startDialogNames;
        RewardDialogLines = rewardDialogLines;
        RewardDialogNames = rewardDialogNames;
        TargetProgress = targetProgress;
        CurrentProgress = currentProgress;
        TargetUniqueID = targetUniqueID;
        BlockMessage = blockMessage;
    }

    public bool Active { get => active; set => active = value; }
    public bool Done { get => done; set => done = value; }
    public bool Rewarded { get => rewarded; set => rewarded = value; }
    public string Name { get => name; set => name = value; }
    public string Summary { get => summary; set => summary = value; }
    public string Location { get => location; set => location = value; }
    public int XpReward { get => xpReward; set => xpReward = value; }
    public int GoldReward { get => goldReward; set => goldReward = value; }
    public int QuestID { get => questID; set => questID = value; }
    public string[] StartDialogLines { get => startDialogLines; set => startDialogLines = value; }
    public string[] StartDialogNames { get => startDialogNames; set => startDialogNames = value; }
    public string[] RewardDialogLines { get => rewardDialogLines; set => rewardDialogLines = value; }
    public string[] RewardDialogNames { get => rewardDialogNames; set => rewardDialogNames = value; }
    public int TargetProgress { get => targetProgress; set => targetProgress = value; }
    public int CurrentProgress { get => currentProgress; set => currentProgress = value; }
    public int TargetUniqueID { get => targetUniqueID; set => targetUniqueID = value; }
    public string BlockMessage { get => blockMessage; set => blockMessage = value; }
}
