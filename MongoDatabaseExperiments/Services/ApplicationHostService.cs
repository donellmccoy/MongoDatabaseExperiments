using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDatabaseExperiments.Models;
using MongoDatabaseExperiments.Options;
using MongoDatabaseExperiments.Services.Interfaces;

namespace MongoDatabaseExperiments.Services;

/// <summary>
/// Managed host of the application.
/// </summary>
public class ApplicationHostService(IDataService dataService, IOptions<AppSettings> options) : IHostedService
{
    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="token">Indicates that the start process has been aborted.</param>
    public async Task StartAsync(CancellationToken token)
    {
        var entity = await dataService.UpsertEntityAsync(new Restaurant
        {
            Name = "My new restaurant",
            Borough = "23",
            Cuisine = "American",
            Address = new Address
            {
                Building = "14",
                Coordinates = [1.2, 5.3, 1.058],
                Street = "12624 Victoria Place Circle",
                ZipCode = "32828"
            }, 
            Grades =
            [
                new GradeEntry
                {
                    Date = DateTime.Now,
                    Grade = "A",
                    Score = 3.2f
                },
                new GradeEntry
                {
                    Date = DateTime.Now,
                    Grade = "B",
                    Score = 3.1f
                }
            ]
        }, token);

        //var restaurants = await dataService.GetEntitiesWhereAsync<Restaurant>(r => r.Id == ObjectId.Parse("659e060a80446fe7295e47c7"), token);
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="token">Indicates that the shutdown process should no longer be graceful.</param>
    public async Task StopAsync(CancellationToken token)
    {
        await Task.CompletedTask;
    }
}
