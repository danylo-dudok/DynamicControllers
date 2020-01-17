using System.Reflection;
using System;

namespace Providers
{
    public class GenericControllerFeatureBuilder
    {
        protected GenericControllerFeatureProvider provider;

        public GenericControllerFeatureBuilder() {}

        public GenericControllerFeatureBuilder(Type type) => UseGenericController(type);

        public GenericControllerFeatureBuilder UseGenericController(Type type)
        {
            provider = new GenericControllerFeatureProvider(type);
            return this;
        }

        public GenericControllerFeatureBuilder AddController(string name, TypeInfo info)
        {
            provider.Entities.AddGenericController(name, info);
            return this;
        }

        public GenericControllerFeatureProvider Build() => provider;
    }
}
