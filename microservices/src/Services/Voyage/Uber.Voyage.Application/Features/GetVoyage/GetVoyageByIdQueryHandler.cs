using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Uber.Shared.Primitives;
using Uber.Voyage.Application.Abstractions;
using Uber.Voyage.Domain.Errors;
using Uber.Voyage.Domain.Repositories;
namespace Uber.Voyage.Application.Features.GetVoyage;

internal sealed class GetVoyageByIdQueryHandler(IDbConnectionFactory dbConnectionFactory) : IRequestHandler<GetVoyageByIdQuery, Result<GetVoyageByIdResponse>>
{
 
    public async Task<Result<GetVoyageByIdResponse?>> Handle(GetVoyageByIdQuery query, CancellationToken ct)
    {
        const string sql = @"
            SELECT 
               *
            FROM Voyages 
            WHERE Id = @VoyageId";

        using IDbConnection connection = dbConnectionFactory.CreateConnection();
        var voyageResponse = await connection.QueryFirstOrDefaultAsync<GetVoyageByIdResponse>(
                  new CommandDefinition(sql, new { query.VoyageId }, cancellationToken: ct));
        return voyageResponse is null ? VoyageErrors.NotFound : voyageResponse;
    }
}
