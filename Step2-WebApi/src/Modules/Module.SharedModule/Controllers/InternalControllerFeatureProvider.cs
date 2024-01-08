﻿using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Module.SharedModule.Common;

namespace Module.SharedModule.Controllers;

public class InternalControllerFeatureProvider : ControllerFeatureProvider
{
    protected override bool IsController(TypeInfo typeInfo)
    {
        Check.NotNull(typeInfo, nameof(typeInfo));
        
        if (!typeInfo.IsClass)
        {
            return false;
        }
        if (typeInfo.IsAbstract)
        {
            return false;
        }
        if (typeInfo.ContainsGenericParameters)
        {
            return false;
        }
        if (typeInfo.IsDefined(typeof(NonControllerAttribute)))
        {
            return false;
        }
        return typeInfo.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase) ||
               typeInfo.IsDefined(typeof(ControllerAttribute));
    }
}