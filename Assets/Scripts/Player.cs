using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Game game;
    public Tool tool = Tool.Hand;
    public SpriteRenderer target;
    public GameObject walking;
    public Rigidbody bulldozerBody;
    public Collider bulldozerBodyCollider;
    public Renderer bulldozerBodyRenderer;
    public Collider bulldozerBladeCollider;
    public Renderer bulldozerBladeRenderer;
    public SpriteRenderer hand;
    public SpriteRenderer spike;
    public SpriteRenderer grabber;
    public SpriteRenderer leafblower;
    public SpriteRenderer bulldozer;
    public float leafblowerFuel = 100;
    public float bulldozerFuel = 100;
    public float bulldozerForceMultiplier = 1;
    public float walkingSpeed = 3;
    public float targetSpeed = 5;
    public float heldRatio { get => (float)held.Count / MaxHeldFromTool(tool); }
    public string heldText
    {
        get
        {
            var max = MaxHeldFromTool(tool);
            return max == 0 ? "" : held.Count + "/" + max;
        }
    }

    Vector3 mousePos;
    Vector3 walkTarget = Vector3.zero;
    List<Trash> held = new List<Trash>();

    public void SetTool(Tool tool)
    {
        this.tool = tool;

        // don't render bulldozer meshes
        // bulldozerBodyRenderer.enabled = bulldozerBladeRenderer.enabled = tool == Tool.Bulldozer;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBulldozer"), LayerMask.NameToLayer("Trash"), tool != Tool.Bulldozer);

        hand.enabled = spike.enabled = grabber.enabled = leafblower.enabled = bulldozer.enabled = false;

        SpriteFromTool(this.tool).enabled = true;
    }

    public void HoldTrash(Trash trash)
    {
        if (!trash.held)
        {
            held.Add(trash);
            trash.gameObject.layer = LayerMask.NameToLayer("TrashHeld");
            trash.rb.constraints = RigidbodyConstraints.FreezeAll;
            trash.renderer.enabled = false;
        }
    }

    public void DropTrash(Vector3 position)
    {
        foreach (var trash in held)
        {
            var dropOffset = new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
            trash.rb.constraints = RigidbodyConstraints.None;
            trash.rb.position = position + dropOffset;
            trash.rb.velocity = Vector3.zero;
            trash.rb.angularVelocity = Vector3.zero;
            StartCoroutine(ShowTrash(trash));
        }

        held.RemoveAll((trash) => true);
    }

    public void ThrowTrash(Vector3 position)
    {
        if (tool == Tool.Grabber && heldRatio > 0)
        {
            foreach (var trash in held)
            {
                var dropOffset = new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
                trash.rb.constraints = RigidbodyConstraints.None;
                trash.rb.position = position + dropOffset;
                trash.rb.velocity = Vector3.zero;
                trash.rb.angularVelocity = Vector3.zero;
                StartCoroutine(ShowTrash(trash));
            }

            StartCoroutine(StopMoving());

            held.RemoveAll((trash) => true);
        }
    }

    IEnumerator ShowTrash(Trash trash)
    {
        yield return new WaitForSeconds(Time.fixedDeltaTime);

        trash.renderer.enabled = true;
    }

    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(Time.deltaTime);

        walkTarget = Vector3.zero;
    }

    void Awake()
    {
        SetTool(tool);
        Physics.IgnoreCollision(bulldozerBladeCollider, game.job.ground.GetComponent<Collider>());
        Physics.IgnoreCollision(bulldozerBladeCollider, game.job.borderLeft);
        Physics.IgnoreCollision(bulldozerBladeCollider, game.job.borderRight);
        Physics.IgnoreCollision(bulldozerBladeCollider, game.job.borderTop);
        Physics.IgnoreCollision(bulldozerBladeCollider, game.job.borderBottom);
    }

    void Update()
    {
        if (game.view == View.Job)
        {
            UpdateMousePos();

            UpdateHighlightedTrash();

            UpdateWalkTarget();

            UpdateFuel();
        }
    }

    void FixedUpdate()
    {
        if (game.view == View.Job)
        {
            if (tool == Tool.Bulldozer)
            {
                UpdateBulldozerPosition();
            }
            else
            {
                UpdateWalkingPosition();
            }
        }
    }

    void UpdateMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void UpdateWalkTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            walkTarget = new Vector3(mousePos.x, mousePos.y, walking.transform.position.z);
        }
    }

    void UpdateHighlightedTrash()
    {
        var position = new Vector3(mousePos.x, mousePos.y, -1);
        target.transform.position = Vector3.Lerp(target.transform.position, position, Time.deltaTime * targetSpeed);

        // foreach (var trash in game.job.trashPile)
        // {
        //     trash.SetHighlight(Vector2.Distance(trash.transform.position, mousePos) < 1f);
        // }
    }

    void UpdateWalkingPosition()
    {
        Vector3 direction = mousePos - walking.transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        walking.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

        if (walkTarget != Vector3.zero)
        {
            walking.transform.position = Vector3.MoveTowards(walking.transform.position, walkTarget, Time.deltaTime * walkingSpeed);

            if (Vector2.Distance(walking.transform.position, walkTarget) < 0.25f)
            {
                walkTarget = Vector3.zero;

                var inRange = game.job.trashPile.FindAll((t) =>
                {
                    return Vector2.Distance(t.transform.position, walking.transform.position) < 1f;
                });

                foreach (var trash in inRange)
                {
                    if (heldRatio < 1 && !held.Contains(trash))
                    {
                        HoldTrash(trash);
                    }
                }

                target.DOColor(heldRatio == 1 ? Color.green : Color.white, 0.3f);
            }
        }
    }

    void UpdateBulldozerPosition()
    {
        var bodyPos = new Vector3(bulldozerBody.position.x, bulldozerBody.position.y, 0);
        var direction = mousePos - bodyPos;
        var distance = Vector2.Distance(mousePos, bodyPos);
        var rotation = Quaternion.LookRotation(direction.normalized * -1, bulldozerBody.transform.forward);
        var rotationFromEuler = Quaternion.Euler(new Vector3(0, 0, rotation.eulerAngles.z));
        bulldozerBody.rotation = rotationFromEuler;

        if (Input.GetMouseButton(0) && distance > 2f && bulldozerBody.velocity.magnitude < 2f)
        {
            var force = new Vector3(direction.x, direction.y, 0).normalized;

            bulldozerBody.AddForce(force * bulldozerForceMultiplier);
        }
    }

    void UpdateFuel()
    {
        if (tool == Tool.LeafBlower && Input.GetMouseButton(1))
        {
            leafblowerFuel = Mathf.Max(0, leafblowerFuel - Time.deltaTime);
        }
        else if (tool == Tool.Bulldozer && Input.GetMouseButton(0))
        {
            bulldozerFuel = Mathf.Max(0, bulldozerFuel - Time.deltaTime);
        }
    }

    int MaxHeldFromTool(Tool tool)
    {
        switch (tool)
        {
            case Tool.Hand:
                return 2;
            case Tool.Spike:
                return 5;
            case Tool.Grabber:
                return 5;
            default:
                return 0;
        }
    }

    SpriteRenderer SpriteFromTool(Tool tool)
    {
        switch (tool)
        {
            case Tool.Hand:
                return hand;
            case Tool.Spike:
                return spike;
            case Tool.Grabber:
                return grabber;
            case Tool.LeafBlower:
                return leafblower;
            case Tool.Bulldozer:
                return bulldozer;
            default:
                return hand;
        }
    }


}
