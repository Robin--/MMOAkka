using System;

namespace MMOAkka.Core.Commands
{
    public class DeleteCharacterCmd
    {

        public Guid Id { get; set; }

        public DeleteCharacterCmd(Guid id)
        {
            Id = id;
        }

    }
}