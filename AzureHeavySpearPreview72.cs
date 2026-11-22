using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public static class AzureHeavySpearPreview72 {
 public static void Render(string textures,string output){
  using(var canvas=new Bitmap(1240,570))using(var g=Graphics.FromImage(canvas))
  using(var title=new Font("Microsoft JhengHei",23))using(var body=new Font("Microsoft JhengHei",16))using(var small=new Font("Microsoft JhengHei",11)){
   g.Clear(Color.FromArgb(29,32,40));g.InterpolationMode=InterpolationMode.NearestNeighbor;g.PixelOffsetMode=PixelOffsetMode.Half;
   g.DrawString("Yuuji alpha-0.2.72｜重矛 → 蒼鋼重矛",title,Brushes.White,26,22);
   string[] ids={"heavy_spear","azure_steel_heavy_spear","heavy_spear_in_hand","azure_steel_heavy_spear_in_hand"};
   string[] labels={"重矛・背包","蒼鋼重矛・背包","重矛・手持貼圖","蒼鋼重矛・手持貼圖"};
   for(int i=0;i<4;i++){
    int x=26+i*306,size=i<2?16:32;g.FillRectangle(Brushes.SlateGray,x,104,256,256);
    using(var image=Image.FromFile(Path.Combine(textures,ids[i]+".png")))g.DrawImage(image,new Rectangle(x,104,256,256),0,0,size,size,GraphicsUnit.Pixel);
    g.DrawString(labels[i],body,Brushes.White,x,403);g.DrawString(size+" × "+size+"；整數倍放大、金屬頭部換色",small,Brushes.LightGray,x,443);
   }
   g.DrawString("使用本版實際PNG；保留原本輪廓、長柄、顯示比例與朝向。這是材質示意，並非遊戲截圖。",small,Brushes.LightGray,26,519);
   canvas.Save(output,ImageFormat.Png);
  }
 }
}
