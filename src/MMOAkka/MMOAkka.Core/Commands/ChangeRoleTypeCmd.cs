using System;
using MMOAkka.Core.Enums;

namespace MMOAkka.Core.Commands
{
    /// <summary>
    /// Command to change the role of an existing character
    /// </summary>
    public class ChangeRoleTypeCmd
    {

        public Guid Id { get; private set; }
        public CharType Role { get; set; }

        public ChangeRoleTypeCmd(Guid id, CharType type)
        {
            Id = id;
            Role = type;
        }

    }
}