using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineLibrary
{
    public class EnginePlayer : EngineCharacter
    {
        public EnginePlayer() : base()
        {

        }
        public EnginePlayer(EngineCamera camera) : base()
        {
            AttachedObjects.Add(camera);
        }

        public EngineCamera ViewCamera
        {
            get { return (EngineCamera)AttachedObjects[0]; }
            set { AttachedObjects[0] = value; }
        }
    }
}
