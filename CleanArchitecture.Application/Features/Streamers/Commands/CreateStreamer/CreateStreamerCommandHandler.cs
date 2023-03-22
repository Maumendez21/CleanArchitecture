using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {

        //private readonly IStreamerRepository _streamerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            Streamer streamerEntity = _mapper.Map<Streamer>(request);

            _unitOfWork.Repository<Streamer>().AddEntity(streamerEntity);
            int result = await _unitOfWork.Complete();

            if (result <= 0) throw new Exception("NO se pudo crear el record de streamer");

            _logger.LogInformation($"Streamer {streamerEntity.Id} fue creadoxitosamente");
            await SendEmail(streamerEntity);
            return streamerEntity.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {
            Email email = new Email
            {
                To = "maumendezm21@hotmail.com",
                Body = "La compañia de streamer se creo correctamente",
                Subject = "Mensaje de alerta"
            };

            try
            {
                bool send = await _emailService.SendEmail(email);

            }
            catch (Exception e)
            {
                _logger.LogError($"Errores enviando el email de streamer {streamer.Id} {e.Message}");
            }

        }
    }
}
