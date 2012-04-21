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

        public Vector2 getViewCenter()
        {
            return viewCenter;
        }

        public void Draw(MovableObject obj)
        {
            Vector2 pos = obj.getPosition();
            Rectangle clip = obj.getClip();
        }

        public void Draw(Map map)
        {
            int tileWidth = 32;
            int mapWidth = map.getWidth();
            int mapHeight = map.getHeight();

            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    float x = (viewport.Width / 2) - (tileWidth / 2) + ((i - viewCenter.X) * tileWidth);
                    float y = (viewport.Height / 2) - (tileWidth / 2) + ((j - viewCenter.Y) * tileWidth);
                    Cell cell = map.getCell(i, j);
                    sb.Draw(map.getTexture(), new Vector2(x, y), new Rectangle(cell.texCoordY * tileWidth, cell.texCoordX * tileWidth, tileWidth, tileWidth), Color.White);
                }
            }
        }
    }
}
