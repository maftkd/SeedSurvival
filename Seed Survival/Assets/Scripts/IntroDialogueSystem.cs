using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogueSystem : MonoBehaviour
{
    public CanvasGroup fader, bg;
    public Text mText;
    public float fadeTime, readTime;
    // Start is called before the first frame update
    void Start()
    {
        bg.alpha = 1;
        fader.alpha = 1;
        StartCoroutine(IntroDialogue());
    }

    private IEnumerator IntroDialogue()
    {
        //dialogue
        string path = Application.streamingAssetsPath + "/introDialogue.txt";
        string dialogue = File.ReadAllText(path);
        string[] lines = dialogue.Split('%');
        float timer = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            mText.text = lines[i];
            timer = 0;
            while (timer < fadeTime)
            {
                fader.alpha = 1 - (timer / fadeTime);
                timer += Time.deltaTime;
                yield return null;
            }
            fader.alpha = 0;
            timer = 0;
            while (timer < readTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            timer = 0;
            while (timer < fadeTime)
            {
                fader.alpha = (timer / fadeTime);
                timer += Time.deltaTime;
                yield return null;
            }
            fader.alpha = 1;
        }
        mText.text = "";
        timer = 0;
        bg.alpha = 0;
        while (timer < fadeTime)
        {
            fader.alpha = 1 - (timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        fader.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
