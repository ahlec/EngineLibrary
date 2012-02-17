using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace EngineLibrary
{
    public class EngineZone
    {
        int _width, _length, _height;
        int[] _indeces;
        Texture2D _texture;
        Vector3 _lowerBoundary, _upperBoundary;
        public Vector3 LowerBoundary { get { return _lowerBoundary; } }
        public Vector3 UpperBoundary { get { return _upperBoundary; } }
        
        List<EngineModel> models = new List<EngineModel>();
        public List<EngineModel> Models { get { return models; } set { models = value; } }

        private PlanePrimitive[] boundaries = new PlanePrimitive[4];

        public CubePrimitive testCube;

        public EngineZone(GraphicsDevice device, ContentManager content)
        {
            _width = 200;
            _length = 200;
            _height = 40;
            _lowerBoundary = new Vector3(0, 0, -1 * _length);
            _upperBoundary = new Vector3(_width, 30, 0);

            // Generate the walls/boundaries of the zone
            boundaries[0] = new PlanePrimitive(Vector3.Zero + _length * -Vector3.UnitZ,
                new Vector2(_width, _length), Color.Green,
                new Vector3(-MathHelper.PiOver2, 0, 0));
            boundaries[1] = new PlanePrimitive(Vector3.Zero + _height * Vector3.UnitY,
                new Vector2(_width, _length), Color.Red, new Vector3(MathHelper.PiOver2, 0, 0));
            boundaries[2] = new PlanePrimitive(Vector3.Zero + _height * Vector3.UnitY,
                new Vector2(_width, _height), Color.Orange, new Vector3(0, MathHelper.PiOver2, 0));
            boundaries[3] = new PlanePrimitive(Vector3.Zero + new Vector3(0, _height, -_length),
                new Vector2(_width, _height), Color.Yellow);

            testCube = new CubePrimitive(new Vector3(10, 5, -50), new Vector3(5, 5, 5));
        }

        public virtual void Draw(GraphicsDevice graphicsDevice, EngineCamera forCamera)
        {
            foreach (PlanePrimitive boundary in boundaries)
                boundary.Draw(graphicsDevice, forCamera);
            foreach (EngineModel model in models)
                model.Draw(forCamera);
            testCube.Draw(graphicsDevice, forCamera);
        }
    }
}