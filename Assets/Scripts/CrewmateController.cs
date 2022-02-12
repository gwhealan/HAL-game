using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrewmateController : MonoBehaviour
{
    public Job job;
    public float crewID = 0f;
    public float walkSpeed = 10f;
    public float runSpeed = 20f;
    public float minIdleInterval = 1f;
    public float maxIdleInterval = 10f;
    [HideInInspector]
    public Bounds idleBound = new Bounds(Vector3.zero, new Vector3(5f,0f,5f));

    //private out CrewmateSchedule schedule;
    private Action action = Action.idle;
    // psychy
    private float stress = 0f, aiTrust = 1f;
    private NavMeshAgent agent;
    private float actionInterval = 0f;
    
    //Schedule Data
    public int scheduleBlocks = 48;
    public float timePerBlock = 1.5f;

    CrewmateSchedule schedule;
    public List<RoomQueue>[] roomAccess;
    //public RoomQueue currRoom;
    float time = 0f;
    float nextTask = 0f;
    Task task;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        schedule = new CrewmateSchedule(scheduleBlocks);

        InitiateSchedule();
        InitiateRoomQueues();
    }

    private void InitiateSchedule() 
    {
        for (int i = 0; i < 10; i++)
        {
            Task t = (Task)Mathf.FloorToInt(Random.Range(0f, 4f));
            int startTime = Mathf.FloorToInt(Random.Range(0f, scheduleBlocks));
            int duration = Mathf.FloorToInt(Random.Range(1f, scheduleBlocks));
            //Debug.Log(string.Format("Insert: {0} {1} {2}", t, startTime, duration));
            schedule.InsertTask(t, startTime, duration);
            //Debug.Log(test.ToString());
        }
        Debug.Log(schedule.ToString());
    }

    private void InitiateRoomQueues()
    {
        int totRooms = System.Enum.GetNames(typeof(RoomID)).Length;
        roomAccess = new List<RoomQueue>[totRooms];
        Transform roomsSrc = GameObject.Find("Rooms").transform;

        RoomQueue room;
        int initRoomCount = 0;
        for (int i = 0; i < roomsSrc.childCount; i++) 
        { 
            room = roomsSrc.GetChild(i).gameObject.GetComponent<RoomQueue>();
            int roomID = (int)room.roomID;
            if(roomAccess[roomID] == null) {
                initRoomCount++;
                roomAccess[roomID] = new List<RoomQueue>();
            }
            roomAccess[roomID].Add(room);
        }
        if (initRoomCount < totRooms)
        {
            Debug.Log("Error: Missing room access.", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //stress += 0.01f * Time.deltaTime;
        time += Time.deltaTime / timePerBlock;
        if (nextTask <= time)
        {
            nextTask = (float)schedule.UpdateTask(Mathf.FloorToInt(time), out task);
            RoomID nextRoom = TaskToRoom(task);
            //RoomQueue roomQ;
            foreach (RoomQueue roomQ in roomAccess[(int)nextRoom]) {
                if (roomQ.IsOpen())
                {
                    agent.destination = roomQ.Access();
                    break;
                }
            }

            Debug.Log(string.Format("Time: {0}; Doing {1} until {2} sec.", time, task, nextTask));
        }
    }

    void LateUpdate()
    {
        /*
        switch (action)
        {
            case Action.idle:
                if (actionInterval <= 0f)
                {
                    agent.destination = _getPointInBound();
                    actionInterval = Random.Range(minIdleInterval, maxIdleInterval);
                }
                break;
            default:
                break;
        }
        */
        agent.speed = walkSpeed + (runSpeed - walkSpeed) * stress;
        actionInterval -= Time.deltaTime;
    }

    private RoomID TaskToRoom(Task taskID) {
        switch (taskID) {
            case Task.work:
                switch (job) {
                    case Job.engineer:
                        return RoomID.engineering;
                    case Job.doctor:
                        return RoomID.science;
                    default:
                        return RoomID.living;
                }
            case Task.exercise:
                return RoomID.gym;
            case Task.sleep:
                return RoomID.quarters;
            default:
                return RoomID.living;
        }
    }

    private Vector3 _getPointInBound()
    {
        float x = idleBound.center.x + Random.Range(-idleBound.extents.x, idleBound.extents.x);
        float z = idleBound.center.z + Random.Range(-idleBound.extents.z, idleBound.extents.z);

        return new Vector3(x, 0, z);
    }
}
