using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemyShip : MonoBehaviour
{
    public float speed = 5.0f;
    public float stoppingDistance = 8f;
    public float retretDistance = 3f;
    public float rotationspeed = 10f;

    private float timeBtwshots;
    public float StartTimeBtwShots;
    public GameObject bullet;
    public Transform firePoint;
    public GameObject mussule;


    public Transform Player;
    
    void Start()
    {
        timeBtwshots = StartTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = transform.position - Player.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation);

        float dis = Vector3.Distance(transform.position, Player.position);
        if(dis > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }
        else if(dis < stoppingDistance && dis > retretDistance)
        {
            transform.position = this.transform.position;
        }
        else if(dis < retretDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.position, -speed * Time.deltaTime);
        }

        
        if(timeBtwshots <= 0)
        {
            spawnVFX();
            timeBtwshots = StartTimeBtwShots;
        }
        else
        {
            timeBtwshots -= Time.deltaTime;
        }
    }

    void spawnVFX()
    {
        GameObject vfx;

        if (firePoint != null)
        {
            vfx = Instantiate(bullet, mussule.transform.position, Quaternion.identity);
            vfx.transform.localRotation = mussule.transform.rotation;
        }
        else
        {
            Debug.Log("No firePoint");
        }
    }
}
