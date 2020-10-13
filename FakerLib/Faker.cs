using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakerLib
{
    public class Faker
    {
        private Stack<Type> dodgestack = new Stack<Type>();
        private List<IGenerator> generators = new PluginLoader().RefreshPlugins();
        private GeneratorContext context = new GeneratorContext(new Random(3228), null, null);
        
        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t)
        {
            var ctors = t.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).OrderByDescending(ctor => ctor.GetParameters().Length).ToList();
            object obj = null;
            dodgestack.Push(t);
            foreach (var ctor in ctors)
            {
                var ctorParams = ctor.GetParameters();
                List<object> ctorValues = new List<object>();
                foreach (var ctorparam in ctorParams)
                {
                    ctorValues.Add(GenerateValue(ctorparam.ParameterType, context));
                }
                try
                {
                    obj = ctor.Invoke(ctorValues.ToArray());
                    break;
                }
                catch
                {
                    continue;
                }
            }
            var fields = t.GetFields();
            foreach (var field in fields)
            {
                if (Equals(field.GetValue(obj),GetDefaultValue(field.FieldType)))
                {
                    field.SetValue(obj, GenerateValue(field.FieldType, context));
                }
            }
            var properties = t.GetProperties();
            foreach (var property in properties)
            {
                if (Equals(property.GetValue(obj),GetDefaultValue(property.PropertyType)))
                {
                    property.SetValue(obj, GenerateValue(property.PropertyType, context));
                }
            }
            dodgestack.Pop();
            return obj;
        }

        private object GenerateValue(Type t, GeneratorContext context)
        {
            object value = null;
            foreach (IGenerator generator in generators)
            {
                if (generator.CanGenerate(t))
                {
                    value = generator.Generate(context);
                    break;
                }
            }
            if (value == null & !dodgestack.Contains(t))
            {
                value = Create(t);
            }
            return value;
        }

        private static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            else
                return null;
        }
    }
}