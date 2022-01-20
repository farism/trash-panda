using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Job : MonoBehaviour, IScreen
{
    public Game game;
    public Location location;
    public GameObject ground;
    public GameObject groundCity;
    public GameObject groundDesert;
    public GameObject groundMeadow;
    public GameObject trashContainer;
    public Collider borderLeft;
    public Collider borderRight;
    public Collider borderTop;
    public Collider borderBottom;
    public GameObject redcup;
    public Player player;
    public float groundSpeed = 1;
    public float spawnRateMin = 0;
    public float spawnRateMax = 1;
    public float cooldown = 5;
    public float cooldownTimer = 0;
    public float cooldownRatio { get => cooldownTimer / 5; }
    public List<Trash> trashPile = new List<Trash>();

    Renderer groundRenderer;
    Material activeMaterial;
    Vector3 origin = Vector3.zero;

    public void Show()
    {
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
        StartCoroutine(SpawnTimer());
    }

    public void Hide()
    {
        StopAllCoroutines();
        DestroyTrashBelowThreshold(-10);
        transform.DOKill();
        gameObject.SetActive(false);
        transform.position = origin;
        transform.localScale = Vector3.zero;
    }

    public void SetLocation(Location location)
    {
        this.location = location;
        GameObject go = GroundFromJobType(this.location.type);
        if (go)
        {
            activeMaterial = go.GetComponent<Renderer>().material;
            groundRenderer.material = activeMaterial;
        }
    }

    public void ActivateTool(Tool tool)
    {
        if (cooldownTimer == 0)
        {
            cooldownTimer = cooldown;

            player.SetTool(tool);
        }
    }

    public void DestroyTrash(GameObject gameObject)
    {
        var trash = gameObject.GetComponent<Trash>();

        trashPile.Remove(trash);

        GameObject.Destroy(gameObject, 0.1f);
    }

    void Awake()
    {
        origin = transform.position;
        groundRenderer = ground.GetComponent<Renderer>();
        gameObject.SetActive(false);
    }

    void Start()
    {
    }

    void Update()
    {
        if (game.view == View.Job)
        {
            UpdateCooldown();

            UpdateGroundTexture();
        }
    }

    void FixedUpdate()
    {
        if (game.view == View.Job)
        {
            UpdateTrashPositions();

            DestroyTrashBelowThreshold();
        }
    }

    void UpdateCooldown()
    {
        cooldownTimer = Mathf.Max(0, cooldownTimer - Time.deltaTime);
    }

    void UpdateGroundTexture()
    {
        if (activeMaterial)
        {
            var offset = activeMaterial.mainTextureOffset;
            offset.y += (Time.deltaTime / 10) * groundSpeed; // 10 is height in units of quad
            activeMaterial.mainTextureOffset = offset;
        }
    }

    void UpdateTrashPositions()
    {
        foreach (var trash in trashPile)
        {
            var pos = trash.rb.position;
            pos.y -= Time.deltaTime * groundSpeed;
            trash.rb.position = pos;
        }
    }

    void DestroyTrashBelowThreshold(float threshold = 1)
    {
        trashPile.RemoveAll((t) =>
        {
            var shouldRemove = t.transform.position.z > threshold; // we have dropped below the ground plane

            if (shouldRemove)
            {
                GameObject.Destroy(t.gameObject, 0.5f);
            }

            return shouldRemove;
        });
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));
        Spawn();
        StartCoroutine(SpawnTimer());
    }

    GameObject GroundFromJobType(JobType type)
    {
        switch (type)
        {
            case JobType.City:
                return groundCity;
            case JobType.Desert:
                return groundDesert;
            case JobType.Meadow:
                return groundMeadow;
            default:
                return groundCity;
        }
    }

    void Spawn()
    {
        var count = Random.Range(0, 2);
        for (var i = 0; i < count; i++)
        {
            var go = Instantiate(redcup);
            go.transform.parent = trashContainer.transform;

            var collider = go.GetComponent<Collider>();
            Physics.IgnoreCollision(collider, borderTop);
            Physics.IgnoreCollision(collider, borderBottom);

            var t = go.GetComponent<Trash>();
            t.transform.localPosition = new Vector3(Random.Range(-3, 3), 0, 0);
            t.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

            trashPile.Add(t);
        }
    }
}
