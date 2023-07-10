using UnityEngine;

namespace Src.Scripts.Weapons
{
    public class Weapon
    {
        public string name;
        public float fireRate;
        public int magazineCapacity;
        public int currentAmmoInMagazine;
        public int totalBullets;
        public int damage;
        public int reloadTime; //reload time in milliseconds
        
        //if there are any bullets in magazine 
        public bool HasAmmoInMag()
        {
            return currentAmmoInMagazine > 0;
        }
        
        //fire the weapon once
        public void Fire()
        {   
            //if no ammo in magazine, reload
            if (!HasAmmoInMag())
                return;
            
            currentAmmoInMagazine--;
        }

        public void Reload()
        {
            //if no bullet left, return
            if (totalBullets <= 0)
                return;
            
            //if magazine is already full, return
            if (currentAmmoInMagazine == magazineCapacity)
                return;
            
            var bulletsToFillMag = magazineCapacity - currentAmmoInMagazine;    //how many bullets needed to fill the mag
            //if bullets needed are less than left bullet, change it to left bullets
            if (bulletsToFillMag < totalBullets)
                bulletsToFillMag = totalBullets;
            
            //add the bullets to mag
            currentAmmoInMagazine += bulletsToFillMag;
            totalBullets -= bulletsToFillMag;
        }
    }
}