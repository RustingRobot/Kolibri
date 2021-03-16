using Kolibri.Engine;
using Kolibri.Source.Workspace;
using Kolibri.Source.Workspace.UIElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Kolibri.Source.Workspace.Windows
{
    class TimelineWindow : Window
    {
        public Timeline timeline;
        public PlaybackWindow pbWindow;

        public List<Layer> layers = new List<Layer>();
    
        //int currentLayer;
        Button addLayerBtn, deleteLayerBtn;
        Button clearFrameBtn;
        Button duplicateFrameBtn;
        Texture2D clearFrame;
        Layer layer1;
        public Layer canvasTimeline;
        public int i; 
        public TimelineWindow( Vector2 POS, Vector2 DIM) : base(POS, DIM, "Timeline")
        {   
            //timeline = new Timeline(this);
            i=1;
            layer1 = new Layer((i-1),this, "Layer 1", new Vector2(10, 58));
            layer1.currentLayer = true;
           // currentLayer = layer1;
            layers.Add(layer1);
            
            i = 2;
          //  canvasTimeline = new Layer(i, this, "Canvas", new Vector2(10, 58+ 30*(i-1)));

            addLayerBtn = new Button(addLayer, this, new Vector2 (72, 24), new Vector2(60,18), "add L");

            clearFrameBtn = new Button(layer1.timeline.clearFrames, this, new Vector2(7, 24), new Vector2(60, 18), "Clear");

            deleteLayerBtn = new Button(deleteLayer, this, new Vector2 (137, 24), new Vector2(60,18), "delete L");

            duplicateFrameBtn = new Button(duplicateFrame, this, new Vector2(222, 24), new Vector2(60,18), "duplicate F");

            clearFrame = Globals.content.Load<Texture2D>("clearFrame");

            clearFrameBtn.normColor = Color.Transparent;
            clearFrameBtn.imgSize = new Vector2(0.6f, 0.6f);
            //clearFrameBtn.model = clearFrame;
        }

        public void addLayer()
        {
            
            layers.Add(new Layer((i-1),this, "Layer " + i, new Vector2(10, 58+ 30*(i-1))));
            layers[layers.Count-1].currentLayer = true;
            for (int i = 0; i < layers.Count-1; i++)
            {
                layers[i].currentLayer = false;
            }
            
            
            i = i+1;
            
            
       }

        public void deleteLayer()
        {
          
        for (int j = 0; j < layers.Count; j++)
            {
                if(layers[j].currentLayer == true)
                if(layers[j].currentLayer == true&&j!=0)
                {
                    if(Globals.canvas.b == true&&Globals.canvas.a==layers.Count)
                    {
                        Globals.canvas.textures.RemoveAt(Globals.canvas.textures.Count-1);
                        Globals.canvas.pixelsList.RemoveAt(Globals.canvas.pixelsList.Count-1);
                        Globals.canvas.a=Globals.canvas.a-1;

                    }
                    layers.RemoveAt(j);
                    layers[j-1].currentLayer = true;
                    for(int m = j; m<layers.Count;m++)
                    {
                        layers[m].layerIndex = layers[m].layerIndex-1;
                        for(int k =0; k<layers[m].timeline.frames.Count;k++)
                        {
                            layers[m].timeline.frames[k].layerIndex = layers[m].timeline.frames[k].layerIndex -1;
                        }
                    }
                    i= i-1;
                } 
            }
           /* for (int j = 0; j < layers.Count; j++)
            {
                if(layers[j].currentLayer == true&&j!=0)
                {
                    if(j== layers.Count-1)
                    {
                        if(Globals.canvas.b == true&&Globals.canvas.a==layers.Count)
                        {
                            Globals.canvas.textures.RemoveAt(Globals.canvas.textures.Count-1);
                            Globals.canvas.pixelsList.RemoveAt(Globals.canvas.pixelsList.Count-1);
                            Globals.canvas.a=Globals.canvas.a-1;

                        }
                        layers.RemoveAt(j);
                        layers[j-1].currentLayer = true;
                        i= i-1;
                    }
                    else
                    {
                        if(Globals.canvas.b == true&&Globals.canvas.a==layers.Count)
                        {
                            Globals.canvas.textures.RemoveAt(Globals.canvas.textures.Count-1);
                            Globals.canvas.pixelsList.RemoveAt(Globals.canvas.pixelsList.Count-1);

                            Globals.canvas.a=Globals.canvas.a-1;

                        }
                        
                        i= i-1;
                    }
                } 
            }*/
            
        }

        public void duplicateFrame()
        {
            for(int i=0;i<layers.Count;i++)
            {
                if(layers[i].currentLayer ==true)
                {
                    layers[i].timeline.frames[layers[i].timeline.currentFrame+1].pixels =layers[i].timeline.frames[layers[i].timeline.currentFrame].pixels;
                }
            }
        }
        public override void Update(Vector2 OFFSET)
        {
             
            base.Update(OFFSET);
            clearFrameBtn.Update(OFFSET);
            addLayerBtn.Update(OFFSET);
            deleteLayerBtn.Update(OFFSET);
            duplicateFrameBtn.Update(OFFSET);
           // timeline.Update();
           for (int m = 0; m < layers.Count; m++)
            {
                
                //change Layer by pressing "Up"
                if (Globals.keyboard.OnPress("Up")==true&&layers[m].currentLayer == true)
                {
                    
                   if(m==0)
                   {
                        layers[m].currentLayer = false;   
                        layers[layers.Count-1].currentLayer = true; 
                   }
                   else
                   {
                        layers[m].currentLayer = false;   
                        layers[m-1].currentLayer = true;
                   }
                
                }
                //change Layer by pressing "Down"
                if (Globals.keyboard.OnPress("Down")==true&&layers[m].currentLayer == true)
                {
                    
                   if(m<layers.Count-1)
                   {
                        layers[m].currentLayer = false;   
                        layers[m+1].currentLayer = true; 
                   }
                   else
                   {
                        layers[m].currentLayer = false;   
                        layers[0].currentLayer = true;
                   }
                
                }
                
                

                layers[m].Update(OFFSET);

                
                
                
                
            }
           
        }

        public override void Draw(Vector2 OFFSET, Color COLOR)
        {
            base.Draw(OFFSET, COLOR);
            beginWindowContent();

            //Buttons
            clearFrameBtn.Draw(OFFSET);
            addLayerBtn.Draw(OFFSET);
            deleteLayerBtn.Draw(OFFSET);
            duplicateFrameBtn.Draw(OFFSET);

            Globals.primitives.DrawLine(new Vector2(pos.X, pos.Y + 47), new Vector2(pos.X + dim.X, pos.Y + 47), 2, new Color(39, 44, 48));
           // timeline.Draw();
           for (int i = 0; i < layers.Count; i++)
            {
                layers[i].Draw(OFFSET);
                if(layers[i].currentLayer == true)      //marking the current layer with a green border
                {
                    Globals.primitives.DrawRect(layers[i].labelBtn.pos, new Vector2(1,layers[i].labelBtn.dim.Y), new Color(85, 209, 23));
                    Globals.primitives.DrawRect(layers[i].labelBtn.pos, new Vector2(layers[i].labelBtn.dim.X,1), new Color(85, 209, 23));
                    Globals.primitives.DrawRect(layers[i].labelBtn.pos + new Vector2(0,layers[i].labelBtn.dim.Y ), new Vector2(layers[i].labelBtn.dim.X,1), new Color(85, 209, 23));
                    Globals.primitives.DrawRect(layers[i].labelBtn.pos + new Vector2(layers[i].labelBtn.dim.X,0), new Vector2(1, layers[i].labelBtn.dim.Y), new Color(85, 209, 23));
                }
            }
           // canvasTimeline.Draw(OFFSET);
            
            //layer1.Draw(OFFSET);
            endWindowContent();
        }
    }
}