using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationManager : MonoBehaviour
{
    public float timeScale = 1;
    public float meditationTimeScale;
    private bool meditating = false;
    public GameObject body;
    public Camera meditationCamera;
    float camNormal, camLow;
    public AudioManager mAudio;
    // Start is called before the first frame update
    void Start()
    {
        camNormal = transform.localPosition.y;
        camLow = camNormal - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && timeScale!=0)
        {
            ToggleMeditation();
        }
    }

    private void ToggleMeditation()
    {
        if (meditating)
        {
            Debug.Log("stopping meditation");
            timeScale = 1;
            StopAllCoroutines();
            StartCoroutine(StopMeditation());
        }
        else
        {
            Debug.Log("starting meditation");
            timeScale = meditationTimeScale;
            transform.GetComponent<DirectionalMovement>().enabled = false;
            mAudio.SetNone();
            StopAllCoroutines();
            StartCoroutine(StartMeditation());
        }
        meditating = !meditating;
    }

    private IEnumerator StopMeditation()
    {
        float timer = 0;
        Vector3 start = transform.localPosition;
        start.y = camLow;
        Vector3 end = start;
        end.y = camNormal;
        while (timer < 1f)
        {
            transform.localPosition = Vector3.Lerp(start, end, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.GetComponent<DirectionalMovement>().enabled = true;
    }

    private IEnumerator StartMeditation()
    {
        float timer = 0;
        Vector3 start = transform.localPosition;
        start.y = camNormal;
        Vector3 end = start;
        end.y = camLow;
        while (timer < 1f)
        {
            transform.localPosition = Vector3.Lerp(start, end, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
