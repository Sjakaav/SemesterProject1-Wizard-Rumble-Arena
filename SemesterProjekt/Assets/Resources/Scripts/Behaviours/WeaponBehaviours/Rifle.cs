using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public class Rifle : MonoBehaviour
    {
        private Weapons _weaponsType;
        public Rifle()
        {
            _weaponsType = Weapons.Rifle;
            
        }

    }
}