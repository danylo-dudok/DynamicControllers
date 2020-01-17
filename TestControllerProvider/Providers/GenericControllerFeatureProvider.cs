using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using Microsoft.AspNetCore.Mvc;

namespace Providers
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        protected readonly Type _type;
        public GenericContollerEntityTypes Entities { get; }
        public GenericControllerFeatureProvider(Type type)
        {
            if (!type.IsGenericType)
                throw new ArgumentException("Argument must be generic", nameof(type));
            _type = type;
            Entities = new GenericContollerEntityTypes(type);
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature) =>
            Entities.Types.ForEach(entityType =>
            {
                var expectedName = entityType.Name + nameof(Controller);
                if (feature.Controllers.All(t => t.Name != expectedName))
                    feature.Controllers.Add(
                        _type
                        .MakeGenericType(entityType.Type.AsType())
                        .GetTypeInfo());
            });
    }

    public class GenericContollerEntityTypes
    {
        public static Dictionary<string, List<(string Name, TypeInfo Type)>> AllTypes { get; }
            = new Dictionary<string, List<(string, TypeInfo)>>();

        public List<(string Name, TypeInfo Type)> Types { get; }
            = new List<(string, TypeInfo)>();

        public GenericContollerEntityTypes(Type type) =>
            AllTypes.Add(type.Name, Types);

        public void AddGenericController(string name, TypeInfo info) =>
            Types.Add((name, info));

        public static string GetExpectedControllerName(TypeInfo info)
        {
            var genericTypeName = info.GenericTypeArguments.First().Name;
            return AllTypes[info.AsType().Name]
                .FirstOrDefault(x => x.Type.Name == genericTypeName)
                .Name;
        }
    }
}
