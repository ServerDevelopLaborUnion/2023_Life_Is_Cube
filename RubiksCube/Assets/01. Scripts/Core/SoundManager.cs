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
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Multiple SoundManager Running");

            Instance = this;

            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void SetMasterVolume()
    {
        _audioMixer.SetFloat("Master", masterSlider.value);
        Debug.Log(1);
    }

    public void SetBGMVolume()
    {
        _audioMixer.SetFloat("BGM", bgmSlider.value);
    }
    
}
