using System;

namespace MMOAkka.Core.Commands
{
    public class GetAllCharactersCmd
    {
        public GetAllCharactersCmd()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

    }
}