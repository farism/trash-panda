using UnityEngine;
using UnityEngine.UIElements;

public class UIControls : MonoBehaviour
{
    public Game game;
    public Texture2D musicOn;
    public Texture2D musicOff;
    public Texture2D effectsOn;
    public Texture2D effectsOff;

    VisualElement root;
    Button jamlink;
    Button homeBtn;
    Button shopBtn;
    Button jobsBtn;
    Button optionsBtn;
    Button infoBtn;
    VisualElement options;
    Button soundMusicBtn;
    Button soundEffectsBtn;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // controls
        homeBtn = root.Q<Button>("HomeBtn");
        homeBtn.clicked += () => game.SetView(View.Home);

        shopBtn = root.Q<Button>("ShopBtn");
        shopBtn.clicked += () => game.SetView(View.Shop);

        jobsBtn = root.Q<Button>("JobsBtn");
        jobsBtn.clicked += () => game.SetView(View.Jobs);

        optionsBtn = root.Q<Button>("OptionsBtn");
        optionsBtn.clicked += () => UI.Toggle(options);

        options = root.Q<VisualElement>("Options");
        options.style.display = DisplayStyle.None;

        soundMusicBtn = root.Q<Button>("SoundMusicBtn");
        soundMusicBtn.clicked += () =>
        {
            game.ToggleSoundMusic();
            soundMusicBtn.style.backgroundImage = game.soundMusic ? musicOn : musicOff;
        };

        soundEffectsBtn = root.Q<Button>("SoundEffectsBtn");
        soundEffectsBtn.clicked += () =>
        {
            game.ToggleSoundEffects();
            soundEffectsBtn.style.backgroundImage = game.soundEffects ? effectsOn : effectsOff;
        };

        infoBtn = root.Q<Button>("InfoBtn");
        infoBtn.clicked += () => game.ui.ShowInfoModal();

        jamlink = root.Q<Button>("JamLink");
        jamlink.clicked += () => Application.OpenURL("https://itch.io/jam/virtual-pet-jam");
    }
}
