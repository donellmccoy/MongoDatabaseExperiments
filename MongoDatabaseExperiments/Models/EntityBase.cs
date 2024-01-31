using MongoDB.Bson;

namespace MongoDatabaseExperiments.Models;

public abstract class EntityBase
{
    public virtual ObjectId Id
    {
        get; set;
    } = ObjectId.GenerateNewId();
}