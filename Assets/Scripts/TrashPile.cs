using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPile : MonoBehaviour, IScreen
{
  public Job job;
  public float threshold = 0;
  public float groundSpeed = 0;
  public float spawnRateMin = 0;
  public float spawnRateMax = 1;
  public List<GameObject> prefabs = new List<GameObject>();

  List<Trash> items = new List<Trash>();

  public void ClearTrash(Trash trash)
  {
    items.Remove(trash);
    GameObject.Destroy(trash.gameObject, 0.1f); ;
  }

  public void ClearAll()
  {
    ClearBelowThreshold(-100);
  }

  public void ClearBelowThreshold(float threshold)
  {
    items.RemoveAll((t) =>
    {
      var shouldRemove = t.transform.position.z > threshold; // we have dropped below the ground plane

      if (shouldRemove)
      {
        GameObject.Destroy(t.gameObject, 0.5f);
      }

      return shouldRemove;
    });
  }

  public List<Trash> FindWithinRange(Vector3 position)
  {
    return items.FindAll((t) =>
    {
      return Vector2.Distance(t.transform.position, position) < 1f;
    });
  }

  public void UpdatePositions()
  {
  }

  void Start()
  {
    Spawn();
    StartCoroutine(SpawnTimer());
  }

  void OnEnable()
  {
    StartCoroutine(SpawnTimer());
  }

  void OnDisable()
  {
    StopAllCoroutines();
  }

  void FixedUpdate()
  {
    if (job.game.view == View.Job)
    {
      UpdateTrashPositions();

      ClearBelowThreshold(threshold);
    }
  }

  void UpdateTrashPositions()
  {
    foreach (var trash in items)
    {
      var pos = trash.rb.position;
      pos.y -= Time.deltaTime * groundSpeed;
      trash.rb.position = pos;
    }
  }

  IEnumerator SpawnTimer()
  {
    yield return new WaitForSeconds(Random.Range(spawnRateMin, spawnRateMax));

    Spawn();

    StartCoroutine(SpawnTimer());
  }

  void Spawn()
  {
    if (job.location.pollution == 0)
    {
      return;
    }

    var count = Random.Range(1, job.location.difficulty == JobDifficulty.Easy ? 1 : 5);

    for (var i = 0; i < count; i++)
    {
      var go = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
      go.transform.parent = transform;

      var collider = go.GetComponent<Collider>();
      Physics.IgnoreCollision(collider, job.borderTop);
      Physics.IgnoreCollision(collider, job.borderBottom);

      var t = go.GetComponent<Trash>();
      t.transform.localPosition = new Vector3(Random.Range(-3, 3), 0, 0);
      t.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

      items.Add(t);
    }
  }
}
