using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jobs : MonoBehaviour, IScreen
{
  public Game game;
  public GameObject globe;
  public Location[] locations;
  public List<int> activeLocations = new List<int>();

  Vector3 origin = Vector3.zero;

  public void Remove(Location location)
  {
    activeLocations.Remove(location.index);
  }

  public void Show()
  {
    transform.position = Vector3.zero;
    globe.transform.position = Vector3.zero;
    globe.transform.DOScale(3.5f, 0.7f).SetEase(Ease.OutBack);
    globe.transform.DORotate(new Vector3(0, -90, 0), 0.7f).SetEase(Ease.OutBack);
  }

  public void Hide()
  {
    transform.position = origin;
    globe.transform.DOKill();
    globe.transform.position = Vector3.zero;
    globe.transform.localScale = Vector3.zero;
    globe.transform.rotation = Quaternion.identity;
  }

  public bool IsActive(Location location)
  {
    return activeLocations.Contains(location.index);
  }

  void Awake()
  {
    origin = transform.position;
    locations = GameObject.FindObjectsOfType<Location>();
    for (var i = 0; i < locations.Length; i++)
    {
      locations[i].index = i;
      locations[i].GetComponent<MeshRenderer>().enabled = false;
    }
  }

  void Update()
  {
    if (game.view != View.Jobs)
    {
      return;
    }

    UpdateGlobeTransform();

    UpdateActiveLocations();
  }

  void UpdateActiveLocations()
  {
    var count = 0;

    while (activeLocations.Count < 5 && count <= locations.Length)
    {
      count++;

      var index = Random.Range(0, locations.Length);

      var location = locations[index];

      if (!activeLocations.Contains(index) && location.pollutionRatio != 0)
      {
        activeLocations.Add(index);
      }
    }
  }

  void UpdateGlobeTransform()
  {
    if (Input.GetMouseButton(0))
    {
      float rotX = Input.GetAxis("Mouse X") * 10;
      float rotY = Input.GetAxis("Mouse Y") * 10;
      globe.transform.rotation = Quaternion.Euler(rotY, -rotX, 0) * globe.transform.rotation;
    }

    globe.transform.localScale += new Vector3(1, 1, 1) * Input.mouseScrollDelta.y * Time.deltaTime * 30;
    globe.transform.localScale = Vector3.Max(Vector3.one, globe.transform.localScale);
  }
}
