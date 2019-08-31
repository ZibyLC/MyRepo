using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject asteroid;
    public GameObject asteroidXL;
    public GameObject scout;
    public GameObject sniper;
    public GameObject heavy;
    public float width=5.0f;
    public float spawnDelayTime = 7;
    private float timeLeft = 0;
    private float startRange;
    private float stopRange;
    
    // Start is called before the first frame update
    void Start()
    {
        startRange = transform.position.x - width;
        stopRange = transform.position.x + width;
        timeLeft = spawnDelayTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft<=0)
        {
            timeLeft = spawnDelayTime;
            if (spawnDelayTime > 1)
            {
                spawnDelayTime = spawnDelayTime * 0.99f;
            }
            float rand = Random.value;
            if(rand<0.8f)
            {
                float xPos = Random.Range(startRange, stopRange);
                GameObject go;
                if(rand<0.4f)
                {
                    if(rand<0.3f)
                    {
                        if(rand<0.1f)
                        {
                            if(rand<0.03f)
                            {
                                go = heavy;
                            }
                            else
                            {
                                go = sniper;
                            }
                        }
                        else
                        {
                            go = scout;
                        }
                    }
                    else
                    {
                        go = asteroidXL;
                    }
                }
                else
                {
                    go = asteroid;
                }
                GameObject go2 = Instantiate(go, new Vector3(xPos, transform.position.y, transform.position.z), transform.rotation);
                if(go==scout)
                {
                    go2.transform.rotation=Quaternion.Euler(new Vector3(0,0,180));
                }
            }
        }
    }
}
