using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public class Pistol : MonoBehaviour
    {
        private Weapons _weaponsType;
        public Pistol()
        {
            _weaponsType = Weapons.Pistol;
        }
    }
}
