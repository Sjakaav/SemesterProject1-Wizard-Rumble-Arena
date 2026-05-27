using System.Linq;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public sealed class SpellTypes : WeaponTypeAbstract
    {
        
        private GameObject[] _resourcesArr;
        public GameObject[] ResourcesArr { get; }
        
        private static SpellTypes CreateInstance()
        {
            return new SpellTypes();
        }
        
        private SpellTypes()  
        {  
            Path = "Prefabs/Weapons/WeaponType_Spells/";
            ResourcesArr = LoadAllPrefabsToArr();
        }  
        private static readonly object Padlock = new object();  
        private static SpellTypes _instance = null;  
        public static SpellTypes Instance  
        {  
            get  
            {  
                if (_instance == null)  
                {  
                    lock (Padlock)  
                    {  
                        if (_instance == null)  
                        {  
                            _instance = CreateInstance();  
                        }  
                    }  
                }  
                return _instance;  
            }  
        }

        protected override GameObject GETResource(Weapons weapons)
        {
            // This is a LINQ expression. Dont ask me what it is. Just know that its awesome!
            // return _resourcesArr.FirstOrDefault(resource => 
            //     !(resource is null) && resource.name.Contains(ConsumablesType.ToString()));
            return null;
        }

        protected override GameObject[] LoadAllPrefabsToArr()
        {
            return Resources.LoadAll<GameObject>(Path);
        }
    }
}