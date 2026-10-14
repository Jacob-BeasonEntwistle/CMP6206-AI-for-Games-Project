using UnityEngine;

// This is a Finite State Machine AI agent

public enum State { Wander, Land, FindFood }

public class PigeonAI : MonoBehaviour
{
    public State currentState = State.Wander;

    private Vector3 destination;

    private float speed = 2f;

    private float hunger = 100f;

    public Transform foodTarget;

    private void Start()
    {
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

        if (hunger <= 70)
        {
            FindFood();
        }
    }

    public void Land()
    {
        Debug.Log("Pigeon landed");

        if (hunger <= 40)
        {
            FindFood();
        }
    }
    // Could have "hunger" which ticks down and gives pigeon a reason to land
    // Worm spotted, pigeon hunger bar a certain threshold, chase worm
    public void FindFood()
    {
        if (foodTarget == null)
        {
            foodTarget = GameObject.FindWithTag("Worm").transform;
        }
    }
}
