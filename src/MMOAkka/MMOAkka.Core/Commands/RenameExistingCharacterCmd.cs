using System;

namespace MMOAkka.Core.Commands
{
    /// <summary>
    /// Command to change the alias/name of an existing character
    /// </summary>
    public class RenameExistingCharacterCmd
    {

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public RenameExistingCharacterCmd(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

    }

}
