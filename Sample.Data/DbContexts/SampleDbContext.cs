using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Sample.Common.Extensions;
using Sample.Data.Configurations;

namespace Sample.Data.DbContexts {
    public class SampleDbContext : DbContext {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            /* We are offloading all modelBuilder configuration by using super classes of IEntityConfiguration<TEntity>
             * ---
             * The following code will use reflection to scan the Sample.Data assembly for classes that inherit from
             * IEntityConfiguration<TEntity> and call the associated Configure method without us needing to explicitly
             * do so.
             */

            var thisType = GetType();
            var configurationType = typeof(IEntityConfiguration<>);
            var configureMethodInfo = thisType.GetMethod(
                nameof(Configure),
                BindingFlags.Static | BindingFlags.NonPublic);

            Assembly.GetAssembly(thisType).GetTypes()
                .Where(t => t.IsClass)
                .ForEach(type => {
                    var interfaceType = type
                        .GetInterfaces()
                        .FirstOrDefault(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == configurationType);

                    if (interfaceType == null) return;

                    var entityType = interfaceType
                        .GetGenericArguments()
                        .FirstOrDefault();
                    
                    var entityConfiguration = type
                        .GetConstructor(Array.Empty<Type>())
                        ?.Invoke(Array.Empty<object>());

                    configureMethodInfo
                        ?.MakeGenericMethod(entityType)
                        .Invoke(this, new [] { entityConfiguration, modelBuilder });
                });
        }

        protected static void Configure<TEntity>(
            IEntityConfiguration<TEntity> entityConfiguration,
            ModelBuilder modelBuilder)
            where TEntity : class {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
        }
    }
}
