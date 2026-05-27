using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once CheckNamespace
namespace Consumable_Entity
{
    public class Spawner2 : MonoBehaviour
    {
        
        private GameObject _weaponContainer;
        /// <summary>
        /// creates a list with type game object
        /// </summary>
        [SerializeField] private List<GameObject> weaponsOnMapList = new List<GameObject>();
        [SerializeField] private List<GameObject> ListOfWeapons = new List<GameObject>();

        private readonly int waitTime = 5; //time between pick up spawns
        private int spawnAnimationTimer = 2; //time before the spawnAnimation gets deleted
        

        public GameObject spawnAnimation;


        // Start is called before the first frame update
        private void Start()
        {
            _weaponContainer = GameObject.Find("Weapons_Container");
            //Start the coroutine we define below named SpawnTimer.
            StartCoroutine(SpawnTimer());
        }

        private IEnumerator SpawnTimer()
        {
            while (weaponsOnMapList.Count < 10) //While loop that checks how many pick ups are on the map/in the list of pick ups on the map
            {
                RemoveNullFromList(weaponsOnMapList);
                
                var position = new Vector3(Random.Range(-70.0f, 35.0f), 20f, -0.5f); //Vector 3 med random position af x og en bestemt til y og z
                var prefabPickUpIndex = Random.Range(0, ListOfWeapons.Count); //List with all the pick up prefabs and a random 

                Instantiate(spawnAnimation, position, Quaternion.identity);
                yield return new WaitForSeconds(spawnAnimationTimer);

                
                
                //Spawns a prefab from the list prefabPickUpIndex with a random x position
                var consumableInstantiated = Instantiate(ListOfWeapons[prefabPickUpIndex], position, Quaternion.identity, _weaponContainer.transform);
                weaponsOnMapList.Add(consumableInstantiated);
                
                //Debug.Log("Cube spawned at timestamp: " + Time.time); //prints a timestamp for when the prefab is spawnes since start
                yield return new WaitForSeconds(waitTime); //yield on a new YieldInstruction that waits for 5 seconds.
            }
        }

        //Stuff
        private void RemoveNullFromList(List<GameObject> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i] == null) 
                {
                    
                    list.Remove(list[i]);
                }
            }
        }
    }
}