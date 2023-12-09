using MassServiceModeling.Elements;
using MassServiceModeling.Model;
using MassServiceModeling.NextElement;

namespace CourseWorkApp.GeneticModels;

public class Model1 : IGeneticModel
{
    public enum Names
    {
        SubProcessCount1, MaxQueue1,
        SubProcessCount2, MaxQueue2,
        SubProcessCount3, MaxQueue3
    }
    
    private Model _model;
    public double SimulationTime;
    public Dictionary<Names, int> Data { get;}

    public Model1(Dictionary<Names, int> data, double simulationTime)
    {
        Data = data;
        _model = CreateModel(Data);
        SimulationTime = simulationTime;
    }

    public void Mutate()
    {
        while (true)
        {
            var parameterIndex = Random.Shared.Next(Data.Count);
            var adder = Random.Shared.Next(-1, 1);
            Data[Enum.GetValues<Names>()[parameterIndex]] += adder;
            if (DataIsValid(Data)) break;
            Data[Enum.GetValues<Names>()[parameterIndex]] -= adder;
        }

        UpdateSMOModel();
    }

    public void Simulate() => _model.Simulate(SimulationTime, printResult: false);
    private void UpdateSMOModel() => _model = CreateModel(Data);

    public static Model CreateModel(Dictionary<Names, int> data)
    {
        Create create = new(delay: 1);
        Process process1 = new(delay: 50, data[Names.SubProcessCount1], "Process1", data[Names.MaxQueue1]);
        Process process2 = new(delay: 7, data[Names.SubProcessCount2], "Process2", data[Names.MaxQueue2]);
        Process process3 = new(delay: 12, data[Names.SubProcessCount3], "Process3", data[Names.MaxQueue3]);

        var container = new NextElementsContainerByQueuePriority();
        container.AddNextElement(process1, 1);
        container.AddNextElement(process2, 2);
        create.NextElementsContainer = container;
        process2.NextElementsContainer = new NextElementContainer(process3);

        return new Model(new List<Element> { create, process1, process2, process3 });

        // |create|     |---> |process1| -> |dispose|
        //     |        |
        //     |--------|   (queue priority)     
        //              |
        //              |---> |process2| -> |process3| -> |dispose|
    }

    public double CalculateProfit()
    {
        return (Data[Names.SubProcessCount1] + Data[Names.SubProcessCount2] + Data[Names.SubProcessCount3]) * -1 +
               (Data[Names.MaxQueue1] + Data[Names.MaxQueue2] + Data[Names.MaxQueue3]) * -100 +
               _model.StatisticHelper.FailurePercent * -1000;
        
        return Data[Names.SubProcessCount1] * -8 +
               Data[Names.MaxQueue1] * -100 +
               Data[Names.SubProcessCount2] * -10 +
               Data[Names.MaxQueue2] * -100 +
               Data[Names.SubProcessCount3] * -7 +
               Data[Names.MaxQueue3] * -100 +
               _model.StatisticHelper.FailurePercent * -1000;
    }
    
    public bool DataIsValid(Dictionary<Names, int> data)
    {
        for (var i = 0; i < data.Count; i++)
        {
            if (i % 2 == 0 && data[(Names)i] <= 0) return false;
            if (i % 2 == 1 && data[(Names)i] < 0) return false;
        }

        return true;
    }
    
    public override string ToString()
    {
        return $"SubProcessCount1: {Data[Names.SubProcessCount1]}\n MaxQueue1: {Data[Names.MaxQueue1]}\n" +
               $"SubProcessCount2: {Data[Names.SubProcessCount2]}\n MaxQueue2: {Data[Names.MaxQueue2]}\n" +
               $"SubProcessCount3: {Data[Names.SubProcessCount3]}\n MaxQueue3: {Data[Names.MaxQueue3]}\n" +
               $"FailurePercent: {_model.StatisticHelper.FailurePercent}\n" +
               $"Fitness: {CalculateProfit()}";
    }
}