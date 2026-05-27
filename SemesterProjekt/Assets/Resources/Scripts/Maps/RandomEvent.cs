using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEvent : MonoBehaviour
{
    Vector3 originalPos; // Original position of object

    public float counter; // Count down timer for object to move
    public float resetCounter; // Time added to coroutine "ResetCounter"
    public float speed = 10f; // Object movement speed
    
    // Selectable object movement directions
    public bool up;
    public bool down;
    public bool right;
    public bool left;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = new Vector3(transform.position.x, transform.position.y, transform.position.z); // Sets original start position of object on play
        counter = Random.Range(10f, 30f); // Sets count down timer to random number between 10 and 30 seconds 
        StartCoroutine(ResetCounter()); // reset counter for object to be reset after "x" seconds
    }

    // Update is called once per frame
    void Update()
    {
        if (counter >= 0f) // Checks if counter is greater than 0 and counts down if true
        {
            counter -= 1f * Time.deltaTime;
        }

        
        if (up == true && counter <= 0f) // Checks if counter is less than 0 and if "up" direction true(toggled in inspector)
        {
            MoveObjectUp();
        }

        if (down == true && counter <= 0f) // Checks if counter is less than 0 and if "down" direction true(toggled in inspector)
        {
            MoveObjectDown();
        }

        if (right == true && counter <= 0f) // Checks if counter is less than 0 and if "right" direction true(toggled in inspector)
        {
            MoveObjectRight();
        }

        if (left == true && counter <= 0f) // Checks if counter is less than 0 and if "left" direction true(toggled in inspector)
        {
            MoveObjectLeft();
        }
    }

    IEnumerator ResetCounter() // reset counter for object to be reset after "x" seconds
    {
        while (true)
        {
            yield return new WaitForSeconds(counter + resetCounter); // wait for given time plus reset time
            transform.position = originalPos; // sets object back to original position after reset time
            counter = Random.Range(10f, 30f); // resets counter to new random number between 10 and 30 seconds
        }
    }

    void MoveObjectUp()
    {
        transform.position = transform.position + new Vector3(0, speed, 0) * Time.deltaTime;
    }

    void MoveObjectDown()
    {
        transform.position = transform.position + new Vector3(0, -speed, 0) * Time.deltaTime;
    }

    void MoveObjectRight()
    {
        transform.position = transform.position + new Vector3(speed, 0, 0) * Time.deltaTime;
    }

    void MoveObjectLeft()
    {
        transform.position = transform.position + new Vector3(-speed, 0, 0) * Time.deltaTime;
    }

}
