using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action
{
    move,
    idle,
    task,
    mutiny
}
public enum Job
{
    captain,
    doctor,
    engineer
}

public enum Task
{
    work,
    exercise,
    relax,
    sleep
}

public enum RoomID
{
    quarters,
    living,
    engineering,
    science,
    gym
}

/*
public class CrewmateSchedule
{
    private struct TaskCard
    {
        public Task task;
        public int startTime;
        public int duration;

        public TaskCard(Task task, int startTime, int duration = 1)
        {
            this.task = task;
            this.startTime = startTime;
            this.duration = duration;
        }
    }

    private List<TaskCard> schedule;
    private int index = 0;

    public CrewmateSchedule()
    {
        ClearSchedule();
    }

    public void ClearSchedule()
    {
        schedule = new List<TaskCard>();
        schedule.Add(new TaskCard(Task.sleep, 46, 14));
        schedule.Add(new TaskCard(Task.relax, 12, 34));
    }

    public void InsertTask(Task task, float time, float dtime)
    {
        int t1 = _mod(Mathf.FloorToInt(time), 48);
        int dT = _mod(Mathf.FloorToInt(dtime), 48);
        int t2 = _mod(t1 + dT, 48);

        // find intersect with t1
        int i1 = _searchInSchedule(t1);
        // find intersect with t2
        int i2 = _searchInSchedule(t2);

        //Debug.Log(string.Format("New Task: {0} {1} {2}\nInserted Between: {3} {4}", task, t1, t2, i1, i2));

        TaskCard e1 = schedule[i1], e2 = schedule[i2];
        e1.duration = _modDistance(e1.startTime, t1);
        int e2_duration = _modDistance(t2, e2.startTime + e2.duration);
        if (_modDistance(t2, t1) < e2_duration) {
            e2_duration = _modDistance(t2, t1);
        }
        e2.duration = e2_duration;
        e2.startTime = t2;

        if (dT < schedule[i1].duration) //Split element
        {
            schedule[i1] = e1;
            //Debug.Log("Split Middle");
            if (i1 + 1 == schedule.Count)
            {
                schedule.Add(e2);
            }
            else
            {
                schedule.Insert(i1 + 1, e2);
            }
        }
        else
        {
            schedule[i1] = e1;
            schedule[i2] = e2;
            if (i1 < i2) // remove internal
            {
                //Debug.Log("remove internal");
                for (int i = i1 + 1; i < i2; i++)
                {
                    //Debug.Log(string.Format("Removing at {0} given {1}\n{2}", i1 + 1, schedule.Count, ToString()));
                    schedule.RemoveAt(i1 + 1);
                }
            }
            else // remove external
            {
                //Debug.Log("Remove External");
                for (int i = schedule.Count - 1; i > i1; i--)
                {
                    //Debug.Log(string.Format("Removing at {0} given {1}\n{2}", schedule.Count - 1, schedule.Count, ToString()));
                    schedule.RemoveAt(schedule.Count - 1);
                }
                for (int i = 0; i < i2; i++)
                {
                    //Debug.Log(string.Format("Removing at {0} given {1}\n{2}", 0, schedule.Count, ToString()));
                    schedule.RemoveAt(0);
                }
                i1 = -1; // offset removal
            }
        }

        // insert new
        if (i1 + 1 == schedule.Count)
        {
            schedule.Add(new TaskCard(task, t1, dT));
        }
        else
        {
            schedule.Insert(i1 + 1, new TaskCard(task, t1, dT));
        }
    }

    public bool UpdateTask(float time, ref Task task, ref float dtime)
    {
        int localTime = _mod(Mathf.FloorToInt(time), 48);
        int test = _checkInTaskCard(schedule[index], localTime);
        while (test == -1) {
            index++;
            _checkInTaskCard(schedule[index], localTime);
        }
        if (task == schedule[task])
        if (task != schedule[task]) {
            task = schedule[task];

        }
    }

    public string ToString() {
        string ret = "";

        foreach (TaskCard tC in schedule) {
            ret += string.Format("{0}: {1}, {2} ({3}) | ", tC.task, tC.startTime, _mod(tC.startTime + tC.duration, 48), tC.duration);
        }
        return ret;
    }

    private int _checkInTaskCard(TaskCard task, int time)
    {
        if (task.startTime > time) // correct overflow
            time += 48;
        int ret;
        if (time == task.startTime)
        {
            ret = 0;
        }
        else if (time > task.startTime && time < task.startTime + task.duration)
        {
            ret = 1;
        }
        else
        {
            ret = -1;
        }
        //Debug.Log(string.Format(" check: {0} {1} {2} = {3}", task.startTime, time, task.duration, ret));
        return ret;
    }

    private int _searchInSchedule(int time) {
        int ret = -1;
        for (int i = 0; i < schedule.Count; i++) {
            if (_checkInTaskCard(schedule[i], time) != -1) {
                ret = i;
            }
        }
        return ret;
    }

    private int _mod(int t, int n)
    {
        while (t >= n)
        {
            t -= n;
        }
        while (t < 0)
        {
            t += n;
        }
        return t;
    }

    private int _modDistance(int i, int j) // assumes i comes before j
    {
        i = _mod(i, 48);
        j = _mod(j, 48);
        if (j < i)
            j += 48;
        return j - i;
    }

    private int _abs(int v)
    {
        if (v < 0) v *= -1;
        return v;
    }
}
*/

public class CrewmateSchedule
{
    private Task[] schedule;
    // public int Size { get { return size; } private set; }
    private int size;

    public CrewmateSchedule(int size)
    {
        schedule = new Task[size];
        this.size = size;
        ClearSchedule();
    }

    public void ClearSchedule()
    {
        for (int i = 0; i < size; i++)
        {
            schedule[i] = Task.relax;
        }
    }

    public int UpdateTask(int time, out Task task)
    {
        int modTime = _modTime(time);
        Task t = schedule[modTime];

        int offset = 0;
        while (schedule[_modTime(modTime + offset)] == t && offset < size) offset++;

        task = t;
        return time + offset;
    }

    public void InsertTask(Task task, int startTime, int duration)
    {
        startTime = _modTime(startTime);
        duration = _modTime(duration);
        for (int i = startTime; i < duration; i++)
        {
            schedule[_modTime(i)] = task;
        }
    }

    public Task[] GetSchedule()
    {
        return schedule;
    }

    public string ToString()
    {
        string ret = "";

        int duration = 1;
        Task curr = schedule[0];
        for (int t = 1; t < size; t++)
        {
            if (schedule[t] == curr)
            {
                duration += 1;
            }
            else
            {
                ret += string.Format("| {0}: {1}-{2} ", curr, t - duration, t);
                curr = schedule[t];
                duration = 1;
            }
        }
        ret += "|";
        return ret;
    }

    private int _modTime(int time)
    {
        while (time < 0)
            time += size;
        while (time >= size)
            time -= size;
        return time;
    }
}
