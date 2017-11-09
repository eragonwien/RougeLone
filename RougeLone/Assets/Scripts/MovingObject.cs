using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

    public float moveTime = .1f; // Time for object to move in second
    public LayerMask blockingLayer; // Layer for collision checking

    private BoxCollider2D boxCollider;
    private Rigidbody2D rBody;
    private float inverseMoveTime;

    // Use this for initialization
    protected virtual void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        rBody = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;

    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        while (true)
        {
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            if (sqrRemainingDistance <= float.Epsilon)
                break;

            Vector3 newPosition = Vector3.MoveTowards(rBody.position, end, inverseMoveTime * Time.deltaTime);
            rBody.MovePosition(newPosition);
            yield return null;
        }
    }

    

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + new Vector2(xDir, yDir);

        boxCollider.enabled = false; // disable so that ray cast wont hit the collider

        hit = Physics2D.Linecast(startPosition, endPosition, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null) // nothing hit
        {
            StartCoroutine(SmoothMovement(endPosition));
            return true;
        }
        return false;
    }

    protected abstract void OnCantMove<T>(T component) where T : Component;

    protected virtual void AttemptMove<T>(int xDir, int yDir) where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null) // nothing was hit by the raycast
        {
            return;
        }

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }
}
