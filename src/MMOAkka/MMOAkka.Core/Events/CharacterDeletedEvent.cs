using System;

namespace MMOAkka.Core.Events
{
    public class CharacterDeletedEvent
    {
        public Guid Id { get; private set; }

        public CharacterDeletedEvent(Guid id)
        {
            Id = id;
        }

    }
}