using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileFlightTime = 2f;
    [SerializeField] GameObject hitEffect;

    bool canDestroy = false;

    private void Awake()
    {
        StartCoroutine(ProjectileFlightTime());
    }

    private void Update()
    {
        if (canDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }

    IEnumerator ProjectileFlightTime()
    {
        canDestroy = false;
        yield return new WaitForSeconds(projectileFlightTime);
        canDestroy = true;
    }
}