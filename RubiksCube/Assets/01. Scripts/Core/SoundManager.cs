using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    private AudioSource audioSource;

    [SerializeField] private AudioClip testClip1;
    [SerializeField] private AudioClip testClip2;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple SoundManager Running");
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayTestClip1()
    {
        audioSource.PlayOneShot(testClip1);
    }

    public void PlayTestClip2()
    {
        audioSource.PlayOneShot(testClip2);
    }

}
