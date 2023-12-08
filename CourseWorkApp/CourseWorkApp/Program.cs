using CourseWorkApp;
using CourseWorkApp.Crossovers;
using CourseWorkApp.Fabrics;
using CourseWorkApp.GeneticModels;

RunTask1();
RunTask2();
return;

static void RunTask1()
{
    var geneticAlgorithm1 = new GeneticAlgorithm<Model1>(new Model1Factory(), new Model1Crossover());
    var bestIndividual1 = geneticAlgorithm1.Run();

    var model1 = Model1.CreateModel(bestIndividual1.GeneticModel.Data);
    model1.Simulate(1000);
    Console.WriteLine();
    Console.WriteLine(bestIndividual1.GeneticModel);
}

static void RunTask2()
{
    var geneticAlgorithm2 = new GeneticAlgorithm<Model2>(new Model2Factory(), new Model2Crossover());
    var bestIndividual2 = geneticAlgorithm2.Run();

    var model2 = Model2.CreateModel(bestIndividual2.GeneticModel.Data);
    model2.Simulate(1000);
    Console.WriteLine();
    Console.WriteLine(bestIndividual2.GeneticModel);
}
