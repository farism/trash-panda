using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    public Game game;
    public Texture2D defaultCursor;
    public Texture2D handCursor;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public VisualElement tooltip;
    public Label tooltipLabel;

    VisualElement root;
    VisualElement dim;
    VisualElement frame;
    VisualElement infoModal;
    UITooltip activeTooltip;
    AudioSource audioSource;
    IScreen activeScreen;
    VisualElement activeShopCategory;
    int activeJobIndex;

    public static void Show(VisualElement ve)
    {
        ve.style.opacity = 1;
        ve.style.display = DisplayStyle.Flex;
    }

    public static void Hide(VisualElement ve)
    {
        ve.style.opacity = 0;
        ve.style.display = DisplayStyle.None;
    }

    public static void Toggle(VisualElement ve)
    {
        if (ve.style.display == DisplayStyle.None)
        {
            Show(ve);
        }
        else
        {
            Hide(ve);
        }
    }

    public void ShowView(View view)
    {
        activeJobIndex = -1;

        if (activeScreen != null)
        {
            activeScreen.Hide();
        }

        if (view == View.Home)
        {
            activeScreen = GetComponent<UIHome>();
        }
        else if (view == View.Shop)
        {
            activeScreen = GetComponent<UIShop>();
        }
        else if (view == View.Jobs)
        {
            activeScreen = GetComponent<UIJobs>();
        }
        else if (view == View.Job)
        {
            activeScreen = GetComponent<UIJob>();
        }

        activeScreen.Show();
    }

    public void ShowInfoModal()
    {
        Show(infoModal);
    }

    public void ShowDim(bool show)
    {
        dim.pickingMode = show ? PickingMode.Position : PickingMode.Ignore;

        if (show)
        {
            dim.AddToClassList("show");
        }
        else
        {
            dim.RemoveFromClassList("show");
        }
    }

    public void SetTooltip(UITooltip tip)
    {
        activeTooltip = tip;
        SetTooltipText(tip.target.tooltip);
        tooltip.AddToClassList("active");
        tooltip.BringToFront();
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        dim = root.Q<VisualElement>("Dim");

        // modals
        infoModal = root.Q<VisualElement>("InfoModal");
        root.Query<Button>(className: "modal-close").ForEach(el =>
        {
            el.clicked += () => Hide(el.parent.parent);
        });

        tooltip = root.Q<VisualElement>("Tooltip");

        tooltipLabel = tooltip.Q<Label>();

        SetupAudioCallbacks();

        SetupTooltips(root);

        StartCoroutine(UpdateTooltip());
    }

    void Update()
    {
    }

    void SetupTooltips(VisualElement ve)
    {
        foreach (var child in ve.Children())
        {
            if (child.tooltip != "")
            {
                child.AddManipulator(new UITooltip(game));
            }

            SetupTooltips(child);
        }
    }

    void SetupAudioCallbacks()
    {
        foreach (var button in root.Query<Button>().ToList())
        {
            button.RegisterCallback<MouseEnterEvent>((ctx) =>
            {
                game.soundManager.PlayEffect(Effect.ButtonHover);
            });

            button.clicked += () =>
            {
                game.soundManager.PlayEffect(Effect.ButtonClick);
            };
        }
    }

    IEnumerator UpdateTooltip()
    {
        yield return new WaitForSeconds(0.5f);

        if (activeTooltip != null)
        {
            SetTooltipText(activeTooltip.target.tooltip);
        }

        StartCoroutine(UpdateTooltip());
    }


    void SetTooltipText(string tooltip)
    {
        tooltipLabel.text = tooltip;
    }
}
