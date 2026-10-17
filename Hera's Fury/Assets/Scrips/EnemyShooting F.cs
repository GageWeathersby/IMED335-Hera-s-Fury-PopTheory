using System;
using UnityEngine;

public class EnemyShootingF : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float fireRate = 2.5f;

    private float nextFireTime;
    private Transform playerTranaform;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        {
            if (player != null)
            {
                playerTranaform = player.transform;
            }
        }
    }

    // Update is called once per frame
   private void Update()
    {
        if (playerTranaform != null && Time.time >= nextFireTime)
        {
            ShootArrow();
            nextFireTime = Time.time + fireRate; 
        }
    }

    private void ShootArrow()
    {
        Instantiate(arrowPrefab, transform.position, Quaternion.identity); 
    }

}
