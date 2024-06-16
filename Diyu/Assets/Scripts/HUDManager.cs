using System;
using System.Collections.Generic;
using Abilities;
using Entities;
using Gems;
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

    private string _statsValue;
    private TextMeshProUGUI _statsHUD2;
    
    private string _abilitiesValue;
    private TextMeshProUGUI _abilitiesHUD2;
    
    private string _buffsValue;
    private TextMeshProUGUI _buffsHUD2;

    private Image _passiveAbilityIcon;
    public GameObject abilityP;
    public GameObject ability1;
    public GameObject ability2;
    public GameObject ability3;
    public GameObject ability4;
    private Image _ability1Icon;
    private Image _ability2Icon;
    private Image _ability3Icon;
    private Image _ability4Icon;
    public GameObject weapon;
    private Image _weaponIcon;
    
    public Slider hp;
    public TextMeshProUGUI hpValue;
    
    public GameObject frame1;
    public GameObject frame2;
    public GameObject frame3;
    public GameObject frame4;
    public GameObject frameP;
    public GameObject frameW;
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
    private Image _frameP;
    private Image _frameW;
    public TextMeshProUGUI weaponName;

    public GameObject ability1Cd;
    public GameObject ability2Cd;
    public GameObject ability3Cd;
    public GameObject ability4Cd;
    public GameObject abilityPCd;
    private Image _ability1IconCd;
    private Image _ability2IconCd;
    private Image _ability3IconCd;
    private Image _ability4IconCd;
    private Image _abilityPIconCd;
    
    public TextMeshProUGUI cd1;
    public TextMeshProUGUI cd2;
    public TextMeshProUGUI cd3;
    public TextMeshProUGUI cd4;
    public TextMeshProUGUI cd5;
    
    private bool _inChoice = false;
    public GameObject choice;

    public TextMeshProUGUI roundTime;
    public TextMeshProUGUI roundNumber;
    public TextMeshProUGUI foundAbility;
    
    [Header("Buffs")] 
    public List<GameObject> buffList;
    public List<GameObject> buffIconList;
    public List<GameObject> buffIconCdList;
    private List<Image> _buffIconCdList;
    private List<Image> _buffIconList;

    [Header("Debuffs")] 
    public List<GameObject> debuffList;
    public List<GameObject> debuffIconList;
    public List<GameObject> debuffIconCdList;
    private List<Image> _debuffIconCdList;
    private List<Image> _debuffIconList;
    
    [Header("Desc")] 
    public InfoTextManager managerAbility1;
    public InfoTextManager managerAbility2;
    public InfoTextManager managerAbility3;
    public InfoTextManager managerAbility4;
    public InfoTextManager managerAbilityP;
    //public InfoTextManager managerWeapon;
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
        _inChoice = false;
        choice.SetActive(false);
        _buffIconCdList = new List<Image>();
        _buffIconList = new List<Image>();
        _debuffIconCdList = new List<Image>();
        _debuffIconList = new List<Image>();
        _resources = player.resources;
        _ability1Icon = ability1.GetComponent<Image>();
        _ability2Icon = ability2.GetComponent<Image>();
        _ability3Icon = ability3.GetComponent<Image>();
        _ability4Icon = ability4.GetComponent<Image>();
        _weaponIcon = weapon.GetComponent<Image>();
        _passiveAbilityIcon = abilityP.GetComponent<Image>();
        _frame1 = frame1.GetComponent<Image>();
        _frame2 = frame2.GetComponent<Image>();
        _frame3 = frame3.GetComponent<Image>();
        _frame4 = frame4.GetComponent<Image>();
        _frameP = frameP.GetComponent<Image>();
        _frameW = frameW.GetComponent<Image>();
        _ability1IconCd = ability1Cd.GetComponent<Image>();
        _ability2IconCd = ability2Cd.GetComponent<Image>();
        _ability3IconCd = ability3Cd.GetComponent<Image>();
        _ability4IconCd = ability4Cd.GetComponent<Image>();
        _abilityPIconCd = abilityPCd.GetComponent<Image>();
        foreach (var t in buffIconList)
        {
            _buffIconList.Add(t.GetComponent<Image>());
        }
        foreach (var t in buffIconCdList)
        {
            _buffIconCdList.Add(t.GetComponent<Image>());
        }
        foreach (var t in debuffIconList)
        {
            _debuffIconList.Add(t.GetComponent<Image>());
        }
        foreach (var t in debuffIconCdList)
        {
            _debuffIconCdList.Add(t.GetComponent<Image>());
        }
        StartInfo();
        SetupGemNames();
    }

    private readonly DescribableObject _gem1 = new DescribableObject();
    private readonly DescribableObject _gem2 = new DescribableObject();
    private readonly DescribableObject _gem3 = new DescribableObject();
    private readonly DescribableObject _gem4 = new DescribableObject();
    private readonly DescribableObject _gem5 = new DescribableObject();
    private readonly DescribableObject _gem6 = new DescribableObject();
    private readonly DescribableObject _gem7 = new DescribableObject();
    private readonly DescribableObject _gem8 = new DescribableObject();
    private void SetupGemNames()
    {
        
        _gem1.displayName = new GemHealPower(Rarities.COMMON,null).displayName;
        _gem1.baseDesc = new GemHealPower(Rarities.COMMON, null).displayDesc;
        _gem2.displayName = new GemLifeSteal(Rarities.COMMON,null).displayName;
        _gem2.baseDesc = new GemLifeSteal(Rarities.COMMON, null).displayDesc;
        _gem3.displayName = new GemAD(Rarities.COMMON,null).displayName;
        _gem3.baseDesc = new GemAD(Rarities.COMMON, null).displayDesc;
        _gem4.displayName = new GemAR(Rarities.COMMON,null).displayName;
        _gem4.baseDesc = new GemAR(Rarities.COMMON, null).displayDesc;
        _gem5.displayName = new GemHP(Rarities.COMMON,null).displayName;
        _gem5.baseDesc = new GemHP(Rarities.COMMON, null).displayDesc;
        _gem6.displayName = new GemAS(Rarities.COMMON,null).displayName;
        _gem6.baseDesc = new GemAS(Rarities.COMMON, null).displayDesc;
        _gem7.displayName = new GemMR(Rarities.COMMON,null).displayName;
        _gem7.baseDesc = new GemMR(Rarities.COMMON, null).displayDesc;
        _gem8.displayName = new GemAP(Rarities.COMMON,null).displayName;
        _gem8.baseDesc = new GemAP(Rarities.COMMON, null).displayDesc;
    }
    
    private void StartInfo()
    {
        managerAbility1.describableObject = player.abilityList[0];
        managerAbility2.describableObject = player.abilityList[1];
        managerAbility3.describableObject = player.abilityList[2];
        managerAbility4.describableObject = player.abilityList[3];
        managerAbilityP.describableObject = player.classPassive;
        _gem1.displayDesc = _gem1.baseDesc + $"\nCurrent bonus : {Math.Round(player.hpoBonus*100)}%";
        managerGem1.describableObject = _gem1;
        _gem2.displayDesc = _gem2.baseDesc + $"\nCurrent bonus : {Math.Round(player.lsBonus*100)}%";
        managerGem2.describableObject = _gem2;
        _gem3.displayDesc = _gem3.baseDesc + $"\nCurrent bonus : {player.adBonus}";
        managerGem3.describableObject = _gem3;
        _gem4.displayDesc = _gem4.baseDesc + $"\nCurrent bonus : {player.arBonus}";
        managerGem4.describableObject = _gem4;
        _gem5.displayDesc = _gem5.baseDesc + $"\nCurrent bonus : {player.hpBonus}";
        managerGem5.describableObject = _gem5;
        _gem6.displayDesc = _gem6.baseDesc + $"\nCurrent bonus : {Math.Round(player.asBonus*100)}%";
        managerGem6.describableObject = _gem6;
        _gem7.displayDesc = _gem7.baseDesc + $"\nCurrent bonus : {player.mrBonus}";
        managerGem7.describableObject = _gem7;
        _gem8.displayDesc = _gem8.baseDesc + $"\nCurrent bonus : {player.apBonus}";
        managerGem8.describableObject = _gem8;
    }
    
    private void Update()
    {
        UpdateIcon();
        UpdateHealth();
        UpdateBuffs();
        UpdateDebuffs();
        StartInfo();
        UpdateTimer();
        player.inEvent = _inChoice;
    }

    private void UpdateTimer()
    {
        roundTime.text = $"{(int)player.roundTimer/60}:{(int)player.roundTimer%60}";
        roundNumber.text = $"Round {player.roundNumber}";
    }
    
    public void Choice1()
    {
        _inChoice = false;
        player.abilityList[0].OnEnd();
        Ability backup = player.abilityList[0];
        player.abilityList[0] = player.awaitingChange;
        player.awaitingChange = null;
        Instantiate(_resources.lootList[2],player.model.transform.position,Quaternion.identity); //set rarity needed
        choice.SetActive(false);
    }
    
    public void Choice2()
    {
        _inChoice = false;
        player.abilityList[1].OnEnd();
        Ability backup = player.abilityList[1];
        player.abilityList[1] = player.awaitingChange;
        player.awaitingChange = null;
        Instantiate(_resources.lootList[2],player.model.transform.position,Quaternion.identity); //set rarity needed
        choice.SetActive(false);
    }
    
    public void Choice3()
    {
        _inChoice = false;
        player.abilityList[2].OnEnd();
        Ability backup = player.abilityList[2];
        player.abilityList[2] = player.awaitingChange;
        player.awaitingChange = null;
        Instantiate(_resources.lootList[2],player.model.transform.position,Quaternion.identity); //set rarity needed
        choice.SetActive(false);
    }
    
    public void Choice4()
    {
        _inChoice = false;
        player.abilityList[3].OnEnd();
        Ability backup = player.abilityList[3];
        if (player.classId == 6)
        {
            player.awaitingChange.ChangeRarity(1);
            player.awaitingChange.ChangeRarity(1);
            if (backup.Rarity == Rarities.MYTHIC)
            {
                backup.ChangeRarity(-1);
            }
            else
            {
                backup.ChangeRarity(-1);
                backup.ChangeRarity(-1);
            }
                
        }
        else
        {
            player.awaitingChange.ChangeRarity(1);
            backup.ChangeRarity(-1);
        }
        player.abilityList[3] = player.awaitingChange;
        player.awaitingChange = null;
        GameObject newGem = Instantiate(_resources.lootList[2],player.model.transform.position,Quaternion.identity); //set rarity needed
        newGem.GetComponent<GemOrb>()._rarity = backup.Rarity;
        newGem.GetComponent<GemOrb>().UpdateInfo();
        choice.SetActive(false);
    }
    
    public void Choice5()
    {
        _inChoice = false;
        Ability backup = player.awaitingChange;
        player.awaitingChange = null;
        Instantiate(_resources.lootList[2],player.model.transform.position,Quaternion.identity); //set rarity needed
        choice.SetActive(false);
    }

    public void ActivateChoice()
    {
        _inChoice = true;
        choice.SetActive(true);
        foundAbility.text =
            $"You found an {player.awaitingChange.displayName} Ability Fragment !\nChoose which ability to discard (discarded ability will be turned into a random Gem of the same Rarity.)";
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
    
    private Sprite GetIconRarity(Rarities ability)
    {

        switch (ability)
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

    private void UpdateDebuffs()
    {
        for (int i = 0; i < debuffList.Count; i++)
        {
            if (i < player.debuffList.Count)
            {
                debuffList[i].SetActive(true);
                _debuffIconCdList[i].sprite = _resources.debuffIconList[player.debuffList[i].iconId];
                _debuffIconList[i].sprite = _resources.debuffIconList[player.debuffList[i].iconId];
                var duration = player.debuffList[i].Duration;
                var maxDuration = player.debuffList[i].maxDuration;
                if (duration != null && maxDuration != null)
                {
                    _debuffIconCdList[i].fillAmount = (float) (duration / maxDuration);
                }
                else
                {
                    _debuffIconCdList[i].fillAmount = 0;
                }
            }
            else
            {
                debuffList[i].SetActive(false);
            }
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
        weaponName.text = player.primaryWeapon.Name;
        _weaponIcon.sprite = _resources.weaponIconList[player.primaryWeapon.id-1];
        _frameP.sprite = GetIconRarity(player.primaryWeapon.Rarity);
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
        
        if (player.classPassive is AbilityNone_0)
        {
            abilityP.SetActive(false);
            abilityPCd.SetActive(false);
            cd5.text = "";
        }
        else
        {
            if (player.classPassive.State == States.COOLDOWN)
            {
                cd5.text = $"{Math.Round(player.classPassive.CurrentCooldown,1)}";
                _abilityPIconCd.fillAmount = player.classPassive.CurrentCooldown / player.classPassive.Cooldown;
            }
            else
            {
                cd5.text = "";
                abilityP.SetActive(true);
                abilityPCd.SetActive(true);
                _abilityPIconCd.fillAmount = 0;
                _passiveAbilityIcon.sprite = _resources.passiveAbilityIconList[player.classPassive.id-201];
                _abilityPIconCd.sprite = _resources.passiveAbilityIconList[player.classPassive.id-201];
                _frameP.sprite = GetIconRarity(player.classPassive);
            }
        }
    }
}