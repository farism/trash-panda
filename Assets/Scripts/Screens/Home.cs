using UnityEngine;
using DG.Tweening;

public class Home : MonoBehaviour, IScreen
{
    public Game game;
    public GameObject raccoonFront;
    public GameObject raccoonSide;
    public GameObject background;
    public SpriteRenderer backgroundImage;

    Vector3 origin = Vector3.zero;
    float edge = 0;
    float backgroundX;
    float scaleX;

    public void Show()
    {
        transform.DOKill();
        transform.position = Vector3.zero;
        transform.DOScale(1.1f, 0.7f).From(1.25f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        transform.DOKill();
        transform.position = origin;
        transform.localScale = Vector3.zero;
        background.transform.position = Vector3.zero;
        backgroundX = 0;
    }

    public void Feed(FoodScriptableObject food)
    {
        Debug.Log(food);
    }

    public void Play(ToyScriptableObject toy)
    {
        Debug.Log(toy);
    }

    public void Sleep(Furniture furniture)
    {
        Debug.Log(furniture);
    }

    void Awake()
    {
        origin = transform.position;
    }

    void Start()
    {
    }

    void Update()
    {
        if (game.view != View.Home)
        {
            return;
        }

        UpdateRaccoon();

        UpdateBackground();
    }

    void UpdateRaccoon()
    {
        var delta = background.transform.position.x - backgroundX;

        if (Mathf.Abs(delta) > 0.1)
        {
            raccoonFront.SetActive(false);
            raccoonSide.SetActive(true);
            var direction = Mathf.Sign(backgroundX) * -1;
            var sx = raccoonSide.transform.localScale.x;
            var sy = raccoonSide.transform.localScale.y;
            var sz = raccoonSide.transform.localScale.z;
            var scale = new Vector3(Mathf.Abs(sx) * direction, sy, sz);
            raccoonSide.transform.localScale = scale;
        }
        else
        {
            raccoonFront.SetActive(true);
            raccoonSide.SetActive(false);
        }
    }

    void UpdateBackground()
    {
        var halfwidth = (backgroundImage.bounds.extents.x * 2 - 7.2f) / 2;

        var p = background.transform.position;

        if (Input.mousePosition.x < 50)
        {
            edge = halfwidth;

            backgroundX = edge;

            p.x = Mathf.Lerp(p.x, backgroundX, Time.deltaTime * 2);
        }
        else if (Input.mousePosition.x > Screen.width - 50)
        {
            edge = -halfwidth;

            backgroundX = edge;

            p.x = Mathf.Lerp(p.x, backgroundX, Time.deltaTime * 2);
        }
        else
        {
            if (edge != 0)
            {
                backgroundX = p.x + (backgroundX - p.x) / 5;
            }

            p.x = Mathf.Lerp(p.x, backgroundX, Time.deltaTime * 5);

            edge = 0;
        }

        background.transform.position = p;
    }
}
