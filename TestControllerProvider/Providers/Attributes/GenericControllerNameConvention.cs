using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using static Providers.GenericContollerEntityTypes;

namespace Providers.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GenericControllerNameConvention : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller) =>
            controller.ControllerName = GetExpectedControllerName(controller.ControllerType);
    }
}
