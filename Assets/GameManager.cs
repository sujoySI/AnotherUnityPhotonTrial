using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject SceneCamera;
    public Text PingText;
    public GameObject disconnectUI;
    private bool Off = false;

    public GameObject PlayerFeed;
    public GameObject FeedGrid;


    private void Awake()
    {
        GameCanvas.SetActive(true);
    }

    private void Update()
    {
        CheckInput();
        PingText.text = "Ping: " + PhotonNetwork.GetPing();
    }

    private void CheckInput()
    {
        if(Off && Input.GetKeyDown(KeyCode.Escape))
        {
            disconnectUI.SetActive(false);
            Off= false;
        }
        else if(!Off && Input.GetKeyDown(KeyCode.Escape)) 
        {
            disconnectUI.SetActive(true);
            Off = true;
        }
    }

    public void SpawnPlayer()
    {
        float randomValue = Random.Range(-0.5f,0.5f);

        PhotonNetwork.Instantiate(PlayerPrefab.name, new Vector2(this.transform.position.x * randomValue, this.transform.position.y), Quaternion.identity, 0);
        GameCanvas.SetActive(false);
        SceneCamera.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }

    private void OnPhotonPlayerConnected (PhotonPlayer player)
    {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.NickName + " joined the game";
        obj.GetComponent<Text>().color = Color.green;
    }

    private void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.NickName + " left the game";
        obj.GetComponent<Text>().color = Color.red;
    }
}
