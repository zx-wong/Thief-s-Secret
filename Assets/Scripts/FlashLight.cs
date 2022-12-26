using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlashLight : MonoBehaviour
{

    private bool FlashLightEnabled;
    public GameObject flashLight;
    public GameObject lightObj;

    public AudioClip fsound;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {


        //equip flashlight
        if (Input.GetKeyDown(KeyCode.F))
        {
            audio.clip = fsound;
            audio.Play();

            FlashLightEnabled = !FlashLightEnabled;
        }

        if (FlashLightEnabled)
        {
            flashLight.SetActive(true);
        }
        else
        {
            flashLight.SetActive(false);
        }

    }
}
