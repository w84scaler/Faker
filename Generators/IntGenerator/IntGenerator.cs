using FakerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntGenerator
{
    class IntGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        public object Generate(GeneratorContext context)
        {
            return 20;
        }
    }
}
