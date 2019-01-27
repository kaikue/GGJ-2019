using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LetterContent : MonoBehaviour
{
    public _GLOBAL_GAME_DATA data;
    public GameObject parchment;
    public TextMeshProUGUI letterTextBox;
    public GameObject fadedrop;
    public TextMeshProUGUI narrationTextBox;
    public TextMeshProUGUI specialTopTextBox;
    public TextMeshProUGUI specialBottomTextBox;
    public float scrollSpeed;

    public float fadeSpeed;
    private FadeStatus currentFade = FadeStatus.None;
    private FadeStatus narrationFade = FadeStatus.None;
    private FadeStatus specialTopFade = FadeStatus.None;
    private FadeStatus specialBottomFade = FadeStatus.None;
    private bool introComplete = false;
    private bool epilogueComplete = false;

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
        Debug.Log("Starting LetterScene with level=" + data.level + " levelComplete="
            + data.levelComplete);

        if (data.level == _GLOBAL_GAME_DATA.levelCount)
        {
            StartCoroutine(RunEpilogue());
            return;
        }

        //set letter details in separate function
        //they will be long and don't make much sense to place serially with logic
        SetLetterDetails();

        //Actively move letter, and fade in and out appropriately
        parchment.GetComponent<Rigidbody2D>().velocity = new Vector2(0, scrollSpeed);
        float fadeDelay = 0;
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
            parchment.transform.position = new Vector3(parchment.transform.position.x, 
                level_postLevelStartPosition[data.level], parchment.transform.position.z);
        }
        else
        {
            //all we can do so far is prefix and fade
            letterTextBox.text = level_textPrefix[data.level];
        }

        //Perform the intro if necessary but set stuff up for the letter still running
        if (data.level == 0 && data.levelComplete == false)
        {
            StartCoroutine(RunIntro());
            //also give us some time with the parchment scroll
            parchment.transform.position = new Vector3(parchment.transform.position.x, 
                -725, parchment.transform.position.z);
            fadeDelay = -1;
        }

        StartCoroutine(WaitForFadeIn(fadeDelay));
    }

    // Update is called once per frame
    void Update()
    {
        //check if we're just done
        if(data.level == _GLOBAL_GAME_DATA.levelCount && epilogueComplete)
            SceneManager.LoadScene("MainMenu");

        //first do backdrop if necessary
        Color backdropColor = fadedrop.GetComponent<Image>().color;
        if (Input.GetButtonDown("Fire1"))
            currentFade = FadeStatus.FadeOut;
        if(currentFade == FadeStatus.FadeOut)
        {
            //if we're fading out of the scene, take both visuals and audio with you
            backdropColor.a += fadeSpeed;
            AudioSource a = GetComponent<AudioSource>();
            a.volume -= fadeSpeed;
            if (backdropColor.a >= 1)
            {
                //When the letter fades out, we're also ready for the next state
                currentFade = FadeStatus.None;
                if (data.levelComplete)
                {
                    ++data.level;
                    data.levelComplete = false;
                    SceneManager.LoadScene("LetterScene");
                }
                else
                {
                    data.levelComplete = true;
                    SceneManager.LoadScene("SampleScene");
                }
            }
            //Debug.Log("Fading Out at " + backdropColor.a);
            fadedrop.GetComponent<Image>().color = backdropColor;
        }
        else if (currentFade == FadeStatus.FadeIn)
        {
            backdropColor.a -= fadeSpeed;
            if (backdropColor.a <= 0)
                currentFade = FadeStatus.None;
            //Debug.Log("Fading In at " + backdropColor.a);
            fadedrop.GetComponent<Image>().color = backdropColor;
        }

        //next do narration if necessary
        Color narrationColor = narrationTextBox.GetComponent<TextMeshProUGUI>().color;
        if (narrationFade == FadeStatus.FadeIn)
        {
            narrationColor.a += fadeSpeed;
            //Debug.Log("Fading In at " + narrationColor.a);
            if (narrationColor.a >= 1)
                narrationFade = FadeStatus.None;
            narrationTextBox.GetComponent<TextMeshProUGUI>().color = narrationColor;
        }
        else if (narrationFade == FadeStatus.FadeOut)
        {
            narrationColor.a -= fadeSpeed;
            //Debug.Log("Fading Out at " + narrationColor.a);
            if (narrationColor.a <= 0)
                narrationFade = FadeStatus.None;
            narrationTextBox.GetComponent<TextMeshProUGUI>().color = narrationColor;
        }
        //next do special if necessary
        Color specialTopColor = specialTopTextBox.GetComponent<TextMeshProUGUI>().color;
        if (specialTopFade == FadeStatus.FadeIn)
        {
            specialTopColor.a += fadeSpeed;
            //Debug.Log("Fading In at " + specialTopColor.a);
            if (specialTopColor.a >= 1)
                specialTopFade = FadeStatus.None;
            specialTopTextBox.GetComponent<TextMeshProUGUI>().color = specialTopColor;
        }
        else if (specialTopFade == FadeStatus.FadeOut)
        {
            specialTopColor.a -= fadeSpeed;
            //Debug.Log("Fading Out at " + specialTopColor.a);
            if (specialTopColor.a <= 0)
                specialTopFade = FadeStatus.None;
            specialTopTextBox.GetComponent<TextMeshProUGUI>().color = specialTopColor;
        }
        Color specialBottomColor = specialBottomTextBox.GetComponent<TextMeshProUGUI>().color;
        if (specialBottomFade == FadeStatus.FadeIn)
        {
            specialBottomColor.a += fadeSpeed;
            //Debug.Log("Fading In at " + narrationColor.a);
            if (specialBottomColor.a >= 1)
                specialBottomFade = FadeStatus.None;
            specialBottomTextBox.GetComponent<TextMeshProUGUI>().color = specialBottomColor;
        }
        else if (specialBottomFade == FadeStatus.FadeOut)
        {
            specialBottomColor.a -= fadeSpeed;
            //Debug.Log("Fading Out at " + narrationColor.a);
            if (specialBottomColor.a <= 0)
                specialBottomFade = FadeStatus.None;
            specialBottomTextBox.GetComponent<TextMeshProUGUI>().color = specialBottomColor;
        }
    }

    public IEnumerator WaitForFadeIn(float fadeDelay)
    {
        if (fadeDelay >= 0)
        {
            yield return new WaitForSeconds(fadeDelay);
            if(data.levelComplete)
                StartCoroutine(WaitForFadeOut(level_endLetterFadeOutTime[data.level]));
            else
                StartCoroutine(WaitForFadeOut(level_startLevelFadeOutTime[data.level]));
        }
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
        /*narrationTextBox.text = "They are the letters he sent to his family...";
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
            yield return new WaitForSeconds(0.1f);*/
        specialTopTextBox.text = "They are the letters he sent to his sweetheart...";
        specialTopFade = FadeStatus.FadeIn;
        while (specialTopFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        specialBottomTextBox.text = "back home.";
        specialBottomFade = FadeStatus.FadeIn;
        while (specialBottomFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        specialTopFade = FadeStatus.FadeOut;
        specialBottomFade = FadeStatus.FadeOut;
        while (specialBottomFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        introComplete = true;
    }

    private IEnumerator RunEpilogue()
    {
        //end the story
        specialTopTextBox.text = "There were no more letters...";
        specialTopFade = FadeStatus.FadeIn;
        while (specialTopFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        float survivors = 0;
        for (int l = 0; l < _GLOBAL_GAME_DATA.levelCount; ++l)
        {
            if (data.levelSuccess[l])
                ++survivors;
            Debug.Log("Someone survived");
        }
        Debug.Log("Win Condition = " + _GLOBAL_GAME_DATA.levelCount / 2);
        if ((float) _GLOBAL_GAME_DATA.levelCount / 2.0f >= survivors)
        {
            specialBottomTextBox.text = "with the help of his surviving friends, your grandfather made it home after the war.";
        }
        else
        {
            specialBottomTextBox.text = "with his friends gone, your grandfather never made it home.";
        }
        specialBottomFade = FadeStatus.FadeIn;
        while (specialBottomFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(0.5f);
        specialTopFade = FadeStatus.FadeOut;
        specialBottomFade = FadeStatus.FadeOut;
        while (specialBottomFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationTextBox.text = "";
        //Provide the results
        //TODO make this easier
        if (data.levelSuccess[0])
            narrationTextBox.text += "Franklin came home.";
        else
            narrationTextBox.text += "Franklin didn't make it home.";
        narrationTextBox.text += "\n";
        if (data.levelSuccess[1])
            narrationTextBox.text += "Franky came home.";
        else
            narrationTextBox.text += "Franky didn't make it home.";
        narrationFade = FadeStatus.FadeIn;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationFade = FadeStatus.FadeOut;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        //Salute
        narrationTextBox.text = "We thank our military service men and women "
            + "who can't be home right now so we can GGJ.";
        narrationFade = FadeStatus.FadeIn;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        narrationFade = FadeStatus.FadeOut;
        while (narrationFade != FadeStatus.None)
            yield return new WaitForSeconds(0.1f);
        epilogueComplete = true;
    }

    private void SetLetterDetails()
    {
        level_startLevelFadeOutTime[0] = 13;
        level_postLevelStartPosition[0] = -50;
        level_endLetterFadeOutTime[0] = 25.5f;
        level_textPrefix[0] = "Dear Mary,\n\n\tThis letter must be brief. It's been over a week "
            + "since we began our attack on Carentan. They were ready for us. I'm sure by the "
            + "time you get this letter you'll have heard about Franklin.\n\n";
        level_textSuccess[0] = "Who'd have thought we grew up with Achilles? He saved us by "
            + "destroying that tank, you know?";
        level_textFailure[0] = "Please tell Mrs. Thomas that Franky fought bravely. I haven't "
            + "found the heart to write her myself.";
        level_textSuffix[0] = "I'm sorry but I have to go now. I'll write again soon.\n\n"
            + "With love,\n\nJames";

        level_startLevelFadeOutTime[1] = 13.5f;
        level_postLevelStartPosition[1] = -50;
        level_endLetterFadeOutTime[1] = 25.5f;
        level_textPrefix[1] = "Dear Mary,\n\n\tDo you remember how clear the sky was "
            + "that night in the cornfield before I left? We had a sky like that here "
            + "when we were on patrol. Do you remember Mrs. Thomas' boy, John?"
            + "Perhaps news has already reached you.\n\n";
        level_textSuccess[1] = "He deserves all the praise he gets. I think of him "
            + "differently now. Not the Franky we grew up with.";
        level_textFailure[1] = "I wish there was something I could have done. Maybe I "
            + "should have taken point. I'll write Mrs. Thomas a letter tonight.";
        level_textSuffix[1] = "\nYour letters are so important to me. I'll write again "
            + "as soon as I can.\n\nWith love,\n\nJames";
    }
}
