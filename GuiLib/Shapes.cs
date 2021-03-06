﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GuiLib {
    static class Shapes {
        private static Texture2D blankTexture;

        public static Texture2D CreateSquare(int w, int h, Color color) {
            Texture2D rectangle = new Texture2D(GUIRoot.graphicsDevice, w, h, false, SurfaceFormat.Color);
            Color[] colorData = new Color[w * h];
            for (int i = 0; i < w * h; i++) { 
                colorData[i] = color; 
            }
            rectangle.SetData<Color>(colorData);
            return rectangle;
        }

        public static void LoadContent() {
            blankTexture = new Texture2D(GUIRoot.graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            blankTexture.SetData(new[] { Color.White });
        }

        public static void DrawLineSegment(this SpriteBatch spriteBatch, Vector2 Vector21, Vector2 Vector22, Color color, float lineWidth) {
            float angle = (float)Math.Atan2(Vector22.Y - Vector21.Y, Vector22.X - Vector21.X);
            float length = Vector2.Distance(Vector21, Vector22);

            spriteBatch.Draw(blankTexture, Vector21, null, color, angle, Vector2.Zero, new Vector2(length, lineWidth), SpriteEffects.None, 0);
        }

        public static void DrawPolygon(List<Vector2> vertex, Color color, float lineWidth) {
            if (vertex.Count > 0) {
                for (int i = 0; i < vertex.Count - 1; i++) {
                    DrawLineSegment(GUIRoot.spriteBatch, vertex[i], vertex[i + 1], color, lineWidth);
                }
                DrawLineSegment(GUIRoot.spriteBatch, vertex[vertex.Count - 1], vertex[0], color, lineWidth);
            }
        }

        public static void DrawRectangle(float w, float h, Vector2 pos, Color color, float lineWidth) {
            if (lineWidth == 0) {
                GUIRoot.spriteBatch.Draw(blankTexture, pos, null, color, 0, Vector2.Zero, new Vector2(w, h), SpriteEffects.None, 0);
            } else {
                List<Vector2> vertex = new List<Vector2>();
                vertex.Add(new Vector2(pos.X, pos.Y));
                vertex.Add(new Vector2(pos.X + w, pos.Y));
                vertex.Add(new Vector2(pos.X + w, pos.Y + h));
                vertex.Add(new Vector2(pos.X, pos.Y + h));
                DrawPolygon(vertex, color, lineWidth);
            }
        }

        public static void DrawRectangle(Rectangle s, Color color, int lineWidth) {
            if (lineWidth == 0) {
                GUIRoot.spriteBatch.Draw(blankTexture, new Vector2(s.X, s.Y), null, color, 0, Vector2.Zero, new Vector2(s.Width, s.Height), SpriteEffects.None, 0);
            } else {
                List<Vector2> vertex = new List<Vector2>();
                vertex.Add(new Vector2(s.X, s.Y));
                vertex.Add(new Vector2(s.X + s.Width, s.Y));
                vertex.Add(new Vector2(s.X + s.Width, s.Y + s.Height));
                vertex.Add(new Vector2(s.X, s.Y + s.Height));
                DrawPolygon(vertex, color, lineWidth);
            }
        }

        public static void DrawCircle(Vector2 center, float radius, Color color, int lineWidth, int segments) {
            List<Vector2> vertex = new List<Vector2>();
            for (int i = 0; i < segments; i++) { vertex.Add(Vector2.Zero); }
            double increment = Math.PI * 2.0 / segments;
            double theta = 0.0;
            for (int i = 0; i < segments; i++) {
                vertex[i] = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                theta += increment;
            }
            DrawPolygon(vertex, color, lineWidth);
        }
    }
}
