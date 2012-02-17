using System;
using System.Collections.Generic;

namespace EngineLibrary
{
    public class StandardPlayer : EnginePlayer
    {
        public StandardPlayer(EngineCamera camera) : base(camera)
        {
            HealthStat = new PlayerStat("Health", 10f, 0f);
        }
        public PlayerStat HealthStat { get; set; }
    }
}
