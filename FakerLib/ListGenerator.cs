using System;
using System.Collections;
using System.Linq;

namespace FakerLib
{
    class ListGenerator : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IList));
        }

        public object Generate(GeneratorContext context)
        {
            IList list = (IList)Activator.CreateInstance(context.TargetType);
            Type itemtype = list.GetType().GetGenericArguments().Single();
            int listlength = context.Random.Next(1, 10);
            for (int i = 0; i < listlength; i++)
            {
                list.Add(context.Faker.Create(itemtype));
            }
            return list;
        }
    }
}
