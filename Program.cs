using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using NLog;

namespace NlogMongoEx
{
    class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static MongoClient dbClient = new MongoClient("mongodb://localhost/27017");
        static void Main(string[] args)
        {
            var database = dbClient.GetDatabase("27017");

            //collection exists
            var collectionExists = database.ListCollections().ToList().Any(x => x["name"] == "myCollection");
            
            
            
            if (!collectionExists)
            {
                //create collection
                database.CreateCollection("myCollection");
            }

            //get collection
            var coll = database.GetCollection<BsonDocument>("myCollection");

            var documents = coll.Find(new BsonDocument()).ToList();
            Console.WriteLine("The list of databases on this server is: ");
            MongoModel myModel = new MongoModel
            {
                Date = DateTime.Now,
                Exception = "test",
            };

            //insert data in colleciton
            coll.InsertOne(myModel.ToBsonDocument());

            foreach (var item in documents)
            {
                Console.WriteLine(item);
            }
            
            //delete one record from collection
            coll.DeleteOne("{_id : ObjectId('63b42565174dc52f64135486')}");

            Console.ReadLine();
        }
        
    }
}
