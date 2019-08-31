using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using Entities;

public class PlayerController : Ship
{
    public int maxLives = 5;
    public float deadZone = 0.1f;
    public float startX;
    //public GameObject gameOver;
    public Text t_life;
    //public Text gameOver_score;
    private Rigidbody2D rb2d;
    private Vector3 startPosition;
    private int num_of_guns = 1;
    public GameObject uiButton;
    public GameObject menu;

    protected override sealed void TakeDamage(Collider2D someCollider)
    {
        int damage = 0;
        if(someCollider.gameObject.tag == "Projectile" || someCollider.gameObject.tag == "Asteroid" || someCollider.gameObject.tag == "Ship")
        {
            damage = 1;
            transform.position = startPosition;
            Destroy(someCollider.gameObject);
        }
        else if(someCollider.gameObject.tag == "LifeBonus")
        {
            damage = -1;
            Destroy(someCollider.gameObject);
        }
        else if(someCollider.gameObject.tag == "ReloadBonus")
        {
            reloadDelay = reloadDelay * 0.8f;
            Destroy(someCollider.gameObject);
        }
        else if (someCollider.gameObject.tag == "ExtraGun")
        {
            num_of_guns += 1;
            ActivateGuns();
            Destroy(someCollider.gameObject);
        }
        life -= damage;
        if(life>maxLives)
        {
            life = maxLives;
        }
        if(life<=0)
        {
            Menu m = menu.GetComponent<Menu>();
            m.GameOver();
            //GameOver
        }
        t_life.text = "HP:" + life.ToString();
    }

    private void ActivateGuns()
    {
        if(num_of_guns==1)
        {
            foreach(GunScript gun in guns)
            {
                if(gun.gameObject.name=="Gun")
                {
                    gun.gameObject.SetActive(true);
                }
                else
                {
                    gun.gameObject.SetActive(false);
                }
            }
        }
        else if(num_of_guns==2)
        {
            foreach (GunScript gun in guns)
            {
                if (gun.gameObject.name != "Gun")
                {
                    gun.gameObject.SetActive(true);
                }
                else
                {
                    gun.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (GunScript gun in guns)
            {
                gun.gameObject.SetActive(true);
            }
        }
    }

    public void ShootByButton()
    {
        Shoot();
    }


    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
        InitGuns();
        t_life.text = "HP:" + life.ToString();
        ActivateGuns();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToShootRemained>0)
        {
            timeToShootRemained -= Time.deltaTime;
        }
        if (!EventSystem.current.IsPointerOverGameObject(uiButton.GetInstanceID()))
        {
            if (Input.GetMouseButtonDown(0))
            {
                startX = Input.mousePosition.x;
            }
            if (Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x > startX + deadZone)
                {
                    rb2d.velocity = new Vector2(xVelocity, 0);
                }
                else if (Input.mousePosition.x < startX - deadZone)
                {
                    rb2d.velocity = new Vector2(-xVelocity, 0);
                }
                else
                {
                    rb2d.velocity = Vector2.zero;
                }
            }
        }
            if (Input.GetMouseButtonUp(0))
            {
                rb2d.velocity = Vector2.zero;
            }
        

    }

    void OnTriggerEnter2D(Collider2D someCollider)
    {
        TakeDamage(someCollider);
    }
}
