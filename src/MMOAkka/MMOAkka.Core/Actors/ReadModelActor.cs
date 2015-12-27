using System;
using Akka.Actor;
using MMOAkka.Core.Actors;
using Nest;

namespace MMOAkka.Core.Actors
{

    public class ReadModelActor : ReceiveActor
    {

        private readonly ElasticClient _client;
        private string EntityName { get; }

        public ReadModelActor()
        {

            EntityName = "CharacterReadModel";
            var node = new Uri("https://wHff2JNx60SaHebOUY8pdgMw9MpiQmSk:@akkasalinas.east-us.azr.facetflow.io");
            var config = new ConnectionSettings(node, "my_index");
            _client = new ElasticClient(config);

            Receive<CharacterReadModel>(state =>
            {
                 _client.DeleteAsync<CharacterReadModel>(state.Id.ToString());
                 _client.IndexAsync<CharacterReadModel>(state, i => i.Type(EntityName).Id(state.Id.ToString()));
            });

        }

    }
}
