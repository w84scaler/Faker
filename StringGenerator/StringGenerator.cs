using FakerLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace StringGenerator
{
    class StringGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        public object Generate(GeneratorContext context)
        {
            return "german";
        }
    }
}
