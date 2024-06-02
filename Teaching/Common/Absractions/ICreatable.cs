namespace Teaching.Common.Absractions;

public interface ICreatable<TIdentity, TCreateModel>
{
    Task<TIdentity> CreateAsync(TCreateModel createModel);
}
