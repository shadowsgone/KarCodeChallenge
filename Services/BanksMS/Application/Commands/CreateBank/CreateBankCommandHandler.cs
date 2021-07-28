using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.CreateBank
{
    public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, int>
    {
        private readonly ILogger<CreateBankCommandHandler> _logger;
        private readonly IBankContext _context;

        public CreateBankCommandHandler(ILogger<CreateBankCommandHandler> logger, IBankContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<int> Handle(CreateBankCommand request, CancellationToken cancellationToken)
        {
            //TODO: We should validate that this bank, consider using AutoMapper 
            request.Bank.CreatedDate = DateTimeOffset.UtcNow;
            await _context.Banks.AddAsync(request.Bank);
            await _context.SaveChangesAsync();

            return request.Bank.Id;
        }
    }
}
