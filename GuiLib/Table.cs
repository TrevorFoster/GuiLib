using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiLib {
    class Table : Control {
        private List<List<Control>> table;
        public Size tableDimensions;
        private int columnWidth, rowHeight;

        public Table() {
            tableDimensions = new Size();
            table = new List<List<Control>>();
        }

        public override void initialize() {
            foreach (List<Control> row in table) {
                foreach (Control element in row) {
                    if (!element.initialized) {
                        element.initialize();
                    }
                }
            }

            base.initialize();
        }

        public void addRow() {
            table.Add(new List<Control>());
            tableDimensions.Height++;
        }

        public void addColumn(int row) {
            if (row < 0 || row > table.Count - 1) {
                return;
            }
            table[row].Add(new Control());
            if (table[row].Count > tableDimensions.Width) {
                tableDimensions.Width = table[row].Count;
            }
        }

        public void setColumnData(int row, int column, Control data) {
            if (row < 0 || column < 0) return;

            while(row > tableDimensions.Height - 1){
                this.addRow();
            }

            while(column > table[row].Count - 1){
                this.addColumn(row);
            }

            if (this.initialized && !data.initialized) {
                data.initialize();
            }
            table[row][column] = data;
        }

        protected override void setSize(int Width, int Height) {
            realSize = new Size(Width, Height);
        }

        public override void update(Vector2 offset) {
            columnWidth = size.Width / tableDimensions.Width;
            rowHeight = size.Height / tableDimensions.Height;

            for (int row = 0; row < tableDimensions.Height; row++) {
                for (int column = 0; column < table[row].Count; column++) {
                    if (table[row][column].size.Width > columnWidth)
                        columnWidth = table[row][column].size.Width;
                    if (table[row][column].size.Height > rowHeight)
                        rowHeight = table[row][column].size.Height;
                }
            }

            for (int row = 0; row < tableDimensions.Height; row++) {
                for (int column = 0; column < table[row].Count; column++) {
                    table[row][column].update(offset + new Vector2(column * columnWidth, row * rowHeight));
                }
            }
            base.update(offset);
        }

        public override void draw(Vector2 offset) {
            for (int row = 0; row < tableDimensions.Height; row++) {
                for (int column = 0; column < table[row].Count; column++) {
                    table[row][column].draw(offset + new Vector2(column * columnWidth, row * rowHeight));
                }
            }
        }

        
    }
}
