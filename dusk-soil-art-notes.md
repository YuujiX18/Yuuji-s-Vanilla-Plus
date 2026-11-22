# 暮紫土材質修正：alpha-0.2.67

參照本機Minecraft26.3客戶端實際的dirt、grass_block_side、grass_block_side_overlay、grass_block_top、mycelium_side及mycelium_top，觀察原版土粒密度、表層邊界和垂下覆蓋方式；未複製原版PNG或縮小高解析圖。

三張圖均原生16×16、不透明，一個pixelmap字元對應一個遊戲像素。

- 浮灰土：六色偏暖灰／灰褐土粒，小顆粒及少量兩像素相連碎屑，減少大塊灰色石片感。
- 暮紫土頂：四色暮紫低矮植被，顆粒／小叢與原土層紋理分開設計。
- 暮紫土側：六色土層加相同四種暮紫植被色，不規則垂邊，逐列覆蓋深度為4、4、6、5、3、4、7、5、4、3、5、6、4、4、7、5像素。最低端深色小尖端呈現下垂感；下方土層取用浮灰土相同位置的像素。

generate-ecology-art.mjs採固定種子生成可編輯pixelmap；EcologyPixelCompiler.cs寫入PNG。其餘暮芽、暮翼蛾及其生怪蛋原始圖示與前版逐位元組相同。

DuskSoilPreview67.cs以實際成品PNG呈現調整前後的方塊面；這是材質模型示意，不是遊戲截圖。
