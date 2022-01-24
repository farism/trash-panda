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
  public Player player;
  public TrashPile trashPile;
  public float groundSpeed = 1;
  public float cooldown = 5;
  public float cooldownTimer = 0;
  public float cooldownRatio { get => cooldownTimer / 5; }

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
    trashPile.ClearAll();
    StopAllCoroutines();
    transform.DOKill();
    gameObject.SetActive(false);
    transform.position = origin;
    transform.localScale = Vector3.zero;
  }

  public void SetLocation(Location loc)
  {
    if (location != loc)
    {
      player.Reset();
    }

    location = loc;
    GameObject go = GroundFromJobType(location.type);
    if (go)
    {
      activeMaterial = go.GetComponent<Renderer>().material;
      groundRenderer.material = activeMaterial;
    }
  }

  public void ActivateTool(Tool tool)
  {
    if (
      tool == Tool.Hand && !game.inventory.toolHand ||
      tool == Tool.Spike && !game.inventory.toolSpike ||
      tool == Tool.Grabber && !game.inventory.toolGrabber ||
      tool == Tool.LeafBlower && !game.inventory.toolLeafBlower ||
      tool == Tool.Bulldozer && !game.inventory.toolBulldozer)
    {
      return;
    }

    if (cooldownTimer == 0)
    {
      cooldownTimer = cooldown;

      player.SetTool(tool);
    }
  }

  public void DisposeTrash(Trash trash)
  {
    trashPile.ClearTrash(trash);
    location.RemovePollution(1);
    game.inventory.AddCurrency(1);
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

}
