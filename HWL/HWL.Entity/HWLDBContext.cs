using HWL.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HWL.Entity
{
    public class HWLDBContext
    {
        public static HWLEntities GetDBContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("DB ConnectionString");

            var builder = new DbContextOptionsBuilder<HWLEntities>();
            builder.UseSqlServer(connectionString);

            return new HWLEntities(builder.Options);
        }
    }
}
