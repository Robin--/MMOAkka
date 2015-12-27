using System;
using MMOAkka.Core.Enums;

namespace MMOAkka.Core.Actors
{
    public class CharacterReadModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CharType Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}