// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using Mongo2Go;
// using MongoDB.Driver;
// using MongoDB.Driver.Linq;
//
// namespace qa
// {
//
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             var mongoDbRunner = MongoDbRunner.Start(singleNodeReplSet: true, singleNodeReplSetWaitTimeout: 10);
//
//
//             var mongoUrl = new MongoUrl(mongoDbRunner.ConnectionString);
//
//             var client = new MongoClient(mongoUrl);
//
//             client.EnsureReplicationSetReady();
//
//             var database = client.GetDatabase(Guid.NewGuid().ToString());
//
//             database.CreateCollection("person");
//
//
//             var session = client.StartSession();
//
//             session.StartTransaction();
//
//             var collection = database.GetCollection<Person>("person");
//
//             var role = database.GetCollection<Role>("role");
//
//             collection.InsertOne(session,new Person()
//             {
//                 Id = 1,
//                 Name = "Qa"
//             });
//
//
//             //var qq= collection.Find<Person>(session, Builders<Person>.Filter.Empty).ToList();
//
//             var qq= collection.AsQueryable(session).ToList();
// database.CreateCollection("role");
//             var qqx= role.AsQueryable().Where(x => x.Id == 1).ToList();
//
//
//
//             session.CommitTransaction();
//         }
//     }
//
//     public class Person
//     {
//         public int Id { get; set; }
//         public string Name { get; set; }
//     }
//
//     public class Role
//     {
//         public int Id { get; set; }
//
//         public string Name { get; set; }
//     }
//
//     internal static class MongoClientExtension
//     {
//         private static readonly TimeSpan InitialDelay = TimeSpan.FromMilliseconds(500);
//         private static readonly TimeSpan MaxDelay = TimeSpan.FromSeconds(5000);
//
//
//         public static void EnsureReplicationSetReady(this IMongoClient mongoClient)
//         {
//             var delay = InitialDelay;
//             var database = mongoClient.GetDatabase("__dummy-db");
//             try
//             {
//                 while (true)
//                 {
//                     try
//                     {
//                         _ = database.GetCollection<DummyEntry>("__dummy");
//                         database.DropCollection("__dummy");
//
//                         var session = mongoClient.StartSession();
//
//                         try
//                         {
//                             session.StartTransaction();
//                             session.AbortTransaction();
//                         }
//                         finally
//                         {
//                             session.Dispose();
//                         }
//                         break;
//                     }
//                     catch (NotSupportedException) { }
//
//                     Thread.Sleep(delay);
//                     delay = Min(Double(delay), MaxDelay);
//                 }
//             }
//             finally
//             {
//                 mongoClient.DropDatabase("__dummy-db");
//             }
//         }
//
//         private static TimeSpan Min(TimeSpan left, TimeSpan right)
//         {
//             return new TimeSpan(Math.Min(left.Ticks, right.Ticks));
//         }
//
//         private static TimeSpan Double(TimeSpan timeSpan)
//         {
//             long ticks;
//             try
//             {
//                 ticks = checked(timeSpan.Ticks * 2);
//             }
//             catch (OverflowException)
//             {
//                 if (timeSpan.Ticks >= 0)
//                     return TimeSpan.MaxValue;
//
//                 return TimeSpan.MinValue;
//             }
//
//             return new TimeSpan(ticks);
//         }
//
//         private sealed class DummyEntry
//         {
//             public int Id { get; set; }
//         }
//     }
// }
