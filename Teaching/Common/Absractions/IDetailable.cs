namespace Teaching.Common.Absractions;

public interface IDetailable<TIdentity, TDetailModel>
{
    Task<TDetailModel> GetDetailAsync(TIdentity identity);
}
