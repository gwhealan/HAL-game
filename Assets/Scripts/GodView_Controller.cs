using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GodView_Controller : MonoBehaviour
{
    // BUTTON MOVEMENT PARAM.

    public float movementReductionPerSecond = 0.5f;

    public float buttonMoveMultX = 1000f;
    public float buttonMoveMultY = 1000f;

    // MOUSE MOVEMENT PARAM.

    // invert mouse values (-1:invert or 1:regular folks)
    public int invertX = 1;
    public int invertY = 1;

    public float mouseMoveMultX = 0.5f;
    public float mouseMoveMultY = 0.5f;

    // ZOOM

    public float scaleDelta = 10f;

    // BOUNDS

    [SerializeField]
    public float XBound {
        get { return XExtent * 2; }
        set { 
            XExtent = value / 2;
            SetBound();
        }
    }
    [SerializeField]
    private float XExtent = 50f;

    [SerializeField]
    public float YBound {
        get { return YExtent * 2; }
        set { YExtent = value / 2; }
    }
    [SerializeField]
    private float YExtent = 28.125f;

    //PRIVATE

    static float minZBound = 25f;

    Bounds cameraBound = new Bounds();

    //Vector3 movementV = new Vector3(); // For WASD movement
    // Start is called before the first frame update
    void Start()
    {
        //movementV = new Vector3();
        Cursor.visible = true;

        SetBound();

        transform.position = cameraBound.ClosestPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void SetBound() 
    {
        float sqrt3 = Mathf.Sqrt(3);

        float ZExtent = (XExtent * sqrt3) / 2; // use XExtent since larger on camera (16:9, 4:3)

        cameraBound.center = new Vector3(0, ZExtent + minZBound, 0);
        cameraBound.extents = new Vector3(XExtent, ZExtent, YExtent);
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
            inputV.z += invertY * Input.GetAxis("Mouse Y") * mouseMoveMultY;
        }
        else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if(Input.GetKey(KeyCode.W)) {
                inputV.z += 1;
            }
            if (Input.GetKey(KeyCode.S)) {
                inputV.z -= 1;
            }
            if (Input.GetKey(KeyCode.A)) {
                inputV.x -= 1;
            }
            if (Input.GetKey(KeyCode.D)) {
                inputV.x += 1;
            }

            inputV = inputV.normalized;
            inputV.x *= buttonMoveMultX;
            inputV.z *= buttonMoveMultY;
        }

        //scale

        inputV.y = -1 * Input.GetAxis("Mouse ScrollWheel") * scaleDelta * 1000;

        transform.position = cameraBound.ClosestPoint(transform.position + inputV * Time.deltaTime);
    }
}
