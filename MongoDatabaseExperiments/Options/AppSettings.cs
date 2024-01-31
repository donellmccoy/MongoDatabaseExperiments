using Microsoft.Extensions.Logging;

namespace MongoDatabaseExperiments.Options;

public class AppSettings
{
    public string ConnectionString
    {
        get;
        set;
    }

    public string DatabaseName
    {
        get;
        set;
    }

    public bool DetailedErrorsEnabled
    {
        get;
        set;
    }
    
    public bool SensitiveDataLoggingEnabled
    {
        get;
        set;
    }
    
    public bool EnableChecks
    {
        get;
        set;
    }

    public LogLevel LogLevel
    {
        get;
        set;
    }
}