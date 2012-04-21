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
        protected double direction;
        protected List<Cell> curPath = new List<Cell>();
        protected Vector2 position;
        protected Cell currentCell = null;
        protected Cell goalCell = null;
        protected Boolean isMoving = false;
        protected float speed = 0.5f;
        public Cell getCurrentCell()
        {
            return currentCell;
        }
        public void setCurrentCell(Cell c)
        {
            currentCell = c;
        }
        public Cell getGoalCell()
        {
            return goalCell;
        }
        public void setGoalCell(Cell c)
        {
            goalCell = c;
        }
        public double getDirection()
        {
            return direction;
        }
        public Vector2 getPosition()
        {
            return position;
        }
        public bool updateMovement(GameTime timer)
        {
            bool canMove = true;
            if (curPath.Count > 0)
            {
                isMoving = true;
                if (curPath[0].passable)
                {
                    Vector2 normalVec = curPath[0].coord * 32 - position;
                    //Vector2 jumpAmount = normalVec * speed * timer.ElapsedGameTime.Milliseconds;
                    if (normalVec.Length() < 3)
                    {
                        position = curPath[0].coord * 32;
                        updateDirection();
                        currentCell = curPath[0];
                        curPath.RemoveAt(0);
                    }
                    else
                    {
                        normalVec.Normalize();
                        Vector2 jumpAmount = normalVec * speed * timer.ElapsedGameTime.Milliseconds;
                        position = position + jumpAmount;
                    }
                }
                else
                {
                    canMove = false;
                }
            }
            else
            {
                isMoving = false;
            }
            return canMove;
        }
        public void setPath(List<Cell> path)
        {
            curPath = path;
            curPath.Reverse();
            curPath.RemoveAt(0);
            updateDirection();
        }

        public double AngleBetween_1(Vector2 a, Vector2 b)
        {
            var dotProd = Vector2.Dot(a, b);
            var lenProd = a.Length() * b.Length();
            var divOperation = dotProd / lenProd;
            return Math.Acos(divOperation) * (180.0 / Math.PI);
        }

        private void updateDirection()
        {
            if (curPath.Count > 0)
            {
                direction = AngleBetween_1(position, curPath[0].coord * 32);
            }
        }
        public abstract Texture2D getTexture();
        public abstract void setTexture(Texture2D texture);
        public abstract Color getColor();
        public abstract void setColor(Color color);
        public abstract Rectangle getClip();
        public abstract void setClip(Rectangle clip);
    }
}
