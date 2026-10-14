using UnityEngine;

// This is a Finite State Machine AI agent

public class Pigeon : MonoBehaviour
{
    private Vector3 destination;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Wander();
        }
    }

    private Vector3 FlyTo()
    {
        destination = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));

        if (destination.y < 0.05)
        {
            return FlyTo();
        }
        if (destination.y > 0.75 && destination.y < 2)
        {
            Land();
            return destination;
        }
        else
        {
            return destination;
        }
    }

    public void Wander()
    {
        transform.position = FlyTo();
    }
    public void Land()
    {
        Debug.Log("Pigeon landed");
    }
}
