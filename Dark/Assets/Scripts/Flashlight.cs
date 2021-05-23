using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    public Player player;
    public GameObject flashLight;
    public Bar chargeBar;
    public Text batteriesCountText;
    
    private Light2DEmitter lightParameters;
    private bool isLightDamageOn;
    private bool isLightHealOn;
    public float Charge { get; private set; } = 1000f;
    public uint BatteriesCount { get; private set; }
    public uint StartBatteriesCount { get; } = 3;

    public AudioSource TurnOn;
    public AudioSource TurnOff;

    private float damageLightSize = 5;
    public float MAXFlashlightCharge { get; } = 1000f;
    private float damageLightConeAngle = 30;
    private float healPower = 1;
    
    private static readonly Color HealLightColor = new Color(0.37f,0.29f,0.545f,0.5f);
    private static readonly Color DamageLightColor = new Color(1,1,1,0.1f);

    private const float HealLightSize = 0.75f;
    private const float HealLightConeAngle = 360;
    private const string DamageLightEventFilter = "DamageEnemy";
    private const string HealLightEventFilter = "";
    
    void Start()
    {
        BatteriesCount = StartBatteriesCount;
        
        lightParameters = flashLight.GetComponent<Light2DEmitter>();
        
        chargeBar.SetMaxValue(MAXFlashlightCharge);
        Charge = MAXFlashlightCharge;
        chargeBar.SetValue(Charge);
        
        UpdateBatteriesCountText();
    }

    void Update()
    {
        if (player.Health == 0)
        {
            TurnOffFlashlight();
            return;
        }

        if (Charge != 0)
            FlashLightUpdate();
        else if (BatteriesCount != 0)
        {
            AddBatteries(-1);
            Charge = MAXFlashlightCharge;
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
        
        if (Input.GetKeyDown(GameManager.GM.KeyDamageLight) && !isLightHealOn && !isLightDamageOn)
        {
            TurnOn.Play();
            isLightDamageOn = true;
        }
        else if (Input.GetKeyDown(GameManager.GM.KeyDamageLight) && isLightDamageOn)
        {
            TurnOff.Play();
            isLightDamageOn = false;
        }
        else if (Input.GetKeyDown(GameManager.GM.KeyDamageLight) && isLightHealOn)
        {
            TurnOn.Play();
            isLightHealOn = false;
        }
        else if (Input.GetKeyDown(GameManager.GM.KeyHealLight) && isLightHealOn)
        {
            TurnOff.Play();
            isLightHealOn = false;
        }
        else if (Input.GetKeyDown(GameManager.GM.KeyHealLight) && !isLightHealOn)
        {
            TurnOn.Play();
            isLightHealOn = true;
            isLightDamageOn = false;
        }
        if (!isLightDamageOn && !isLightHealOn)
            TurnOffFlashlight();
        else
            AddCharge(-1);
    }

    public void AddCharge(float change)
    {
        if (change + Charge <= 0)
            Charge = 0;
        else if (Charge + change >= MAXFlashlightCharge)
            Charge = MAXFlashlightCharge;
        else
            Charge += change;
        chargeBar.SetValue(Charge);
    }

    private void TurnOffFlashlight()
    {
        isLightDamageOn = false;
        isLightHealOn = false;
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
            position.y = -0.06f;
            position.x = -0.08f;
        }

        else if (animatorStateInfo.IsName("PlayerBackWalkAnimation") ||
            animatorStateInfo.IsName("PlayerBackStandAnimation"))
        {
            rotation = damageLightConeAngle / 2 + 180;
            position.x = -0.068f;
        }

        else if (animatorStateInfo.IsName("PlayerForwardWalkAnimation") ||
            animatorStateInfo.IsName("PlayerForwardStandAnimation"))
        {
            rotation = damageLightConeAngle / 2;
            position.x = 0.12f;
            position.y = -0.05f;
        }
        else 
            throw new NotImplementedException();
        flashLight.transform.rotation = Quaternion.Euler(0,0,rotation);
        flashLight.transform.position = position + player.transform.position;
    }
    
    public void AddBatteries(int change)
    {
        if (change + BatteriesCount <= 0)
            BatteriesCount = 0;
        else
            BatteriesCount = (uint) (BatteriesCount + change);
        UpdateBatteriesCountText();
    }
    
    private void UpdateBatteriesCountText()
    {
        batteriesCountText.text = BatteriesCount.ToString();
    }

    private void ChangeLightParameters(float coneAngle, float lightSize, Color lightColor, string eventFilter)
    {
        lightParameters.coneAngle = coneAngle;
        lightParameters.lightSize = lightSize;
        lightParameters.lightColor = lightColor;
        lightParameters.eventPassedFilter = eventFilter;
    }
    
    
}
