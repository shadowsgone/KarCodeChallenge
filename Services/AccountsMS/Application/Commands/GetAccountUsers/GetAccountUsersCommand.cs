using MediatR;

namespace Application.Commands.GetAccountUsers
{
    public class GetAccountUsersCommand : IRequest<GetAccountUsersResponse>
    {
        public int Id { get; set; }
        public GetAccountUsersCommand(int id)
        {
            Id = id;
        }
    }
}
