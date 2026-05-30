using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Uber.Shared.Primitives;

namespace Uber.Voyage.Application.Features.GetVoyage;

public sealed record GetVoyageByIdQuery(Guid VoyageId) : IRequest<Result<GetVoyageByIdResponse>>;

