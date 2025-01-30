
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Common.Security.Request
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute
    {
        public string? Permissions { get; set; }
        public string? Roles { get; set; }
        public string? Policies { get; set; }
    }
}
