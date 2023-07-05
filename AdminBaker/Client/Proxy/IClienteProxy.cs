﻿using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IClienteProxy : ICrudRestHelper<ClienteDtoRequest, ClienteDto>
{
    
}