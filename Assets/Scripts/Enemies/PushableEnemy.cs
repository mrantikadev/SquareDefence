using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PushableEnemy : MonoBehaviour, IPushable
{
    [Header("Configuration")]
    [SerializeField] private EnemySO config;

    [Header("Push Settings")]
    [SerializeField] private float pushForce;
    [SerializeField] private float pushDuration;
    [SerializeField] private float smoothTime;

    private float pushTimer = 0f;
    private bool isPushed = false;

    private float normalSpeed;
    
    private Vector2 currentVelocity;
    private Vector2 targetVelocity;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalSpeed = config.Speed;
        targetVelocity = Vector2.down * normalSpeed;
    }

    void Update()
    {
        if (isPushed)
        {
            pushTimer -= Time.deltaTime;

            if (pushTimer <= 0f)
            {
                isPushed = false;
                targetVelocity = Vector2.down * normalSpeed;
            }
        }

        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
    }

    public void ApplyPushBack(Vector2 sourcePosition)
    {
        Vector2 direction = ((Vector2)transform.position - sourcePosition).normalized;
        targetVelocity = direction * pushForce;

        isPushed = true;
        pushTimer = pushDuration;
    }
}
