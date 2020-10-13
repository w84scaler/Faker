using System;
using System.Collections.Generic;
using System.Text;
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
