using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemSelector sItem;
    public AudioClip seedPlant,fruitEat;
    private AudioSource sAudio;
    public Transform treePrefab;
    private DirectionalMovement mDir;
    // Start is called before the first frame update
    void Start()
    {
        sAudio = transform.GetComponent<AudioSource>();
        mDir = transform.GetComponent<DirectionalMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (sItem.numSeeds > 0 && sItem.seedSelected)
                PlantSeed();
            else if (sItem.numFruit > 0 && !sItem.seedSelected)
                EatFruit();
        }
    }

    private void PlantSeed()
    {
        //play seed drop audio
        sAudio.clip = seedPlant;
        sAudio.Play();
        //instantiate tree in front of player
        Vector3 flatForward = Camera.main.transform.forward;
        flatForward.y = 0;
        Vector3 newPos = transform.position + flatForward.normalized;
        newPos.y -= transform.localPosition.y; //this should be based on terrain height
        Instantiate(treePrefab, newPos, Quaternion.identity);
        //subtract number of seeds
        sItem.Planted();
    }

    private void EatFruit()
    {
        sAudio.clip = fruitEat;
        sAudio.Play();
        sItem.Eaten();
        mDir.EatFruit();
    }
}
