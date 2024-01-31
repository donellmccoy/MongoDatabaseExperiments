using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDatabaseExperiments.Models;

public class Restaurant
{
    [BsonId]
    public ObjectId Id
    {
        get;
        set;
    } = ObjectId.GenerateNewId();

    //[BsonElement("name")]
    public string Name
    {
        get; set;
    }

    //[BsonElement("cuisine")]
    public string Cuisine
    {
        get; set;
    }

    //[BsonElement("address")]
    public Address Address
    {
        get; set;
    }

    //[BsonElement("borough")]
    public string Borough
    {
        get; set;
    }

    //[BsonElement("grades")]
    public List<GradeEntry> Grades
    {
        get; set;
    }
}