using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DisposalArea : MonoBehaviour
{
    Job job;
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Trash")
        {
            collider.gameObject.transform
                .DOScale(0, 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete(() => job.DestroyTrash(collider.gameObject));
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

    void OnMouseDown()
    {
        Debug.Log("here");
    }
}
