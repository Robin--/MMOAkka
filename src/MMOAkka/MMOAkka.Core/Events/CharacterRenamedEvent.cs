using System;

namespace MMOAkka.Core.Events
{
    public class CharacterRenamedEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public CharacterRenamedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}