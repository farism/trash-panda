using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Job : MonoBehaviour, IScreen
{
    public Game game;
    public GameObject ground;
    public GameObject groundCity;
    public GameObject groundDesert;
    public GameObject groundMeadow;
    public Collider borderLeft;
    public Collider borderRight;
    public Collider borderTop;
    public Collider borderBottom;
    public GameObject redcup;
    public Player player;
    public float spawnRateMin = 0;
    public float spawnRateMax = 1;
    public float cooldown = 5;
    public float cooldownTimer = 0;
    public float cooldownRatio { get => cooldownTimer / 5; }
    public List<Trash> trashPile = new List<Trash>();

    Location location;
    Renderer groundRenderer;
    Material activeMaterial;
    Vector3 origin = Vector3.zero;

    public void Show()
    {
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        transform.DOKill();
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

    public void DestroyTrash(Trash trash)
    {

    }

    void Awake()
    {
        origin = transform.position;
        groundRenderer = ground.GetComponent<Renderer>();
        gameObject.SetActive(false);
    }

    void Start()
    {
        StartCoroutine(SpawnTimer());
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
            offset.y += Time.deltaTime / 10;
            activeMaterial.mainTextureOffset = offset;
        }
    }

    void UpdateTrashPositions()
    {
        foreach (var trash in trashPile)
        {
            var pos = trash.rb.position;
            pos.y -= Time.deltaTime;
            trash.rb.position = pos;
        }
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));

        if (game.view == View.Job)
        {
            Spawn();
        }

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

            var collider = go.GetComponent<Collider>();
            Physics.IgnoreCollision(collider, borderTop);
            Physics.IgnoreCollision(collider, borderBottom);

            var t = go.GetComponent<Trash>();
            t.transform.position = new Vector3(Random.Range(-3, 3), 10, -2);
            t.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

            trashPile.Add(t);
        }
    }
}
