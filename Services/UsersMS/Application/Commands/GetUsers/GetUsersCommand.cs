using MediatR;

namespace Application.Commands.GetUsers
{
    public class GetUsersCommand : IRequest<GetUsersResponse>
    {
        //TODO: we can add different params here to allow for paging and filtering.
    }
}
