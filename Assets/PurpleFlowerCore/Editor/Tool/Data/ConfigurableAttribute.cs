using System;
namespace Pditine.Tool
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ConfigurableAttribute : Attribute
    {
        public string MenuName { get; }
        public string[] Tags { get; }
        
        public ConfigurableAttribute(string menuName = "", params string[] tags)
        {
            MenuName = menuName;
            Tags = tags;
        }
    }
}