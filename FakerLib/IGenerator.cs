using System;

namespace FakerLib
{
    public interface IGenerator
    {
        object Generate(GeneratorContext context);
        bool CanGenerate(Type type);
    }
    public class GeneratorContext
    {
        public Random Random { get; }
        public Type TargetType { get; }
        public Faker Faker { get; }

        public GeneratorContext(Random random, Type targetType, Faker faker)
        {
            Random = random;
            TargetType = targetType;
            Faker = faker;
        }
    }
}
