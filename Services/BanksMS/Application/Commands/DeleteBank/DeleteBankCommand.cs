using Domain;
using MediatR;

namespace Application.Commands.DeleteBank
{
    public class DeleteBankCommand : IRequest<Bank>
    {
        public int Id { get; set; }
        public DeleteBankCommand(int id)
        {
            Id = id;
        }
    }
}
