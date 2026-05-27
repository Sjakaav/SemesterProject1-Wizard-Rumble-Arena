using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private float maxSize = 2f; // Max size of the object
    private float growFactor = 10f; //float for how fast the object grow
    private float waitTime = 2f; //float for how long it wait before we detroy the object after it spawns
     
    void Start()
    {
         StartCoroutine(Scale()); // starts the corountine that upscales the spawnAnimation
    }
     
    IEnumerator Scale() //corountine that upscales the spawnAnimation
    {
             while(maxSize > transform.localScale.x)
             {
                 //scales the object where is thinks the objects scale starts at (1,1,1)
                 transform.localScale += new Vector3(1, 1, 1) * (growFactor * Time.deltaTime);
                 yield return null; //waits for next frame to continue the loop
             }
             yield return new WaitForSeconds(waitTime); //wait x amount of seconds before destroying the object
             Destroy(gameObject); //destroys the object
    }   
}
