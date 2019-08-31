using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public static class Constants
    {
        public static Vector2 aliveArea = new Vector2(20, 20);
    }

    public abstract class Entity : MonoBehaviour
    {
        protected void Utilize()
        {
            if (Mathf.Abs(transform.position.x) > Constants.aliveArea.x || Mathf.Abs(transform.position.y) > Constants.aliveArea.y)
            {
                Destroy(gameObject);
            }
        }
    }

    public class Projectile : Entity
    {
        void Update()
        {
            Utilize();
        }
    }

    public class Dense : Entity
    {
        public int life;
        public int scoreWeight;
        public GameObject reloadBonus;
        public GameObject denseBonus;
        public GameObject gunBonus;
        public float dropChance = 0.5f;

        protected virtual void Drop()
        {
            if (Random.value<dropChance)
            {
                if(Random.value<dropChance)
                {
                    if (Random.value < dropChance)
                    {
                        GameObject go = Instantiate(gunBonus, transform.position, transform.rotation);
                    }
                    else
                    {
                        GameObject go = Instantiate(reloadBonus, transform.position, transform.rotation);
                    }
                }
                else
                {
                    GameObject go = Instantiate(denseBonus, transform.position, transform.rotation);
                }
            }
        }

        private void Die()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            ScoreController sc = player.GetComponent<ScoreController>();
            sc.ChangeScore(scoreWeight);
            Drop();
            Destroy(gameObject);
        }

        protected virtual void TakeDamage(Collider2D someObject)
        {
            if (someObject.gameObject.tag == "PlayerProjectile")
            {
                life -= 1;
                Destroy(someObject.gameObject);
                if (life <= 0)
                {
                    Die();
                }
            }
        }

        void OnTriggerEnter2D(Collider2D someObject)
        {
            TakeDamage(someObject);
        }

        void Update()
        {
            Utilize();
        }
    }

    public class Ship : Dense
    {
        protected List<GunScript> guns;
        public float reloadDelay = 5;
        public float xVelocity = 1;
        protected float timeToShootRemained = 0;
        protected void InitGuns()
        {
            guns = new List<GunScript>();
            foreach (Transform child in transform)
            {
                if(child.gameObject.tag=="Gun")
                {
                    guns.Add(child.gameObject.GetComponent<GunScript>());
                }
            }
        }

        protected void Shoot()
        {
            if(timeToShootRemained<=0)
            {
                timeToShootRemained = reloadDelay;
                foreach(GunScript gun in guns)
                {
                    if(gun.gameObject.activeSelf)
                    {
                        gun.Shoot();
                    }
                }
            }
        }

        void Start()
        {
            InitGuns();
        }

        /*void OnTriggerEnter2D(Collider2D someObject)
        {
            TakeDamage(someObject);
            if(ToPosition(someObject, "FirePosition1"))
            {

            }
        }

        void Update()
        {

        }*/

    }

    public class Scout : Ship
    {
        protected GameObject player;
        protected Rigidbody2D rb2d;
        protected bool onPosition = false;

        protected bool ToPosition(Collider2D someObject, string tag)
        {
            if (someObject.gameObject.tag == tag)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void Start()
        {
            InitGuns();
            rb2d = this.GetComponent<Rigidbody2D>();
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public class Sniper : Scout
    {
        public float changePosTime = 3;
        public float timeToChangePosRemained = 0;
        protected enum ChangePosStatus {MoveLeft, MoveRight, Stop, GetOut};
        protected ChangePosStatus status = ChangePosStatus.Stop;
        protected void Rotate()
        {
            Vector3 difference = player.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(-difference.x, difference.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        }

        protected override void Drop()
        {
            GameObject go = Instantiate(gunBonus, transform.position, transform.rotation);
        }

        protected override void TakeDamage(Collider2D someObject)
        {
            base.TakeDamage(someObject);
            if(someObject.gameObject.tag=="Wall")
            {
                if(status==ChangePosStatus.MoveLeft)
                {
                    rb2d.velocity = new Vector2(xVelocity, rb2d.velocity.y);
                    status = ChangePosStatus.GetOut;
                    timeToChangePosRemained = changePosTime;
                }
                else if(status== ChangePosStatus.MoveRight)
                {
                    rb2d.velocity = new Vector2(-xVelocity, rb2d.velocity.y);
                    status = ChangePosStatus.GetOut;
                    timeToChangePosRemained = changePosTime;
                }
            }
        }
    }

    public class Heavy : Scout
    {
        protected override void Drop()
        {
            GameObject go1 = Instantiate(denseBonus, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.rotation);
            GameObject go2 = Instantiate(denseBonus, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation);
        }
    }


}
