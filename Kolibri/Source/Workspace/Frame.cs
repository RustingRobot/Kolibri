using Kolibri.Engine;
using Kolibri.Engine.Input;
using Kolibri.Source;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Kolibri.Source.Workspace
{
    public class Frame: UIElement
    {
        UInt32[] pixels;

        Frame predFrame;
        Frame succFrame;
        Canvas canvas;
        public Frame(Canvas CANVAS, Window WINDOW,Frame PRED, Frame SUCC, Vector2 POS, Vector2 DIM): base(WINDOW, POS, DIM)
        {
            canvas = CANVAS;
            pos = POS;
            dim = DIM;
            pixels = new uint[(int)canvas.dim.X*(int)canvas.dim.Y]; //hier wäre ja die Dimension des Objektes Canvas der Klasse Canvas gut, aber wie lässt sich das aufrufen 
            for (int i = 0; i < pixels.Length; i++) /*wenn ein Frame hinzugefügt wird, ist der erstmal Weiß, 
                                                    dann kann man ja wenn man die Timeline hat, sich in einem 
                                                    bestimmten Frame befindet(also der aktuelle), und auf 
                                                    einen Button klickt der dann die Pixels vom Canvas auf den 
                                                    aktuellen Frame kopiert*/
            {
                pixels[i] = 0xFFFFFFFF;
            }
            predFrame = PRED;
            succFrame = SUCC;
        }

        public override void Update(Vector2 OFFSET)
        {
            
        }
        public override void Draw(Vector2 OFFSET)
        {
           /*so n kleines Vorschau-Fenster für den Frame(wie Folien bei Präsentationen) 
           sollten in der Timeline erstellt werden, dass dort immer der aktuellen Frame 
           dann abgebildet wird, also dass man da z.B. drei Frames anzeigen lässt und 
           dann wenn man auf so nen Pfeil klickt, der halt die Frames davor bzw. dahinter 
           anzeigt und ein markierter Frame ist der aktuelle*/
        }
    }
}