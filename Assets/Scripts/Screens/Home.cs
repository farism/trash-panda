using UnityEngine;
using DG.Tweening;

public class Home : MonoBehaviour, IScreen
{
    public Game game;
    public GameObject raccoonFront;
    public GameObject raccoonSide;
    public GameObject background;
    public SpriteRenderer backgroundImage;
    public Bed smallbed;
    public Bed beanbag;
    public Bed futon;
    public Bed bigbed;

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
        game.inventory.ConsumeFood(food);
        game.stats.AddEnergy(food.energy);
        game.stats.AddHunger(food.hunger);
        game.stats.AddLove(food.love);
    }

    public void Play(ToyScriptableObject toy)
    {
        game.stats.AddEnergy(toy.energy);
        game.stats.AddHunger(toy.hunger);
        game.stats.AddLove(toy.love);
    }

    public void UpdateFurniture()
    {
        bigbed.gameObject.SetActive(false);
        futon.gameObject.SetActive(false);
        beanbag.gameObject.SetActive(false);
        smallbed.gameObject.SetActive(false);

        if (game.inventory.furnitureBigBed)
        {
            bigbed.gameObject.SetActive(true);
        }
        else if (game.inventory.furnitureFuton)
        {
            futon.gameObject.SetActive(true);
        }
        else if (game.inventory.furnitureBeanbag)
        {
            beanbag.gameObject.SetActive(true);
        }
        else if (game.inventory.furnitureSmallBed)
        {
            smallbed.gameObject.SetActive(true);
        }
    }

    void Awake()
    {
        origin = transform.position;
    }

    void Start()
    {
        UpdateFurniture();
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
