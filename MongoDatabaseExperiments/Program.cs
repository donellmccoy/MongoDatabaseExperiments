using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDatabaseExperiments.Constants;
using MongoDatabaseExperiments.Models;
using MongoDatabaseExperiments.Options;
using MongoDatabaseExperiments.Services;
using MongoDatabaseExperiments.Services.Interfaces;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace MongoDatabaseExperiments;

internal class Program
{
    private static async Task Main()
    {
        var host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(builder => builder.AddJsonFile(FileNames.SettingsFileName, false, true))
            .ConfigureServices((context, services) =>
            {
                services.Configure<AppSettings>(context.Configuration);
                services.AddHostedService<ApplicationHostService>();
                services.AddSingleton<IDataService, DataService>();
                services.AddDbContextFactory<ApplicationDatabaseContext>((provider, builder) =>
                {
                    ConventionRegistry.Register("CamelCase", new ConventionPack { new CamelCaseElementNameConvention() }, _ => true);

                    BsonClassMap.RegisterClassMap<Restaurant>(classMap => classMap.AutoMap());
                    BsonClassMap.RegisterClassMap<GradeEntry>(classMap => classMap.AutoMap());
                    BsonClassMap.RegisterClassMap<Address>(classMap => classMap.AutoMap());
                    
                    var settings = provider.GetService<IOptions<AppSettings>>().Value;
                    var client = new MongoClient(settings.ConnectionString);
                    var database = client.GetDatabase(settings.DatabaseName);

                    builder.UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);
                    builder.EnableDetailedErrors(settings.DetailedErrorsEnabled);
                    builder.EnableSensitiveDataLogging(settings.SensitiveDataLoggingEnabled);
                    builder.EnableThreadSafetyChecks(settings.EnableChecks);
                    builder.LogTo(message =>
                    {
                        Debug.WriteLine(message);
                        Console.WriteLine(message);
                    }, settings.LogLevel);
                });
            })
            .Build();

        await host.StartAsync();
    }
}