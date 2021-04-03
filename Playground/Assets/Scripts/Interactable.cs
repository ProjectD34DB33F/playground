using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= radius)
        {
            Interact();
        }
    }
}