using System;
using System.Collections.Generic;
using System.Text;

namespace Uber.Voyage.Domain.Repositories;

public interface IVoyageRepository
{

    //Task<Domain.Entities.Voyage?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Domain.Entities.Voyage voyage, CancellationToken ct = default);
    //Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}
