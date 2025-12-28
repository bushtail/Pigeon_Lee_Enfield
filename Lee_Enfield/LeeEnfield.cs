using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Mod;
using Range = SemanticVersioning.Range;

namespace LeeEnfield;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.pigeon.enfield";
    public override string Name { get; init; } = "Lee-Enfield";
    public override string Author { get; init; } = "PigeonSPT";
    public override List<string>? Contributors { get; init; } = ["bushtail"];
    public override SemanticVersioning.Version Version { get; init; } = new(typeof(ModMetadata).Assembly.GetName().Version?.ToString(3));
    public override Range SptVersion { get; init; } = new("~4.0.0");
    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, Range>? ModDependencies { get; init; } = new()
    {
        { "com.wtt.commonlib", new Range("~2.0.0") }
    };
    public override string? Url { get; init; }
    public override bool? IsBundleMod { get; init; } = true;
    public override string License { get; init; } = "MIT";
}


[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 2)]
public class LeeEnfield(WTTServerCommonLib.WTTServerCommonLib wttCommon) : IOnLoad
{
    public async Task OnLoad()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        await wttCommon.CustomItemServiceExtended.CreateCustomItems(assembly);
        await wttCommon.CustomHideoutRecipeService.CreateHideoutRecipes(assembly);
        await wttCommon.CustomAssortSchemeService.CreateCustomAssortSchemes(assembly);
        await Task.CompletedTask;
    }
}
