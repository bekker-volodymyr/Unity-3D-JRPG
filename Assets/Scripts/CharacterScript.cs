using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private float speed = 300f;
    private CharacterController _characterController;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");
        
        if(Mathf.Abs(dx) > 0 && Mathf.Abs(dy) > 0)
        {
            dx *= 0.707f; // /= Mathf.Sqrt(2f);
            dy *= 0.707f; // /= Mathf.Sqrt(2f);
        }

        _characterController.SimpleMove(speed * Time.deltaTime * (dx * Camera.main.transform.right + dy * Camera.main.transform.forward));          // new Vector3(dx, 0, dy));
    }
}
