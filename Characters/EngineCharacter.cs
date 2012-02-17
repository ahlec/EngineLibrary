using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EngineLibrary
{
    public class EngineCharacter : EngineObject
    {
        public EngineCharacter() : base()
        {
        }
        public float MovementSpeed { get; set; }
    }
}
