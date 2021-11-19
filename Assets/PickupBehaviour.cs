using UnityEngine;
using UnityEngine.Events;

public class PickupBehaviour : MonoBehaviour
{
    public UnityEvent onPickup;
    public float minY;
    public float maxY;
    public float startX;

    public void StartPickup()
    {
        transform.position = new Vector3(startX, Random.Range(minY,maxY), transform.position.z);
        gameObject.SetActive(true);
    }

    public void HidePickup()
    {
        gameObject.SetActive(false);
    }

    public void OnPickedUp()
    {
        ResetPickup();
    }

    public void OnScoreUpdated(int score)
    {
        if (score == 1)
            StartPickup();
    }

    void Start()
    {
        startX = transform.position.x;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
            onPickup.Invoke();
    }

    //Whenever a flower is picked up, it reappears at a random Y at the opposite side of the screen.
    void ResetPickup()
    {
        Vector3 newPos = new Vector3(-transform.position.x, Random.Range(minY,maxY), transform.position.z);
        transform.position = newPos;
    }
}
