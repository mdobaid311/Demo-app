using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.Common.DTOs
{
    public class AuthResponseDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
    }
}
