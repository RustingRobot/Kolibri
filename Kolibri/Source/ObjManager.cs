using Kolibri.Engine;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Kolibri.Source.Workspace.Windows;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Kolibri.Source
{
    public class ObjManager //adds and removes all objects on screen
    {
        public Vector2 offset;
        public static List<Window> Windows = new List<Window>();
        public Menu menu;
        public static string projName = "MyProjekt";
        public ObjManager()
        {
            Globals.PassWindow = AddWindow;
            offset = Vector2.Zero;

            Window canvasWindow = new CanvasWindow(new Vector2(50, 50), new Vector2(400, 400));
            Globals.canvas = new Canvas(canvasWindow, new Vector2(20, 35), new Vector2(200, 200));
            //add windows
            Globals.PassWindow(new TimelineWindow(new Vector2(500, 350), new Vector2(400, 200)));
            Globals.PassWindow(new PlaybackWindow(new Vector2(50, 500), new Vector2(300, 200)));
            Globals.PassWindow(new ColorPickerWindow(new Vector2(500, 50), new Vector2(400, 280)));
            Globals.PassWindow(new ToolsWindow(new Vector2(500, 500), new Vector2(200, 200)));
            //Globals.PassWindow(new TestWindow(new Vector2(50, 50), new Vector2(300, 200)));
            Globals.PassWindow(canvasWindow);
            menu = new Menu(Vector2.Zero, new Vector2(1000, 24));
        }
        public void Update()
        {
            for (int i = Windows.Count -1; i > -1; i--) //dynamic windows update loop
            {
                Windows[i].Update(offset);
                if (Windows[i].delete)
                {
                    Windows.RemoveAt(i);
                    i--;
                }
            }
            DockSpace.Update();
            menu.Update(offset);
        }

        public void Draw()
        {
            for (int i = 0; i < Windows.Count; i++) //dynamic windows draw loop
            {
                Windows[i].Draw(offset, new Color(56, 58, 60));
            }
            menu.Draw(offset, new Color(39, 44, 48));
            DockSpace.Draw();
        }

        public virtual void AddWindow(object INFO)
        {
            Windows.Add((Window)INFO);
        }

        public static void toFront(Window WINDOW)
        {
            Windows.Remove(WINDOW);
            Windows.Add(WINDOW);
        }

        public static void toBack(Window WINDOW)
        {
            Windows.Remove(WINDOW);
            Windows = Windows.Prepend(WINDOW).ToList();
        }

        public static void CWRemoveUpdate(Window removedWindow, Window Top, Window Bottom, Window Right, Window Left)   //update other windows if attached window was undocked
        {
            for (int i = 0; i < Windows.Count(); i++)
            {
                if (Windows[i].TopCW == removedWindow) Windows[i].TopCW = Top;
                if (Windows[i].BottomCW == removedWindow) Windows[i].BottomCW = Bottom;
                if (Windows[i].RightCW == removedWindow) Windows[i].RightCW = Right;
                if (Windows[i].LeftCW == removedWindow) Windows[i].LeftCW = Left;
            }
        }

        public static void createGif()  //work in progress
        {
        //    TimelineWindow timelineWindow = (TimelineWindow)ObjManager.Windows.Find(x => x.GetType().Name == "TimelineWindow");
        //    if (timelineWindow != null) return;
        //    PlaybackWindow playbackWindow = (PlaybackWindow)ObjManager.Windows.Find(x => x.GetType().Name == "PlaybackWindow");
        //    if (playbackWindow != null) return;

        //    using (FileStream fileStream = new FileStream(@$"D:\{projName}.gif", FileMode.Create))
        //    {
        //        using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
        //        {
        //            // header
        //            binaryWriter.Write(new char[3] { 'G', 'I', 'F' });
        //            binaryWriter.Write(new char[3] { '8', '9', 'a' });

        //            // logical screen descriptor
        //            binaryWriter.Write((ushort)2);
        //            binaryWriter.Write((ushort)2);
        //            binaryWriter.Write((byte)0x91);
        //            binaryWriter.Write((byte)0);
        //            binaryWriter.Write((byte)0);

        //            // global color table
        //            binaryWriter.Write((byte)255);
        //            binaryWriter.Write((byte)255);
        //            binaryWriter.Write((byte)255);
        //            binaryWriter.Write((byte)0);
        //            binaryWriter.Write((byte)0);
        //            binaryWriter.Write((byte)0);
        //            binaryWriter.Write((byte)255);
        //            binaryWriter.Write((byte)0);
        //            binaryWriter.Write((byte)0);
        //            binaryWriter.Write((byte)0);
        //            binaryWriter.Write((byte)255);
        //            binaryWriter.Write((byte)0);

        //            // application extension
        //            binaryWriter.Write((byte)0x21);
        //            binaryWriter.Write((byte)0xff);
        //            binaryWriter.Write((byte)0x0b);
        //            binaryWriter.Write('N');
        //            binaryWriter.Write('E');
        //            binaryWriter.Write('T');
        //            binaryWriter.Write('S');
        //            binaryWriter.Write('C');
        //            binaryWriter.Write('A');
        //            binaryWriter.Write('P');
        //            binaryWriter.Write('E');
        //            binaryWriter.Write('2');
        //            binaryWriter.Write('.');
        //            binaryWriter.Write('0');
        //            binaryWriter.Write((byte)3);
        //            binaryWriter.Write((byte)1);
        //            binaryWriter.Write((byte)0xff);
        //            binaryWriter.Write((byte)0xff);
        //            binaryWriter.Write((byte)0);

        //            // graphic control extension
        //            binaryWriter.Write((byte)0x21);
        //            binaryWriter.Write((byte)0xf9);
        //            binaryWriter.Write((byte)4);
        //            binaryWriter.Write((byte)4);
        //            binaryWriter.Write((ushort)100);
        //            binaryWriter.Write((byte)0);
        //            binaryWriter.Write((byte)0);

        //            // image descriptor
        //            binaryWriter.Write((byte)0x2c);
        //            binaryWriter.Write((ushort)0);
        //            binaryWriter.Write((ushort)0);
        //            binaryWriter.Write((ushort)Globals.canvas.dim.X);
        //            binaryWriter.Write((ushort)Globals.canvas.dim.Y);
        //            binaryWriter.Write((byte)0);

        //            // image data
        //            for (int i = playbackWindow.startFrame; i < playbackWindow.endFrame; i++)
        //            {
        //                for (int j = 0; j < timelineWindow.timeline.Layers[0].Frames[i].pixels.Length; j++)
        //                {
        //                    binaryWriter.Write((byte)timelineWindow.timeline.Layers[0].Frames[i].pixels[j]);
        //                }
        //            }

        //            // terminating character
        //            binaryWriter.Write((byte)0x3b);
        //        }
        //    }
        }
    }
}
