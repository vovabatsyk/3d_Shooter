using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float damage = 21f;
    public float fireRate = 10f;
    public float force = 155f;
    public float range = 50f;
    public ParticleSystem muzzleFlash;
    public Transform bulletSpawn;
    public AudioClip shotSFX;
    public AudioSource _audioSource;
    public GameObject hitEffect;

    public Camera _camera;
    private float nextFire = 0f;

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }
    public void Shoot()
    {
        _audioSource.PlayOneShot(shotSFX);
        muzzleFlash.Play();

        RaycastHit hit;


        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range))
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impact, 2f);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
        }
    }
}
