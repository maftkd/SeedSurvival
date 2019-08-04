using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public enum WalkState {NONE = 0, STEALTH = 1, SLOW = 2, NORMAL = 3, FAST = 4, KICK = 5};
    public WalkState walkState = WalkState.NONE;    

    public float pingPeriod;

    public Animator animator;

    public AudioSource[] voices;

    public AudioClip[] clips;
    public AudioClip[] bridgeClips;
    public bool onBridge;

    private float stepPeriod;
    private float walkTimer = 0;
    private int curVoice = 0;

    [Header("Debug components")]
    public Text stateText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (walkState != WalkState.NONE)
        {
            walkTimer += Time.deltaTime;
            if (walkTimer >= stepPeriod)
            {
                //queue audio on appropriate source
                if (onBridge)
                {
                    voices[curVoice].clip = bridgeClips[Random.Range(0, bridgeClips.Length)];
                }
                else
                {
                    voices[curVoice].clip = clips[Random.Range(0, clips.Length)];
                }
                voices[curVoice].Play();

                //inc voice
                curVoice++;
                if(curVoice >= voices.Length)
                {
                    curVoice = 0;
                }
                walkTimer = 0;
                //reset timer
            }
        }
    }

    public void SetStealth(){
        walkState=WalkState.STEALTH;    
        stateText.text="M:"+walkState.ToString(); 
        //temp code
        animator.SetBool("isWalking",false);
    }

    public void SetNone(){
        walkState=WalkState.NONE;
        stateText.text="M:"+walkState.ToString();
        animator.SetBool("isWalking",false);
    }
    
    public void SetWalk(float speedMod){
        stepPeriod = 0.45f / speedMod;
        if(walkState== WalkState.NONE)
            walkTimer = stepPeriod;
        animator.SetBool("isWalking",true);
        animator.SetFloat("walkSpeed",speedMod);
        if(speedMod==1){
            walkState=WalkState.NORMAL;
            stateText.text="M:"+walkState.ToString();
        }
        else if(speedMod<1){
            walkState=WalkState.SLOW;
            stateText.text="M:"+walkState.ToString();
        }
        else if(speedMod>1){
            walkState=WalkState.FAST;
            stateText.text="M:"+walkState.ToString();
        }
    }  
}