using UnityEngine;

public class Gun : MonoBehaviour
{
    public float bulletDamage = 10f;
    public float Range = 10f;
    public int bulletAmount = 1;
    public float WeaponSpreed;
    public float AimWeaponSpreed = 0.05f;
    public float NormalWeaponSpreed = 0.2f;
    public float origenalFireRate = 1;
    private float fireRate;
    public GameObject bulletImpactEffect;

    public LayerMask LayerMask;

    Aim aim;
    AudioSource audioSource;
    GunRecoil gunRecoil;

    private void Start()
    {
        WeaponSpreed = NormalWeaponSpreed;
        aim = GetComponent<Aim>();
        audioSource = GetComponent<AudioSource>();
        gunRecoil = GetComponentInParent<GunRecoil>();
    }

    void Update()
    {
        fireRate -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            if (fireRate <= 0)
            {
                Shoot();
                fireRate = origenalFireRate;
            }
        }
    }

    void Shoot()
    {
        audioSource.Play();
        gunRecoil.RecoilFire();

        if (aim.isAming)
        {
            WeaponSpreed = AimWeaponSpreed;
        }
        else
        {
            WeaponSpreed = NormalWeaponSpreed;
        }

        for (int i = 0; i < bulletAmount; i++)
        {
            RaycastHit hit;

            Vector3 ShootDir = transform.forward + (new Vector3(Random.Range(-WeaponSpreed, WeaponSpreed), Random.Range(-WeaponSpreed, WeaponSpreed), Random.Range(-WeaponSpreed, WeaponSpreed)));
            Vector3 ShootPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

            if (Physics.Raycast(ShootPos, ShootDir, out hit, Range, LayerMask))
            {
                Debug.Log(hit.transform.name);
                Vector3 hitPos = hit.point;
                GameObject impact = 
                Instantiate(bulletImpactEffect, hitPos, Quaternion.LookRotation(hit.normal));

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(bulletDamage);
                }
                MeshDestroy meshDestroy = hit.transform.GetComponent<MeshDestroy>();
                if (meshDestroy != null)
                {
                    meshDestroy.DestroyMesh();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 ShootDir = transform.forward + (new Vector3(Random.Range(-WeaponSpreed, WeaponSpreed), Random.Range(-WeaponSpreed, WeaponSpreed), Random.Range(-WeaponSpreed, WeaponSpreed)));
        Vector3 ShootPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ShootPos, ShootDir * Range);
    }
}
