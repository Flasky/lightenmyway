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
                LangDic.Add("Hold", "");
                LangDic.Add("Hold Description", "");
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
                break;

            case LanguageEnum.ZH_HK:
                break;

            case LanguageEnum.EN:
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
                LangDic.Add("Hold", "");
                LangDic.Add("Hold Description", "");
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
                LangDic.Add("Pause", "Pause");
                LangDic.Add("Skip Text", "Tap screen to skip");
                LangDic.Add("Restart", "Restart");
                LangDic.Add("Select Level", "Select Level");
                LangDic.Add("Level Clear", "Level Clear");
                LangDic.Add("Level Failed", "Level Failed");
                break;
        }
    }
}
