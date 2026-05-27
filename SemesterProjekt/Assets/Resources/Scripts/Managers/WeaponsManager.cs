using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public class WeaponsManager
    {
        private List<GameObject> _listOfWeapons = new List<GameObject>();

        public List<GameObject> ListOfWeapons => _listOfWeapons;

        private Component[] _weaponsBehaviours;

        private static WeaponsManager CreateInstance()
        {
            return new WeaponsManager();
        }

        private WeaponsManager()
        {
            if (SpellTypes.Instance.ResourcesArr != null)
            {
                foreach (var o in SpellTypes.Instance.ResourcesArr)
                {
                    _listOfWeapons.Add(o);
                }
            }

            if (RifleTypes.Instance.ResourcesArr != null)
            {
                foreach (var o in RifleTypes.Instance.ResourcesArr)
                {
                    _listOfWeapons.Add(o);
                }
            }
            Move(_listOfWeapons,3, 1);
        }

        private static readonly object Padlock = new object();
        private static WeaponsManager _instance = null;

        public static WeaponsManager Instance
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


        public string PrintList()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var o in _listOfWeapons)
            {
                sb.Append(o.name).Append(" | ");
            }

            return sb.ToString();
        }

        public void CheckForBehaviour(int i, GameObject o)
        {
            var path = Application.dataPath + "/Resources/Scripts/Behaviours/WeaponBehaviours/";
            var fileEntries = Directory.GetFiles(path);
            List<FileInfo> fileInfos = new List<FileInfo>();
            foreach (var fileEntry in fileEntries)
            {
                FileInfo info = new FileInfo(fileEntry);
                fileInfos.Add(info);
            }

            foreach (var entry in fileInfos)
            {
                if (entry.Name.Equals(_listOfWeapons[i].name + ".cs"))
                {
                    badAddComponent(_listOfWeapons[i].name, o);
                }
            }
        }

        private void badAddComponent(string s, GameObject o)
        {

            switch (s)
            {
                case "Fireball":
                    o.AddComponent<Fireball>();
                    break;
                case "Iceball":
                    o.AddComponent<Iceball>();
                    break;
                case "Manabullet":
                    o.AddComponent<Manabullet>();
                    break;
                case "HeavyPistol":
                    o.AddComponent<HeavyPistol>();
                    break;
                case "Pistol":
                    o.AddComponent<Pistol>();
                    break;
                case "Rifle":
                    o.AddComponent<Rifle>();
                    break;
            }

        }


        private static void Move<T>(List<T> list, int oldIndex, int newIndex)
        {
            // exit if positions are equal or outside array
            if ((oldIndex == newIndex) || (0 > oldIndex) || (oldIndex >= list.Count) || (0 > newIndex) ||
                (newIndex >= list.Count)) return;
            // local variables
            var i = 0;
            T tmp = list[oldIndex];
            // move element down and shift other elements up
            if (oldIndex < newIndex)
            {
                for (i = oldIndex; i < newIndex; i++)
                {
                    list[i] = list[i + 1];
                }
            }
            // move element up and shift other elements down
            else
            {
                for (i = oldIndex; i > newIndex; i--)
                {
                    list[i] = list[i - 1];
                }
            }
            // put element from position 1 to destination
            list[newIndex] = tmp;
        }

    }
}

    