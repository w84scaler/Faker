using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakerLib
{
    public interface IFaker
    {
        T Create<T>();
    }

    public class Faker : IFaker
    {
        private Stack<Type> dodgestack;
        private List<IGenerator> generators;
        private Random random;
        public Faker()
        {
            dodgestack = new Stack<Type>();
            generators = new PluginLoader().RefreshPlugins();
            generators.Add(new ListGenerator());
            random = new Random(3228);
        }
        
        public T Create<T>()
        {
            return (T)GenerateValue(new GeneratorContext(random, typeof(T), this));
        }

        internal object Create(Type t)
        {
            object obj = null;
            dodgestack.Push(t);
            var ctors = t.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).OrderByDescending(ctor => ctor.GetParameters().Length).ToList();
            if (ctors.Count != 0 && obj == null)
            {
                foreach (var ctor in ctors)
                {
                    var ctorParams = ctor.GetParameters();
                    List<object> ctorValues = new List<object>();
                    foreach (var ctorparam in ctorParams)
                    {
                        ctorValues.Add(GenerateValue(new GeneratorContext(random, ctorparam.ParameterType, this)));
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
            }
            else
            {
                obj = Activator.CreateInstance(t);
            }
            var fields = t.GetFields();
            foreach (var field in fields)
            {
                if (Equals(field.GetValue(obj),GetDefaultValue(field.FieldType)))
                {
                    field.SetValue(obj, GenerateValue(new GeneratorContext(random, field.FieldType, this)));
                }
            }
            var properties = t.GetProperties();
            foreach (var property in properties)
            {
                if (Equals(property.GetValue(obj),GetDefaultValue(property.PropertyType)))
                {
                    property.SetValue(obj, GenerateValue(new GeneratorContext(random, property.PropertyType, this)));
                }
            }
            dodgestack.Pop();
            return obj;
        }

        private object GenerateValue(GeneratorContext context)
        {
            object value = null;
            foreach (IGenerator generator in generators)
            {
                if (generator.CanGenerate(context.TargetType))
                {
                    value = generator.Generate(context);
                    break;
                }
            }
            if (value == null & !dodgestack.Contains(context.TargetType))
            {
                value = Create(context.TargetType);
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