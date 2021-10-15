using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMultiplayer : MonoBehaviour
{
    public void BtnReintento()
    {
        SceneManager.LoadScene("Loading");
        //SceneManager.LoadScene(1); es lo mismo
    }
    public void BtnMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
