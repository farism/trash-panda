using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Trash : MonoBehaviour
{
    new public Collider collider;
    public Rigidbody rb;
    Outline outline;

    public void SetHighlight(bool highlighted)
    {
        outline.OutlineColor = highlighted ? Color.green : Color.clear;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        outline = GetComponent<Outline>();
    }

    void Update()
    {
    }
}
