using AEAssist;

namespace Dali.Drg.Data;

public static class DrgBuffs
{
    public const uint
        龙眼预备 = 1863,
        小樱花 = 118,
        大樱花 = 2719;
    public static uint SakuraDot =>
    Core.Me.Level >= 86 ? 大樱花 : 小樱花;
}