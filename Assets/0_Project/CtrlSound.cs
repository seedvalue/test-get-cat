using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlSound : MonoBehaviour
{
    public static CtrlSound Instance;
    
    [SerializeField] private AudioSource sndButtonClick = null;


    public void PlayClick()
    {
        Debug.Log("CtrlSound : PlayClick");
        if(sndButtonClick) sndButtonClick.Play();
        else Debug.LogError("CtrlSound : PlayClick : SndButtonClick == NULL ");
    }
    
    
    private void Awake()
    {
        Instance = this;
    }
    
    
}
