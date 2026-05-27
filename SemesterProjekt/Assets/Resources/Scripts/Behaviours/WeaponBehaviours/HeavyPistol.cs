using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public class HeavyPistol : MonoBehaviour
    {
        private Weapons _weaponsType;
        public HeavyPistol()
        {
            _weaponsType = Weapons.HeavyPistol;
        }
    }
}
