using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    private AudioSource audioSource;

    [SerializeField] private AudioClip testClip1;
    [SerializeField] private AudioClip testClip2;
    private AudioMixer _audioMixer;

    Slider masterSlider;
    Slider bgmSlider;
    Slider sfxSlider;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple SoundManager Running");
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        _audioMixer = GetComponent<AudioMixer>();
    }

    public void PlayTestClip1()
    {
        audioSource.PlayOneShot(testClip1);
    }

    public void PlayTestClip2()
    {
        audioSource.PlayOneShot(testClip2);
    }

    public void SetMasterVolume()
    {
        _audioMixer.SetFloat("Master", masterSlider.value);
    }

    public void SetBGMVolume()
    {
        _audioMixer.SetFloat("BGM", bgmSlider.value);
    }

    public void SetSFXVolume()
    {
        _audioMixer.SetFloat("SFX", sfxSlider.value);
    }
}
