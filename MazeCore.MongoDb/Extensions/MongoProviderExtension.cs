using MazeCore.MongoDb.Context;
using MazeCore.MongoDb.Repository;
using MazeCore.MongoDb.Settings;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System;

namespace MazeCore.MongoDb.Extensions
{
    public static class MongoProviderExtension
    {
        public static IServiceCollection AddMongoDbContext(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMongoDbSetting), typeof(MongoDbSetting));

            services.AddScoped(typeof(IMongoContext), typeof(MongoContext));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            return services;
        }


        public static IServiceCollection AddMongoDbContext(this IServiceCollection services, Func<MongoDbSettingBuilder, MongoDbSettingBuilder> builder)
        {
            var settingBuilder = new MongoDbSettingBuilder();
            var setting = builder(settingBuilder).Build();

            services.AddScoped<IMongoDbSetting, MongoDbSetting>(s => new MongoDbSetting
            {
                ConnectionString = setting.ConnectionString,
                DatabaseName = setting.DatabaseName,
            });

            services.AddScoped(typeof(IMongoContext), typeof(MongoContext));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            return services;
        }

        public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IMongoDbSetting setting)
        {
            services.AddScoped<IMongoDbSetting, MongoDbSetting>(s => new MongoDbSetting
            {
                ConnectionString = setting.ConnectionString,
                DatabaseName = setting.DatabaseName,
            });

            services.AddScoped(typeof(IMongoContext), typeof(MongoContext));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            return services;
        }

        public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IOptions<IMongoDbSetting> optionsSetting)
        {
            if (optionsSetting is null || optionsSetting.Value is null)
            {
                throw new NullReferenceException($"{nameof(IMongoDbSetting)} is null");
            }

            var setting = optionsSetting.Value;
            services.AddScoped<IMongoDbSetting, MongoDbSetting>(s => new MongoDbSetting
            {
                ConnectionString = setting.ConnectionString,
                DatabaseName = setting.DatabaseName,
            });

            services.AddScoped(typeof(IMongoContext), typeof(MongoContext));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            return services;
        }
    }
}
