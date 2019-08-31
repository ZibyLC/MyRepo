using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class ProjectileBehaviour : Projectile
{
    private Rigidbody2D rb2d;
    public float projectile_speed=5f;

    private Vector2 CalcSpeed()
    {
        float angle = (this.transform.rotation.eulerAngles.z+90f)*Mathf.Deg2Rad;
        float vx = projectile_speed * Mathf.Cos(angle);
        float vy = projectile_speed * Mathf.Sin(angle);
        return new Vector2(vx, vy);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        rb2d.velocity = CalcSpeed();
    }
}
