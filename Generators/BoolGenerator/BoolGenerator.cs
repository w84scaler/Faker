using System;
using FakerLib;

namespace BoolGenerator
{
    class BoolGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(bool);
        }

        public object Generate(GeneratorContext context)
        {
            return true;
        }
    }
}
