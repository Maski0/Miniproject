using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform Target;
    public float range = 15f;

    private Transform barrale;
    public float RotationSpeed;

    public float FireRate = 1f;
    private float fireCount = 0f;
    public GameObject bullet;
    public Transform firePoint1;
    public Transform firePoint2;
    private bool toshoot = false;
    void Start()
    {
        barrale = transform.GetChild(0).gameObject.transform;
    }

    void Update()
    {
        float dis = Vector3.Distance(transform.position, Target.position);
        if(dis < range)
        {
            rotatetoTarget();
            toshoot = true;
        }
        else
        {
            toshoot = false;
        }

        if(fireCount<0 && toshoot)
        {
            Invoke("Shoot", 1f);
            fireCount = 1f / FireRate;
        }
        fireCount -= Time.deltaTime;
    }

    void rotatetoTarget()
    {
        Vector3 dir = Target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = lookRotation.eulerAngles;
        Vector3 rotation = Quaternion.Lerp(barrale.rotation,lookRotation,Time.deltaTime * RotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        barrale.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    void Shoot()
    {
        bool isrightshoot = true;
        GameObject ex;
        if(isrightshoot)
        {
            ex = Instantiate(bullet, firePoint1.position, Quaternion.identity);
            ex.transform.localRotation = barrale.rotation;
            isrightshoot = false;
        }
        else
        {
            ex = Instantiate(bullet, firePoint1.position, Quaternion.identity);
            ex.transform.localRotation = barrale.rotation;
            isrightshoot = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
