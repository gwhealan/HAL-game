using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseQueue : MonoBehaviour
{
    public RoomID roomID = RoomID.living;

    private Vector3[] stations;
    private LinkedList<int> openStations;
    private Dictionary<int, int> closedStations;
    protected BoxCollider roomBound;

    public void Start()
    {
        roomBound = transform.gameObject.GetComponent<BoxCollider>();

        InitiateQueue();
    }

    public void InitiateQueue() 
    {
        //Initiate Objects
        stations = new Vector3[transform.childCount];
        openStations = new LinkedList<int>();
        closedStations = new Dictionary<int, int>();
        //Add values
        if (transform.child > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                stations[i] = child.position;
                openStations.AddLast(i);
            }
        }
        else
        {
            openStations.AddLast(transform.position);
        }
    }

    public Vector3 Access(out BoxCollider enterBox)
    {
        enterBox = roomBound;
        return transform.position;
    }

    public void Enter(ref NavMeshAgent controller)
    {
        return transform.position;
    }

    public void Leave(ref NavMeshAgent controller)
    { 
    
    }

    public bool IsOpen()
    {
        return true;
    }

    protected Vector3 
}

/*
public class RoomQueue : MonoBehaviour
{
    private struct WaitCard
    {
        NavMeshAgent controller;
        float time;
    }
    public RoomID roomID;
    public float wanderMinInterval = 1f;
    public float wanderMaxInterval = 5f;
    public float minStationUse = 60f;
    public float maxStationUse = 120f;
    // Queue
    [SerializeField]
    private Bound overflowBox;
    private List<WaitCard> queue = new List<WaitCard>();

    // Stations
    private int stationCount;
    private Vector3[] stationPos;
    private WaitCard[] stations;
    private bool[] stationOpen;
    // Start is called before the first frame update
    void Start()
    {
        stationCount = transform.childCount;
        if (stationCount == 0) {
            Debug.Log(string.Format("Room {0} does not contain any stations", roomID));
        }
        stationPos = new Vector3[stationCount];
        stations = new ScheduleCard[stationCount];
        stationOpen = new bool[stationCount];
        for (int i = 0; i < stationCount; i++) {
            stationPos[i] = transform.GetChild(i).position;
            stationOpen[i] = true;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    void Access(NavMeshAgent controller) {
        controller.destination = overflowBox.center;
    }

    bool Enter(NavMeshAgent controller) {
        for (int i = 0; i < stationCount; i++) {
            if (stationOpen[i]) {
                stationOpen[i] = false;
                stations[i] = controller;
                return true;
            }
        }
        queue.Add();
    }

    bool Leave(NavMeshAgent controller) { 
    
    }

    private Vector3 getBoundPoint() {
        float x = overflowBox.center.x + Random.Range(-overflowBox.extent.x, overflowBox.extent.x);
        float z = overflowBox.center.z + Random.Range(-overflowBox.extent.z, overflowBox.extent.z);

        return new Vector3(x,0,z);
    }
}
*/
