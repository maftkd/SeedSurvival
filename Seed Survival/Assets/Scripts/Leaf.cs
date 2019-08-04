using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public Color springColor;
    public Color fallColor;
    public Color winterColor;
    public DayCycle day;
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        day = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayCycle>();
        //randomize our colors slightly
        springColor.r += Random.Range(0, .02f);
        springColor.g += Random.Range(-.02f, .02f);
        springColor.b += Random.Range(0, .02f);
        //fall color
        fallColor.r += Random.Range(-.02f, .02f);
        fallColor.g += Random.Range(-.02f, .02f);
        fallColor.b += Random.Range(0, .02f);
        //winter color
        winterColor.b += Random.Range(0, .02f);
        winterColor.g += Random.Range(0, .02f);
        mat = transform.GetComponent<MeshRenderer>().sharedMaterial;
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSeason()
    {
        StartCoroutine(FadeIn());
    }

    private Color GetColorBasedOnSeason()
    {
        Color c;
        switch (day.seasonCode)
        {
            case 0:
            case 1:
                c = springColor;
                break;
            case 2:
                c = fallColor;
                break;
            case 3:
                c = winterColor;
                break;
            default:
                c = Color.green;
                break;
        }
        return c;
    }
    private IEnumerator FadeIn()
    {
        Color startColor = mat.GetColor("_Color");
        Color endColor = GetColorBasedOnSeason();
        endColor.a = 1;
        
        //Color startColor = new Color(spring)
        float timer = 0;
        while(timer < 3)
        {
            timer += Time.deltaTime;
            mat.SetColor("_Color", Color.Lerp(startColor, endColor, timer / 3));
            yield return null;
        }
    }
}
