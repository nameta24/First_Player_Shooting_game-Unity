using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeExpoBlaster : Gun
{
    [SerializeField]
    private BulletExplodeOnCollission bullet_prefab;

    protected bool can_shoot;
    public AudioClip Bullet;
    public float volume = 0.2f;

    [SerializeField]
    [Range(1, 10)]
    protected float delay = 1; // delay in seconds before we can shoot again


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        can_shoot = true;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
       
    }


    protected override void shoot()
    {

        if (can_shoot && gameObject.activeSelf && is_in_players_pocket && PlayerInput.instance.mouse_left_click  && current_bullet > 0)
        {

            
            current_bullet -= 1;


            BulletExplodeOnCollission bullet_ = Instantiate(bullet_prefab);
            AudioSource.PlayClipAtPoint(Bullet, transform.position, volume);
            bullet_.transform.position = shoot_start_position.position;

            // shoot bullet foward
            bullet_.GetComponent<Rigidbody>().AddForce(shoot_start_position.forward * 4000f, ForceMode.Acceleration);



            StartCoroutine(coolDownToReshoot());

        }
    }


    private IEnumerator coolDownToReshoot() // cool down before we can shoot again
    {
        can_shoot = false;

        yield return new WaitForSeconds(delay);

        can_shoot = true;
    }


}
