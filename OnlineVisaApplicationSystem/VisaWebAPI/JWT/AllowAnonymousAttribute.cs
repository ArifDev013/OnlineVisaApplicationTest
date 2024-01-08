using System;

namespace VisaWebAPI.JWT
{

    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    { 
    }

}
