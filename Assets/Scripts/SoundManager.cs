using UnityEngine;
using DG.Tweening;

public enum Music
{
    Home,
    Shop,
    Job,
    Job2
}

public enum Effect
{
    ButtonHover,
    ButtonClick,
}

public class SoundManager : MonoBehaviour
{
    public AudioClip buttonHover;
    public AudioClip buttonClick;
    public AudioClip home;
    public AudioClip shop;
    public AudioClip job;
    public AudioClip job2;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(Music music)
    {
        AudioClip clip = null;

        if (music == Music.Home)
        {
            clip = home;
        }
        else if (music == Music.Shop)
        {
            clip = shop;
        }

        if (clip != audioSource.clip)
        {
            DOTween
                .To(() => audioSource.volume, (val) => audioSource.volume = val, 0, 1)
                .OnComplete(() =>
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                    DOTween.To(() => audioSource.volume, (val) => audioSource.volume = val, 1, 1);
                });
        }
    }

    public void PlayEffect(Effect effect)
    {
        if (effect == Effect.ButtonHover)
        {
            audioSource.PlayOneShot(buttonHover, 2);
        }
        else if (effect == Effect.ButtonClick)
        {
            audioSource.PlayOneShot(buttonClick, 2);
        }
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
