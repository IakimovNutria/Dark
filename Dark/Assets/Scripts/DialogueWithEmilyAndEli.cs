public class DialogueWithEmilyAndEli : Dialogue
{
    private Dialogue dialogue;
    private new void Start()
    {
        dialogue = this;
        dialogue.Start();
        canObjectBeInteracted = false;
        isPlayerReadyToInteract = true;
    }

    protected override bool ActivateCondition()
    {
        return GameManager.GM.StoryBools["isPlayerHelpEli"];
    }
    
    private new void OnGUI()
    {
        dialogue.OnGUI();
    }
}
