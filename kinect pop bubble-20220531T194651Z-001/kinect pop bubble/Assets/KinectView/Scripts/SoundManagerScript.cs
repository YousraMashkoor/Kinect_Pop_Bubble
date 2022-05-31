using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip bubblePopSoundVar;
    static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        bubblePopSoundVar = Resources.Load<AudioClip>("popSound");
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        /*switch (clip)
        {
            case "popSound":
                audioSrc.PlayOneShot(bubblePopSoundVar);
                break;
        } */

        if (clip == "popSound")
        {
            audioSrc.PlayOneShot(bubblePopSoundVar);
        } 
    }
}
