using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Persistence;
using MMOAkka.Core.Commands;
using MMOAkka.Core.Enums;
using MMOAkka.Core.Events;

namespace MMOAkka.Core.Actors
{
    public class CharacterActor : PersistentActor
    {
        private string _name;
        private Guid _id;
        private CharType _role;
        private bool _isDeleted;

        private readonly IActorRef _readModelPublisher;

        public override string PersistenceId { get { return _id.ToString(); } }

        public CharacterActor(Guid id)
        {
            _readModelPublisher = Context.ActorOf(Props.Create(() => new ReadModelActor()), "ReadModelActor");
            _id = id;
        }

        private void Apply(NewCharacterCreatedEvent @evt)
        {
            _id = @evt.Id;
            _name = @evt.Name;
            _role = @evt.Role;
            _readModelPublisher.Tell(GetReadModel());
            Console.WriteLine("Character Created: " + _name);
        }

        private void Apply(CharacterRoleChangedEvent @evt)
        {
            _role = @evt.Role;
            _readModelPublisher.Tell(GetReadModel());
            Console.WriteLine("Character Role Changed: " + _role.ToString());
        }

        private void Apply(CharacterRenamedEvent @evt)
        {
            _name = @evt.Name;
            _readModelPublisher.Tell(GetReadModel());
            Console.WriteLine("Character Name Changed: " + _name);
        }

        private void Apply(CharacterDeletedEvent @evt)
        {
            _isDeleted = true;
            _readModelPublisher.Tell(GetReadModel());
            Console.WriteLine("Character Deleted: " + _name);
        }

        protected override bool ReceiveRecover(object message)
        {
            if (message is NewCharacterCreatedEvent)
            {
                Apply(message as NewCharacterCreatedEvent);          
            }
            else if (message is CharacterRenamedEvent)
            {
                Apply(message as CharacterRenamedEvent);
            }
            else if (message is CharacterRoleChangedEvent)
            {
                Apply(message as CharacterRoleChangedEvent);
            }
            else return false;
            return true;
        }

        protected override bool ReceiveCommand(object message)
        {
            if (message is CreateNewCharacterCmd)
            {
                var cmd = message as CreateNewCharacterCmd;
                var eventToLog = new NewCharacterCreatedEvent(cmd.Id, cmd.Name, cmd.Role);
                Persist(eventToLog, Apply);
                
            }
            else if (message is RenameExistingCharacterCmd)
            {
                var cmd = message as RenameExistingCharacterCmd;
                var eventToLog = new CharacterRenamedEvent(cmd.Id, cmd.Name);
                Persist(eventToLog, Apply);
            }
            else if (message is ChangeRoleTypeCmd)
            {
                var cmd = message as ChangeRoleTypeCmd;
                var eventToLog = new CharacterRoleChangedEvent(cmd.Id, cmd.Role);
                Persist(eventToLog, Apply);
            }
            else if (message is DeleteCharacterCmd)
            {
                var cmd = message as DeleteCharacterCmd;
                var eventToLog = new CharacterDeletedEvent(cmd.Id);
                Persist(eventToLog, Apply);
            }
            else if (message is GetCharacterByIdCmd)
            {
                Task.Run(() =>
                {
                    var rm = GetReadModel();
                    _readModelPublisher.Tell(rm);
                    return rm;
                }).PipeTo(Sender);
            }
            else return false;
            return true;
        }

        private CharacterReadModel GetReadModel()
        {
            return new CharacterReadModel {Id = _id, Name = _name, Role = _role, IsDeleted = _isDeleted};
        }

    }
}
