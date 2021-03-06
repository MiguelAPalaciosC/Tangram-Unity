using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput; 
    public InputField joinInput;

    public void createRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void joinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("TangramMulti");
    }
    public void BtnVolver()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
