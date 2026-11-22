using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;

// Original16px dagger sprites. Pixel coordinates are the artwork; no source sword is resampled.
public static class DaggerPixelArt74 {
 static readonly string[] Ids={"wooden","stone","copper","iron","golden","diamond","netherite","ruby","reinforced_alloy"};
 static readonly string[] Names={"木製匕首","石製匕首","銅製匕首","鐵製匕首","金製匕首","鑽石匕首","獄髓匕首","紅寶石匕首","蒼鋼匕首"};
 // Transparent, metal outline/shadow/mid/light/highlight, wood outline/shadow/light.
 static readonly string[] Palettes={
  "00000000,392810,59401e,84602d,a47e42,c4a263,30210e,674622,96703b",
  "00000000,353535,575757,818181,aaaaaa,cdcdcd,30210e,674622,96703b",
  "00000000,512a21,80432f,b76943,e39166,ffc49a,30210e,674622,96703b",
  "00000000,414141,6b6b6b,a6a6a6,d8d8d8,ffffff,30210e,674622,96703b",
  "00000000,69400a,a66b07,e7b216,ffe348,fff8a5,30210e,674622,96703b",
  "00000000,063e35,087e6f,18b7a0,55e8d7,b2fff0,30210e,674622,96703b",
  "00000000,242027,3c343c,5c5057,82737b,b3a2a9,262027,544348,7f6467",
  "00000000,430b17,741127,b91d3b,e44960,ff8a99,30210e,674622,96703b",
  "00000000,10232f,183646,2c697b,5da7ba,8ac7d4,30210e,674622,96703b"
 };
 static char[][] Shape(){
  // Reference silhouette is a compact broad blade, flared little guard and short hilt.
  // The user's128px image is16 logical pixels at8x. Place its native outline one pixel
  // right and up so the clipped left/bottom endpoints fit inside a transparent border.
  // Shade roles are remapped to each existing material palette, rather than copying colors.
  string[] silhouette={
   "0000000000000000",
   "0000000000000000",
   "0000000000000000",
   "0000000000000000",
   "0000000000000000",
   "0000000011110000",
   "0000000123340000",
   "0000001232400000",
   "0005012324000000",
   "0056123240000000",
   "0057532400000000",
   "0008754000000000",
   "0089A76500000000",
   "58BA055000000000",
   "57A0000000000000",
   "0550000000000000"
  };
  const string symbols="0123456789AB";
  const string swatches="014311326867";
  var rows=new char[16][];for(int y=0;y<16;y++)rows[y]=new string('0',16).ToCharArray();
  for(int y=1;y<16;y++)for(int x=0;x<15;x++){
   char pixel=silhouette[y][x];if(pixel!='0')rows[y-1][x+1]=swatches[symbols.IndexOf(pixel)];
  }
  // One small glint keeps the near-tip edge legible at inventory scale.
  rows[5][9]='5';
  return rows;
 }
 static Color ColorOf(string value){return Color.FromArgb(value.Length==8?unchecked((int)Convert.ToUInt32(value,16)):unchecked((int)(0xff000000|Convert.ToUInt32(value,16))));}
 public static void Generate(string root,string preview){
  var shape=Shape();var textures=Path.Combine(root,"src/main/resources/assets/yuuji/textures/item");Directory.CreateDirectory(textures);
  for(int i=0;i<Ids.Length;i++){
   string id=Ids[i]+"_dagger";var palette=Palettes[i].Split(',');
   var map=new StringBuilder(Palettes[i]).Append('\n');foreach(var row in shape)map.Append(row).Append('\n');
   File.WriteAllText(Path.Combine(root,id+".pixelmap"),map.ToString(),new UTF8Encoding(false));
   using(var bmp=new Bitmap(16,16,PixelFormat.Format32bppArgb)){
    for(int y=0;y<16;y++)for(int x=0;x<16;x++)bmp.SetPixel(x,y,ColorOf(palette[shape[y][x]-'0']));
    bmp.Save(Path.Combine(textures,id+".png"),ImageFormat.Png);
   }
   string models=Path.Combine(root,"src/main/resources/assets/yuuji/models/item");string items=Path.Combine(root,"src/main/resources/assets/yuuji/items");
   Directory.CreateDirectory(models);Directory.CreateDirectory(items);
   File.WriteAllText(Path.Combine(models,id+".json"),"{\n  \"parent\": \"minecraft:item/handheld\",\n  \"textures\": {\"layer0\": \"yuuji:item/"+id+"\"}\n}\n",new UTF8Encoding(false));
   File.WriteAllText(Path.Combine(items,id+".json"),"{\n  \"model\": {\"type\": \"minecraft:model\", \"model\": \"yuuji:item/"+id+"\"}\n}\n",new UTF8Encoding(false));
  }
  Preview(textures,preview);
 }
 static void Preview(string textures,string output){
  using(var bmp=new Bitmap(1000,940))using(var g=Graphics.FromImage(bmp))using(var title=new Font("Microsoft JhengHei",24))using(var body=new Font("Microsoft JhengHei",17))using(var small=new Font("Microsoft JhengHei",12)){
   g.Clear(Color.FromArgb(29,32,40));g.InterpolationMode=InterpolationMode.NearestNeighbor;g.PixelOffsetMode=PixelOffsetMode.Half;
   g.DrawString("Yuuji alpha-0.2.75｜參考圖匕首",title,Brushes.White,30,22);
   g.DrawString("實際物品PNG · 原生16×16 · 背包與手持共用 · 整數倍放大",small,Brushes.LightGray,32,70);
   for(int i=0;i<Ids.Length;i++){
    int x=32+(i%3)*324,y=113+(i/3)*258;
    g.FillRectangle(Brushes.DimGray,x,y,192,192);g.DrawRectangle(Pens.Gray,x,y,191,191);
    using(var image=Image.FromFile(Path.Combine(textures,Ids[i]+"_dagger.png"))){
     g.DrawImage(image,new Rectangle(x,y,192,192),0,0,16,16,GraphicsUnit.Pixel);
     g.FillRectangle(Brushes.Gray,x+214,y+122,48,48);g.DrawRectangle(Pens.Silver,x+212,y+120,51,51);
     g.DrawImage(image,new Rectangle(x+214,y+122,48,48),0,0,16,16,GraphicsUnit.Pixel);
    }
    g.DrawString(Names[i],body,Brushes.White,x,y+201);g.DrawString("背包大小",small,Brushes.LightGray,x+207,y+176);
   }
   g.DrawString("直接讀取本版PNG；沒有抗鋸齒、模糊或高解析度細節。此圖為材質預覽，並非遊戲截圖。",small,Brushes.LightGray,32,907);
   Directory.CreateDirectory(Path.GetDirectoryName(output));bmp.Save(output,ImageFormat.Png);
  }
 }
}
