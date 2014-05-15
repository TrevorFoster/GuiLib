using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiLib {
    class AnimationSet {
        public List<Animation> animations;

        public AnimationSet() {
            animations = new List<Animation>();
        }

        public void setFrame(int frame) {
            foreach (Animation animation in animations) {
                animation.frame = frame;
            }
        }
    }
}
