using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;
    public Tool tool = Tool.Hand;
    public GameObject target;
    public GameObject walkingBody;
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
    public LayerMask trashLayerMask;
    public float bulldozerForceMultiplier = 1;
    public float walkingSpeed = 3;
    public float targetSpeed = 5;

    Vector3 mousePos;
    Vector3 walkTarget = Vector3.zero;
    List<Trash> held = new List<Trash>();

    public void SetTool(Tool tool)
    {
        this.tool = tool;

        // don't render bulldozer meshes
        bulldozerBodyRenderer.enabled = bulldozerBladeRenderer.enabled = tool == Tool.Bulldozer;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Trash"), tool != Tool.Bulldozer);

        hand.enabled = spike.enabled = grabber.enabled = leafblower.enabled = bulldozer.enabled = false;

        SpriteFromTool(this.tool).enabled = true;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
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
        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        // pos.z = 0;
        mousePos = pos;
    }

    void UpdateWalkTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            walkTarget = new Vector3(mousePos.x, mousePos.y, walkingBody.transform.position.z);
        }
    }

    void UpdateHighlightedTrash()
    {
        // var colliders = Physics.OverlapSphere(mousePos, 1, trashLayerMask);
        var position = new Vector3(mousePos.x, mousePos.y, -1);
        target.transform.position = Vector3.Lerp(target.transform.position, position, Time.deltaTime * targetSpeed);

        foreach (var trash in game.job.trashPile)
        {
            trash.SetHighlight(Vector2.Distance(trash.transform.position, mousePos) < 1f);
        }
    }

    void UpdateWalkingPosition()
    {
        Vector3 direction = mousePos - walkingBody.transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        walkingBody.transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

        if (walkTarget != Vector3.zero)
        {
            walkingBody.transform.position = Vector3.MoveTowards(walkingBody.transform.position, walkTarget, Time.deltaTime * walkingSpeed);

            if (Vector2.Distance(walkingBody.transform.position, walkTarget) < 0.25f)
            {
                walkTarget = Vector3.zero;

                var toRemove = new List<Trash>();

                foreach (var trash in game.job.trashPile)
                {
                    if (Vector2.Distance(trash.transform.position, walkingBody.transform.position) < 1f)
                    {
                        toRemove.Add(trash);
                    }
                }

                foreach (var trash in toRemove)
                {
                    game.job.trashPile.Remove(trash);

                    Object.Destroy(trash.gameObject, 0.1f);
                }
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
