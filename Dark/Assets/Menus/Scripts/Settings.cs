using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Transform buttonsObject;
    public GameObject playerCanvas;

    private Event keyEvent;
    private TextMeshProUGUI buttonText;
    private bool waitingForKey;
    private KeyCode newKey;

    public void SetSettings()
    {
        for (int i = 0; i < buttonsObject.childCount; i++)
        {
            var button = buttonsObject.GetChild(i);
            switch (button.name)
            {
                case ("ForwardButton"):
                    button.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.KeyUp.ToString();
                    break;
                case ("BackwardButton"):
                    button.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.KeyDown.ToString();
                    break;
                case ("LeftButton"):
                    button.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.KeyLeft.ToString();
                    break;
                case ("RightButton"):
                    button.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.KeyRight.ToString();
                    break;
                case ("DamageButton"):
                    button.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.KeyDamageLight.ToString();
                    break;
                case ("HealButton"):
                    button.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.KeyHealLight.ToString();
                    break;
                case ("ObjInteractionButton"):
                    button.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.KeyObgectsInteraction.ToString();
                    break;
                case ("DialoguesButton"):
                    button.GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.KeyDialogues.ToString();
                    break;
            }
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;
        var thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        thisButton.GetComponentInChildren<TextMeshProUGUI>().text = "";

        yield return WaitForKey();

        switch(keyName)
        {
            case ("ForwardButton"):
                SetKey("KeyUp");
                break;
            case ("BackwardButton"):
                SetKey("KeyDown");
                break;
            case ("LeftButton"):
                SetKey("KeyLeft");
                break;
            case ("RightButton"):
                SetKey("KeyRight");
                break;
            case ("DamageButton"):
                SetKey("KeyDamageLight");
                break;
            case ("HealButton"):
                SetKey("KeyHealLight");
                break;
            case ("ObjInteractionButton"):
                SetKey("KeyObgectsInteraction");
                UpdateInteractionHint();
                break;
            case ("DialoguesButton"):
                SetKey("KeyDialogues");
                UpdateDialogueHint();
                break;
        }
        yield return null;
    }

    private void SetKey(string field)
    {
        var property = typeof(GameManager).GetProperty(field);
        property.SetValue(GameManager.GM, newKey);
        buttonText.text = newKey.ToString();
    }

    private void UpdateInteractionHint()
    {
        if (playerCanvas != null)
            playerCanvas.GetComponent<CanvasHints>().SetInteractionHint();
    }

    private void UpdateDialogueHint()
    {
        if (playerCanvas != null)
            playerCanvas.GetComponent<CanvasHints>().SetDialogueHint();
    }

    public void SendText(TextMeshProUGUI text)
    {
        buttonText = text;
    }

    private void OnGUI()
    {
        keyEvent = Event.current;


        if (keyEvent.isKey && waitingForKey)
        {
            waitingForKey = false;
            newKey = keyEvent.keyCode;
        }
    }
}
