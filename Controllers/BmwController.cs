// using api.Models;
// using api.Settings;
// using Microsoft.AspNetCore.Mvc;
// using MongoDB.Bson;
// using MongoDB.Driver;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BmwController : ControllerBase
{
    private readonly IMongoCollection<Car> _collection;

    public BmwController(IMongoClient client,IMongoDbSettings dbSettings)
    {
       var dbName = client.GetDatabase(dbSettings.DatabaseName);
       _collection = dbName.GetCollection<Car>("BMW");
    }

    [HttpPost("add")]
    public ActionResult<Car> Create(Car adminInput)
    {
        bool hasDocs = _collection.AsQueryable().Where<Car>(r => r.Model == adminInput.Model).Any();

        if(hasDocs)
            return BadRequest($"mashini ba in model {adminInput.Model} nist");

        Car car = new Car(
            Id: null,
            Name: adminInput.Name,
            Position: adminInput.Position,
            Model: adminInput.Model,
            FirstBuild: adminInput.FirstBuild
        );
        _collection.InsertOne(car);

        return car;
    }

    [HttpGet("get-all")]
    public ActionResult<IEnumerable<Car>> GetAll()
    {
        List<Car> cars = _collection.Find<Car>(new BsonDocument()).ToList();

        if(!cars.Any())
            return NoContent();

        return cars;
    }

    [HttpPut("update/adminInput")]
    public ActionResult<UpdateResult> UpdateAddById(string adminId, Car adminIn)
    {
        var UpdateAdd = Builders<Car>.Update
        .Set((Car doc) => doc.Model, adminIn.Model)
        .Set(doc => doc.Position, adminIn.Position)
        .Set(doc => doc.FirstBuild, adminIn.FirstBuild);

        return _collection.UpdateOne<Car>(doc => doc.Id == adminId, UpdateAdd);
    }

    [HttpDelete("delete/{adminId}")]
    public ActionResult<DeleteResult> Delete(string adminId)
    {
        return _collection.DeleteOne<Car>(doc => doc.Id == adminId);
    }
}
