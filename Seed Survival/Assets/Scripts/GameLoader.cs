using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public GameObject loadReminder;
    public Button loadButton;
    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/saveData.json"))
            loadButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        Destroy(loadReminder);
        SceneManager.LoadScene("Game");
    }

    public void LoadGame()
    {
        if(File.Exists(Application.persistentDataPath+"/saveData.json"))
            SceneManager.LoadScene("Game");
    }

    public void SeeCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
