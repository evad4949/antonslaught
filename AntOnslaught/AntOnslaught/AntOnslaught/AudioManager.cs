﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace AntOnslaught
{
    class AudioManager
    {
        Dictionary<int, SoundEffect> effectsToPlay;
        Dictionary<int, SoundEffect> effects;
        Dictionary<int, Song> songs;

        enum Songs
        {

        }

        enum Effect
        {

        }

        public AudioManager(ContentManager Content)
        {
            //Load Songs
            
            //Load Effects

        }

        public bool queueEffect(Effect effect)
        {
            SoundEffect e = null;
            if (effects.TryGetValue((int)effect, out e))
            {
                try
                {
                    effectsToPlay.Add((int)effect, e);
                }
                catch (ArgumentException ae)
                {
                    //effectsToPlay already contains the effect we want to play. We shouldn't play it multiple times.
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void playEffects()
        {
            foreach (KeyValuePair<int, SoundEffect> ef in effectsToPlay)
            {
                ef.Value.Play();
            }
            effectsToPlay.Clear();
        }

        public bool playSong(Songs song)
        {
            Song s = null;
            if (songs.TryGetValue((int)song, out s))
            {
                MediaPlayer.Stop();
                MediaPlayer.Play(s);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
