using System;
using Mongo2Go;
using MongoDB.Driver;
using Shouldly;
using Xunit;

namespace Mongo2goTests
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class MongoUnitTests
    {
        [Fact]
        public void Test()
        {
            var mongoDbRunner = MongoDbRunner.Start(singleNodeReplSet: true, singleNodeReplSetWaitTimeout: 10);


            var mongoUrl = new MongoUrl(mongoDbRunner.ConnectionString);

            var client = new MongoClient(mongoUrl);

            client.EnsureReplicationSetReady();

            var database = client.GetDatabase(Guid.NewGuid().ToString());

            database.CreateCollection("person");


            var session = client.StartSession();

            session.StartTransaction();

            var collection = database.GetCollection<Person>("person");


            collection.InsertOne(session,new Person()
            {
                Id = 1,
                Name = "Qa"
            });

            session.CommitTransaction();

            var person = collection.AsQueryable().FirstOrDefault();

            person.ShouldNotBeNull();
        }
    }
}
