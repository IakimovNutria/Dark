using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public Player.Player player;
    public GameObject flashLight;
    public Bar chargeBar;
    public Text batteryCountText;
    
    private Light2DEmitter lightParameters;
    private bool isLightOn;
    private float flashlightCharge;
    private uint batteriesCount = 2;
    private float lightSize = 5;
    private float maxFlashlightCharge = 1000;
    
    // Start is called before the first frame update
    void Start()
    {
        lightParameters = flashLight.GetComponent<Light2DEmitter>();
            
        chargeBar.SetMaxValue(maxFlashlightCharge);
        flashlightCharge = maxFlashlightCharge;
        chargeBar.SetValue(flashlightCharge);
        
        UpdateBatteryCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if (flashlightCharge != 0)
            FlashLightUpdate();
        else if (batteriesCount != 0)
        {
            batteriesCount--;
            UpdateBatteryCountText();
            flashlightCharge = maxFlashlightCharge;
        }
        else
            lightParameters.lightSize = 0;
    }
    
    private void FlashLightUpdate()
    { 
        if (Input.GetButtonDown(player.keyLight)) 
            isLightOn = !isLightOn;
        if (!isLightOn) 
        {
            lightParameters.lightSize = 0; 
            return;
        }
    
        flashlightCharge--;
        chargeBar.SetValue(flashlightCharge);
    
        lightParameters.lightSize = lightSize;
        var coneAngle = lightParameters.coneAngle;
        var animatorStateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
                
        if (animatorStateInfo.IsName("PlayerLeftWalkAnimation") ||
            animatorStateInfo.IsName("PlayerLeftStandAnimation"))
        {
            flashLight.transform.rotation = Quaternion.Euler(0,0,coneAngle / 2 + 90);
        }
        else if (animatorStateInfo.IsName("PlayerRightWalkAnimation") ||
                 animatorStateInfo.IsName("PlayerRightStandAnimation"))
        {
            flashLight.transform.rotation = Quaternion.Euler(0,0,coneAngle / 2 - 90);
        }
        else if (animatorStateInfo.IsName("PlayerBackWalkAnimation") ||
                 animatorStateInfo.IsName("PlayerBackStandAnimation"))
        {
            flashLight.transform.rotation = Quaternion.Euler(0,0,coneAngle / 2 + 180);
        }
        else if (animatorStateInfo.IsName("PlayerForwardWalkAnimation") ||
                 animatorStateInfo.IsName("PlayerForwardStandAnimation"))
        {
            flashLight.transform.rotation = Quaternion.Euler(0,0,coneAngle / 2);
        }
        else if (animatorStateInfo.IsName("PlayerRightDie") ||
                 animatorStateInfo.IsName("PlayerLeftDie") || 
                 animatorStateInfo.IsName("PlayerBackDie") ||
                 animatorStateInfo.IsName("PlayerForwardDie"))
            lightParameters.lightSize = 0;
        else 
            throw new NotImplementedException();
    }

    private void UpdateBatteryCountText()
    {
        batteryCountText.text = batteriesCount.ToString();
    }
}
