using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public Transform appleIcon, seedIcon;
    public Text appleCount, seedCount;
    public float transitionTime;
    public Text selectedState;
    public bool seedSelected = true;
    public int numSeeds = 1;
    public int numFruit;
    // Start is called before the first frame update
    void Start()
    {
        if(numSeeds>0)
            selectedState.text = "Selected: Seed";
        seedCount.text = "x" + numSeeds;
        appleCount.text = "x" + numFruit;
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
        if (numSeeds < 1 && seedSelected)
        {
            selectedState.text = "Selected: None";
        }
        seedCount.text = "x"+numSeeds;
    }

    public void Eaten()
    {
        numFruit--;
        if (Random.value < 0.25f)
        {
            numSeeds++;
        }
        if (numFruit < 1 && !seedSelected)
        {
            selectedState.text = "Selected: None";
        }
        else if (numSeeds >= 1 && seedSelected)
        {
            selectedState.text = "Selected: Seed";
        }
        seedCount.text = "x" + numSeeds;
        appleCount.text = "x" + numFruit;
    }

    public void Collect()
    {
        Debug.Log("Received apple");
        numFruit++;
        appleCount.text = "x" + numFruit;
    }
}
