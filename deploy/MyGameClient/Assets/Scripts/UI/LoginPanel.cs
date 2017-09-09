using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;
public class LoginPanel : BasePanel {

    public InputField usernameIF;
    public InputField passwordIF;
    public Text hintMessage;

    private LoginRequest loginRequest;

    void Start()
    {
        loginRequest = GetComponent<LoginRequest>();
    }

    public void OnLoginButton()
    {
        hintMessage.text = "";
        loginRequest.Username = usernameIF.text;
        loginRequest.Password = passwordIF.text;
        loginRequest.DefaultRequest();
    }
    public void OnRegisterButton()
    {
        UIManager.Instance.ShowPanel(UIPanelType.Register);
    }
    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            // 跳转到下一个场景 
            //SceneManager.LoadScene("Game");
            Debug.Log("登录游戏");
        }
        else
        {
            hintMessage.text = "用户名或密码错误";
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
