using System.Collections.Generic;

public class Language {

    public enum LanguageEnum {ZH_CN, ZH_HK, EN}
    public Dictionary<string, string> LangDic;

    public Language(LanguageEnum languageEnum) {
        LangDic = new Dictionary<string, string>();
        SwitchLanguage(languageEnum);
    }

    public void SwitchLanguage(LanguageEnum languageEnum) {
        switch (languageEnum) {
            case LanguageEnum.ZH_CN:
                LangDic.Clear();
                // credits
                LangDic.Add("Credits", "制作团队");
                LangDic.Add("Programming", "程序");
                LangDic.Add("Design", "策划&美术");
                LangDic.Add("Business", "市场经理");
                LangDic.Add("Thanks", "特别鸣谢");
                LangDic.Add("Gio", "乔瓦尼·利翁");
                LangDic.Add("Rhys", "瑞斯·琼斯");
                LangDic.Add("Hanna", "卫以文");
                LangDic.Add("Andrew", "陈瀚铭");
                LangDic.Add("ZYD", "朱一荻");
                LangDic.Add("WHY", "王涵怡");
                LangDic.Add("YC", "杨晨");

                // control
                LangDic.Add("Control", "操作");
                LangDic.Add("Drag", "拖曳（摇杆）");
                LangDic.Add("Drag Description","移动角色");
                LangDic.Add("Tap", "点击");
                LangDic.Add("Tap Description", "和用户界面以及部分道具互动");
                LangDic.Add("Hold n Drag", "按住并拖动");
                LangDic.Add("Hold n Drag Description", "使用道具");
                LangDic.Add("Pinch", "捏合");
                LangDic.Add("Pinch Description", "缩放画面");

                // option
                LangDic.Add("Option", "选项");
                LangDic.Add("SFX", "音效");
                LangDic.Add("Music", "音乐");
                LangDic.Add("Language", "选择语言");

                // levels HUD
                LangDic.Add("Pause", "暂停");
                LangDic.Add("Skip Text", "点击屏幕跳过");
                LangDic.Add("Restart", "重新开始本关");
                LangDic.Add("Select Level", "选择关卡");
                LangDic.Add("Level Clear", "成功过关");
                LangDic.Add("Level Failed", "通关失败");

                // tutorial
                LangDic.Add("Tut1", "用手柄来移动角色！");
                LangDic.Add("Tut2", "在黑暗中，角色的理智值会下降……");
                LangDic.Add("Tut3", "如果理智值跌到0，她会被吓死的！");
                LangDic.Add("Tut4", "拖曳并且把道具放在地上");
                LangDic.Add("Tut5", "在亮处，理智值会回复哦！");

                //tutimg
                LangDic.Add("Tut stone", "消耗一个道具，点击去除这种石头");
                LangDic.Add("Tut rock", "要清理石头？ 点它三次！");
                LangDic.Add("Tut flower", "要摘花？点它三次！");
                LangDic.Add("Tut crack", "小心这些裂缝！");
                LangDic.Add("Tut doll1", "把玩偶放到脚边的安全地带");
                LangDic.Add("Tut doll2", "角色会在玩偶的位置复活");
                break;

            case LanguageEnum.ZH_HK:
                LangDic.Clear();
                // credits
                LangDic.Add("Credits", "製作團隊");
                LangDic.Add("Programming", "程序");
                LangDic.Add("Design", "設計&美術");
                LangDic.Add("Business", "市場經理");
                LangDic.Add("Thanks", "特別鳴謝");
                LangDic.Add("Gio", "喬瓦尼·利翁");
                LangDic.Add("Rhys", "瑞斯·瓊斯");
                LangDic.Add("Hanna", "衛以文");
                LangDic.Add("Andrew", "陳瀚銘");
                LangDic.Add("ZYD", "朱一荻");
                LangDic.Add("WHY", "王涵怡");
                LangDic.Add("YC", "楊晨");

                // control
                LangDic.Add("Control", "操作");
                LangDic.Add("Drag", "拖曳（搖杆）");
                LangDic.Add("Drag Description", "移動角色");
                LangDic.Add("Tap", "點擊");
                LangDic.Add("Tap Description", "和用戶界面以及部份道具互動");
                LangDic.Add("Hold n Drag", "按住并拖動");
                LangDic.Add("Hold n Drag Description", "使用道具");
                LangDic.Add("Pinch", "捏合");
                LangDic.Add("Pinch Description", "縮放畫面");

                // option
                LangDic.Add("Option", "選項");
                LangDic.Add("SFX", "音效");
                LangDic.Add("Music", "音樂");
                LangDic.Add("Language", "選擇語言");

                // levels HUD
                LangDic.Add("Pause", "暫停");
                LangDic.Add("Skip Text", "點擊螢幕跳過");
                LangDic.Add("Restart", "重新開始本關");
                LangDic.Add("Select Level", "選擇關卡");
                LangDic.Add("Level Clear", "成功過關");
                LangDic.Add("Level Failed", "通關失敗");

                // tutorial
                LangDic.Add("Tut1", "用搖杆來移動角色！");
                LangDic.Add("Tut2", "在黑暗中，角色的理智值會下降……");
                LangDic.Add("Tut3", "如果理智值降到0，她會被嚇死的！");
                LangDic.Add("Tut4", "拖曳並把道具放在地上");
                LangDic.Add("Tut5", "在亮處，理智值會恢復哦！");

                //tutimg
                LangDic.Add("Tut stone", "消耗一個道具，點擊去除這種石頭");
                LangDic.Add("Tut rock", "要清理石頭？點它三次！");
                LangDic.Add("Tut flower", "要摘花？點它三次！");
                LangDic.Add("Tut crack", "小心這些裂縫");
                LangDic.Add("Tut doll1", "把玩偶放到腳邊的安全地帶");
                LangDic.Add("Tut doll2", "角色會在玩偶的位置復活");
                break;

            case LanguageEnum.EN:
                LangDic.Clear();
                // credits
                LangDic.Add("Credits", "CREDITS");
                LangDic.Add("Programming", "Programming");
                LangDic.Add("Design", "Arts & Design");
                LangDic.Add("Business", "Marketing");
                LangDic.Add("Thanks", "Special Thanks");
                LangDic.Add("Gio", "Giovanni Lion");
                LangDic.Add("Rhys", "Rhys Jones");
                LangDic.Add("Hanna", "Hanna Wirman");
                LangDic.Add("Andrew", "Andrew Chen");
                LangDic.Add("ZYD", "Yidi Zhu");
                LangDic.Add("WHY", "Sherry Wang");
                LangDic.Add("YC", "Hailey Yang");

                // control
                LangDic.Add("Control", "CONTROL");
                LangDic.Add("Drag", "Drag (Controller)");
                LangDic.Add("Drag Description","Move Character");
                LangDic.Add("Tap", "Tap");
                LangDic.Add("Tap Description", "Interactive with the user interface and items");
                LangDic.Add("Hold n Drag", "Hold & Drag");
                LangDic.Add("Hold n Drag Description", "Use items");
                LangDic.Add("Pinch", "Pinch");
                LangDic.Add("Pinch Description", "Zoom the map");

                // option
                LangDic.Add("Option", "OPTION");
                LangDic.Add("SFX", "SFX");
                LangDic.Add("Music", "Music");
                LangDic.Add("Language", "Language");

                // levels HUD
                LangDic.Add("Pause", "Pause");
                LangDic.Add("Skip Text", "Tap screen to skip");
                LangDic.Add("Restart", "Restart");
                LangDic.Add("Select Level", "Select Level");
                LangDic.Add("Level Clear", "Level Clear");
                LangDic.Add("Level Failed", "Level Failed");

                // tutorial
                LangDic.Add("Tut1", "Use controller to move!");
                LangDic.Add("Tut2", "Her sanity drops in darkness...");
                LangDic.Add("Tut3", "If sanity drops to 0, she will die!");
                LangDic.Add("Tut4", "Drag and put items on the groud");
                LangDic.Add("Tut5", "Sanity increases in light!");

                //tutimg
                LangDic.Add("Tut stone", "Tap to remove stones, this costs items");
                LangDic.Add("Tut rock", "Remove rocks? Tap for 3 times!");
                LangDic.Add("Tut flower", "Pick flowers? Tap for 3 times!");
                LangDic.Add("Tut crack", "Be careful about those cracks！");
                LangDic.Add("Tut doll1", "Put the doll on somewhere near and safe");
                LangDic.Add("Tut doll2", "Character will revive on where the doll is");
                break;
        }
    }
}
