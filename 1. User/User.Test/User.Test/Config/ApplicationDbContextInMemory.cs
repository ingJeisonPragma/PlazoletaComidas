using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.DataBase;

namespace User.Test.Config
{
    public static class ApplicationDbContextInMemory
    {
        public static UserDBContext Get()
        {
            var options = new DbContextOptionsBuilder<UserDBContext>()
                .UseInMemoryDatabase(databaseName: $"User.Db")
                .Options;

            return new UserDBContext(options);
        }
    }
}
