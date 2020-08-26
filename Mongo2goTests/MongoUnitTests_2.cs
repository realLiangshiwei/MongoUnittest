using System;
using Mongo2Go;
using MongoDB.Driver;
using Shouldly;
using Xunit;

namespace Mongo2goTests
{

    public class MongoUnitTests_2
    {
        private MongoDbRunner _runner;
        private MongoClient _client;

        public MongoUnitTests_2()
        {
            _runner = MongoDbRunner.Start(singleNodeReplSet: true, singleNodeReplSetWaitTimeout: 10);
            var mongoUrl = new MongoUrl(_runner.ConnectionString);

            _client = new MongoClient(mongoUrl);
            _client.EnsureReplicationSetReady();
        }

        [Fact]
        public void Test()
        {
            var database = _client.GetDatabase(Guid.NewGuid().ToString());

            var session = _client.StartSession();

            session.StartTransaction();

            database.CreateCollection("person");

            var collection = database.GetCollection<Person>("person");


            collection.InsertOne(session, new Person()
            {
                Id = 1,
                Name = "Qa"
            });

            session.CommitTransaction();

            var person = collection.AsQueryable().FirstOrDefault();

            person.ShouldNotBeNull();
        }

        [Fact]
        public void Test2()
        {
            var database = _client.GetDatabase(Guid.NewGuid().ToString());

            var session = _client.StartSession();

            session.StartTransaction();

            database.CreateCollection("person");

            var collection = database.GetCollection<Person>("person");


            collection.InsertOne(session, new Person()
            {
                Id = 1,
                Name = "Qa"
            });

            session.CommitTransaction();

            var person = collection.AsQueryable().FirstOrDefault();

            person.ShouldNotBeNull();
        }

        [Fact]
        public void Test3()
        {
            var database = _client.GetDatabase(Guid.NewGuid().ToString());

            var session = _client.StartSession();

            session.StartTransaction();

            database.CreateCollection("person");

            var collection = database.GetCollection<Person>("person");


            collection.InsertOne(session, new Person()
            {
                Id = 1,
                Name = "Qa"
            });

            session.CommitTransaction();

            var person = collection.AsQueryable().FirstOrDefault();

            person.ShouldNotBeNull();
        }

        [Fact]
        public void Test4()
        {
            var database = _client.GetDatabase(Guid.NewGuid().ToString());

            var session = _client.StartSession();

            session.StartTransaction();

            database.CreateCollection("person");

            var collection = database.GetCollection<Person>("person");


            collection.InsertOne(session, new Person()
            {
                Id = 1,
                Name = "Qa"
            });

            session.CommitTransaction();

            var person = collection.AsQueryable().FirstOrDefault();

            person.ShouldNotBeNull();
        }

        [Fact]
        public void Test5()
        {
            var database = _client.GetDatabase(Guid.NewGuid().ToString());

            var session = _client.StartSession();

            session.StartTransaction();

            database.CreateCollection("person");

            var collection = database.GetCollection<Person>("person");


            collection.InsertOne(session, new Person()
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
