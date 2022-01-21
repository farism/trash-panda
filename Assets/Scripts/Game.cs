using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Game : MonoBehaviour
{
    public Texture2D cursor;
    public View view;
    public SpriteRenderer loading;
    public UI ui;
    public SoundManager soundManager;
    public Home home;
    public Shop shop;
    public Jobs jobs;
    public Job job;
    public Status stats { get; private set; } = new Status();
    public Inventory inventory { get; private set; } = new Inventory();
    public bool soundMusic { get; private set; } = true;
    public bool soundEffects { get; private set; } = true;
    public float sleepCooldown = 0;

    // sum of total pollution
    public int pollution
    {
        get
        {
            int pollution = 0;

            for (var i = 0; i < jobs.locations.Length; i++)
            {
                pollution += jobs.locations[i].pollution;
            }

            return pollution;
        }
    }

    // sum of total max pollution
    public int pollutionMax
    {
        get
        {
            int pollutionMax = 0;

            for (var i = 0; i < jobs.locations.Length; i++)
            {
                pollutionMax += jobs.locations[i].pollutionMax;
            }

            return pollutionMax;
        }
    }

    public float pollutionRatio { get => (float)pollution / (pollutionMax * 0.8f); } // lose when we reach 80% ratio

    Vector3 mousePos;
    Vector3 rotation = Vector3.zero;
    IScreen activeScreen;
    bool isLoading = false;

    public void SetView(View v)
    {
        if (isLoading || v == view)
        {
            return;
        }

        isLoading = true;

        loading.DOFade(1, 0.3f).OnComplete(() =>
        {
            if (activeScreen != null)
            {
                activeScreen.Hide();
            }

            view = v;

            SetActiveScreen(v);

            activeScreen.Show();

            ui.ShowView(view);

            loading.DOFade(0, 0.3f).OnComplete(() =>
            {
                isLoading = false;
            });
        });
    }

    public void StartJob(int index)
    {
        job.SetLocation(jobs.locations[index]);
        SetView(View.Job);
    }

    public void ToggleSoundMusic()
    {
        soundMusic = !soundMusic;
        soundManager.SetMusicEnabled(soundMusic);
    }

    public void ToggleSoundEffects()
    {
        soundEffects = !soundEffects;
        soundManager.SetEffectsEnabled(soundEffects);
    }

    void Awake()
    {
        // Load();
        StartCoroutine(Save());
        Cursor.SetCursor(cursor, new Vector2(10, 10), CursorMode.Auto);
    }

    void Start()
    {
        home.Hide();
        shop.Hide();
        jobs.Hide();
        job.Hide();
        SetActiveScreen(view);
        activeScreen.Show();
        ui.ShowView(view);
        StartJob(0);
    }

    void Update()
    {
        stats.Update(view);

        sleepCooldown = Mathf.Max(0, sleepCooldown - Time.deltaTime);
    }

    void SetActiveScreen(View v)
    {
        if (v == View.Home)
        {
            activeScreen = home;
            soundManager.PlayMusic(Music.Home);
        }
        else if (v == View.Shop)
        {
            activeScreen = shop;
            soundManager.PlayMusic(Music.Shop);
        }
        else if (v == View.Jobs)
        {
            activeScreen = jobs;
        }
        else if (v == View.Job)
        {
            soundManager.PlayMusic(Music.Job);
            activeScreen = job;
        }
    }

    void Load()
    {
        var str = PlayerPrefs.GetString("inventory", "");

        if (str != "")
        {
            inventory = JsonUtility.FromJson<Inventory>(str);
        }
    }

    IEnumerator Save()
    {
        yield return new WaitForSeconds(5);

        PlayerPrefs.SetString("inventory", JsonUtility.ToJson(inventory).ToString());
        PlayerPrefs.Save();

        StartCoroutine(Save());
    }
}