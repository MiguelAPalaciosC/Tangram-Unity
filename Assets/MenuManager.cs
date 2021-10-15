using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BtnSingleplayer()
    {
        SceneManager.LoadScene("TangramSingle");
        //SceneManager.LoadScene(1); es lo mismo
    }
    public void BtnMultiplayer()
    {
        SceneManager.LoadScene("Loading");
    }

    public void BtnQuit()
    {
        Debug.Log("");
        Application.Quit();
    }
}
