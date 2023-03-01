using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;
    public AudioSource bulletImpact;
    public AudioSource stepSound;
    public AudioSource doorSound;
    public AudioSource retreteSound;
    public AudioSource clickSound;
    // Start is called before the first frame update
    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        } else
        {
            Destroy(gameObject);
        }
    }


}
