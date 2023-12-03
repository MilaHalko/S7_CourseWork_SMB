namespace CourseWorkApp.GenericAlgorithmCollector;

public enum names
{
    process1Queue,
    process2Queue,
    process3Queue,
    subProcesses1,
    subProcesses2,
    subProcesses3
}

public class DataVectorModel1
{
    public static Dictionary<names, (int min, int max)> bounds { get; set; } = new();
    public int[] parameters = new int[names.GetNames(typeof(names)).Length];

    public DataVectorModel1(int process1Queue, int process2Queue, int process3Queue, int subProcesses1,
        int subProcesses2, int subProcesses3)
    {
        parameters[(int)names.process1Queue] = process1Queue;
        parameters[(int)names.process2Queue] = process2Queue;
        parameters[(int)names.process3Queue] = process3Queue;
        parameters[(int)names.subProcesses1] = subProcesses1;
        parameters[(int)names.subProcesses2] = subProcesses2;
        parameters[(int)names.subProcesses3] = subProcesses3;
        SetBounds();
    }

    public static DataVectorModel1 Crossover(DataVectorModel1 firstParent, DataVectorModel1 secondParent)
    {
        var child = new DataVectorModel1(0, 0, 0, 0, 0, 0);
        for (var i = 0; i < firstParent.parameters.Length; i++)
        {
            if (Random.Shared.NextDouble() < 0.5) // first parent wins
                child.parameters[i] = firstParent.parameters[i];
            else // second parent wins
                child.parameters[i] = secondParent.parameters[i];
        }

        return child;
    }

    public void Mutate()
    {
        var randomParameterIndex = Random.Shared.Next(0, parameters.Length);
        parameters[randomParameterIndex] += Random.Shared.Next(-2, 2);
    }

    public static DataVectorModel1 NewRandomVector()
    {
        DataVectorModel1 vector = new(0, 0, 0, 0, 0, 0);
        for (int i = 0; i < vector.parameters.Length; i++)
            vector.parameters[i] = Random.Shared.Next(bounds[(names)i].min, bounds[(names)i].max);
        return vector;
    }
    
    public static bool CheckAlive(DataVectorModel1 vector)
    {
        for (int i = 0; i < vector.parameters.Length; i++)
        {
            if (vector.parameters[i] < bounds[(names)i].min || vector.parameters[i] > bounds[(names)i].max)
                return false;
        }

        return true;
    }


    private static void SetBounds()
    {
        bounds.Add(names.process1Queue, (0, 5));
        bounds.Add(names.process2Queue, (0, 5));
        bounds.Add(names.process3Queue, (0, 5));
        bounds.Add(names.subProcesses1, (0, 3));
        bounds.Add(names.subProcesses2, (0, 3));
        bounds.Add(names.subProcesses3, (0, 3));
    }
}