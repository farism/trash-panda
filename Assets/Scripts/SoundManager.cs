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

  AudioSource musicSource;
  AudioSource effectsSource;

  void Awake()
  {
    var audios = GetComponents<AudioSource>();
    musicSource = audios[0];
    effectsSource = audios[1];
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
    else if (music == Music.Job)
    {
      clip = job;
    }

    if (clip != musicSource.clip)
    {
      DOTween
      .To(() => musicSource.volume, (val) => musicSource.volume = val, 0, 1)
      .OnComplete(() =>
      {
        musicSource.clip = clip;
        musicSource.Play();
        DOTween.To(() => musicSource.volume, (val) => musicSource.volume = val, 0.5f, 1);
      });

    }
  }

  public void PlayEffect(Effect effect)
  {
    if (effect == Effect.ButtonHover)
    {
      effectsSource.PlayOneShot(buttonHover, 2);
    }
    else if (effect == Effect.ButtonClick)
    {
      effectsSource.PlayOneShot(buttonClick, 2);
    }
  }

  public void SetMusicEnabled(bool enabled)
  {
    musicSource.enabled = enabled;
  }

  public void SetEffectsEnabled(bool enabled)
  {
    effectsSource.enabled = enabled;
  }

  void Start()
  {
  }

  void Update()
  {
  }
}
