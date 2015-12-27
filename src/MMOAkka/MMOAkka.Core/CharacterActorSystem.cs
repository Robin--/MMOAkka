using System;
using Akka.Actor;
using Akka.Persistence.SqlServer;
using MMOAkka.Core.Actors;

namespace MMOAkka.Core
{
    public static class CharacterActorSystem
    {
        private static ActorSystem ActorSystem;

        public static void Create()
        {
            ActorSystem = ActorSystem.Create("MmoCharactorSystem");
            SqlServerPersistence.Init(ActorSystem);
            ActorReferences.CharacterManager = ActorSystem.ActorOf<CharacterManagerActor>();
        }

        public static void Shutdown()
        {
            ActorSystem.Shutdown();
            ActorSystem.AwaitTermination(TimeSpan.FromSeconds(1));
        }

        public static class ActorReferences
        {
            public static IActorRef CharacterManager { get; set; }
        }
    }
}
