using Teaching.V1.Auth.Controllers;

namespace Teaching.Common.Absractions
{
    public interface ICrudable<TIdentity, TCreateModel, TUpdateModel, TDetailModel> :
    ICreatable<TIdentity, TCreateModel>,
    IUpdatable<TIdentity, TUpdateModel>,
    IDetailable<TIdentity, TDetailModel>,
    IDeletable<TIdentity>
    where TIdentity : BaseIdentityModel
    { }
}
