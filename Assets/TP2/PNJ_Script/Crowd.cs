using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour
{
    public int id = 0;
    public AudioClip clip;


    private float delaiMax = 10.25f, delaiLive = 0;
    private Animator animator;
    private bool wasClapping = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if(id == 0)
            wasClapping = true;
    }

    void Update()
    {
        delaiLive += Time.deltaTime;
        if(delaiLive >= delaiMax)
        {
            delaiLive = 0;

            if(clip != null)
            {
                GetComponent<AudioSource>().PlayOneShot(clip);
            }

            if (wasClapping)
            {
                animator.SetBool("Cheer", true);
                animator.SetBool("Clap", false);
                wasClapping = false;
            }
            else
            {
                animator.SetBool("Clap", true);
                animator.SetBool("Cheer", false);
                wasClapping = true;
            }
        }
    }
}
