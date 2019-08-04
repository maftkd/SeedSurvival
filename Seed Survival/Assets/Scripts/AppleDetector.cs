using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Apple")
            other.transform.GetComponent<Apple>().StopAllCoroutines();
            StartCoroutine(other.transform.GetComponent<Apple>().Collect());
    }
}
