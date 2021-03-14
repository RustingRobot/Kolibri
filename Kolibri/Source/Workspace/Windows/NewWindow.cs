using Kolibri.Engine;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    public class NewWindow : Window
    {
        Textfield canvasX, canvasY, ProjName;
        Button cancelBtn, OKBtn;

        public NewWindow() : base(new Vector2(Globals.screenWidth / 2 - 125, Globals.screenHeight / 2 - 70), new Vector2(250,140), "New")
        {
            popupWindow = true;
            ProjName = new Textfield(this, new Vector2(55, 30), new Vector2(183, 25), "MyProject");
            canvasX = new Textfield(this, new Vector2(55, 60), new Vector2(57, 25), "") { numberField = true, defaultContent = "100" };
            canvasY = new Textfield(this, new Vector2(180, 60), new Vector2(57, 25), "") { numberField = true, defaultContent = "100" };
            cancelBtn = new Button(cancel, this, new Vector2(127, 105), new Vector2(60, 25), "Cancel");
            OKBtn = new Button(OK, this, new Vector2(197, 105), new Vector2(40, 25), "OK");
        }

        void cancel()
        {
            closeWindow();
        }

        void OK()
        {
            for (int i = 0; i < ObjManager.Windows.Count; i++)
            {
                ObjManager.Windows[i].delete = true;
            }
            Window canvasWindow = new CanvasWindow(new Vector2(50, 50), new Vector2(400, 400));
            Globals.canvas = new Canvas(canvasWindow, new Vector2(20, 35), new Vector2(int.Parse(canvasX.content), int.Parse(canvasY.content)));
            Globals.PassWindow(canvasWindow);
            ObjManager.projName = ProjName.content;
            Globals.interactWindow = null;
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            canvasY.Draw(OFFSET);
            canvasX.Draw(OFFSET);
            cancelBtn.Draw(OFFSET);
            OKBtn.Draw(OFFSET);
            ProjName.Draw(OFFSET);
            Globals.primitives.DrawTxt("width:", new Vector2(10, 65) + pos, Globals.fontSize, Color.Gray);
            Globals.primitives.DrawTxt("height:", new Vector2(125, 65) + pos, Globals.fontSize, Color.Gray);
            Globals.primitives.DrawTxt("name:", new Vector2(10, 35) + pos, Globals.fontSize, Color.Gray);
        }

        public override void Update(Vector2 OFFSET)
        {
            if (canvasX.content != "" && int.Parse(canvasX.content) > 1000) canvasX.content = "1000";
            if (canvasY.content != "" && int.Parse(canvasY.content) > 1000) canvasY.content = "1000";
            base.Update(OFFSET);
            canvasX.Update(OFFSET);
            canvasY.Update(OFFSET);
            cancelBtn.Update(OFFSET);
            OKBtn.Update(OFFSET);
            ProjName.Update(OFFSET);
        }
    }
}
