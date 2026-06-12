using System;
using System.Collections.Generic;
using System.Text;
using Uber.Voyage.Domain.Sagas;

namespace Uber.Voyage.Domain.Repositories;

public interface IVoyageSagaRepository
{
    Task<VoyageSagaState?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<VoyageSagaState> AddAsync(VoyageSagaState voyageSagaState, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);

}
