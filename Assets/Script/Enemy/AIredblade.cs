using System.Collections;
using UnityEngine;

public class AIredblade : AIBase
{
    protected override IEnumerator ShootWithDelay()
    {
        canShoot = false;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        bullet.transform.right = transform.right;

        yield return new WaitForSeconds(shootInterval);

        canShoot = true;
    }
}