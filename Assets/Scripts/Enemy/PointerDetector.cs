using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerDetector : MonoBehaviour
{
    [SerializeField] private GameObject marker;

    private bool isHovered = false;

    void Update()
    {
        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform raycast and check if it hits a GameObject
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log($"Mouse is over {gameObject.name}");
            // Check if the hit GameObject is the one you're interested in
            if (hit.collider.gameObject != gameObject)
            {
                isHovered = false;
            }
            else
            {
                isHovered = true;
            }
        }
    }

    private void LateUpdate()
    {
        ToggleMarker();
    }

    void ToggleMarker()
    {
        marker.SetActive(isHovered); 
    }
}
