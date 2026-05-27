using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Player
{
#pragma warning disable
    public class PlayerController : MonoBehaviour
    {
        // Ref variables
        private GameObject _projectileContainer;
        private Animator _ManaBlockAnimator;
        private Animator _playerAnimator;
        private ManaShieldBehaviour _manaShieldBehaviour;
        private Rigidbody2D _rigidbody2D;

        [Header("Player Stats")] [SerializeField]
        private double health;

        public HealthBar healthBar;

        [SerializeField] private int playerIndex;

        private bool _hit;

        public bool Hit
        {
            get => _hit;
            set => _hit = value;
        }


        [Header("Variables for shoot ")]
        //VARIABLES FOR SHOOT ---------
        public GameObject currentWeapon;
        
        public Transform equipPoint;

        private List<GameObject> _prefabProjectileList;
        private GameObject _shootPointOnWep;

        private List<GameObject> equipablesList;

        private string Path = "Prefabs/Weapons/Projectiles/";
        private string EquipablesPath = "Prefabs/Equipables/";


        public int _timesShot = 0;

        public int TimesShot
        {
            get => _timesShot;
            set => _timesShot = value;
        }


        private void Start()
        {
            _prefabProjectileList = LoadAllPrefabsToArr(Path);
            equipablesList = LoadAllPrefabsToArr(EquipablesPath);
            _ManaBlockAnimator = gameObject.transform.GetChild(2).GetComponent<Animator>();
            _playerAnimator = gameObject.GetComponent<Animator>();
            _projectileContainer = GameObject.Find("Projectile_Container");
            _shootPointOnWep = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
            _manaShieldBehaviour = gameObject.transform.GetChild(2).GetComponent<ManaShieldBehaviour>();
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            healthBar.SetMaxHealth(health);
        }

        private List<GameObject> LoadAllPrefabsToArr(string path)
        {
            return new List<GameObject>(Resources.LoadAll<GameObject>(path));
        }

        public int GetPlayerIndex()
        {
            return playerIndex;
        }

        //Method for making the player take damage and calling the Die method if the player reach 0 HP
        public void TakeDamage(double damage)
        {
            _playerAnimator.SetTrigger("Damge");
            Hit = true;
            
            
            health -= damage;
            
            healthBar.SetHealth(health);

            if (health <= 0)
            {
                Die();
            }
        }
        
        

        

        //Method for destroying the player if they reach 0 Health points
        private void Die()
        {
            Destroy(gameObject);
        }


        public void Shoot()
        {
            if (currentWeapon != null)
            {
                GameObject projectile;
                Projectile projectileComponent;
                try
                {
                    try
                    {
                        projectile = Instantiate(currentWeapon, _shootPointOnWep.transform.position,
                            _shootPointOnWep.transform.rotation,
                            _projectileContainer.transform);
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"Instantiate Failed {e}");
                        throw;
                    }
                    projectileComponent = projectile.GetComponent<Projectile>();
                    projectileComponent.Parent = gameObject;
                }
                catch (Exception e)
                {
                    Debug.Log($"An error accored {e}");
                    throw;
                }

                TimesShot++;
                var dmg = projectileComponent.GetDamage(TimesShot);
                if (dmg < 0)
                {
                    TimesShot = 0;
                    DeEquipWeapon();
                    currentWeapon = null;
                }
            }
        }

        public void Block()
        {
            _ManaBlockAnimator.SetTrigger("Block");
            _manaShieldBehaviour.Parent = gameObject;
            _manaShieldBehaviour.HitStuff();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            CheckWeaponCollistion(other);
            
            
        }

        private void CheckWeaponCollistion(Collision2D other)
        {
            foreach (var o in _prefabProjectileList)
            {
                if (other.gameObject.name == o.name + "(Clone)")
                {
                    EquipWeapon(o);
                    Destroy(other.gameObject);
                }
            }
        }


        //Spawns an the weapons so it looks like the player is holding them
        //and also destroys them when the player picks up a new weapon/pickup
        private void EquipWeapon(GameObject o)
        {
            foreach (var o1 in equipablesList)
            {
                if (o.gameObject.name + "(Equipable)" == o1.name)
                {
                    var weaponEquiped = Instantiate(o1, equipPoint);
                    currentWeapon = o;
                    _shootPointOnWep = weaponEquiped.transform.GetChild(0).gameObject;
                }
            }

            DeEquipWeapon();

            
        }

        private void DeEquipWeapon()
        {
            if (equipPoint.childCount > 1)
            {
                Destroy(equipPoint.transform.GetChild(0).gameObject);
            }
            
            

        }
    }
}