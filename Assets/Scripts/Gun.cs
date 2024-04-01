using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private float _range;
    [SerializeField] private float _damage;

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Camera fpsCam;

    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        particleSystem.Play();
        sound.Play();

        GameObject bul = Instantiate(bullet, transform.position, fpsCam.transform.rotation);
        Destroy(bul, 2f);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, _range))
        {
            Damageble damageble = hit.transform.gameObject.GetComponent<Damageble>();

            if (damageble != null)
            {
                damageble.TakeDamage(_damage);
            }
        }
    }
}
