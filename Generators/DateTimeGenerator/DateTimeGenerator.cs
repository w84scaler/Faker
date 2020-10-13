using FakerLib;
using System;

namespace DateTimeGenerator
{
    class DateTimeGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

        public object Generate(GeneratorContext context)
        {
            int year = context.Random.Next(1, 2021);
            int month = context.Random.Next(1, 13);
            int day = context.Random.Next(1, 29);
            int hour = context.Random.Next(24);
            int minute = context.Random.Next(60);
            int second = context.Random.Next(60);
            return new DateTime(year, month, day, hour, minute, second);
        }
    }
}
