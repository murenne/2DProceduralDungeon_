using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagememt : MonoBehaviour
{
    [Header("hp ep exp bar")]
    [SerializeField] private Image  _hpBarImageOut;
    [SerializeField] private Image  _hpBarImageIn;
    [SerializeField] private Slider _expBar;
    [SerializeField] private Image _magicBar;
    [SerializeField] private float _hpBarEffectSpeed;

    [Header("items")]
    [SerializeField] private Text _hpBottleNumber;
    [SerializeField] private Text _goldCoinNumber;

    [Header("status current number")]
    [SerializeField] private Text _atkCurrentNumber;
    [SerializeField] private Text _defCurrentNumber;
    [SerializeField] private Text _atsCurrentNumber;
    [SerializeField] private Text _adfCurrentNumber;

    [Header("status level up number")]
    [SerializeField] private Text _atkLevelupNumber;
    [SerializeField] private Text _defLevelupNumber;
    [SerializeField] private Text _atsLevelupNumber;
    [SerializeField] private Text _adfLevelupNumber;
    [SerializeField] private GameObject[] _levelupAnimation;

    [Header("map")]
    [SerializeField] private GameObject _miniMapManager;
    [SerializeField] private bool _isMapShowing = false;

    private PlayerController _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // setup 
        _atkCurrentNumber.text = _player.GetComponent<PlayerStatus>().atk.ToString();
        _defCurrentNumber.text = _player.GetComponent<PlayerStatus>().def.ToString();
        _atsCurrentNumber.text = _player.GetComponent<PlayerStatus>().ats.ToString();
        _adfCurrentNumber.text = _player.GetComponent<PlayerStatus>().adf.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // bars
        _expBar.value = _player.GetComponent<PlayerStatus>().currentExp;
        _expBar.maxValue = _player.GetComponent<PlayerStatus>().nextLevelExp[_player.GetComponent<PlayerStatus>().playerLevel];

        _hpBarImageOut.fillAmount = _player.GetComponent<PlayerController>().playerCurrentHP / _player.GetComponent<PlayerController>().playerMaxHP;
        _hpBarImageIn.fillAmount = _hpBarImageIn.fillAmount > _hpBarImageOut.fillAmount ? _hpBarImageIn.fillAmount -= _hpBarEffectSpeed : _hpBarImageIn.fillAmount = _hpBarImageOut.fillAmount;

        _magicBar.fillAmount = _player.GetComponent<PlayerMagicAttack>().magicAttackTime / _player.GetComponent<PlayerMagicAttack>().timer;

        // map
        if (Input.GetKeyDown(KeyCode.M))
        {
            _isMapShowing = !_isMapShowing;
            _miniMapManager.SetActive(_isMapShowing);
        }

        // items
        _hpBottleNumber.text = _player.GetComponent<PlayerItems>().healthPotionNum.ToString();
        _goldCoinNumber.text = _player.GetComponent<PlayerItems>().goldCoinNum.ToString();
    }

    /// <summary>
    /// plauy level up animation
    /// </summary>
    public void LevelupAnimation()
    {
        for (int i = 0; i < _levelupAnimation.Length; i++)
        {
            _atkLevelupNumber.text = "+" + (_player.GetComponent<PlayerStatus>().currentATK - _player.GetComponent<PlayerStatus>().previousATK);
            _defLevelupNumber.text = "+" + (_player.GetComponent<PlayerStatus>().currentDEF - _player.GetComponent<PlayerStatus>().previousDEF);
            _atsLevelupNumber.text = "+" + (_player.GetComponent<PlayerStatus>().currentATS - _player.GetComponent<PlayerStatus>().previousATS);
            _adfLevelupNumber.text = "+" + (_player.GetComponent<PlayerStatus>().currentADF - _player.GetComponent<PlayerStatus>().previousADF);

            _levelupAnimation[i].GetComponent<Animator>().SetTrigger("levelup");
        }
    }
}
