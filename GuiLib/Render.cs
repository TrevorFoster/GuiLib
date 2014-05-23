using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiLib {
    
    class Render {
        public delegate void RenderStrategy();
        private RenderStrategy renderStrategy;
        public event EventHandler needsRender;

        public Render() {

        }

        public void setStrategy(Delegate strategy){
            renderStrategy = (RenderStrategy)strategy;
        }
    }
}
