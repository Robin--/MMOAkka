using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using MMOAkka.Core.Commands;

namespace MMOAkka.Core.Actors
{
    public class CharacterManagerActor : ReceiveActor
    {

        private readonly Dictionary<Guid, IActorRef> _actorRefs;

        public CharacterManagerActor()
        {
            _actorRefs = new Dictionary<Guid, IActorRef>();

            Receive<GetAllCharactersCmd>(cmd => GetAllExistingCharacters(cmd));
            Receive<GetCharacterByIdCmd>(cmd => GetCharacterById(cmd));
            Receive<CreateNewCharacterCmd>(cmd => CreateNewCharacter(cmd));
            Receive<RenameExistingCharacterCmd>(cmd => RenameExistingCharacter(cmd));
            Receive<ChangeRoleTypeCmd>(cmd => ChangeRoleType(cmd));
            Receive<DeleteCharacterCmd>(cmd => DeleteCharacter(cmd));
        }

        private void GetAllExistingCharacters(GetAllCharactersCmd cmd)
        {
            Task.Run(() =>
            {
                return _actorRefs.Select(x => x.Key).ToList();
            }).PipeTo(Sender);

        }

        private void GetCharacterById(GetCharacterByIdCmd cmd)
        {

            var newCharacter = !_actorRefs.ContainsKey(cmd.Id);
            if (newCharacter)
            {
                IActorRef createNew = Context.ActorOf(Props.Create(() => new CharacterActor(cmd.Id)), cmd.Id.ToString());
                _actorRefs.Add(cmd.Id, createNew);
                var getState = createNew.Ask(cmd);
                getState.PipeTo(Sender);
            }
            else
            {
                var getState = _actorRefs[cmd.Id].Ask(cmd);
                getState.PipeTo(Sender);
            }


        }

        private void CreateNewCharacter(CreateNewCharacterCmd cmd)
        {
            var newCharacter = !_actorRefs.ContainsKey(cmd.Id);
            if (newCharacter)
            {
                IActorRef createNew = Context.ActorOf(Props.Create(() => new CharacterActor(cmd.Id)), cmd.Id.ToString());
                _actorRefs.Add(cmd.Id, createNew);
                createNew.Tell(cmd);
            }
        }

        private void RenameExistingCharacter(RenameExistingCharacterCmd cmd)
        {
            var characterExists = _actorRefs.ContainsKey(cmd.Id);
            if (characterExists)
            {
                IActorRef existing = _actorRefs[cmd.Id];
                existing.Tell(cmd);
            }
            else
            {
                IActorRef createNew = Context.ActorOf(Props.Create(() => new CharacterActor(cmd.Id)), cmd.Id.ToString());
                _actorRefs.Add(cmd.Id, createNew);
                createNew.Tell(cmd);
            }
        }

        private void ChangeRoleType(ChangeRoleTypeCmd cmd)
        {
            var characterExists = _actorRefs.ContainsKey(cmd.Id);
            if (characterExists)
            {
                IActorRef existing = _actorRefs[cmd.Id];
                existing.Tell(cmd);
            }
            else
            {
                IActorRef createNew = Context.ActorOf(Props.Create(() => new CharacterActor(cmd.Id)), cmd.Id.ToString());
                _actorRefs.Add(cmd.Id, createNew);
                createNew.Tell(cmd);
            }
        }

        private void DeleteCharacter(DeleteCharacterCmd cmd)
        {
            var characterExists = _actorRefs.ContainsKey(cmd.Id);
            if (characterExists)
            {
                IActorRef existing = _actorRefs[cmd.Id];
                existing.Tell(cmd);
            }
            else
            {
                IActorRef createNew = Context.ActorOf(Props.Create(() => new CharacterActor(cmd.Id)), cmd.Id.ToString());
                _actorRefs.Add(cmd.Id, createNew);
                createNew.Tell(cmd);
            }

        }
    }
}
