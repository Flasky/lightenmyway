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
                LangDic.Add("Credits", "");
                LangDic.Add("Programming", "");
                LangDic.Add("Design", "");
                LangDic.Add("Business", "");
                LangDic.Add("ZYD", "");
                LangDic.Add("WHY", "");

                // control
                LangDic.Add("Control", "");
                LangDic.Add("Drag", "");
                LangDic.Add("Drag Description","");
                LangDic.Add("Tap", "");
                LangDic.Add("Tap Description", "");
                LangDic.Add("Hold n Drag", "");
                LangDic.Add("Hold n Drag Description", "");
                LangDic.Add("Pinch", "");
                LangDic.Add("Pinch Description", "");

                // option
                LangDic.Add("Option", "");
                LangDic.Add("SFX", "音效");
                LangDic.Add("Music", "音乐");
                LangDic.Add("Language", "选择语言");

                // levels HUD
                LangDic.Add("Pause", "暂停");
                LangDic.Add("Skip Text", "点击屏幕跳过");
                LangDic.Add("Restart", "重新开始本关");
                LangDic.Add("Select Level", "选择关卡");
                LangDic.Add("Level Clear", "关卡胜利");
                LangDic.Add("Level Failed", "关卡失败");

                // tutorial
                LangDic.Add("Tut1", "用手柄来移动角色！");
                LangDic.Add("Tut2", "在黑暗中，角色的理智值会下降……");
                LangDic.Add("Tut3", "如果理智值跌到0，她会被吓晕的！");
                LangDic.Add("Tut4", "拖曳并且把道具放在地上");
                LangDic.Add("Tut5", "在亮处，理智值会回复哦！");
                break;

            case LanguageEnum.ZH_HK:
                LangDic.Clear();

                // credits
                LangDic.Add("Credits", "");
                LangDic.Add("Programming", "");
                LangDic.Add("Design", "");
                LangDic.Add("Business", "");
                LangDic.Add("ZYD", "");
                LangDic.Add("WHY", "");

                // control
                LangDic.Add("Control", "");
                LangDic.Add("Drag", "");
                LangDic.Add("Drag Description","");
                LangDic.Add("Tap", "");
                LangDic.Add("Tap Description", "");
                LangDic.Add("Hold n Drag", "");
                LangDic.Add("Hold n Drag Description", "");
                LangDic.Add("Pinch", "");
                LangDic.Add("Pinch Description", "");

                // option
                LangDic.Add("Option", "");
                LangDic.Add("SFX", "");
                LangDic.Add("Music", "");
                LangDic.Add("Language", "");

                // levels HUD
                LangDic.Add("Pause", "");
                LangDic.Add("Skip Text", "");
                LangDic.Add("Restart", "");
                LangDic.Add("Select Level", "");
                LangDic.Add("Level Clear", "");
                LangDic.Add("Level Failed", "");

                // tutorial
                LangDic.Add("Tut1", "");
                LangDic.Add("Tut2", "");
                LangDic.Add("Tut3", "");
                LangDic.Add("Tut4", "");
                LangDic.Add("Tut5", "");
                break;

            case LanguageEnum.EN:
                LangDic.Clear();

                // credits
                LangDic.Add("Credits", "CREDITS");
                LangDic.Add("Programming", "Programming");
                LangDic.Add("Design", "Arts & Design");
                LangDic.Add("Business", "Business Development");
                LangDic.Add("ZYD", "Yidi Zhu");
                LangDic.Add("WHY", "Sherry Wang");
                LangDic.Add("YC", "Hailey Yang");

                // control
                LangDic.Add("Control", "CONTROL");
                LangDic.Add("Drag", "Drag (Joystick)");
                LangDic.Add("Drag Description","Move Character");
                LangDic.Add("Tap", "Tap");
                LangDic.Add("Tap Description", "Interactive with the user interface and some items");
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
                LangDic.Add("Tut3", "If sanity drops to 0, she will faint!");
                LangDic.Add("Tut4", "Drag and put items on the groud");
                LangDic.Add("Tut5", "Sanity increases in light!");
                break;
        }
    }
}
