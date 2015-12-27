using System;

namespace MMOAkka.Core.Commands
{
    public class GetCharacterByIdCmd
    {
        public GetCharacterByIdCmd(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
}