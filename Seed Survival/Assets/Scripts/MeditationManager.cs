using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationManager : MonoBehaviour
{
    public float timeScale = 1;
    public float meditationTimeScale;
    private bool meditating = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ToggleMeditation();
        }
    }

    private void ToggleMeditation()
    {
        if (meditating)
        {
            timeScale = 1;            
        }
        else
        {
            timeScale = meditationTimeScale;
        }
        meditating = !meditating;
    }
}
