using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Uber.Voyage.Application.Abstractions;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}