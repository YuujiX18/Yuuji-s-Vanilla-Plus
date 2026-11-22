# 蒼鋼重矛素材：alpha-0.2.72

以現有重矛的pixelmap作為形狀來源，僅將矛頭及沉重核心配重部位的五級金屬色映射為現有蒼鋼工具色：`#10232F`、`#183646`、`#2C697B`、`#5DA7BA`、`#8AC7D4`。輪廓及透明遮罩保持原樣，原本長柄與較下方像素保持原色。

背包圖示16×16，手持圖32×32，一個來源字元直接對應一個PNG像素。沿用原版矛的display_context切換、1.95swap_animation_scale及minecraft:item/spear_in_hand父模型。

重建方式：執行generate-azure-heavy-spear-art.mjs產生兩張可編輯pixelmap及三份模型JSON，再用既有HeavySpearPixelCompiler.Compile寫入PNG。沒有修改普通重矛、蒼鋼普通矛、升級模板或其他既有PNG。

AzureHeavySpearPreview72以實際PNG繪製前後比較，並標示非遊戲截圖。
