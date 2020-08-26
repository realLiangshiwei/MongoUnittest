﻿using System;
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
        private MongoDbRunner _runner;
        private MongoClient _client;

        public MongoUnitTests()
        {
            _runner = MongoDbRunner.Start(singleNodeReplSet: true, singleNodeReplSetWaitTimeout: 10);
            var mongoUrl = new MongoUrl(_runner.ConnectionString);

            _client = new MongoClient(mongoUrl);
        }

        [Fact]
        public void Test()
        {

            _client.EnsureReplicationSetReady();

            var database = _client.GetDatabase(Guid.NewGuid().ToString());

            database.CreateCollection("person");


            var session = _client.StartSession();

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
