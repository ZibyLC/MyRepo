using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class VelocitySetter : Projectile
{
    Rigidbody2D rb2d;
    public float speed = -2f;
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, speed);
    }
}
