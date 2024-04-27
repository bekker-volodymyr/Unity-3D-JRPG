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

            if (Input.GetMouseButtonDown(0) && isHovered)
            {
                OnClick();
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

    void OnClick()
    {
        // Implement your click logic here
        Debug.Log("Object clicked: " + gameObject.name);
        GameManager.Instance.AttackEnded += OnPlayerAttackEnded;
        GameManager.Instance.Attack(gameObject);
    }

    private void OnPlayerAttackEnded()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Instance.AttackEnded -= OnPlayerAttackEnded;
    }
}
