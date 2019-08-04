using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public Transform appleIcon, seedIcon;
    public float transitionTime;
    public Text selectedState;
    private bool seedSelected = true;
    public int numSeeds = 1;
    public int numFruit;
    // Start is called before the first frame update
    void Start()
    {
        selectedState.text = "Selected: Seed";
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            StopAllCoroutines();
            StartCoroutine(ToggleSeed());
        }
        
    }

    private IEnumerator ToggleSeed()
    {
        Vector3 targetPos;
        Vector3 startPos;
        if (seedSelected)
        {
            targetPos = appleIcon.position;
            startPos = seedIcon.position;
            seedSelected = false;
            if (numFruit > 0)
            {
                //put fruit in hand
                selectedState.text = "Selected: Fruit";
            }
            else
            {
                //put nothing in hand
                selectedState.text = "Selected: None";
            }
        }
        else
        {
            targetPos = seedIcon.position;
            startPos = appleIcon.position;
            seedSelected = true;
            if (numSeeds > 0)
            {
                //put seed in hand
                selectedState.text = "Selected: Seed";
            }
            else
            {
                //put nothing in hand
                selectedState.text = "Selected: None";
            }
        }
        float timer = 0;
        while (timer < transitionTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timer / transitionTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void Planted()
    {
        numSeeds--;
        if (numSeeds < 1)
        {
            selectedState.text = "Selected: None";
        }
    }
}
