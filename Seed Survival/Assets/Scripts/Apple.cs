using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private MeditationManager mMan;
    public float growTime;
    public float growRate;
    public float decayTime;
    public Color decayColor;
    public float collectTime;
    private Transform appleParent;
    bool decay = false;
    bool collected = false;
    private Material myMat;
    // Start is called before the first frame update
    void Start()
    {
        mMan = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<MeditationManager>();
        appleParent = GameObject.FindGameObjectWithTag("AppleParent").transform;
        myMat = transform.GetComponent<MeshRenderer>().material;
        StartCoroutine(Grow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Grow()
    {
        float timer = 0;
        while (timer < growTime) {
            float delta = Time.deltaTime * mMan.timeScale;
            timer += Time.deltaTime * mMan.timeScale;
            yield return null;
            transform.localScale += Vector3.one * growRate * delta;
        }
        transform.SetParent(appleParent);
        StartCoroutine(Fall());
    }

    private IEnumerator Fall()
    {
        transform.GetComponent<Rigidbody>().useGravity = true;
        while (!decay)
        {
            yield return true;
        }
        StartCoroutine(Decay());
    }

    private IEnumerator Decay()
    {
        float timer = 0;
        Color start = myMat.color;
        Color end = decayColor;
        while (timer < decayTime)
        {
            timer += Time.deltaTime * mMan.timeScale;
            myMat.SetColor("_Color", Color.Lerp(start, end, timer / decayTime));

            yield return null;
        }
        Destroy(transform.gameObject);
    }

    private IEnumerator Collect()
    {
        collected = true;
        float timer = 0;

        transform.GetComponent<Rigidbody>().useGravity = false;
        while (timer < collectTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.position, timer / collectTime);
            yield return null;
        }
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemSelector>().Collect();
        Destroy(transform.gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit ground");
        decay = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
            StartCoroutine(Collect());
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !collected)
            StartCoroutine(Collect());
    }
}
