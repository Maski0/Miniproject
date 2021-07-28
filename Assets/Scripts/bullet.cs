using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class bullet : MonoBehaviour
{
    public float speed = 12f;
    public enum BulletType { playerBullet, enemyBullet ,TurretBullet};
    public CameraShaker camerashake;
    public BulletType Bullettype;
    public GameObject hitFVX;

    private void Update()
    {
        switch (Bullettype)
        {
            case BulletType.playerBullet:
                if (speed != 0)
                {
                    transform.position += transform.forward * (speed * Time.deltaTime);
                }
                break;
            case BulletType.enemyBullet:
                if (speed != 0)
                {
                    transform.position += transform.forward * (-speed * Time.deltaTime);
                }
                break;
            case BulletType.TurretBullet:
                if(speed!=0)
                {
                    transform.position += transform.forward * (speed * Time.deltaTime);
                }
                break;
            default:
                break;
        }
        
        Invoke("destroy", 3f);
    }
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        if (collision.gameObject.CompareTag("canCollide"))
        {
            speed = 0f;
            if(hitFVX != null)
            {
                var obj = Instantiate(hitFVX, pos, rotation); 
            }
            destroy();
        }
        switch (Bullettype)
        {
            case BulletType.playerBullet:
                if(collision.gameObject.CompareTag("Enemy"))
                {
                    speed = 0f;
                    if (hitFVX != null)
                    {
                        var obj = Instantiate(hitFVX, pos, rotation);
                    }
                    destroy();
                }
                if (collision.gameObject.CompareTag("BossShip"))
                {
                    speed = 0f;
                    if (hitFVX != null)
                    {
                        var obj = Instantiate(hitFVX, pos, rotation);
                    }
                    destroy();
                }
                break;
            case BulletType.enemyBullet:
                if(collision.gameObject.CompareTag("Player"))
                {
                    speed = 0f;
                    if (hitFVX != null)
                    {
                        var obj = Instantiate(hitFVX, pos, rotation);
                    }
                    destroy();
                }
                break;
            case BulletType.TurretBullet:
                if (collision.gameObject.CompareTag("Player"))
                {
                    speed = 0f;
                    if (hitFVX != null)
                    {
                        var obj = Instantiate(hitFVX, pos, rotation);
                    }
                    destroy();
                }
                break;

            default:
                break;
        }

    }
    
    void destroy()
    {
        Destroy(gameObject);
    }

}
