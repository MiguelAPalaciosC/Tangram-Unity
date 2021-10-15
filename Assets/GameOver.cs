using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void BtnReintento()
    {
        SceneManager.LoadScene("TangramSingle");
        //SceneManager.LoadScene(1); es lo mismo
    }
    public void BtnMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
