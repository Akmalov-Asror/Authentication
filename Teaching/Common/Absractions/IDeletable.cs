namespace Teaching.Common.Absractions;

public interface IDeletable<TIdentity>
{
    Task<bool> DeleteAsync(TIdentity id);
}
