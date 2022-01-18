using UnityEngine;
using DG.Tweening;

public class Shop : MonoBehaviour, IScreen
{
    public Game game;

    Vector3 origin = Vector3.zero;

    public void Show()
    {
        transform.position = Vector3.zero;
        transform.DOScale(1.1f, 0.7f).From(1.25f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        transform.DOKill();
        transform.position = origin;
        transform.localScale = Vector3.zero;
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
    }
}
