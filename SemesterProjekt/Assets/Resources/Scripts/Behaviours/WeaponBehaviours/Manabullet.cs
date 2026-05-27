using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public class Manabullet : MonoBehaviour
    {
        private Weapons _weaponsType;
        public Manabullet()
        {
            _weaponsType = Weapons.Manaball;
        }
    }
}
