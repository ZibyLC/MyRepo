using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class HeavyBehaviour : Heavy
{
    // Update is called once per frame
    void Update()
    {
        timeToShootRemained -= Time.deltaTime;
        if (timeToShootRemained <= 0 && onPosition)
        {
            Shoot();
        }
    }

    void OnTriggerEnter2D(Collider2D someCollider)
    {
        if (ToPosition(someCollider, "FireLine1"))
        {
            onPosition = true;
        }
        if(ToPosition(someCollider, "FireLine2"))
        {
            reloadDelay = reloadDelay / 2;
        }
        TakeDamage(someCollider);
    }
}
