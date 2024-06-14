using System;
using System.Collections.Generic;
using Abilities;
using Entities;
using Managers;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = System.Diagnostics.Debug;

public class HUDManager : NetworkBehaviour
{
    public NewPlayer player;
    private ResourceManager _resources;

    public Canvas hud;
    public GameObject statsHUD;
    private string _statsValue;
    private TextMeshProUGUI _statsHUD2;
    
    public GameObject abilitiesHUD;
    private string _abilitiesValue;
    private TextMeshProUGUI _abilitiesHUD2;
    
    public GameObject buffsHUD;
    private string _buffsValue;
    private TextMeshProUGUI _buffsHUD2;

    private RawImage _passiveAbilityIcon;
    public GameObject ability1;
    public GameObject ability2;
    public GameObject ability3;
    public GameObject ability4;
    private Image _ability1Icon;
    private Image _ability2Icon;
    private Image _ability3Icon;
    private Image _ability4Icon;
    
    public Slider hp;
    public TextMeshProUGUI hpValue;
    
    public GameObject frame1;
    public GameObject frame2;
    public GameObject frame3;
    public GameObject frame4;
    public Sprite frameNone;
    public Sprite frameCommon;
    public Sprite frameUncommon;
    public Sprite frameRare;
    public Sprite frameEpic;
    public Sprite frameLegendary;
    public Sprite frameMythic;
    private Image _frame1;
    private Image _frame2;
    private Image _frame3;
    private Image _frame4;

    public GameObject ability1Cd;
    public GameObject ability2Cd;
    public GameObject ability3Cd;
    public GameObject ability4Cd;
    private Image _ability1IconCd;
    private Image _ability2IconCd;
    private Image _ability3IconCd;
    private Image _ability4IconCd;
    
    public TextMeshProUGUI cd1;
    public TextMeshProUGUI cd2;
    public TextMeshProUGUI cd3;
    public TextMeshProUGUI cd4;


    [Header("Buffs")] 
    public List<GameObject> buffList;
    public List<GameObject> buffIconList;
    public List<GameObject> buffIconCdList;
    private List<Image> _buffIconCdList;
    private List<Image> _buffIconList;

    [Header("Desc")] 
    public InfoTextManager managerAbility1;
    public InfoTextManager managerAbility2;
    public InfoTextManager managerAbility3;
    public InfoTextManager managerAbility4;
    public InfoTextManager managerAbilityP;
    public InfoTextManager managerWeapon;
    public InfoTextManager managerBuff1;
    public InfoTextManager managerBuff2;
    public InfoTextManager managerBuff3;
    public InfoTextManager managerBuff4;
    public InfoTextManager managerBuff5;
    public InfoTextManager managerBuff6;
    public InfoTextManager managerBuff7;
    public InfoTextManager managerGem1;
    public InfoTextManager managerGem2;
    public InfoTextManager managerGem3;
    public InfoTextManager managerGem4;
    public InfoTextManager managerGem5;
    public InfoTextManager managerGem6;
    public InfoTextManager managerGem7;
    public InfoTextManager managerGem8;
    
    
    
    private void Start()
    {
        _buffIconCdList = new List<Image>();
        _buffIconList = new List<Image>();
        _resources = player.resources;
        _ability1Icon = ability1.GetComponent<Image>();
        _ability2Icon = ability2.GetComponent<Image>();
        _ability3Icon = ability3.GetComponent<Image>();
        _ability4Icon = ability4.GetComponent<Image>();
        _frame1 = frame1.GetComponent<Image>();
        _frame2 = frame2.GetComponent<Image>();
        _frame3 = frame3.GetComponent<Image>();
        _frame4 = frame4.GetComponent<Image>();
        _ability1IconCd = ability1Cd.GetComponent<Image>();
        _ability2IconCd = ability2Cd.GetComponent<Image>();
        _ability3IconCd = ability3Cd.GetComponent<Image>();
        _ability4IconCd = ability4Cd.GetComponent<Image>();
        foreach (var t in buffIconList)
        {
            _buffIconList.Add(t.GetComponent<Image>());
        }
        foreach (var t in buffIconCdList)
        {
            _buffIconCdList.Add(t.GetComponent<Image>());
        }
        StartInfo();
    }

    private void StartInfo()
    {
        managerAbility1.describableObject = player.abilityList[0];
        managerAbility2.describableObject = player.abilityList[1];
        managerAbility3.describableObject = player.abilityList[2];
        managerAbility4.describableObject = player.abilityList[3];
    }
    
    private void Update()
    {
        UpdateIcon();
        UpdateHealth();
        UpdateBuffs();
    }

    private void UpdateHealth()
    {
        hp.value = player.health;
        hp.maxValue = player.maxHealth;
        hpValue.text = $"{Math.Round(player.health)} / {Math.Round(player.maxHealth)}";
    }


