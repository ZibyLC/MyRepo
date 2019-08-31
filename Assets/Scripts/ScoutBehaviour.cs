using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class ScoutBehaviour : Scout
{
    public float deadZone = 0.1f;
    
    // Update is called once per frame
    void Update()
    {
        if(onPosition)
        {
            if((transform.position.x-player.transform.position.x)>deadZone)
            {
                rb2d.velocity = new Vector2(-xVelocity,rb2d.velocity.y);
            }
            else if ((transform.position.x - player.transform.position.x) < -deadZone)
            {
                rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
            }
            else
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }

            timeToShootRemained -= Time.deltaTime;
            if(timeToShootRemained<=0)
            {
                Shoot();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D someCollider)
    {
        if(ToPosition(someCollider,"FireLine1"))
        {
            //rb2d.velocity = Vector2.zero;
            onPosition = true;
        }
        TakeDamage(someCollider);
    }
}
