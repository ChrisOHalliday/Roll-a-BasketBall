using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //collection of points that the platforms will travel too
    public Transform[] platformPoints = new Transform[2];
    
    //stores the previous and next destination as points in memory to be allocated and changed
    private Transform nextDestination, previousDestination;

    [SerializeField]
    private float platformMoveSpeed = 3.0f;

    /* the time elapsed since the beginng of platform travel. time expected for the platform to travel between given points
    calculated using the distance between the waypoints and the set speed of the platform */
    private float elapsedTime,expectedTime;

    private float timePlatformDelay = 2.0f;

    //value used to keep track of which point the platform will go to next
    private int destinationValue = 1;

    private void Start()
    {
        //function begins Moving the platform
        MovePlatform();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        
        //Moves platform from one point to another in smooth increments using the Lerp function. smoothstep slows down movement at each extreme 
        float completionPercentage = elapsedTime / expectedTime;
        completionPercentage = Mathf.SmoothStep(0,1,completionPercentage);
        transform.position = Vector3.Lerp(previousDestination.position, nextDestination.position, completionPercentage);

        //switches destination for platform once current destination has been reached.
        if (completionPercentage >= 1)
        {
            Debug.Log("Ive reached checkpoint " + destinationValue );
            
            timePlatformDelay -= Time.deltaTime;

            //moves platform again after short time delay
            if (timePlatformDelay <= 0)
            {
                timePlatformDelay = 2.0f;
                SwitchWayPoint();
                MovePlatform();
            }

        }

    }

     private void MovePlatform()
     {
        //resets elapsed time for future distance and increment movement calculations
        elapsedTime = 0.0f;


        if (destinationValue == 1)
        {
            previousDestination = platformPoints[0];
            nextDestination = platformPoints[1];
        }
        if (destinationValue == 0)
        {
            previousDestination = platformPoints[1];
            nextDestination = platformPoints[0];
        }


        float distanceToWaypoint = Vector3.Distance(previousDestination.position,nextDestination.position);

        //finds the amount of time it sould take for platform to reach destination at set speed
        expectedTime = distanceToWaypoint / platformMoveSpeed;
     }

    
    //Function to switch the destination value according to the current point the platform is located.
    private void SwitchWayPoint()
    {
        switch (destinationValue)
        {
            case 0:
                destinationValue = 1;
                break;
            case 1: 
                destinationValue = 0; 
                break;
            default:
                break;
        }
    }
   
}
