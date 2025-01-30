using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Infrastructure.Common.Persistence
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
    }
}
