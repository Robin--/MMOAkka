using System;
using MMOAkka.Core.Enums;

namespace MMOAkka.Core.Events
{

    public class NewCharacterCreatedEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public CharType Role { get; private set; }

        public NewCharacterCreatedEvent(Guid id, string name, CharType type)
        {
            Id = id;
            Name = name;
            Role = type;
        }

    }
}
