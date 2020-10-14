using FakerLib;
using System;

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
            int StringLength = context.Random.Next(1, 20);
            string str = String.Empty;
            for (int i = 0; i < StringLength; i++)
            {
                int register = context.Random.Next(0, 2);
                str += register == 0 ? (char)context.Random.Next(65, 91) : (char)context.Random.Next(97, 123);
            }
            return str;
        }
    }
}
