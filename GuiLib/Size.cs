using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GuiLib {
    class Size {

        private int width;
        public int Width {
            get { return width; }
            set {
                width = value;
                onFieldChange("w");
            }
        }
        private int height;
        public int Height {
            get { return height; }
            set {
                height = value;
                onFieldChange("h");
            }
        }

        public event PropertyChangedEventHandler fieldChanged = null;

        public Size() : this(0, 0) { }

        public Size(int width, int height) {
            Width = width;
            Height = height;
        }

        protected virtual void onFieldChange(string fieldName) {
            PropertyChangedEventHandler handler = fieldChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(fieldName));
        }


        public Size Clone() {
            return new Size(this.Width, this.Height);
        }

        // arithmetic operators
        public static Size operator +(Size size1, Size size2) {
            return new Size(size1.Width + size2.Width, size1.Height + size2.Height);
        }

        public static Size operator -(Size size1, Size size2) {
            return new Size(size1.Width - size2.Width, size1.Height - size2.Height);
        }

        public static Size operator *(Size size1, Size size2) {
            return new Size(size1.Width * size2.Width, size1.Height * size2.Height);
        }

        public static Size operator /(Size size1, Size size2) {
            Size newSize = new Size();
            newSize.Width = (size2.Width != 0) ? size1.Width / size2.Width : 0;
            newSize.Height = (size2.Height != 0) ? size1.Height / size2.Height : 0;

            return newSize;
        }

        public override string ToString() {
            return string.Format("({0}, {1})", Width, Height);
        }

        public override bool Equals(object obj) {
            if (obj == null || !(obj is Size)) {
                return false;
            } else {
                Size temp = (Size)obj;
                return temp.Width == this.Width && temp.Height == this.Height;
            }
        }

        public override int GetHashCode() {
            return Width ^ Height;
        }

        public static bool operator ==(Size size1, Size size2) {
            return size1.Width == size2.Width && size1.Height == size2.Height;
        }

        public static bool operator !=(Size size1, Size size2) {
            return size1.Width != size2.Width && size1.Height != size2.Height;
        }
    }
}
