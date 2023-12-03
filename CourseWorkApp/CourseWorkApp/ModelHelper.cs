using CourseWorkApp.FitnessSpecification;
using CourseWorkApp.GenericAlgorithmCollector;
using MassServiceModeling.Elements;
using MassServiceModeling.NextElement;

namespace CourseWorkApp;

public static class ModelHelper
{
    public static FitnessModel GetModel1(DataVectorModel1 data)
    {
        Create create = new(); 
        Process process1 = new(delay: 25, subProcessCount: data.parameters[(int)names.subProcesses1], maxQueue: data.parameters[(int)names.process1Queue]);
        Process process2 = new(delay: 7, subProcessCount: data.parameters[(int)names.subProcesses2], maxQueue: data.parameters[(int)names.process2Queue]);
        Process process3 = new(delay: 12, subProcessCount: data.parameters[(int)names.subProcesses3], maxQueue: data.parameters[(int)names.process3Queue]);
        
        var container = new NextElementsContainerByQueuePriority();
        container.AddNextElement(process1, 1);
        container.AddNextElement(process2, 2);
        create.NextElementsContainer = container;
        process2.NextElementsContainer = new NextElementContainer(process3);
        
        return new FitnessModel(new List<Element> {create, process1, process2, process3}, 100);
        
        // |create|     |---> |process1| -> |dispose|
        //     |        |
        //     |--------|   (queue priority)     
        //              |
        //              |---> |process2| -> |process3| -> |dispose|
    }
}