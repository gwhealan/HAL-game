using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock
{
    public int dMin, dSec;

    private int minute = 1;
    private float second = 0f;

    public Clock(float time = 0f, int deltaMin, float deltaSec)
    {
        dMin = deltaMin;
        dSec = deltaSec;
        if (time > 0)
            AddTime(time);
    }

    public int GetTime()
    {
        return minute;
    }

    public void AddTime(float dTime)
    {
        dTime /= dSec;

        float tFloat = (float)minute + deltaT + second;
        int tInt = Mathf.FloorToInt(tFloat);
        int min = tInt;
        while (min >= dMin)
            min -= dMin;
        second = tFloat - tInt;
        minute = min;
    }
}

public class CrewmateScheduler : MonoBehaviour
{
    // Inspector Stuff
    [SerializeField]
    private int crewmemberCount = 4;
    public int CrewmemberCount {
        get { return crewmemberCount; } set;
    }
    [SerializeField]
    private int monthBlocks = 1;
    [SerializeField]
    private int dayBlocks = 48;
    public float blockTime = 1f;

    // Schedule Stuff
    private Task[] schedules;

    // Update Stuff
    private Clock timer;
    private PriorityQueue eventQueue;

    // Scheduler Helper Functions
    private Task _getTask(int crewID, Clock timer, out int duration)
    {
        _getTask t = schedules[crewID * dayBlocks + minute];
        while()
        return 
    }

    private void _setTask(int crewID, Clock timer, Task task, int duration = 1)
    {
        for(int dT = 0; dT < duration; dT++)
            schedules[crewID * dayBlocks + time + dT] = task;
    }

    // Timing Helper Functions
    private int _modTime(int time, int mod)
    {
        while (time >= mod)
            time -= mod;
        return time;
    }

    private void _updateTime(float deltaT)
    {
        deltaT /= blockTime;

        float tFloat = (float)time + deltaT + secondsTime;
        int tInt = Mathf.FloorToInt(tFloat);
        secondsTime = tFloat - tInt;
        time = _modTime(tInt, monthBlocks * dayBlocks);
    }
    // Start is called before the first frame update
    void Start()
    {
        eventQueue = new PriorityQueue<int, int>(); // crewID, time
        
        int blockCount = crewmemberCount * dayBlocks;
        schedules = new Task[blockCount];
        ClearSchedule();
    }

    // Update is called once per frame
    void Update()
    {
        _updateTime(ref localTime, time.deltaTime);
        while (localTime <= eventQueue.Peek()) {
            int crewID = eventQueue.Dequeue();
            int duration = 1;
            Task t = _getTask(crewID, localTime);
            while (t == _getTask(crewID, _modTime((float)localTime + duration), dayBlocks) && duration < dayBlocks)
                duration++;

        }
        // _updateTime(ref localTime, time.deltaTime);
    }
    void ClearSchedule()
    {
        for (int i = 0; i < monthBlocks * dayBlocks; i++)
        {
            schedules[i] = Task.relax;
        }
    }

    void SetFullSchedule(Task[] schedule)
    {
        if (schedule.length == dayBlocks * crewmemberCount)
        {
            this.schedules = schedule;
        }
        throw Exception("Error: Full schedule is {0} blocks, not {1} blocks.".format(dayBlocks * crewmemberCount, schedule.length));
    }

    void SetCrewSchedule(int crewID, Task[] schedule)
    {
        if (schedule.Length == dayBlocks)
        {
            for (int i = 0; i < dayBlocks; i++)
            {
                _setTask(crewID, i, schedule[i]);
            }
        }
        throw Exception("Error: Crew schedule is {0} blocks, not {1} blocks.".format(dayBlocks, schedule.length));
    }

    Task[] GetFullSchedule() 
    {
        return schedules;
    }

    Task[] GetCrewSchedule(int crewID)
    {
        Task ret = new Task[dayBlocks];
        for (int i = 0; i < dayBlocks; i++) {
            ret[i] = _getTask(crewID, i);
        }
        return ret;
    }
*/
}
