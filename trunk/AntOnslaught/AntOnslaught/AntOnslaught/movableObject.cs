﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace AntOnslaught
{
    abstract class MovableObject : Drawable
    {
        public enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT, 
            UPRIGHT,
            UPLEFT,
            DOWNRIGHT,
            DOWNLEFT
        }
        protected Direction currentDirection;
        protected List<Cell> curPath = new List<Cell>();
        protected Vector2 goal;
        protected Vector2 position;
        public Direction getDirection()
        {
            return currentDirection;
        }
        public Vector2 getPosition()
        {
            return position;
        }
        public Vector2 getGoal()
        {
            return goal;
        }
        public void setGoal(Vector2 goal)
        {
            this.goal = goal;
        }
        protected float speed = 0.5f;
        public bool updateMovement(GameTime timer)
        {
            bool canMove = true;
            if (curPath.Count > 0)
            {
                if (curPath[0].passable)
                {
                    Vector2 normalVec = curPath[0].coord * 32 -  position;
                    if (normalVec.Length() <= 3)
                    {
                        position = curPath[0].coord * 32;
                        updateDirection();
                        curPath.RemoveAt(0);
                    }
                    else
                    {
                        normalVec.Normalize();
                        position = position + normalVec * speed * timer.ElapsedGameTime.Milliseconds;
                    }
                }
                else
                {
                    canMove = false;
                }
            }
            return canMove;
        }
        public void setPath(List<Cell> path)
        {
            curPath = path;
            curPath.Reverse();
        }
        private void updateDirection()
        {
            if (curPath.Count > 1)
            {
                if (curPath[0].coord.X == curPath[1].coord.X)
                {
                    if (curPath[0].coord.Y > curPath[1].coord.Y)
                    {
                        currentDirection = Direction.UP;
                    }
                    else
                    {
                        currentDirection = Direction.DOWN;
                    }
                }
                else if (curPath[0].coord.Y == curPath[1].coord.Y)
                {
                    if (curPath[0].coord.X > curPath[1].coord.X)
                    {
                        currentDirection = Direction.LEFT;
                    }
                    else
                    {
                        currentDirection = Direction.RIGHT;
                    }
                }
                else
                {
                    if (curPath[0].coord.X > curPath[1].coord.X)
                    {
                        if (curPath[0].coord.Y > curPath[1].coord.Y)
                        {
                            currentDirection = Direction.UPLEFT;
                        }
                        else
                        {
                            currentDirection = Direction.DOWNLEFT;
                        }
                    }
                    else
                    {
                        if (curPath[0].coord.Y > curPath[1].coord.Y)
                        {
                            currentDirection = Direction.UPRIGHT;
                        }
                        else
                        {
                            currentDirection = Direction.DOWNRIGHT;
                        }
                    }
                }
            }
        }
        //protected void move(Direction direction)
        //{
        //    if (direction == Direction.UP)
        //    {
        //        position.Y -= speed;
        //    }
        //    else if (direction == Direction.DOWN)
        //    {
        //        position.Y += speed;
        //    }
        //    else if (direction == Direction.LEFT)
        //    {
        //        position.X -= speed;
        //    }
        //    else if (direction == Direction.RIGHT)
        //    {
        //        position.X += speed;
        //    }
        //}
        //public void findPath(Vector2 goal)
        //{
             
        //}
        public abstract Texture2D getTexture();
        public abstract void setTexture(Texture2D texture);
        public abstract Color getColor();
        public abstract void setColor(Color color);
        public abstract Rectangle getClip();
        public abstract void setClip(Rectangle clip);
    }
}
