using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

// Model-face preview from the final16x16 PNGs. No generated concept artwork is substituted.
public static class DuskSoilPreview67 {
 static void Face(Graphics g,Image texture,PointF a,PointF b,PointF c,float shade){
  using(var attrs=new ImageAttributes()){
   attrs.SetColorMatrix(new ColorMatrix(new float[][]{new[]{shade,0f,0f,0f,0f},new[]{0f,shade,0f,0f,0f},new[]{0f,0f,shade,0f,0f},new[]{0f,0f,0f,1f,0f},new[]{0f,0f,0f,0f,1f}}));
   g.DrawImage(texture,new[]{a,b,c},new RectangleF(0,0,16,16),GraphicsUnit.Pixel,attrs);
  }
 }
 static void Cube(Graphics g,string textures,float x,float y){
  using(var top=Image.FromFile(Path.Combine(textures,"dusk_soil_top.png")))using(var side=Image.FromFile(Path.Combine(textures,"dusk_soil_side.png"))){
   Face(g,top,new PointF(x,y),new PointF(x+100,y+50),new PointF(x-100,y+50),1.0f);
   Face(g,side,new PointF(x-100,y+50),new PointF(x,y+100),new PointF(x-100,y+250),0.88f);
   Face(g,side,new PointF(x,y+100),new PointF(x+100,y+50),new PointF(x,y+300),0.73f);
  }
 }
 public static void Render(string before,string after,string output){
  using(var image=new Bitmap(960,640))using(var g=Graphics.FromImage(image))using(var title=new Font("Microsoft JhengHei",24,FontStyle.Bold))using(var label=new Font("Microsoft JhengHei",19))using(var small=new Font("Microsoft JhengHei",13)){
   g.Clear(Color.FromArgb(237,239,243));g.InterpolationMode=InterpolationMode.NearestNeighbor;g.PixelOffsetMode=PixelOffsetMode.Half;
   g.DrawString("暮紫土 · alpha-0.2.67",title,Brushes.DarkSlateGray,28,22);
   g.DrawString("調整前",label,Brushes.DimGray,190,100);g.DrawString("調整後",label,Brushes.DarkSlateGray,670,100);
   Cube(g,before,240,155);Cube(g,after,720,155);
   g.DrawString("短色帶／較大的灰色色塊",small,Brushes.DimGray,116,490);
   g.DrawString("不規則垂邊／細碎灰褐土粒",small,Brushes.DarkSlateGray,592,490);
   g.DrawString("頂面與垂邊共用暮紫色系；垂邊深入3–7像素。",small,Brushes.DarkSlateGray,248,544);
   g.DrawString("直接使用遊戲內16×16材質繪製方塊示意，非遊戲截圖。",small,Brushes.SlateGray,28,600);
   image.Save(output,ImageFormat.Png);
  }
 }
}
