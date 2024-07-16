using System.Collections;
using UnityEngine;

public class AIenemy : AIBase
{
    public Transform firePoint;

    protected override IEnumerator ShootWithDelay()
    {
        canShoot = false;

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        yield return new WaitForSeconds(shootInterval);

        canShoot = true;
    }
}