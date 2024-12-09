using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private bool isPressed;

    private float releaseDelay;
    private float maxDragDistance = 2f;

    private Rigidbody2D rb;
    private SpringJoint2D sj;
    private Rigidbody2D slingRb;
    private LineRenderer lr;

    [HideInInspector] public BallSpawner spawner; // Reference to the spawner

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        lr = GetComponent<LineRenderer>();
        slingRb = sj.connectedBody;

        lr.enabled = false;

        releaseDelay = 1 / (sj.frequency * 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            DragBall();
        }
    }

    private void DragBall()
    {
        SetLineRendererPosition();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        float distance = Vector2.Distance( mousePosition, slingRb.position );

        if (distance > maxDragDistance)
        {
            Vector2 direction = (mousePosition - slingRb.position).normalized;
            rb.position = slingRb.position + direction * maxDragDistance;
        }
        else
        {
            rb.position = mousePosition;
        }
    }

    private void SetLineRendererPosition()
    {
        Vector3[] position = new Vector3[2];
        position[0] = rb.position;
        position[1] = slingRb.position;
        lr.SetPositions(position);
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
        lr.enabled = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine(Release());
        lr.enabled = false;
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds( releaseDelay );
        sj.enabled = false;

        // Start the timer to destroy the ball
        StartCoroutine(DestroyAfterDelay(5f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Notify the spawner to clear the current ball reference
        if (spawner != null)
        {
            spawner.ClearCurrentBall();
            spawner.SpawnBall(); // Spawn a new ball immediately
        }

        Destroy(gameObject); // Destroy the ball after the delay
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Calculate collision force
        float collisionForce = collision.relativeVelocity.magnitude;

        // Send the collision force to the material
        var materialHandler = collision.gameObject.GetComponent<MaterialHandler>();
        if (materialHandler != null)
        {
            materialHandler.TakeDamage(collisionForce);
        }

        //Debug.Log($"Collision Force: {collisionForce}");
    }
}
