using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour
{
    public float spinSpeed;
    private MeditationManager mMan;
    public int seasonCode;
    public int dayCode;
    public Text seasonText;
    public bool isSunny = true;
    public ParticleSystem stars;
    // Start is called before the first frame update
    void Start()
    {
        mMan = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<MeditationManager>();
        StartCoroutine(TrackSeasons());
        seasonText.text = "Season/Day: " + seasonCode + "/" + dayCode;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spinSpeed * Time.deltaTime*mMan.timeScale, 0, 0);
    }

    private IEnumerator TrackSeasons()
    {
        float dayTime = 360 / spinSpeed;
        float nightTime = dayTime * .42f;
        while (true)
        {
            float dayTimer = 0;
            while (dayTimer < dayTime)
            {
                dayTimer += Time.deltaTime * mMan.timeScale;
                yield return null;
                if (isSunny)
                {
                    if (dayTimer > nightTime)
                    {
                        isSunny = false;
                        stars.Play();
                    }
                }
            }

            dayCode++;
            isSunny = true;
            stars.Stop();
            if(dayCode > 2)
            {
                dayCode = 0;
                seasonCode++;

                if (seasonCode > 3)
                {
                    seasonCode = 0;
                }
                GameObject [] leaves = GameObject.FindGameObjectsWithTag("Leaf");
                foreach(GameObject g in leaves)
                {
                    g.GetComponent<Leaf>().SetSeason();
                }
            }
            seasonText.text = "Season/Day: " + seasonCode+"/"+dayCode;
        }
    }
}
