using System.Diagnostics;
using System.Text.Json;
using HashHandler.Common;
using HashHandler.HashProvider;
using HashHandler.Models;
using HashHandler.Services.Interfaces;
using HashHandler.Shared.Models.Domain;
using HashHandler.Shared.Repositories.Interfaces;

namespace HashHandler.Services
{
    public class HashService : IHashService
    {
        private readonly IHashesRepository _hashesRepository;
        private readonly IHashProvider _hashProvider;
        private readonly IRabbitMqService _rabbitMqService;

        public HashService(IHashesRepository hashesRepository,
            IHashProvider hashProvider,
            IRabbitMqService rabbitMqService)
        {
            _hashesRepository = hashesRepository;
            _hashProvider = hashProvider;
            _rabbitMqService = rabbitMqService;
        }

        public async Task<HashesResponse> GetHashesAsync(CancellationToken cancellationToken)
        {
            var hashes = await _hashesRepository.GetHashesAsync(cancellationToken);
            
            var hashResponses = hashes
                .GroupBy(x => x.Date)
                .Select(h => new HashResponse
                {
                    Date = h.Key,
                    Count = h.Count()
                })
                .ToArray();
            
            return new HashesResponse
            {
                HashResponse = hashResponses
            };
        }

        public async Task GenerateAndSendAsync(CancellationToken cancellationToken)
        {
            var hashes = await GenerateHashesAsync(cancellationToken);
            await SendHashesAsync(hashes, cancellationToken);
        }

        private async Task<HashesData> GenerateHashesAsync(CancellationToken cancellationToken)
        {
            return await _hashProvider.GenerateAsync(Defaults.HashQuantity, cancellationToken);
        }

        private async Task SendHashesAsync(HashesData hashes, CancellationToken cancellationToken)
        {
            var message = JsonSerializer.Serialize(hashes);
            await _rabbitMqService.SendMessage(message, cancellationToken);
        }
    }
}