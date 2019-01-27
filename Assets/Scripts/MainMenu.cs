using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public _GLOBAL_GAME_DATA data;
    public TextMeshProUGUI display;
    
    // Start is called before the first frame update
    void Start()
    {
        if(data.level == 0)
            display.text = "Game not started";
        else
            display.text = "Completed Level " + data.level;

        data.level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //TEMPORARILY MAKE first combat scene first
        //eventually make letter first
        SceneManager.LoadScene("SampleScene");
    }
}
