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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        data.level = 0;
        data.levelComplete = false;
        data.levelSuccess = new bool[_GLOBAL_GAME_DATA.levelCount];
        SceneManager.LoadScene("LetterScene");
        //SceneManager.LoadScene("SampleScene");
    }

    public void RollCredits()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
