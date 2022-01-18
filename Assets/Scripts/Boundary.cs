using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boundary : MonoBehaviour
{
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
}
