using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public static class EcologyPixelCompiler {
 public static void Compile(string input,string output){
  var lines=File.ReadAllLines(input);var hex=lines[0].Split(',');var colors=new Color[hex.Length];
  for(int i=0;i<hex.Length;i++)colors[i]=Color.FromArgb(hex[i].Length==8?unchecked((int)Convert.ToUInt32(hex[i],16)):unchecked((int)(0xff000000|Convert.ToUInt32(hex[i],16))));
  int width=lines[1].Length,height=lines.Length-1;
  using(var bmp=new Bitmap(width,height,PixelFormat.Format32bppArgb)){
   for(int y=0;y<height;y++){if(lines[y+1].Length!=width)throw new Exception("Bad row");for(int x=0;x<width;x++)bmp.SetPixel(x,y,colors[lines[y+1][x]-'0']);}
   bmp.Save(output,ImageFormat.Png);
  }
 }
 public static void Preview(string textures,string output){
  using(var bmp=new Bitmap(1000,430))using(var g=Graphics.FromImage(bmp))using(var font=new Font("Microsoft JhengHei",17))using(var small=new Font("Microsoft JhengHei",12)){
   g.Clear(Color.FromArgb(28,30,38));g.InterpolationMode=InterpolationMode.NearestNeighbor;g.PixelOffsetMode=PixelOffsetMode.Half;
   g.DrawString("Yuuji alpha-0.2.67｜終界生態材質",font,Brushes.White,24,16);
   var ids=new string[]{"ash_soil","dusk_soil_top","dusk_soil_side","dusk_sprout"};
   var labels=new string[]{"浮灰土","暮紫土・頂面","暮紫土・側面","暮芽"};
   for(int i=0;i<4;i++){
    int x=30+i*240;g.FillRectangle(Brushes.DimGray,x,80,192,192);
    using(var image=Image.FromFile(Path.Combine(textures,"block",ids[i]+".png")))g.DrawImage(image,new Rectangle(x,80,192,192),0,0,16,16,GraphicsUnit.Pixel);
    g.DrawString(labels[i],font,Brushes.White,x,290);g.DrawString("16 × 16；整數倍放大",small,Brushes.LightGray,x,325);
   }
   g.DrawString("直接讀取本版遊戲材質；此圖為材質預覽，並非遊戲截圖。",small,Brushes.LightGray,24,389);
   bmp.Save(output,ImageFormat.Png);
  }
 }
}


