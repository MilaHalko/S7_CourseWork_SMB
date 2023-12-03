namespace CourseWorkApp;

public record GeneticAlgorithmConfig
{
    public int PopulationSize { get; set; } = 10;
    public int ChildrenCount { get; set; } = 5;
    public int IterationsMaxNumber { get; set; } = 100;
    public float MutationChance { get; set; } = 0.1f;
}