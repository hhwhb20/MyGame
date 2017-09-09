﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class RegisterPanel : BasePanel {

    public InputField usernameIF;
    public InputField passwordIF;
    public Text hintMessage;

    private RegisterRequest registerRequest;

    void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();
    }

    public void OnRegisterButton()
    {
        hintMessage.text = "";
        registerRequest.username = usernameIF.text;
        registerRequest.password = passwordIF.text;
        registerRequest.DefaultRequest();
    }
    public void OnBackButton()
    {
        UIManager.Instance.ShowPanel(UIPanelType.Login);
    }
    public void OnReigsterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            hintMessage.text = "注册成功，请返回登录";
        }
        else if (returnCode == ReturnCode.Failed)
        {
            hintMessage.text = "用户名重复，请更改用户名重新注册";
        }
    }
    public override void OnEnter()
    {
        this.gameObject.SetActive(true);
    }

    public override void OnPause()
    {

    }

    public override void OnResume()
    {

    }

    public override void OnExit()
    {
        this.gameObject.SetActive(false);
    }
}
