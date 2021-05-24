using System.Xml.Linq;
using UnityEngine;

public class Chest : Interaction, ISavable
{
    [SerializeField]
    private int batteries;
    
    private Flashlight flashlight;

    private void Start()
    {
        flashlight = FindObjectOfType<Flashlight>();
        InteractionInitialize();
    }

    private void Update()
    {
        if (batteries == 0)
            canObjectBeInteracted = false;
        else if (isPlayerReadyToInteract && Input.GetKeyDown(GameManager.GM.KeyObjectsInteraction))
        {
            GameManager.GM.ChestSound.Play();
            interactionIndicator.SetActive(false);
            flashlight.AddBatteries(batteries);
            batteries = 0;
        }
    }

    public void SetBatteriesCount(int batteriesCount)
    {
        batteries = batteriesCount;
    }
    
    public XElement GetElement()
    {
        var entityName = new XAttribute("name", name);
        var entityType = new XAttribute("type", "chest");
        var batteriesCount = new XAttribute("batteries", batteries);
        return new XElement("instance", entityName, entityType, batteriesCount);
    }
}
