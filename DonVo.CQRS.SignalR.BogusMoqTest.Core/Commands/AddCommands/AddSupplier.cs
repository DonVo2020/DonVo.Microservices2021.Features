using AutoMapper;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.AddCommands
{
    public static class AddSupplier
    {
        public record Command(string Name, string ContactName, string PhoneNo, string FaxNo, string Address, string GSTReg) : IRequest<SupplierDetailResponse>;

        public class Handler : IRequestHandler<Command, SupplierDetailResponse>
        {
            private readonly ISupplierRepository _supplierRepository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly ILogger<Handler> _logger;
            private readonly IMapper _mapper;

            public Handler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, ILogger<Handler> logger, IMapper mapper)
            {
                _supplierRepository = supplierRepository;
                _unitOfWork = unitOfWork;
                _logger = logger;
                _mapper = mapper;
            }

            public async Task<SupplierDetailResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var supplier = new Supplier()
                {
                    Name = request.Name,
                    ContactName = request.ContactName,
                    PhoneNo = request.PhoneNo,
                    FaxNo = request.FaxNo,
                    Address = request.Address,
                    GSTReg = request.GSTReg
                };

                _logger.LogInformation($"Adding supplier {request}");
                await _supplierRepository.AddSupplierAsync(supplier, cancellationToken);

                if (!await _unitOfWork.SaveChangesAsync(cancellationToken))
                {
                    _logger.LogError($"Failed to add supplier: {request}");
                    throw new DbUpdateException("Failed to add supplier");
                }

                return _mapper.Map<SupplierDetailResponse>(supplier);
            }
        }

    }
}
