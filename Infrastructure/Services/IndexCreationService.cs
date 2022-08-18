using Domain.Entities;
using Microsoft.Extensions.Hosting;
using Redis.OM;

namespace Infrastructure.Services;

public class IndexCreationService : IHostedService
{
    private readonly RedisConnectionProvider _provider;
    public IndexCreationService(RedisConnectionProvider provider)
    {
        _provider = provider;
    }

    /// <summary>
    /// Checks redis to see if the index already exists, if it doesn't create a new index
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var info = (await _provider.Connection.ExecuteAsync("FT._LIST")).ToArray().Select(x => x.ToString());
        if (info.All(x => x != "enrollment-idx" || x != "student-idx" || x != "course-idx" || x != "instructor-idx" || x != "department-idx" || x != "officeassignments-idx"))
        {
            await _provider.Connection.CreateIndexAsync(typeof(Student));
            await _provider.Connection.CreateIndexAsync(typeof(Enrollment));
            await _provider.Connection.CreateIndexAsync(typeof(Course));
            await _provider.Connection.CreateIndexAsync(typeof(Instructor));
            await _provider.Connection.CreateIndexAsync(typeof(Department));
            await _provider.Connection.CreateIndexAsync(typeof(OfficeAssignment));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}