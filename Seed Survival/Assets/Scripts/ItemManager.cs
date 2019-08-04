using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemSelector sItem;
    public AudioClip seedPlant;
    private AudioSource sAudio;
    public Transform treePrefab;
    // Start is called before the first frame update
    void Start()
    {
        sAudio = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && sItem.numSeeds>0)
        {
            PlantSeed();
            
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
        newPos.y = 0;
        Instantiate(treePrefab, newPos, Quaternion.identity);
        //subtract number of seeds
        sItem.Planted();
    }
}
