using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDatabaseExperiments.Models;

public class GradeEntry 
{
    //[BsonElement("grade_entry_id")]
    public string GradeEntryId
    {
        get; set;
    } = Guid.NewGuid().ToString();

    //[BsonElement("date")]
    public DateTime Date
    {
        get; set;
    }

    //[BsonElement("grade")]
    public string Grade
    {
        get; set;
    }

    //[BsonElement("score")]
    public float Score
    {
        get; set;
    }
}