    private Sprite GetIconRarity(Ability ability)
    {
        if (ability is AbilityNone_0)
        {
            return frameNone;
        }

        switch (ability.Rarity)
        {
            case Rarities.COMMON:
                return frameCommon;
            case Rarities.UNCOMMON:
                return frameUncommon;
            case Rarities.RARE:
                return frameRare;
            case Rarities.EPIC:
                return frameEpic;
            case Rarities.LEGENDARY:
                return frameLegendary;
            case Rarities.MYTHIC:
                return frameMythic;
            default:
                return frameNone;
        }
    }

    private void UpdateBuffs()
    {
        for (int i = 0; i < buffList.Count; i++)
        {
            if (i < player.buffList.Count)
            {
                buffList[i].SetActive(true);
                _buffIconCdList[i].sprite = _resources.buffIconList[player.buffList[i].iconId];
                _buffIconList[i].sprite = _resources.buffIconList[player.buffList[i].iconId];
                var duration = player.buffList[i].Duration;
                var maxDuration = player.buffList[i].maxDuration;
                if (duration != null && maxDuration != null)
                {
                    _buffIconCdList[i].fillAmount = (float) (duration / maxDuration);
                }
                else
                {
                    _buffIconCdList[i].fillAmount = 0;
                }
            }
            else
            {
                buffList[i].SetActive(false);
            }
        }
    }
    
    private void UpdateIcon()
    {
        if (player.abilityList[0] is AbilityNone_0)
        {
            ability1.SetActive(false);
            ability1Cd.SetActive(false);
            cd1.text = "";
        }
        else
        {
            if (player.abilityList[0].State == States.COOLDOWN)
            {
                cd1.text = $"{Math.Round(player.abilityList[0].CurrentCooldown,1)}";
                _ability1IconCd.fillAmount = player.abilityList[0].CurrentCooldown / player.abilityList[0].Cooldown;
            }
            else
            {
                cd1.text = "";
                ability1.SetActive(true);
                ability1Cd.SetActive(true);
                _ability1IconCd.fillAmount = 0;
                _ability1Icon.sprite = _resources.abilityIconList[player.abilityList[0].id-1];
                _ability1IconCd.sprite = _resources.abilityIconList[player.abilityList[0].id-1];
                _frame1.sprite = GetIconRarity(player.abilityList[0]);
            }
        }
        
        if (player.abilityList[1] is AbilityNone_0)
        {
            ability2.SetActive(false);
            ability2Cd.SetActive(false);
            cd2.text = "";
        }
        else
        {
            if (player.abilityList[1].State == States.COOLDOWN)
            {
                cd2.text = $"{Math.Round(player.abilityList[1].CurrentCooldown,1)}";
                _ability2IconCd.fillAmount = player.abilityList[1].CurrentCooldown / player.abilityList[1].Cooldown;
            }
            else
            {
                cd2.text = "";
                ability2.SetActive(true);
                ability2Cd.SetActive(true);
                _ability2IconCd.fillAmount = 0;
                _ability2Icon.sprite = _resources.abilityIconList[player.abilityList[1].id-1];
                _ability2IconCd.sprite = _resources.abilityIconList[player.abilityList[1].id-1];
                _frame2.sprite = GetIconRarity(player.abilityList[1]);
            }
        }
        
        if (player.abilityList[2] is AbilityNone_0)
        {
            ability3.SetActive(false);
            ability3Cd.SetActive(false);
            cd3.text = "";
        }
        else
        {
            if (player.abilityList[2].State == States.COOLDOWN)
            {
                cd3.text = $"{Math.Round(player.abilityList[2].CurrentCooldown,1)}";
                _ability3IconCd.fillAmount = player.abilityList[2].CurrentCooldown / player.abilityList[2].Cooldown;
            }
            else
            {
                cd3.text = "";
                ability3.SetActive(true);
                ability3Cd.SetActive(true);
                _ability3IconCd.fillAmount = 0;
                _ability3Icon.sprite = _resources.abilityIconList[player.abilityList[2].id-1];
                _ability3IconCd.sprite = _resources.abilityIconList[player.abilityList[2].id-1];
                _frame3.sprite = GetIconRarity(player.abilityList[2]);
            }
        }
        
        if (player.abilityList[3] is AbilityNone_0)
        {
            ability4.SetActive(false);
            ability4Cd.SetActive(false);
            cd4.text = "";
        }
        else
        {
            if (player.abilityList[3].State == States.COOLDOWN)
            {
                cd4.text = $"{Math.Round(player.abilityList[3].CurrentCooldown,1)}";
                _ability4IconCd.fillAmount = player.abilityList[3].CurrentCooldown / player.abilityList[3].Cooldown;
            }
            else
            {
                cd4.text = "";
                ability4.SetActive(true);
                ability4Cd.SetActive(true);
                _ability4IconCd.fillAmount = 0;
                _ability4Icon.sprite = _resources.abilityIconList[player.abilityList[3].id-1];
                _ability4IconCd.sprite = _resources.abilityIconList[player.abilityList[3].id-1];
                _frame4.sprite = GetIconRarity(player.abilityList[3]);
            }
        }
        
        /*if (player.classPassive is AbilityNone_0)
        {
            passiveAbilityIcon.visible = false;
        }
        else
        {
            passiveAbilityIcon.image = _resources.passiveAbilityIconList[player.classPassive.id - 201];
        }*/
    }
}