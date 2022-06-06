using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField usernameInput;
    string inputText;
    public Text buttonText;
    AudioManager aud;
    public GameObject controlPanel;
    public GameObject optionsPanel;


    private void Awake()
    {
        //FindObjectOfType<AudioManager>().Play("MainMenu");
    }

    private void Start()
    {
        inputText = PlayerPrefs.GetString("SaveName");
        usernameInput.text = inputText;
    }

    public void OnClickConnect()
    {
        if(usernameInput.text.Length>=3)
        {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
    public override void OnConnectedToMaster()
    {

        SceneManager.LoadScene("Gamemode");
    }

    public void OnClickOptions()
    {
        controlPanel.SetActive(false);
        optionsPanel.SetActive(true);
        
    }

    public void OnLeaveOptions()
    {
        optionsPanel.SetActive(false);
        controlPanel.SetActive(true);

    }

    public void ChangeName()
    {
        PhotonNetwork.NickName = usernameInput.text;
    }

    public void SaveName()
    {
        inputText = usernameInput.text;
        PlayerPrefs.SetString("SaveName", inputText);
    }
}
