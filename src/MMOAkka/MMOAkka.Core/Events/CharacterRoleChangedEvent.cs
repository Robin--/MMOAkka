using System;
using MMOAkka.Core.Enums;

namespace MMOAkka.Core.Events
{
    public class CharacterRoleChangedEvent
    {
        public Guid Id { get; private set; }
        public CharType Role { get; private set; }

        public CharacterRoleChangedEvent(Guid id, CharType role)
        {
            Id = id;
            Role = role;
        }

    }
}