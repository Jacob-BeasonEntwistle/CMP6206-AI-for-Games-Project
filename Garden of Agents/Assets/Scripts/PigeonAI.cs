using JetBrains.Annotations;
using UnityEngine;

// This is a Finite State Machine AI agent

public enum State { None, Wander, Land, WanderOnFoot, FindFood }

public class PigeonAI : MonoBehaviour
{
    public State currentState;

    private Vector3 destination;
    private float groundLevel = 0.5f;

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

    private float debugTimer = 1f;
    private bool shownCurrentState = false;


    public Transform foodTarget;

    public GameObject ground;

    private void Start()
    {
        currentState = State.Wander;

        //hunger = maxHunger;
        hunger = 50f;

        // Doesn't require there to be a worm, but if there is, it sets it to the food target
        if (foodTarget == null)
        {
            GameObject worm = GameObject.FindWithTag("Worm");

            if (worm != null)
            {
                foodTarget = worm.transform;
            }
        }

        // Prevents the pigeon heading straight for (0, 0, 0) on game start
        destination = RandomPoint();
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
            case State.WanderOnFoot:
                WanderOnFoot();
                break;
            case State.FindFood:
                FindFood();
                break;
        }

        if (shownCurrentState == false)
        {
            Debug.Log($"[{this.name}] Current State: {currentState}");
            shownCurrentState = true;
        }

        hunger -= 1 * Time.deltaTime;

        debugTimer += Time.deltaTime;

        if (debugTimer >= 1f)
        {
            Debug.Log($"[{this.name}] Hunger Level: {(int)hunger}");
            debugTimer = 0f;
        }

        energyTimer += Time.deltaTime;
        if (energyTimer >= 40f)
        {
            Debug.Log($"[{this.name}]: Tired");

            isTired = true;
            isRested = false;
            energyTimer = 0f;
        }

        if (isTired && currentState == State.Land)
        {
            restTimer += Time.deltaTime;
            if (restTimer >= 20f)
            {
                Debug.Log($"[{this.name}]: Rested and ready");

                isTired = false;
                isRested = true;
                restTimer = 0f;
            }
        }
    }

    private Vector3 RandomPoint()
    {
        Debug.Log($"[{this.name}]: Random point selected");

        // Set the destination to a random point in a zone
        return new Vector3(
            Random.Range(-12.5f, 12.5f), 
            Random.Range(groundLevel, 20.0f), 
            Random.Range(-18f, 19f)
        );
    }

    // Turns the pigeon AI to face the direction it's flying in
    private void TurnToFace(Vector3 target)
    {
        // (transform.pos - destination because pigeon mesh is backwards)
        Quaternion rotation = Quaternion.LookRotation(transform.position - target, Vector3.up);
        transform.rotation = rotation;
        
        // Use this to slowly turn towards location - might be useful for realistic turning
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    public void Wander()
    {
        if (isGrounded)
        {
            isGrounded = false;
        }

        // If the distance between the current position and the destination position is less that 0.1
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            Debug.Log($"[{this.name}]: Reached location");

            // Pick a new random destination
            destination = RandomPoint();
        }

        TurnToFace(destination);

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
        Debug.Log($"[{this.name}]: landing");

        if (Vector3.Distance(transform.position, ground.transform.position) > 0.1f)
        {
            TurnToFace(ground.transform.position);

            transform.position = Vector3.MoveTowards(
                transform.position,
                ground.transform.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            Debug.Log($"[{this.name}]: landed");
            currentState = State.WanderOnFoot;
        }
    }

    public void WanderOnFoot()
    {
        if (!isGrounded)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            isGrounded = true;
        }

        // If the distance between the current position and the destination position is less that 0.1
        if (Vector3.Distance(transform.position, destination) < 0.1f)
        {
            destination = RandomPoint();
        }

        TurnToFace(destination);

        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(destination.x, groundLevel, destination.z),
            speed * Time.deltaTime
        );

        if (hunger <= hungryThreshold)
        {
            currentState = State.FindFood;
        }

        else if (isRested)
        {
            currentState = State.Wander;
        }
    }

    // Could have "hunger" which ticks down and gives pigeon a reason to land
    // Worm spotted, pigeon hunger bar a certain threshold, chase worm
    public void FindFood()
    {
        // Looks for worm but doesn't crash if there is none
        if (foodTarget == null)
        {
            GameObject worm = GameObject.FindWithTag("Worm");

            if (worm != null)
            {
                foodTarget = worm.transform;
            }
        }

        if (isGrounded)
        {
            TurnToFace(foodTarget.position);

            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(foodTarget.transform.position.x, groundLevel, foodTarget.transform.position.z),
                speed * Time.deltaTime
            );
        }
        else
        {
            currentState = State.Land;
        }

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
            Debug.Log($"[{this.name}]: is close enough to worm to eat");
            reachedFood = true;
        }
    }

    private void Eat()
    {
        hunger += 50f;

        Destroy(foodTarget.gameObject);

        foodTarget = null;

        Debug.Log($"[{this.name}]: ate a worm");

        if (hunger >= maxHunger)
        {
            hunger = maxHunger;
        }
    }
}
