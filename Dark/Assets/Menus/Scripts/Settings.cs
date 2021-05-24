using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Transform buttonsObject;

    private Event keyEvent;
    private TextMeshProUGUI buttonText;
    private bool waitingForKey;
    private KeyCode newKey;

    public void SetSettings()
    {
        for (var i = 0; i < buttonsObject.childCount; i++)
        {
            var button = buttonsObject.GetChild(i);
            button.GetComponentInChildren<TextMeshProUGUI>().text = button.name switch
            {
                "ForwardButton" => GameManager.GM.KeyUp.ToString(),
                "BackwardButton" => GameManager.GM.KeyDown.ToString(),
                "LeftButton" => GameManager.GM.KeyLeft.ToString(),
                "RightButton" => GameManager.GM.KeyRight.ToString(),
                "DamageButton" => GameManager.GM.KeyDamageLight.ToString(),
                "HealButton" => GameManager.GM.KeyHealLight.ToString(),
                "ObjInteractionButton" => GameManager.GM.KeyObjectsInteraction.ToString(),
                _ => button.GetComponentInChildren<TextMeshProUGUI>().text
            };
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
            case "ForwardButton":
                SetKey("KeyUp");
                break;
            case "BackwardButton":
                SetKey("KeyDown");
                break;
            case "LeftButton":
                SetKey("KeyLeft");
                break;
            case "RightButton":
                SetKey("KeyRight");
                break;
            case "DamageButton":
                SetKey("KeyDamageLight");
                break;
            case "HealButton":
                SetKey("KeyHealLight");
                break;
            case "ObjInteractionButton":
                SetKey("KeyObjectsInteraction");
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
