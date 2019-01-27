using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterContent : MonoBehaviour
{
    public _GLOBAL_GAME_DATA data;
    public GameObject parchment;
    public TextMeshProUGUI textBox; 
    public float scrollSpeed;

    private float[] level_startLevelFadeOutTime = new float[_GLOBAL_GAME_DATA.levelCount];
    private float[] level_postLevelStartPosition = new float[_GLOBAL_GAME_DATA.levelCount];
    private float[] level_endLetterFadeOutTime = new float[_GLOBAL_GAME_DATA.levelCount];
    private string[] level_textPrefix = new string[_GLOBAL_GAME_DATA.levelCount];
    private string[] level_textSuccess = new string[_GLOBAL_GAME_DATA.levelCount];
    private string[] level_textFailure = new string[_GLOBAL_GAME_DATA.levelCount];
    private string[] level_textSuffix = new string[_GLOBAL_GAME_DATA.levelCount];

    // Start is called before the first frame update
    void Start()
    {
        //set letter details in separate function
        //they will be long and don't make much sense to place serially with logic
        SetLetterDetails();

        //Actively move letter, and fade in and out appropriately
        parchment.GetComponent<Rigidbody2D>().velocity = new Vector2(0, scrollSpeed);
        float fadeTime;
        if(data.levelComplete)
        {
            //populate text
            textBox.text = level_textPrefix[data.level];
            if (data.levelSuccess[data.level])
                textBox.text += level_textSuccess[data.level];
            else
                textBox.text += level_textFailure[data.level];
            textBox.text += level_textSuffix[data.level];
            //start where we left of with the letter - part way down
            parchment.transform.position = new Vector3(0, level_postLevelStartPosition[data.level], 0);
            //now fade and move on to next level
            fadeTime = level_endLetterFadeOutTime[data.level];
            ++data.level;
        }
        else
        {
            //all we can do so far is prefix and fade
            textBox.text = level_textPrefix[data.level];
            fadeTime = level_startLevelFadeOutTime[data.level];
        }

        StartCoroutine(FadeOut(fadeTime));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator FadeOut(float timeout)
    {
        yield return new WaitForSeconds(timeout);
    }

    private void SetLetterDetails()
    {
        level_startLevelFadeOutTime[0] = 10; //TODO - demo number, actually manually adjust this
        level_postLevelStartPosition[0] = -300;
        level_endLetterFadeOutTime[0] = 10;
        level_textPrefix[0] = "Dear Mary,\n\n\tThis letter must be brief. It's been over a week "
            + "since we began our attack on Carentan. They were ready for us. I'm sure by the "
            + "time you get this letter you'll have heard about Franklin.\n\n";
        level_textSuccess[0] = "Who'd have thought we grew up with Achilles? He saved us by "
            + "destroying that tank, you know?";
        level_textFailure[0] = "Please tell Mrs. Thomas that Franklin fought bravely. I haven't "
            + "found the heart to write her myself.";
        level_textSuffix[0] = "I'm sorry but I have to go now. I'll write again soon.\n\n"
            + "With love,\n\nJames";

        level_startLevelFadeOutTime[1] = 10;
        level_postLevelStartPosition[1] = -300;
        level_endLetterFadeOutTime[1] = 10;
        level_textPrefix[1] = "Dear Mary,\n\n\tDo you remember how clear the sky was "
            + "that night in the cornfield before I left? We had a sky like that here. "
            + "Franky was point man on patrol. Perhaps news has already reached you.\n\n";
        level_textSuccess[1] = "He deserves all the praise he gets. I think of him "
            + "differently now. Not the Franky we grew up with.";
        level_textFailure[1] = "I wish there was something I could have done. Maybe I "
            + "should have taken point. I'll write Mrs. Thomas a letter tonight.";
        level_textSuffix[1] = "\nYour letters are so important to me. I'll write again "
            + "as soon as I can.";
    }
}
