using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Drop this to VerticalScrollbar in

public class ScrollHider : MonoBehaviour
{
    private Scrollbar _scrollbar = null;
    private Image _scrollImage = null;
    
    private void OnScrollUpdated(float value)
    {
        if (_scrollbar.size < 1F)
        {
            _scrollbar.handleRect.gameObject.SetActive(true);
            _scrollImage.enabled = true;
        }
        else
        {
            _scrollbar.handleRect.gameObject.SetActive(false);
            _scrollImage.enabled = false;
        }
    }


    private void Awake()
    {
        _scrollbar = transform.GetComponentInChildren<Scrollbar>();
        _scrollImage = transform.GetComponentInChildren<Image>();
        _scrollbar.onValueChanged.AddListener(OnScrollUpdated);
        OnScrollUpdated(1F);
    }
}
