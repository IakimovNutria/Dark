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
    public Text batteriesCountText;
    
    private Light2DEmitter lightParameters;
    private bool isLightDamageOn;
    private bool isLightHealOn;
    private float flashlightCharge;
    private uint batteriesCount = 2;
    private float damageLightSize = 5;
    private float maxFlashlightCharge = 1000;
    private float damageLightConeAngle = 30;
    private float healPower = 1;
    
    private static readonly Color HealLightColor = new Color(0.37f,0.29f,0.545f,0.5f);
    private static readonly Color DamageLightColor = new Color(1,1,1,0.1f);

    private const float HealLightSize = 0.75f;
    private const float HealLightConeAngle = 360;
    private const string DamageLightEventFilter = "DamageEnemy";
    private const string HealLightEventFilter = "";
    
    // Start is called before the first frame update
    void Start()
    {
        lightParameters = flashLight.GetComponent<Light2DEmitter>();
            
        chargeBar.SetMaxValue(maxFlashlightCharge);
        flashlightCharge = maxFlashlightCharge;
        chargeBar.SetValue(flashlightCharge);
        
        UpdateBatteriesCountText();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Health == 0)
        {
            TurnOffFlashlight();
            return;
        }

        if (flashlightCharge != 0)
            FlashLightUpdate();
        else if (batteriesCount != 0)
        {
            ChangeBatteriesCount(-1);
            flashlightCharge = maxFlashlightCharge;
        }
        else
            lightParameters.lightSize = 0; 
    }
    
    private void FlashLightUpdate()
    {
        if (isLightHealOn)
        {
            ChangeLightParameters(HealLightConeAngle, HealLightSize, HealLightColor, HealLightEventFilter);
            player.ChangeHealthAmount(healPower);
        }
        
        else if (isLightDamageOn)
        {
            ChangeLightParameters(damageLightConeAngle, damageLightSize, DamageLightColor, DamageLightEventFilter);
            ChangeFlashlightTransform();
        }
        
        if (Input.GetButtonDown(player.keyDamageLight) && !isLightHealOn)
            isLightDamageOn = !isLightDamageOn;
        else if (Input.GetButtonDown(player.keyHealLight) && !isLightDamageOn)
            isLightHealOn = !isLightHealOn;
        
        if (!isLightDamageOn && !isLightHealOn)
            TurnOffFlashlight();
        else
            ChangeCharge(-1);
    }

    public void ChangeCharge(float change)
    {
        if (change + flashlightCharge <= 0)
            flashlightCharge = 0;
        else if (flashlightCharge + change >= maxFlashlightCharge)
            flashlightCharge = maxFlashlightCharge;
        else
            flashlightCharge += change;
        chargeBar.SetValue(flashlightCharge);
    }
    private void TurnOffFlashlight()
    {
        lightParameters.lightSize = 0;
    }
    
    private void ChangeFlashlightTransform()
    {
        var animatorStateInfo = player.animator.GetCurrentAnimatorStateInfo(0);
        float rotation;
        var position = new Vector3();
        if (animatorStateInfo.IsName("PlayerLeftWalkAnimation") ||
            animatorStateInfo.IsName("PlayerLeftStandAnimation"))
        {
            rotation = damageLightConeAngle / 2 + 90;
            position.y = -0.06f;
        }

        else if (animatorStateInfo.IsName("PlayerRightWalkAnimation") ||
            animatorStateInfo.IsName("PlayerRightStandAnimation"))
        {
            rotation = damageLightConeAngle / 2 - 90;
            //position. = ...
        }

        else if (animatorStateInfo.IsName("PlayerBackWalkAnimation") ||
            animatorStateInfo.IsName("PlayerBackStandAnimation"))
        {
            rotation = damageLightConeAngle / 2 + 180;
            //position. = ...
        }

        else if (animatorStateInfo.IsName("PlayerForwardWalkAnimation") ||
            animatorStateInfo.IsName("PlayerForwardStandAnimation"))
        {
            rotation = damageLightConeAngle / 2;
            //position. = ...
        }
        else 
            throw new NotImplementedException();
        flashLight.transform.rotation = Quaternion.Euler(0,0,rotation);
        flashLight.transform.position = position + player.transform.position;
    }
    
    public void ChangeBatteriesCount(int change)
    {
        if (change + batteriesCount <= 0)
            batteriesCount = 0;
        else
            batteriesCount = (uint) (batteriesCount + change);
        UpdateBatteriesCountText();
    }
    
    private void UpdateBatteriesCountText()
    {
        batteriesCountText.text = batteriesCount.ToString();
    }

    private void ChangeLightParameters(float coneAngle, float lightSize, Color lightColor, string eventFilter)
    {
        lightParameters.coneAngle = coneAngle;
        lightParameters.lightSize = lightSize;
        lightParameters.lightColor = lightColor;
        lightParameters.eventPassedFilter = eventFilter;
    }
    
}
