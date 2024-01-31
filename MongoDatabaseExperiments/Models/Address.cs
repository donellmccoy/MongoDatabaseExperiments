using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDatabaseExperiments.Models;

public class Address
{
    //[BsonElement("building")]
    public string Building
    {
        get; set;
    }

    //[BsonElement("coordinates")]
    public double[] Coordinates
    {
        get; set;
    }

    //[BsonElement("street")]
    public string Street
    {
        get; set;
    }

    //[BsonElement("zip_code")]
    public string ZipCode
    {
        get; set;
    }
}