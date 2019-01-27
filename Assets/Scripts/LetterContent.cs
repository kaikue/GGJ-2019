using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterContent : MonoBehaviour
{
    public _GLOBAL_GAME_DATA data;
    public GameObject parchment;
    public TextMeshProUGUI letterTextBox;
    public GameObject fadedrop;
    public TextMeshProUGUI narrationTextBox;
    public float scrollSpeed;

    public float fadeSpeed;
    private FadeStatus currentFade = FadeStatus.None;
    private FadeStatus narrationFade = FadeStatus.None;
    private bool introComplete = false;

    private enum FadeStatus
    {
        None,
        FadeIn,
        FadeOut
    }

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
        float fadeDelay;
        if(data.levelComplete)
        {
            //populate text
            letterTextBox.text = level_textPrefix[data.level];
            if (data.levelSuccess[data.level])
                letterTextBox.text += level_textSuccess[data.level];
            else
                letterTextBox.text += level_textFailure[data.level];
            letterTextBox.text += level_textSuffix[data.level];
            //start where we left of with the letter - part way down
            parchment.transform.position = new Vector3(0, level_postLevelStartPosition[data.level], 0);
            //now fade and move on to next level
            fadeDelay = level_endLetterFadeOutTime[data.level];
            ++data.level;
        }
        else
        {
            //all we can do so far is prefix and fade
            letterTextBox.text = level_textPrefix[data.level];
            fadeDelay = level_startLevelFadeOutTime[data.level];
        }

        //Perform the intro if necessary but set stuff up for the letter still running
        if (data.level == 0 && data.levelComplete == false)
        {
            StartCoroutine(RunIntro());
            //also give us some time with the parchment scroll
            parchment.transform.position = new Vector3(parchment.transform.position.x, 
                -800, parchment.transform.position.z);
            fadeDelay = -1;
        }

        StartCoroutine(WaitForFadeIn(fadeDelay));
    }

    // Update is called once per frame
    void Update()
    {
        //first do backdrop if necessary
        Color backdropColor = fadedrop.GetComponent<Image>().color;
        if(currentFade == FadeStatus.FadeIn)
        {
            backdropColor.a -= fadeSpeed;
            if (backdropColor.a <= 0)
                currentFade = FadeStatus.None;
            //Debug.Log("Fading In at " + backdropColor.a);
            fadedrop.GetComponent<Image>().color = new Color(backdropColor.r,
                backdropColor.g, backdropColor.b, backdropColor.a);
        }
        else if(currentFade == FadeStatus.FadeOut)
        {
            backdropColor.a += fadeSpeed;
            if (backdropColor.a >= 1)
                currentFade = FadeStatus.None;
            //Debug.Log("Fading Out at " + backdropColor.a);
            fadedrop.GetComponent<Image>().color = new Color(backdropColor.r,
                backdropColor.g, backdropColor.b, backdropColor.a);
        }
        //next do narration if necessary
        Color narrationColor = narrationTextBox.GetComponent<TextMeshProUGUI>().color;
        if (narrationFade == FadeStatus.FadeIn)
        {
            narrationColor.a += fadeSpeed;
            //Debug.Log("Fading In at " + narrationColor.a);
            if (narrationColor.a >= 1)
                narrationFade = FadeStatus.None;
            narrationTextBox.GetComponent<TextMeshProUGUI>().color = new Color(narrationColor.r,
                narrationColor.g, narrationColor.b, narrationColor.a);
        }
        else if (narrationFade == FadeStatus.FadeOut)
        {
            narrationColor.a -= fadeSpeed;
            //Debug.Log("Fading Out at " + narrationColor.a);
            if (narrationColor.a <= 0)
                narrationFade = FadeStatus.None;
            narrationTextBox.GetComponent<TextMeshProUGUI>().color = new Color(narrationColor.r,
                narrationColor.g, narrationColor.b, narrationColor.a);
        }

    }

    public IEnumerator WaitForFadeIn(float fadeDelay)
    {
        if (fadeDelay > 0)
            yield return new WaitForSeconds(fadeDelay);
        else
        {
            while (!introComplete)
                yield return new WaitForSeconds(0.1f);
            StartCoroutine(WaitForFadeOut(level_startLevelFadeOutTime[data.level]));
        }
        currentFade = FadeStatus.FadeIn;
    }

    public IEnumerator WaitForFadeOut(float timeout)
    {
        yield return new WaitForSeconds(timeout);
        currentFade = FadeStatus.FadeOut;
    }


    private IEnumerator RunIntro()
    {
        narrationTextBox.text = "You were helping clean your grandparents attic";
        narrationFade = FadeStatus.FadeIn;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationFade = FadeStatus.FadeOut;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationTextBox.text = "You find a small, unassuming box in the corner";
        narrationFade = FadeStatus.FadeIn;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationFade = FadeStatus.FadeOut;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationTextBox.text = "It contains several old, yellowed papers from when"
            + " your grandfather served in World War II";
        narrationFade = FadeStatus.FadeIn;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(1f);
        narrationFade = FadeStatus.FadeOut;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        //TODO merge these next two
        narrationTextBox.text = "They are the letters he sent to his family...";
        narrationFade = FadeStatus.FadeIn;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationFade = FadeStatus.FadeOut;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationTextBox.text = "...back home";
        narrationFade = FadeStatus.FadeIn;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationFade = FadeStatus.FadeOut;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        introComplete = true;
    }

    private IEnumerator DoNarrationFade()
    {
        narrationFade = FadeStatus.FadeIn;
        while(narrationFade != FadeStatus.None)
           yield return new WaitForSeconds(0.1f); //this needs to be long enough that we're completely faded in - just do tweaks for now
        narrationFade = FadeStatus.FadeOut;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f); //this needs to be long enough that we're completely faded in - just do tweaks for now
    }

    private void SetLetterDetails()
    {
        level_startLevelFadeOutTime[0] = 15; //TODO - demo number, actually manually adjust this
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
