using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WndDetails : MonoBehaviour
{
    [Header("Buttons:")]
    [SerializeField] private Button buttonBack = null;
    [SerializeField] private Button buttonMenu = null;
    [SerializeField] private Button buttonChangeColor = null;
    [SerializeField] private Button buttonGetInfo = null;
    
    [Header("Text:")] 
    [SerializeField] private TextMeshProUGUI textName = null;
    [SerializeField] private TMP_InputField inputDescription = null;

    [Header("Picture:")] [SerializeField] private RawImage ImageQrCode = null;
    
    [Header("Select Color:")]
    [SerializeField] private GameObject wndColorPicker = null;
    
    [Space]
    [SerializeField] private SwitchToggle switchToggle;
    
    private void ButtonsAddListeners()
    {
        Debug.Log("WndDetails : ButtonsAddListeners");
        buttonBack.onClick.AddListener(OnclickBack);
        buttonMenu.onClick.AddListener(OnclickMenu);
        buttonGetInfo.onClick.AddListener(OnclickGetInfo);
        buttonChangeColor.onClick.AddListener(OnclickChangeColor);
        switchToggle.toggle.onValueChanged.AddListener(OnclickToggle);
    }

    private void ShowColorPicker(bool isShow)
    {
        Debug.Log("WndDetails : ShowColorPicker : isShow = " + isShow);
        if(wndColorPicker)wndColorPicker.SetActive(isShow);
        else Debug.LogError("WndDetails : ShowColorPicker : wndColorPicker is NULL");
    }


    [SerializeField] private GameObject WndExit = null;
    private void ShowWndExit(bool isShow)
    {
        WndExit.SetActive(isShow);
    }
    
    
    #region BUTTONS

    void OnclickBack()
    {
        Debug.Log("WndDetails : OnclickBack");
        PlayClickSound();
        ShowWndExit(true);
        StartCoroutine(ExitAppDelayed(2F));
    }
    
    void OnclickMenu()
    {
        Debug.Log("WndDetails : OnclickMenu");
        PlayClickSound();
    }

    void OnclickChangeColor()
    {
        Debug.Log("WndDetails : OnclickChangeColor");
        ShowColorPicker(true);
    }
    
    void OnclickToggle(bool isTrue)
    {
        Debug.Log("WndDetails : OnclickToggle : isTrue = " + isTrue);
        PlayClickSound();
        if(CtrlApi.Instance) CtrlApi.Instance.SetCatEnabled(isTrue);
    }
    
    void OnclickGetInfo()
    {
        Debug.Log("WndDetails : OnclickGetInfo");
        PlayClickSound();
        if(CtrlApi.Instance) CtrlApi.Instance.GetInfo();
    }
    
    private void PlayClickSound()
    {
        Debug.Log("WndDetails : PlayClickSound");
        if(CtrlSound.Instance != null) CtrlSound.Instance.PlayClick();
        else Debug.LogError("WndDetails : PlayClickSound : CtrlSound.Instance is NULL"); 
    }

    #endregion


   
    
   
    //Action from Ctrl API on cat updated
    private void OnCatUpdated(Cat cat)
    {
        Debug.Log("WndDetails : OnCatUpdated : cat.name = " + cat.name);
        SetName(cat.name);
        SetDescription(cat.description);
        SetQrCode(cat.qr_code);
        SetButtonColor(cat.color);
        SetEnable(cat.enable);
    }

    private IEnumerator ExitAppDelayed(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("EXIT...");
        Application.Quit();
    }
    
    
    #region Set Cat Ui
    private void SetName(string name)
    {
        if (textName) textName.text = name;
        else Debug.LogError("WndDetails : SetName : textName is NULL");
    }
    
    private void SetDescription(string description)
    {
        if (inputDescription) inputDescription.text = description;
        else Debug.LogError("WndDetails : SetDescription : textDescription is NULL");
        RefreshScrollVisibility();
    }

    private void RefreshScrollVisibility()
    {
        //inputDescription.lineLimit.
    }

    private void SetQrCode(string base64Str)
    {
        Debug.Log("WndDetails : SetQrCode : base64Str = " + base64Str);
        var texQrCode = new Texture2D (1, 1);
        texQrCode.LoadImage(Convert.FromBase64String(base64Str));
        ImageQrCode.texture = texQrCode;
    }
    
    private void SetButtonColor(string color)
    {
        Debug.Log("WndDetails : SetColor : color = " + color);
        if (buttonChangeColor == null) Debug.LogError("WndDetails : SetColor : buttonChangeColor is NULL");
        else
        {
            string colorTmp = "#" + color;
            Color col = Color.magenta;
            UnityEngine.ColorUtility.TryParseHtmlString(colorTmp, out col);
            SetButtonColor(buttonChangeColor, col);
        }
    }

    
    private void SetEnable(bool isEnable)
    {
        Debug.Log("WndDetails : SetEnable : isEnable = " + isEnable);
        switchToggle.toggle.isOn = isEnable;
        switchToggle.OnSwitch(isEnable);
    }
    
    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.pressedColor = color;
        colors.selectedColor = color;
        colors.highlightedColor = color;
        button.colors = colors;
    }
    

    #endregion
    
    
    #region UNITY
    
    
    private void Awake()
    {
        ButtonsAddListeners();
        ShowColorPicker(false);
        ShowWndExit(false);
    }

    private void Start()
    {
        // Subscribe cat
        if (CtrlApi.Instance)
            CtrlApi.Instance.OnCatUpdated += OnCatUpdated;
    }

    #endregion
    
}
