using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Animator anim;

    bool moving, attacking, damaged;


    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f, desiredRotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!attacking && !damaged)
        {
            Move();
        }
    }

    void Move()
    {

        float cycles = Time.time / period;  // continually growing over time

        const float tau = Mathf.PI * 2;  // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau);  // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;   // recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;

        Vector3 lastPosition = transform.position;

        transform.position = startingPosition + offset;

        Vector3 direction = transform.position - lastPosition;
        direction.y = 0f;

        //Debug.Log("direction: " + direction);

        if (direction != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), desiredRotationSpeed);



    }
}
