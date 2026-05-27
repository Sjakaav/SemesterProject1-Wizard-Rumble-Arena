using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public class RifleTypes : WeaponTypeAbstract
    {
        private GameObject[] _resourcesArr;
        public GameObject[] ResourcesArr { get; }
        
        private static RifleTypes CreateInstance()
        {
            return new RifleTypes();
        }
        
        private RifleTypes()  
        {  
            Path = "Prefabs/Weapons/WeaponType_Rifles/";
            ResourcesArr = LoadAllPrefabsToArr();
        }  
        private static readonly object Padlock = new object();  
        private static RifleTypes _instance = null;  
        public static RifleTypes Instance  
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

        protected sealed override GameObject[] LoadAllPrefabsToArr()
        {
            return Resources.LoadAll<GameObject>(Path);
        }
    }
}