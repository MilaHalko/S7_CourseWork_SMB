using CourseWorkApp.GeneticModels.Model2HospitalElements;
using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Elements;
using MassServiceModeling.Model;
using MassServiceModeling.NextElement;

namespace CourseWorkApp.GeneticModels;

public class Model2 : IGeneticModel
{
    public enum Names
    {
        DoctorsCount,
        DoctorsQueue,
        AttendantsCount,
        AttendantsQueue,
        RegistryWorkersCount,
        RegistryQueue,
        AssistantsCount,
        AssistantsQueue,
    }

    private Model _model;
    public double SimulationTime;
    public Dictionary<Names, int> Data { get; }

    public Model2(Dictionary<Names, int> data, double simulationTime)
    {
        Data = data;
        _model = CreateModel(Data);
        SimulationTime = simulationTime;
    }

    public static Model CreateModel(Dictionary<Names, int> data)
    {
        CreateClient patients = new(new ExponentialRandomizer(15), "Patient");
        DoctorProcess doctors = new(doctorsCount: data[Names.DoctorsCount], "Doctors", "Doctor", data[Names.DoctorsQueue]);
        Process attendants = new(new UniformRandomizer(3, 8), data[Names.AttendantsCount], "Attendants", data[Names.AttendantsQueue], "Attendant");
        Process fromHospitalToLab = new(new UniformRandomizer(2, 5), 25, name: "WayToLab");
        Process labRegistry = new(new ErlangRandomizer(4.5, 3), data[Names.RegistryWorkersCount], "Registry", data[Names.RegistryQueue], "RegistryWorker");
        LabAssistanceProcess labAssistants = new(new ErlangRandomizer(4, 2), data[Names.AssistantsCount], "Assistants", "Assistant", data[Names.AssistantsQueue]);
        Process fromLabToHospital = new(new UniformRandomizer(2, 5), 25, name: "WayToHospital");

        patients.NextElementsContainer = new NextElementContainer(doctors);
        doctors.NextElementsContainer = new NextAfterDoctor(doctors, attendants, fromHospitalToLab);
        fromHospitalToLab.NextElementsContainer = new NextElementContainer(labRegistry);
        labRegistry.NextElementsContainer = new NextElementContainer(labAssistants);
        labAssistants.NextElementsContainer = new NextElementContainer(fromLabToHospital);
        fromLabToHospital.NextElementsContainer = new NextElementContainer(doctors);

        return new Model(new List<Element>()
            { patients, doctors, attendants, fromHospitalToLab, labRegistry, labAssistants, fromLabToHospital });

        // Type 1
        // |patients| -> |doctors| -> |attendants| -> |dispose|
        //
        // Type 2
        // |patients| -> |doctors| -> |fromHospitalToLab| -> |labRegistry| -> |labAssistants| -> |fromLabToHospital| -> |doctors| -> |attendants| -> |dispose|
        //
        // Type 3
        // |patients| -> |doctors| -> |fromHospitalToLab| -> |labRegistry| -> |labAssistants| -> |dispose|
    }

    public void Simulate() => _model.Simulate(SimulationTime, printResult: false);
    private void UpdateSMOModel() => _model = CreateModel(Data);


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

    private bool DataIsValid(Dictionary<Names, int> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (i % 2 == 0 && data[(Names)i] <= 0) return false;
            if (i % 2 == 1 && data[(Names)i] < 0) return false;
        }

        return true;
    }

    public double CalculateProfit()
    {
        var queueMax = -100;
        return Data[Names.DoctorsCount] * -100 +
               Data[Names.DoctorsQueue] * -1 +
               Data[Names.AttendantsCount] * -100 +
               Data[Names.AttendantsQueue] * -1 +
               Data[Names.RegistryWorkersCount] * -100 +
               Data[Names.RegistryQueue] * -1 +
               Data[Names.AssistantsCount] * -100 +
               Data[Names.AssistantsQueue] * -1 +
               _model.StatisticHelper.AverageItemTimeInSystem * -10 +
               _model.StatisticHelper.FailurePercent * -10000;
    }
    
    public override string ToString()
    {
        return $"DoctorsCount: {Data[Names.DoctorsCount]}\n DoctorsQueue: {Data[Names.DoctorsQueue]}\n" +
               $"AttendantsCount: {Data[Names.AttendantsCount]}\n AttendantsQueue: {Data[Names.AttendantsQueue]}\n" +
               $"RegistryWorkersCount: {Data[Names.RegistryWorkersCount]}\n RegistryQueue: {Data[Names.RegistryQueue]}\n" +
               $"AssistantsCount: {Data[Names.AssistantsCount]}\n AssistantsQueue: {Data[Names.AssistantsQueue]}\n" +
               $"AverageItemTimeInSystem: {_model.StatisticHelper.AverageItemTimeInSystem}\n" +
               $"FailurePercent: {_model.StatisticHelper.FailurePercent}\n" +
               $"Fitness: {CalculateProfit()}";
    }
}