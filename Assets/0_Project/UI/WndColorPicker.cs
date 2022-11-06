using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WndColorPicker : MonoBehaviour
{
    [Header("Color Picker:")]
    [SerializeField] private Button buttonColorPickerApply = null;
    [SerializeField] private Button buttonColorPickerCancel = null;
    [SerializeField] private Button buttonColorPickerBackground = null;
    [SerializeField] private CUIColorPicker colorPicker = null;
    //private Action OnColorPickerChanged;
    private void HideThisWindow()
    {
        Debug.Log("WndColorPicker : HideThisWindow");
        gameObject.SetActive(false);
    }
    
    
    private void ButtonsAddListeners()
    {
        Debug.Log("WndColorPicker : ButtonsAddListeners");
        buttonColorPickerApply.onClick.AddListener(OnclickColorPickerApply);
        buttonColorPickerCancel.onClick.AddListener(OnclickColorPickerCancel);
        buttonColorPickerBackground.onClick.AddListener(OnclickBackground);
        colorPicker.SetOnValueChangeCallback(OnColorPickerChanged);
    }
    
    #region BUTTONS_COLOR_PICKER

    void OnclickColorPickerApply()
    {
        Debug.Log("WndColorPicker : OnclickColorPickerApply");
        PlayClickSound();
        HideThisWindow();
        CtrlApi.Instance.SetCatColor(_color);
    }
    
    void OnclickColorPickerCancel()
    {
        Debug.Log("WndColorPicker : OnclickColorPickerCancel");
        PlayClickSound();
        HideThisWindow();
    }
    
    void OnclickBackground()
    {
        Debug.Log("WndColorPicker : OnclickBackground");
        PlayClickSound();
        HideThisWindow();
    }
    

    #endregion
    
    private void PlayClickSound()
    {
        Debug.Log("WndColorPicker : PlayClickSound");
        if(CtrlSound.Instance != null) CtrlSound.Instance.PlayClick();
        else Debug.LogError("WndDetails : PlayClickSound : CtrlSound.Instance is NULL"); 
    }



    private Color _color;
    private void OnColorPickerChanged(Color color)
    {
        _color = color;
    }
    
    
    private void Awake()
    {
        ButtonsAddListeners();
    }
}
