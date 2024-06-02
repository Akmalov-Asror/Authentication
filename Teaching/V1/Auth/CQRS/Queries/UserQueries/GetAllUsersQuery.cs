using MediatR;
using Teaching.V1.Auth.Models.AuthModels;

namespace Teaching.V1.Auth.CQRS.Queries.UserQueries;

public record GetAllUsersQuery : IRequest<IList<UsersModel>> { }
