using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DisposalArea : MonoBehaviour
{
    public Job job;
    public Player player;

    SpriteRenderer spriteRenderer;
    BoxCollider boxCollider;
    float width = 0.5f;
    float height = 5f;
    float positionY = 0;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Start()
    {
        StartCoroutine(Resize());
    }

    void Update()
    {
        UpdateSize();
    }

    void UpdateSize()
    {
        spriteRenderer.size = new Vector2(width, height);
        boxCollider.size = new Vector3(width, height, 5);
        transform.position = new Vector3(transform.position.x, positionY, transform.position.z);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Trash")
        {
            var trash = collider.GetComponent<Trash>();

            collider.gameObject.transform
                .DOScale(0, 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete(() => job.DisposeTrash(trash));
        }
        else if (collider.gameObject.tag == "PlayerWalking")
        {
            player.DropTrash(transform.position);
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
        player.ThrowTrash(transform.position);
    }

    IEnumerator Resize()
    {
        yield return new WaitForSeconds(Random.Range(10, 20));

        DOTween.To(() => width, (val) => width = val, Random.Range(0.4f, 1f), 0.3f);
        DOTween.To(() => height, (val) => height = val, Random.Range(1f, 6f), 0.3f);
        DOTween.To(() => positionY, (val) => positionY = val, Random.Range(-2.5f, 2.5f), 0.3f);

        StartCoroutine(Resize());
    }
}
