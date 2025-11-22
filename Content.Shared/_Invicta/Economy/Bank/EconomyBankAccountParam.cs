using Robust.Shared.Serialization;

namespace Content.Shared._Invicta.Economy.Bank;

[Serializable, NetSerializable]
public enum EconomyBankAccountParam
{
    AccountName,
    Blocked,
    CanReachPayDay,
    JobName,
    Salary
}
