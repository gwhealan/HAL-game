using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodView_Controller : MonoBehaviour
{
    //private float sqrt3 = 1.73205080757f;

    // BUTTON MOVEMENT PARAM.
    [Range(0.1f,1000f)]
    public float buttonMoveMultX = 1000f;
    [Range(0.1f, 1000f)]
    public float buttonMoveMultY = 1000f;

    // MOUSE MOVEMENT PARAM.

    // invert mouse values (-1:invert or 1:regular folks)
    [Tooltip("Multiplier for camera (Set to -1 for inverted, 1 for regular)")]
    public int invertX = 1;
    [Tooltip("Multiplier for camera (Set to -1 for inverted, 1 for regular)")]
    public int invertY = 1;

    [Range(0.1f,100f)]
    public float mouseMoveMultX = 0.5f;
    [Range(0.1f, 100f)]
    public float mouseMoveMultY = 0.5f;
    [Range(1f, 10f)]
    public float scaleDelta = 2f;

    private MeshCollider bound;
    private Vector3 lastDirection;

    // BOUNDS

    //Vector3 movementV = new Vector3(); // For WASD movement
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;

        bound = transform.parent.gameObject.GetComponent<MeshCollider>();

        transform.position = new Vector3(0, 25f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition() 
    {

        // planer movement

        Vector3 inputV = new Vector3();
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            inputV.x += invertX * Input.GetAxis("Mouse X") * mouseMoveMultX;
            inputV.y += invertY * Input.GetAxis("Mouse Y") * mouseMoveMultY;
        }
        else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if(Input.GetKey(KeyCode.W)) {
                inputV.y += 1;
            }
            if (Input.GetKey(KeyCode.S)) {
                inputV.y -= 1;
            }
            if (Input.GetKey(KeyCode.A)) {
                inputV.x -= 1;
            }
            if (Input.GetKey(KeyCode.D)) {
                inputV.x += 1;
            }

            inputV = inputV.normalized;
            inputV.x *= buttonMoveMultX;
            inputV.y *= buttonMoveMultY;
        }

        //scale

        inputV.z = -1 * Input.GetAxis("Mouse ScrollWheel") * scaleDelta * 1000;

        lastDirection = inputV;
        transform.position = bound.ClosestPoint(transform.position + transform.TransformDirection(inputV * Time.deltaTime));
    }
    /*
    void OnTriggerEnter(Collider other) {
        if (other == bound) {
            transform.position = bound.ClosestPoint(transform.position);
            /*
            for (int i = 0; i < 10; i++) {
                switch (i) {
                    case 0:
                        check.direction = Vector3.right;
                        break;
                    case 1:
                        check.direction = Vector3.forward;
                        break;
                    case 2:
                        check.direction = Vector3.left;
                        break;
                    case 3:
                        check.direction = Vector3.back;
                        break;
                    case 4:
                        check.direction = new Vector3(1, 0, 1);
                        break;
                    case 5:
                        check.direction = new Vector3(1, 0, -1);
                        break;
                    case 6:
                        check.direction = new Vector3(-1, 0, -1);
                        break;
                    case 7:
                        check.dircetion = new Vector3(-1, 0, 1);
                        break;
                    case 8:
                        check.direction = Vector3.up;
                        break;
                    case 9:
                        check.direction = Vector3.down;
                        break;
                }
                if (bound.Raycast()) { 
                
                }
            }
            
        }
    }
    */
}
