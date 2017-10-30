using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController {

    private float _yOffset = 0.61f;

    public LayerMask interactableLayerId;

    public Quaternion GetPlayerRotation(Vector3 mousePos, Vector3 currentPosition)
    {
        Vector3 direction = mousePos - currentPosition;
        direction.y += _yOffset;
        Quaternion rotation = Quaternion.LookRotation(direction);
        return rotation;
    }

    public Vector3 GetMouseClickPosition(Vector3 mousePos)
    {
        Vector3 _newTargetPosition = GetMouseGroundPosition(mousePos);
        _newTargetPosition.y = _yOffset;
        return _newTargetPosition;
    }

    public Vector3 GetMouseGroundPosition(Vector3 mousePos)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        float rayLength = (mouseRay.origin.y / mouseRay.direction.y);
        return mouseRay.origin - (mouseRay.direction * rayLength);
    }

    public GameObject GetInteractableGO(Vector3 mousePos)
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hitInfo;

        int layerMask = interactableLayerId.value;
        if (Physics.Raycast(mouseRay, out hitInfo, Mathf.Infinity, layerMask))
        {
            GameObject interactableGO = hitInfo.rigidbody.gameObject;
            return interactableGO;
        }
        return null;
    }

}
