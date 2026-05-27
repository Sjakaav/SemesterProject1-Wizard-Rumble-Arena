using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public abstract class WeaponTypeAbstract
    {
        /// <summary>
        /// Path to consumable Type prefab folder
        /// </summary>
        protected string Path;

        /// <summary>
        /// This method is used to retrieve specific Gameobjects from prefabs
        /// </summary>
        /// <returns></returns>
        protected abstract GameObject GETResource(Weapons weapons);

        /// <summary>
        /// This loads all prefabs in the Path directory into the ResourcesArr array
        /// </summary>
        protected abstract GameObject[] LoadAllPrefabsToArr();
    }

}
