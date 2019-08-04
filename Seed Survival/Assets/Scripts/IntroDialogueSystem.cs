using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogueSystem : MonoBehaviour
{
    public CanvasGroup fader, bg;
    public Text mText, skipText;
    public float fadeTime, readTime;
    bool loading = false;
    // Start is called before the first frame update
    void Start()
    {
        bg.alpha = 1;
        fader.alpha = 1;
        if (GameObject.FindGameObjectWithTag("LoadGame"))
        {
            loading = true;
            StartCoroutine(ResumeDialogue());
            skipText.gameObject.SetActive(false);
        }
        else
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
        skipText.text = "";
        timer = 0;
        bg.alpha = 0;
        while (timer < fadeTime)
        {
            fader.alpha = 1 - (timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        fader.alpha = 0;

        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<ItemManager>().enabled = true;
        Destroy(transform.gameObject);
    }

    //starts dialogue system and starts load process
    private IEnumerator ResumeDialogue()
    {
        GameStateData gsd;
        string loadPath = Application.persistentDataPath + "/saveData.json";
        using (StreamReader r = new StreamReader(loadPath))
        {
            string json = r.ReadToEnd();
            gsd = JsonUtility.FromJson<GameStateData>(json);
        }
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<PauseMenu>().LoadGame(gsd);
            //dialogue
            string path = Application.streamingAssetsPath + "/loadDialogue.txt";
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
        //load dialogue specific
        mText.text = "Trees Planted: " + gsd.trees.Length;
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
        mText.text = "";
        skipText.text = "";
        timer = 0;
        bg.alpha = 0;
        while (timer < fadeTime)
        {
            fader.alpha = 1 - (timer / fadeTime);
            timer += Time.deltaTime;
            yield return null;
        }
        fader.alpha = 0;
        GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<ItemManager>().enabled = true;
        Destroy(transform.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S) && !loading)
        {
            StopAllCoroutines();
            mText.text = "";
            skipText.text = "";
            fader.alpha = 0;
            bg.alpha = 0;

            GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<ItemManager>().enabled = true;
            Destroy(transform.gameObject);
        }
    }
}
