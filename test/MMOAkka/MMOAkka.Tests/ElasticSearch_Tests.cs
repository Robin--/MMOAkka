using System;
using System.Linq;
using MMOAkka.Core.Actors;
using Nest;
using NUnit.Framework;

namespace MMOAkka.Tests
{
    [TestFixture]
    public class ElasticSearch_Tests
    {

        private ElasticClient _client;
        private string EntityName { get; set; }

        [SetUp]
        public void Setup()
        {
            EntityName = "CharacterReadModel";
            var node = new Uri("https://wHff2JNx60SaHebOUY8pdgMw9MpiQmSk:@akkasalinas.east-us.azr.facetflow.io");
            var config = new ConnectionSettings(node, "my_index");
            _client = new ElasticClient(config);
        }

        [Test]
        public void Should_Remove_All_Docs_In_ElasticSearch()
        {
            _client.DeleteIndex("my_index");
        }

        [Test]
        public void Should_Get_All()
        {
            var searchResults = _client.Search<CharacterReadModel>(s => s.Type(EntityName).Query(q => q.MatchAll()));
            Assert.Greater(searchResults.Documents.Count(),0);
        }


    }
}
