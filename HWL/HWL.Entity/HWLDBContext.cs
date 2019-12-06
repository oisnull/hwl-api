using HWL.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

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

        public static void ExecuteTransaction(HWLEntities db, Action<IDbContextTransaction> execFunc)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    execFunc(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static T ExecuteTransaction<T>(HWLEntities db, Func<IDbContextTransaction, T> execFunc)
        {
            using (IDbContextTransaction transaction = db.Database.BeginTransaction())
            {
                try
                {
                    T t = execFunc(transaction);
                    transaction.Commit();
                    return t;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
