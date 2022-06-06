using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LeaveArena : MonoBehaviour
{
    public GameObject leaveButton;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }
    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while(PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        SceneManager.LoadScene("Launcher");
        DontDestroySound.Instance.gameObject.GetComponent<AudioSource>().UnPause();
    }

    public void OnClickConnectArena()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickConnectSurvival()
    {
        SceneManager.LoadScene("LobbySurvival");
    }

}
