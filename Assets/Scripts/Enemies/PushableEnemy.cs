using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PushableEnemy : MonoBehaviour, IPushable
{
    [SerializeField] private float resetTime = 0.3f;
    private Rigidbody2D rb;
    private Vector2 defaultVelocity;
    private float pushTimer;
    private bool isPushed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultVelocity = rb.velocity;
        isPushed = false;
    }

    private void Update()
    {
        if (isPushed)
        {
            pushTimer += Time.deltaTime;

            if (pushTimer >= resetTime)
            {
                rb.velocity = defaultVelocity;
                isPushed = false;
                pushTimer = 0f;
            }
        }
    }

    public void ApplyPushBack(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        isPushed = true;
        pushTimer = 0f;
    }
}
