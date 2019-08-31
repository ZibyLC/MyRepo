using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject projectile;
    public void Shoot()
    {
        GameObject go= Instantiate(projectile, this.transform.position, this.transform.rotation);
    }
}
