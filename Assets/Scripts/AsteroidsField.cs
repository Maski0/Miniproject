using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsField : MonoBehaviour
{
    public Transform asteroidPrefab;
    public int fieldRadius = 100;
    public int Asteroidcount = 500;


    void Start()
    {
        for (int loop = 0; loop<Asteroidcount;loop++)
        {
            Transform temp = Instantiate(asteroidPrefab, Random.insideUnitSphere * fieldRadius, Random.rotation);
            temp.localScale = new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
            temp.SetParent(this.gameObject.transform);
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldRadius);
    }
}
