namespace Teaching.Common.Absractions;

public interface IUpdatable<TIdentity, TUpdateModel>
{
    Task<TIdentity> UpdateAsync(TIdentity id, TUpdateModel model);
}
