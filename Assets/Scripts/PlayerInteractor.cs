using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour {

    public float interactionRadius = 2f;

    //private SphereCollider sphereCollider;

    public LayerMask interactableLayerId;

    //public void GetClosestInteractable(Vector3 clickLocation)
    //{
    //
    //
    //}

    public bool IsInteractableInRange(Vector3 playerPosition, Vector3 interactablePosition)
    {
        if ((Vector3.Distance(playerPosition, interactablePosition)) > interactionRadius)
        {
            return false;
        }
        return true;
    }

    public void InteractWithItem(GameObject interactableItemGO, bool isInRange)
    {
        Item item = interactableItemGO.GetComponent<ItemOnGround>().item;
        if (interactableItemGO != null && isInRange)
        {
            //if (Inventory.instance.items.Count >= Inventory.instance.space)
            //{
            //    Debug.Log("No room for " + item.name);
            //    return;
            //}

            if (Inventory.instance.AddItem(item))
            {
                Debug.Log("Picking up " + item.name);
                Destroy(interactableItemGO);
            }
        }

    }
}
