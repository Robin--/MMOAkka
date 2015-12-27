using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Akka.Actor;
using MMOAkka.Core;
using MMOAkka.Core.Actors;
using MMOAkka.Core.Commands;
using Nest;

namespace MMOAkka.Api.Controllers
{
    public class CharacterApiController : ApiController
    {

        private string EntityName { get; }
        private readonly ElasticClient _client;

        public CharacterApiController()
        {
            EntityName = "CharacterReadModel";
            var node = new Uri("https://wHff2JNx60SaHebOUY8pdgMw9MpiQmSk:@akkasalinas.east-us.azr.facetflow.io");
            var config = new ConnectionSettings(node, "my_index");
            _client = new ElasticClient(config);

        }

        [HttpGet]
        [Route("api/test")]
        public IHttpActionResult Test()
        {
            return Ok(new { Result = true});
        }

        [HttpGet]
        [Route("api/{id}")]
        public async Task<IHttpActionResult> GetById(string id)
        {
            var cmd = new GetCharacterByIdCmd(Guid.Parse(id));
            var res = await CharacterActorSystem.ActorReferences.CharacterManager.Ask(cmd);
            return Ok(res);
        }

        [HttpGet]
        [Route("api/all")]
        public async Task<IHttpActionResult> Get()
        {
            var searchResults =  await _client.SearchAsync<CharacterReadModel>(s => s.Type(EntityName).Query(q => q.MatchAll()));
            return Ok(searchResults.Documents);
        }

        [HttpGet]
        [Route("api/roles")]
        public IHttpActionResult GetRoles()
        {
            var result = new Dictionary<int, string>();
            result.Add(0, "DPS");
            result.Add(1, "Healer");
            result.Add(2, "Tank");
            return Ok(result.ToList());
        }

        [HttpPost]
        [Route("api/create")]
        public IHttpActionResult CreateNewPlayer([FromBody] CreateNewCharacterCmd cmd)
        {
            var newId = Guid.NewGuid();
            cmd.Id = newId;
            CharacterActorSystem.ActorReferences.CharacterManager.Tell(cmd);
            return Ok(cmd);
        }

        [HttpPost]
        [Route("api/{id}/rename")]
        public IHttpActionResult RenamePlayer(string id, [FromBody] RenameExistingCharacterCmd cmd)
        {
            CharacterActorSystem.ActorReferences.CharacterManager.Tell(cmd);
            return Ok(cmd);
        }

        [HttpPost]
        [Route("api/{id}/role")]
        public IHttpActionResult ChangeRole(string id, [FromBody] ChangeRoleTypeCmd cmd)
        {
            CharacterActorSystem.ActorReferences.CharacterManager.Tell(cmd);
            return Ok(cmd);
        }

    }
}
