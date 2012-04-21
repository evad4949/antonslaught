﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AntOnslaught
{
    class Renderer
    {
        private SpriteBatch sb;
        private Viewport viewport;
        private Vector2 viewCenter; //Which cell to center the map on.

        public Renderer(SpriteBatch sb, Viewport viewport, Vector2 viewCenter)
        {
            this.sb = sb;
            this.viewport = viewport;
            this.viewCenter = viewCenter;
        }

        public void setViewCenter(Vector2 viewCenter)
        {
            this.viewCenter = viewCenter;
        }

        public void Draw(MovableObject obj)
        {

        }

        public void Draw(Map map)
        {
            
        }
    }
}
