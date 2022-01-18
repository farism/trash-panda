using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Job : MonoBehaviour, IScreen
{
    public Game game;
    public GameObject groundCity;
    public GameObject groundDesert;
    public GameObject groundMeadow;
    public Rigidbody player;

    Location location;
    GameObject activeGround;
    Material activeMaterial;
    Vector3 origin = Vector3.zero;
    Vector3 playerPos = Vector3.zero;
    List<Rigidbody> trash = new List<Rigidbody>();
    float nextSpawnTime;

    public void Show()
    {
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one;
        gameObject.SetActive(true);

        foreach (var body in GetComponentsInChildren<Rigidbody>())
        {
            if (body.tag == "Trash")
            {
                trash.Add(body);
            }
        }
    }

    public void Hide()
    {
        transform.DOKill();
        transform.position = origin;
        transform.localScale = Vector3.zero;
    }

    public void SetLocation(Location location)
    {
        groundCity.SetActive(false);
        groundDesert.SetActive(false);
        groundMeadow.SetActive(false);

        if (location.type == JobType.City)
        {
            activeGround = groundCity;
        }
        else if (location.type == JobType.Desert)
        {
            activeGround = groundDesert;
        }
        else if (location.type == JobType.Meadow)
        {
            activeGround = groundMeadow;
        }

        activeGround.SetActive(true);
        activeMaterial = activeGround.GetComponent<Renderer>().material;

        this.location = location;
    }

    void Awake()
    {
        origin = transform.position;
        gameObject.SetActive(false);
    }

    void Start()
    {
    }

    void Update()
    {
        if (game.view == View.Job)
        {
            UpdateGroundTexture();

            UpdateTrashList();
        }
    }

    void FixedUpdate()
    {
        UpdatePlayerPosition();

        UpdateTrashPositions();
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

    void UpdateTrashList()
    {
        nextSpawnTime = Mathf.Max(0, nextSpawnTime - Time.deltaTime);

        if (nextSpawnTime == 0)
        {
            // var go = Instantiate(trashPrefab1);

            // trash.Add(go.GetComponent<Rigidbody>());

            // nextSpawnTime = Random.Range(1, 3);
        }
    }

    void UpdatePlayerPosition()
    {
        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        playerPos = Vector3.Lerp(playerPos, new Vector3(pos.x, pos.y, 0), Time.deltaTime);

        playerPos.x = Mathf.Max(-3.6f, Mathf.Min(3.6f, playerPos.x));

        playerPos.y = Mathf.Max(-5f, Mathf.Min(5f, playerPos.y));

        player.MovePosition(playerPos);

        player.MoveRotation(Quaternion.LookRotation((playerPos - player.transform.position).normalized, Vector3.back));
    }

    void UpdateTrashPositions()
    {
        foreach (var t in trash)
        {
            // var av = t.angularVelocity;
            // av.x = 0;
            // av.y = 0;

            // t.angularVelocity = av;

            // var rot = t.gameObject.transform.rotation;
            // rot.x = 0;
            // rot.y = 0;

            // t.gameObject.transform.rotation = rot;

            var pos = t.position;
            pos.y -= Time.deltaTime;
            t.position = pos;
        }
    }
}
