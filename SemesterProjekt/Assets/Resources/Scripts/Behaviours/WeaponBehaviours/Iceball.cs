using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public class Iceball : MonoBehaviour
    {
        private Weapons _weaponsType;
        public Iceball()
        {
            _weaponsType = Weapons.Iceball;
        }
    }
}