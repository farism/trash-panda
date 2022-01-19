using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boundary : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            collision.gameObject.transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
        }
    }

    void OnMouseEnter()
    {
        spriteRenderer.DOColor(Color.yellow, 0.3f);
    }

    void OnMouseExit()
    {
        spriteRenderer.DOColor(Color.green, 0.3f);
    }
}
