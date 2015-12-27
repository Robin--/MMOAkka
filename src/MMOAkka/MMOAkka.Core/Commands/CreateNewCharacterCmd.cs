using System;
using MMOAkka.Core.Enums;

namespace MMOAkka.Core.Commands
{
    /// <summary>
    /// Command to create a basic character
    /// </summary>
    public class CreateNewCharacterCmd
    {

        public Guid Id { get; set; }
        public string Name { get; private set; }
        public CharType Role { get; set; }

        public CreateNewCharacterCmd(Guid id, string name, CharType type)
        {
            Id = id;
            Name = name;
            Role = type;
        }

    }
}