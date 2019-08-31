using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class SniperBehaviour : Sniper
{
    void Update()
    {
        Rotate();
        if(onPosition==true)
        {
            timeToChangePosRemained -= Time.deltaTime;
            if (timeToChangePosRemained<=0)
            {
                timeToChangePosRemained = changePosTime;
                if(status==ChangePosStatus.Stop)
                {
                    if(Random.value>0.5f)
                    {
                        status = ChangePosStatus.MoveLeft;
                        rb2d.velocity = new Vector2(-xVelocity, rb2d.velocity.y);
                    }
                    else
                    {
                        status = ChangePosStatus.MoveRight;
                        rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
                    }
                }
                else
                {
                    status = ChangePosStatus.Stop;
                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                }
            }
            timeToShootRemained -= Time.deltaTime;
            if (timeToShootRemained <= 0 && transform.position.y>player.transform.position.y)
            {
                Shoot();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D someCollider)
    {
        if (ToPosition(someCollider, "FireLine1"))
        {
            onPosition = true;
        }
        TakeDamage(someCollider);
    }
}
