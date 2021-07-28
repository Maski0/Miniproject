using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public looktowardsmouse looktowardsmouse;

    private GameObject effectTospawn;
    public float timeToFire = 0f;
    public float fireRate = 2;

    void Start()
    {
        effectTospawn = vfx[0];

    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1")&&Time.time>=timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            spawnVFX();
        }
    }

    void spawnVFX()
    {
        GameObject vfx;

        if(firePoint!=null)
        {
            vfx = Instantiate(effectTospawn, firePoint.transform.position, Quaternion.identity);
            if(looktowardsmouse!=null)
            {
                vfx.transform.localRotation = looktowardsmouse.getrotation();
            }
        }
        else
        {
            Debug.Log("No firePoint");
        }
    }
}
