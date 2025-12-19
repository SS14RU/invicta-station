using Content.Shared._Invicta.Economy.DepartmentRewards;
using Content.Shared.Access.Components;
using Content.Shared.PDA;
using Robust.Shared.Localization;
using Robust.Shared.GameObjects;

namespace Content.Server._Invicta.Economy.DepartmentRewards;

/// <summary>
/// Provides PDA instruction overrides for department reward tasks.
/// </summary>
public sealed class DepartmentRewardPdaSystem : EntitySystem
{
    [Dependency] private readonly DepartmentRewardConsoleSystem _departmentRewardConsole = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<DepartmentRewardPdaComponent, PdaCollectInstructionEvent>(OnCollectInstructions);
    }

    private void OnCollectInstructions(EntityUid uid, DepartmentRewardPdaComponent component, ref PdaCollectInstructionEvent args)
    {
        if (args.Handled)
            return;

        if (TryComp<MetaDataComponent>(uid, out var meta) && meta.EntityLifeStage >= EntityLifeStage.Terminating)
            return;

        if (!TryComp<PdaComponent>(uid, out var pda))
            return;

        var idCard = CompOrNull<IdCardComponent>(pda.ContainedId);
        if (idCard == null || idCard.JobDepartments.Count == 0)
            return;

        foreach (var department in idCard.JobDepartments)
        {
            var rewardDepartmentId = GetRewardDepartmentId(department.Id);
            if (!_departmentRewardConsole.TryGetActiveDepartmentTask(uid, rewardDepartmentId, out var task))
                continue;

            var title = ResolveTaskText(task.TitleLocId, task.TitleFallback);
            var description = ResolveTaskText(task.DescriptionLocId, task.DescriptionFallback);
            if (string.IsNullOrWhiteSpace(description))
            {
                var alertLevelKey = pda.StationAlertLevel != null ? $"alert-level-{pda.StationAlertLevel}" : "alert-level-unknown";
                description = Loc.GetString($"{alertLevelKey}-instructions");
            }

            var displayLines = new[]
            {
                Loc.GetString("department-reward-pda-instruction-display-task", ("title", title)),
                Loc.GetString("department-reward-pda-instruction-display-instruction", ("instruction", description))
            };

            args.DisplayText = string.Join('\n', displayLines);
            args.CopyText = Loc.GetString("department-reward-pda-instruction-copy",
                ("title", title),
                ("instruction", description));
            args.Handled = true;
            return;
        }
    }
    // Map legacy department ids to reward account ids.
    private string GetRewardDepartmentId(string departmentId)
    {
        return departmentId switch
        {
            "Logistics" => "NT-Cargo",
            _ => $"NT-{departmentId}"
        };
    }

    private string ResolveTaskText(string? locId, string? fallback)
    {
        if (!string.IsNullOrEmpty(locId))
            return Loc.GetString(locId);

        return fallback ?? string.Empty;
    }
}
