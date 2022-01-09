using UnityEngine;
using DG.Tweening;

public enum View
{
    Home,
    Shop,
    Jobs,
}

public class Game : MonoBehaviour
{
    public View view { get; private set; }
    public int currency { get; private set; } = 0;
    public float energy { get; private set; } = 25;
    public float hunger { get; private set; } = 50;
    public float affection { get; private set; } = 75;
    public bool toolSpike { get; private set; } = true;
    public bool toolGrabber { get; private set; } = false;
    public bool toolLeafblower { get; private set; } = false;
    public bool toolBulldozer { get; private set; } = false;
    public int toolLeafblowerEnergy { get; private set; } = 0;
    public int toolBulldozerEnergy { get; private set; } = 0;
    public bool sound { get; private set; }
    public SpriteRenderer raccoon;
    public GameObject globe;
    public SpriteRenderer shop;
    public UI ui;

    public void SetView(View v)
    {
        view = v;

        HideAll();

        if (view == View.Home)
        {
            raccoon.transform.DOScale(0.5f, 0.3f);
        }
        else if (view == View.Shop)
        {
            shop.DOFade(1, 0.3f);
        }
        else if (view == View.Jobs)
        {
            globe.transform.DOScale(5, 0.3f);
        }
    }

    public void ToggleSound()
    {
        sound = !sound;
        AudioListener.volume = sound ? 1 : 0;
    }

    void Awake()
    {
        HideAll(0);
        SetView(View.Home);
        globe.transform.Rotate(new Vector3(0, 0, 10));
    }

    void Update()
    {
        energy -= Time.deltaTime;
        hunger -= Time.deltaTime;
        affection -= Time.deltaTime;
        globe.transform.Rotate(new Vector3(0, 0.1f, 0));
    }

    void HideAll(float duration = 0.3f)
    {
        raccoon.transform.DOScale(0, duration);
        globe.transform.DOScale(0, duration);
        shop.DOFade(0, duration);
    }
}
