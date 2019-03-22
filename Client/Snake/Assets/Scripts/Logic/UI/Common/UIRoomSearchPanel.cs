﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Framework;
using Framework.UI;

public class UIRoomSearchPanel : UIBasePanel 
{
    public InputField input_roomIP;
    public Button btn_Confirm;
    public Button btn_Cancel;

    bool isRoomIPValid = false;

    public override void Setup()
    {
        base.Setup();

        input_roomIP.onEndEdit.AddListener( ValidateInputRoomIP );
        btn_Confirm.onClick.AddListener(OnClickConfirm);
        btn_Cancel.onClick.AddListener(OnClickCancel);
    }

    void ValidateInputRoomIP(string value)
    {
        if( string.IsNullOrEmpty(value) ){
            isRoomIPValid = false;
            this.LogWarning("Room IP can't be empty.");
        }
        else if( value.IndexOf(" ") >= 0 ){
            isRoomIPValid = false;
            this.LogWarning("Room IP can't contain space.");
        }
        else{
            isRoomIPValid = true;
        }
    }

    void OnClickConfirm()
    {
        if( isRoomIPValid )
            this.Log("OnClickConfirm()");
        
        HideSelf();
    }

    void OnClickCancel()
    {
        HideSelf();
    }

    void HideSelf()
    {
        UIManager.Instance.HideUI(CachedGameObject.name);
    }


    public override void Hide()
    {
        input_roomIP.text = "";

        base.Hide();
    }
}