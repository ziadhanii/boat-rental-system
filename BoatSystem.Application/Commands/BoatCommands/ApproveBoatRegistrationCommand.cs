﻿using BoatSystem.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.BoatCommands
{
    public class ApproveBoatRegistrationCommand : IRequest<Unit>
    {
        public int BoatId { get; set; }
    }

    public class ApproveBoatRegistrationCommandHandler : IRequestHandler<ApproveBoatRegistrationCommand, Unit>
    {
        private readonly IBoatRepository _boatRepository;

        public ApproveBoatRegistrationCommandHandler(IBoatRepository boatRepository)
        {
            _boatRepository = boatRepository;
        }

        public async Task<Unit> Handle(ApproveBoatRegistrationCommand request, CancellationToken cancellationToken)
        {
            var boat = await _boatRepository.GetByIdAsync(request.BoatId);
            if (boat != null)
            {
                boat.IsApproved = true; // تأكد من أن الخاصية موجودة في الكائن Boat
                await _boatRepository.UpdateAsync(boat); // تأكد من تنفيذ عملية التحديث بشكل صحيح
            }
            return Unit.Value;
        }
    }
}
