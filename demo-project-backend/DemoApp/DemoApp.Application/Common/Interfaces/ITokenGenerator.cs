using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Common.Interfaces
{
    public interface ITokenGenerator
    {
        //public string GenerateToken(string userName, string password);
        public string GenerateJWTToken(Guid userId, string firstName, string lastName);
    }
}
