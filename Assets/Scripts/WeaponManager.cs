using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviour
{
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject pistolEquip;
    public GameObject shotgunEquip;

    private GameObject curWeapon;

    public GameObject bullet;
    private GameObject firedBullet;

    private int pistolMaxAmmo = 10;
    //private int shotgunMaxAmmo = 2;

    private float time;
    public float reloadDelay;
    private bool isReloading = false;

    private int curAmmo;
    private int curMaxAmmo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            time += Time.deltaTime;

            if (time >= reloadDelay)
            {
                reloadWeapon();
                //reset our timer to 0
                //time = 0f;
            }
        }
    }


    //Weapon gameobject is passed from player when they collide with the pickup version of the weapon
    public void equipGun(GameObject weapon)
    {
        Debug.Log("Weapon equipped");

        //The equipable version of the gameobject is created
        if (weapon.CompareTag("Pistol") == pistol.CompareTag("Pistol"))
        {
            curWeapon = Instantiate(pistolEquip);
            curMaxAmmo = pistolMaxAmmo;
        }

        else if (weapon.gameObject == shotgun.gameObject)
        {
            curWeapon = Instantiate(pistolEquip);
            //curMaxAmmo = shotgunMaxAmmo;
        }

        //The original gameobject is destroyed
        Destroy(weapon.gameObject);

        //The equippable gameObject is attached to the player
        curWeapon.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
        curWeapon.transform.SetParent(this.gameObject.transform);

        Debug.Log("Equip " + curWeapon.name);

        //Weapon ammo is replenished
        reloadWeapon();
    }

    public int getMaxAmmo()
    {
        return curMaxAmmo;
    }

    public int getCurAmmo()
    {
        return curAmmo;
    }

    public bool getIsReloading()
    {
        return isReloading;
    }    

    public void shootWeapon()
    {
        Debug.Log("Fire pressed");

        //Calls the reload function if ammo is zero
        if (curAmmo <= 0)
        {
            Debug.Log("Reload");
            reloadWeapon();
            //isReloading = true;
        }

        //Bullet is created after reload is complete
        else if (curWeapon.CompareTag("Pistol"))
        {
            Debug.Log("Shoot Pistol");
            firedBullet = Instantiate(bullet);

            //Bullet is moved to the player's position
            firedBullet.transform.position = new Vector2(this.transform.position.x, this.transform.position.y);
            
            //Ammo is adjusted after each fire
            curAmmo--;
        }

        else if (curWeapon == shotgun)
        {

        }
    }

    public void reloadWeapon()
    {
        isReloading = true;

        if (time >= reloadDelay)
        {
            if (curWeapon.CompareTag("Pistol"))
            {
                Debug.Log("Pistol reload");
                curAmmo = pistolMaxAmmo;
            }

            else if (curWeapon.CompareTag("Shotgun"))
            {
                Debug.Log("Shotgun reload");
                //curAmmo = shotgunMaxAmmo;
            }

            time = 0f;
            isReloading = false;
        }

        ////Ensures ammo capacity matches ammo type
        //if (curWeapon.CompareTag("Pistol"))
        //{
        //    Debug.Log("Pistol reload");
        //    curAmmo = pistolMaxAmmo;
        //}

        //else if (curWeapon.CompareTag("Shotgun"))
        //{
        //    Debug.Log("Shotgun reload");
        //    //curAmmo = shotgunMaxAmmo;
        //}
    }
}
