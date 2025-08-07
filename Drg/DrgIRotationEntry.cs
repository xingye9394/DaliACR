using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.Module.Opener;
using AEAssist.CombatRoutine.View.JobView;
using Dali.Drg.GCD;
using Dali.Drg.Settings;
using ImGuiNET;
using Dali.Drg.Qtkey;
using Dali.Drg.OffGcd;
//using Dali.Drg.Opener;
namespace Dali.Drg;
public class DrgIRotationEntry : IRotationEntry
{
    public string AuthorName { get; set; } = "XingyeDali";
    public string OverlayTitle { get; } = "日随Drg";
     public Rotation Build(string settingFolder)
    {
        // 初始化设置
        DrgSettings.Build(settingFolder);
        // 初始化QT （依赖了设置的数据）
        BuildQT();
        var rot = new Rotation(SlotResolvers)
        {
            TargetJob = Jobs.Dragoon,//acr职业
            AcrType = AcrType.Normal,//acr类型，both=通用,Normal=日常,HighEnd=高难
            MinLevel = 1,//支持最小等级
            MaxLevel = 100,//支持最大等级
            Description = "欢迎使用Dali阿赖耶识-龙骑自动战斗系统"
        };
        IOpener GetOpener(uint level)
        {
            if (level < 100)
                return null;
            //return new OpenBase();
            return null;
        }
        // 添加opener
        rot.AddOpener(GetOpener);

        // 添加各种事件回调
        rot.SetRotationEventHandler(new DrgRotationEventHandler());

        // 添加QT开关的时间轴行为
        // rot.AddTriggerAction(new TriggerAction_QT());
        return rot;
    }
    public List<SlotResolverData> SlotResolvers = new()
    {
            new(new Drg_Base(), SlotMode.Gcd),
            new(new Drg_AOE(), SlotMode.Gcd),
            new(new Drg_贯穿尖(), SlotMode.Gcd),
            //GCD队列

            new(new Drg_爆发_Off(), SlotMode.OffGcd),
            //OffGCD队列
    };
        public static JobViewWindow QT { get; private set; }  // 声明当前要使用的UI的实例 示例里使用QT
    // 如果你不想用QT 可以自行创建一个实现IRotationUI接口的类
    public IRotationUI GetRotationUI()
    {
        return QT;
    }
    private DrgSettingsUI settingUI = new();//把JOB记得替换掉
    public void OnDrawSetting()
    {
        settingUI.Draw();
    }
    // 构造函数里初始化QT
    public void BuildQT()
    {
        // JobViewSave是AE底层提供的QT设置存档类 在你自己的设置里定义即可
        // 第二个参数是你设置文件的Save类 第三个参数是QT窗口标题
        QT = new JobViewWindow(DrgSettings.Instance.JobViewSave, DrgSettings.Instance.Save, "Dali");
        QT.SetUpdateAction(OnUIUpdate); // 设置QT中的Update回调 不需要就不设置

        //添加QT分页 第一个参数是分页标题 第二个是分页里的内容
        QT.AddTab("通用", DrawQtGeneral);
        QT.AddTab("Dev", DrawQtDev);

        // 添加QT开关 第二个参数是默认值 (开or关) 第三个参数是鼠标悬浮时的tips
        QT.AddQt(QTKey.GCD, true);
        QT.AddQt(QTKey.AOE, true);
        QT.AddQt(QTKey.爆发, true, "猛枪&连祷");
        QT.AddQt(QTKey.猛枪, true);
        QT.AddQt(QTKey.连祷, true);
        QT.AddQt(QTKey.贯穿尖, false);





        // 添加快捷按钮 (带Data.DrgSkill图标)

        //JOBRotationEntry.QT.AddHotkey("防击退", new HotKeyResolver_NormalSpell(7559, SpellTargetType.Self, false));
        //JOBRotationEntry.QT.AddHotkey("极限技", new HotKeyResolver_LB());
        //JOBRotationEntry.QT.AddHotkey("爆发药", new HotKeyResolver_Potion());
        /*
       // 这是一个自定义的快捷按钮 一般用不到
       // 图片路径是相对路径 基于AEAssist(C|E)NVersion/AEAssist
       // 如果想用AE自带的图片资源 路径示例: Resources/AE2Logo.png
       QT.AddHotkey("极限技", new HotkeyResolver_General("#自定义图片路径", () =>
       {
           // 点击这个图片会触发什么行为
           LogHelper.Print("你好");
       }));
       */


    }
    public void Dispose()
    {
        // TODO release managed resources here
    }
    public void OnUIUpdate()
    {

    }

    public void DrawQtGeneral(JobViewWindow jobViewWindow)
    {
        ImGui.Text("画通用信息");
    }

    public void DrawQtDev(JobViewWindow jobViewWindow)
    {
        ImGui.Text("画Dev信息");
        foreach (var v in jobViewWindow.GetQtArray())
        {
            ImGui.Text($"Qt按钮: {v}");
        }

        foreach (var v in jobViewWindow.GetHotkeyArray())
        {
            ImGui.Text($"Hotkey按钮: {v}");
        }
    }
}


