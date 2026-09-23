using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Animator cannonAnimator;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject ShootOut;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float reloadTime;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private int magazineSize;
    private float bulletsLeft;

    [SerializeField] private float reloadDuration;
    private bool isReloading;


    [SerializeField] private float aimDistance = 100f;
    [SerializeField] private float minAimDistance = 2f;
    [SerializeField] private LayerMask aimMask;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private GameObject muzzlePrefab;

    private void Awake()
    {
        bulletsLeft = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && shootCooldown <= 0 && bulletsLeft > 0)
        {
            cannonAnimator.SetBool("Shoot", true);

            Vector3 shootPosition = ShootOut.transform.position;

            var muzzleEffect = Instantiate(muzzlePrefab, shootPosition, ShootOut.transform.rotation);
            Destroy(muzzleEffect, 0.5f);
            Instantiate(bulletPrefab, shootPosition, GetAimRotation());
            bulletsLeft--;
            shootCooldown = reloadTime;
        }
        else
        {
            cannonAnimator.SetBool("Shoot", false);
        }

        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }

        if (bulletsLeft <= 0)
        {
            TryReload();
        }

        UpdateText();
    }

    private void TryReload()
    {
        if (isReloading || bulletsLeft == magazineSize) return;

        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadDuration);

        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void UpdateText()
    {
        ammoText.SetText(bulletsLeft.ToString() + " / " + magazineSize.ToString());
    }

    private Quaternion GetAimRotation()
    {
        Ray aimRay = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        float distance = aimDistance;

        if (Physics.Raycast(aimRay, out RaycastHit hit, aimDistance, aimMask))
        {
            distance = Mathf.Max(hit.distance, minAimDistance);
        }

        Vector3 aimPoint = aimRay.GetPoint(distance);
        return Quaternion.LookRotation(aimPoint - ShootOut.transform.position);
    }
}
