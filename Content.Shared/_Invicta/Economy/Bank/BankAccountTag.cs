using Robust.Shared.Serialization;

namespace Content.Shared._Invicta.Economy.Bank;

[Serializable, NetSerializable]
public enum BankAccountTag
{
    Department,
    Station,
    Personal,
    CashRegister
}
