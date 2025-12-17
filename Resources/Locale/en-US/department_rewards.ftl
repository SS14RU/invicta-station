department-reward-console-title = Department reward console
department-reward-console-department-label-inline = Department:
department-reward-console-status-label = Account status
department-reward-console-history-label = Operation log
department-reward-console-auth-label = Authorization:
department-reward-console-auth-missing = Card not detected
department-reward-console-tasks-empty = No tasks available for this department.
department-reward-console-history-empty = No tasks have been completed yet.
department-reward-console-reward-amount = Reward: { $amount }
department-reward-console-stage-unlocktime = Available in { $time }
department-reward-stage-wait-previous = Complete the previous task first
department-reward-stage-available = Available
department-reward-stage-completed = Task completed
department-reward-stage-start = Start
department-reward-stage-mid = Mid
department-reward-stage-late = Late
department-reward-console-confirm-button = Approve
department-reward-console-fail-button = Mark as failed
department-reward-console-status-missing = Department account not found. Contact Central Command.
department-reward-console-placeholder-task-title = Awaiting next directive
department-reward-console-placeholder-description = Awaiting confirmation of the current task before issuing the next one.
department-reward-console-placeholder-reward = ??
department-reward-console-placeholder-end-time = ??:??
department-reward-console-role-name = { CAPITALIZE(THE($job)) }
department-reward-console-late-task = Late task
department-reward-console-mid-task = Mid task
department-reward-console-start-task = Start task
department-reward-console-stage-available = Available in { $time }
department-reward-console-stage-blocked = Complete previous task first
department-reward-console-stage-completed = Task completed
department-reward-console-bank-penalty-reason = Penalty for { $department } department task "{ $task }"
department-reward-console-bank-reason = Payout for { $department } department task "{ $task }"
department-reward-console-instruction-title = Task: [color=white]{ $title }[/color]
department-reward-console-instruction-description = Instructions: [color=white]{ $instruction }[/color]
department-reward-console-instruction-reward = Payout: [color=yellow]{ $reward } credits[/color]
department-reward-console-history-penalty = Penalty: { $penalty }
department-reward-console-history-task-complete = Completed task: { $task }
department-reward-console-history-task-failed = Failed task: { $task } (penalty { $penalty } cr)
department-reward-console-history-entry = { $time } — { $description }
department-reward-console-no-history = No tasks completed yet.
department-reward-department-cargo = Cargo
department-reward-department-engineering = Engineering

# Cargo tasks
department-reward-cargo-start-deliver-title = Restock materials
department-reward-cargo-start-deliver-desc = Deliver three crates of basic resources (metal, glass, or plastic) before high load begins.
department-reward-cargo-start-expedition-title = Prep expedition cargo
department-reward-cargo-start-expedition-desc = Collect three supply crates and send them on the expedition shuttle.
department-reward-cargo-start-supply-title = Supply the station
department-reward-cargo-start-supply-desc = Send one crate of meds, tools, and food to the station recipients.
department-reward-cargo-mid-vip-title = VIP delivery
department-reward-cargo-mid-vip-desc = Deliver a high-priority cargo to the CentCom envoy without delays.
department-reward-cargo-mid-outpost-title = Outpost resupply
department-reward-cargo-mid-outpost-desc = Ship essential supplies to the outpost to keep it operational.
department-reward-cargo-mid-weaponry-title = Armory requisition
department-reward-cargo-mid-weaponry-desc = Provide weapons and ammo crates to Security’s armory.
department-reward-cargo-late-evac-title = Evacuation stockpile
department-reward-cargo-late-evac-desc = Prepare evacuation cargo with essentials for emergency departure.
department-reward-cargo-late-armory-title = Late armory support
department-reward-cargo-late-armory-desc = Restock laser weapons, riot gear, and ammunition for late-shift threats.
department-reward-cargo-late-centcom-title = Fulfill the CentCom tithe
department-reward-cargo-late-centcom-desc = Send a high-value goods crate (gold, plasma, uranium) to Central Command.

# Engineering tasks
department-reward-engineering-start-grid-title = Stabilize the grid
department-reward-engineering-start-grid-desc = Bring the station power grid online and balance the load.
department-reward-engineering-start-atmos-title = Atmos readiness
department-reward-engineering-start-atmos-desc = Configure atmos to provide breathable mix across the station.
department-reward-engineering-start-maint-title = Maintenance sweep
department-reward-engineering-start-maint-desc = Inspect and secure maintenance tunnels for safe operation.
department-reward-engineering-mid-repairs-title = Structural repairs
department-reward-engineering-mid-repairs-desc = Repair hull breaches and structural damage reported on the station.
department-reward-engineering-mid-upgrade-title = Power upgrade
department-reward-engineering-mid-upgrade-desc = Upgrade the power network to handle increased station load.
department-reward-engineering-mid-sensors-title = Sensor calibration
department-reward-engineering-mid-sensors-desc = Calibrate station sensors to improve anomaly and threat detection.
department-reward-engineering-late-hull-title = Hull fortification
department-reward-engineering-late-hull-desc = Reinforce critical hull sections against late-shift hazards.
department-reward-engineering-late-grid-title = Grid redundancy
department-reward-engineering-late-grid-desc = Add redundant power routes to keep essential systems online.
department-reward-engineering-late-emergency-title = Emergency preparedness
department-reward-engineering-late-emergency-desc = Ready backup generators and emergency systems for crisis response.

ent-DepartmentRewardConsoleBase = Department reward console
    .desc = A console that assigns department tasks and payouts. Insert an ID card with the required access.
ent-DepartmentRewardConsoleCargo = Department reward console
    .desc = Cargo department reward console.
    .suffix = Cargo
ent-DepartmentRewardConsoleEngineering = Department reward console
    .desc = Engineering department reward console.
    .suffix = Engineering
ent-DepartmentRewardConsoleMaster = Department reward control console
    .desc = A command terminal that shows the status of all department reward consoles.
department-reward-pda-instruction-display-task = [color=#b0b0b0]Task:[/color] [color=white]{ $title }[/color]
department-reward-pda-instruction-display-instruction = [color=#b0b0b0]Instruction:[/color] [color=white]{ $instruction }[/color]
department-reward-pda-instruction-copy = Task: { $title }\nInstruction: { $instruction }
