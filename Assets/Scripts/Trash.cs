using UnityEngine;

public class Trash : MonoBehaviour
{
  new public Collider collider;
  new public Renderer renderer;
  public Rigidbody rb;
  public TrashType type;
  public bool held { get => gameObject.layer == LayerMask.NameToLayer("TrashHeld"); }

  void Awake()
  {
    rb = GetComponent<Rigidbody>();
    collider = GetComponent<Collider>();
    renderer = GetComponent<Renderer>();
  }

  void Update()
  {
  }
}
