using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {
    class AnimationSet {
        public List<Animation> animations;
        public Vector2 origin;

        public AnimationSet() {
            animations = new List<Animation>();
            origin = Vector2.Zero;
        }

        public void update() {
            foreach (Animation animation in animations) {
                animation.update();
            }
        }

        public void draw(Vector2 offset) {
            Vector2 totOffs = offset + origin;

            foreach (Animation animation in animations) {
                animation.draw(totOffs);
            }
        }

        public void setFrames(int frame) {
            foreach (Animation animation in animations) {
                animation.frame = frame;
            }
        }
    }
}
