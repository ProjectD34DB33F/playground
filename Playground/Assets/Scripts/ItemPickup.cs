using UnityEngine;

public class ItemPickup : Interactable
{
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picked up item");
        Destroy(gameObject);
    }
}