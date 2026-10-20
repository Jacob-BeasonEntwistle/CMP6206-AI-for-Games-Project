using UnityEngine;

// This is a Finite State Machine AI agent

public enum State { Wander, Land, FindFood }

public class PigeonAI : MonoBehaviour
{
    public State currentState = State.Wander;

    private Vector3 destination;

    private float speed = 2f;

    private float maxHunger = 100f;
    private float hunger;
    private float hungryThreshold = 40f;
    private bool reachedFood = false;

    private bool isTired = false;
    private bool isRested = true;
    private bool isGrounded = false;
    float energyTimer;
    float restTimer;


    public Transform foodTarget;

    public GameObject ground;

    private void Start()
    {
        hunger = maxHunger;

        if (foodTarget == null)
        {
            foodTarget = GameObject.FindWithTag("Worm").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Wander:
                Wander();
                break;
            case State.Land:
                Land();
                break;
            case State.FindFood:
                FindFood();
                break;
        }

        hunger -= 5 * Time.deltaTime;
        Debug.Log(hunger);

        energyTimer += Time.deltaTime;
        if (energyTimer >= 40f)
        {
            isTired = true;
            isRested = false;
            energyTimer = 0f;
        }

        if (isTired && currentState == State.Land)
        {
            restTimer += Time.deltaTime;
            if (restTimer >= 20f)
            {
                isTired = false;
                isRested = true;
                restTimer = 0f;
            }
        }
    }

    private Vector3 RandomPoint()
    {
        // Set the destination to a random point in a zone
        return new Vector3(
            Random.Range(-15.0f, 15.0f), 
            Random.Range(0.5f, 20.0f), 
            Random.Range(-24.0f, 24.0f)
        );
    }

    public void Wander()
    {
        // If the distance between the current position and the destination position is less that 0.1
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            // Pick a new random destination
            destination = RandomPoint();
        }

        // Move the pigeon towards the chosen destination at the set speed
        transform.position = Vector3.MoveTowards(
            transform.position,
            destination,
            speed * Time.deltaTime
        );

        // If the hunger level is below the "hungry" threshold
        if (hunger <= hungryThreshold)
        {
            // Change the state to FindFood
            currentState = State.FindFood;
        }

        // If becomes tired
        if (isTired)
        {
            // Change current state to Land
            currentState = State.Land;
        }
    }

    public void Land()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            ground.transform.position,
            speed * Time.deltaTime
        );

        if (isGrounded)
        {
            StayOnGround();
        }

        Debug.Log("Pigeon landed");

        if (hunger <= 40)
        {
            currentState = State.FindFood;
        }

        else if (isRested)
        {
            currentState = State.Wander;
        }
    }

    private void StayOnGround()
    {
        Vector3 walkToPoint = RandomPoint();

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(walkToPoint.x, 0.5f, walkToPoint.z),
            speed * Time.deltaTime
        );
    }

    // Could have "hunger" which ticks down and gives pigeon a reason to land
    // Worm spotted, pigeon hunger bar a certain threshold, chase worm
    public void FindFood()
    {
        if (foodTarget == null)
        {
            foodTarget = GameObject.FindWithTag("Worm").transform;
        }

        // MoveTowardsFood();

        if (reachedFood)
        {
            Eat();
        }

        if (hunger >= hungryThreshold)
        {
            currentState = State.Wander;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worm"))
        {
            Debug.Log("PigeonAI is near worm");
            reachedFood = true;
        }

        if (other.CompareTag("Ground"))
        {
            Debug.Log("PigeonAI is on the ground");
            isGrounded = true;
        }
    }

    private void Eat()
    {
        hunger += 50f;

        if (hunger >= maxHunger)
        {
            hunger = maxHunger;
        }
    }
}
