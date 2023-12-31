﻿using AdminBaker.Shared.Response;
using System.Net.Http.Json;

namespace AdminBaker.Client.Proxy;

public abstract class CrudRestHelperBase<TRequest, TResponse> 
    : ICrudRestHelper<TRequest, TResponse> 
    where TRequest : class
    where TResponse : class
{
    protected readonly HttpClient HttpClient;

    protected CrudRestHelperBase(string baseUrl, HttpClient httpClient)
    {
        HttpClient = httpClient;
        BaseUrl = baseUrl;
    }

    public string BaseUrl { get; set; }

    public async Task<PaginationResponse<TResponse>> ListAsync(string? filter, int page = 1, int pageSize = 5)
    {
        var response = await HttpClient.GetFromJsonAsync<PaginationResponse<TResponse>>($"{BaseUrl}{filter}");
        if (response!.Success)
            return response;

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task<ICollection<TResponse>> ListAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<PaginationResponse<TResponse>>($"{BaseUrl}");
        if (response!.Success)
            return response.Data!;

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task<TResponse> FindByIdAsync(int id)
    {
        var response = await HttpClient.GetFromJsonAsync<BaseResponseGeneric<TResponse>>($"{BaseUrl}/{id}");
        if (response!.Success)
            return response.Data!;

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task CreateAsync(TRequest request)
    {
        var response = await HttpClient.PostAsJsonAsync(BaseUrl, request);
        if (response.IsSuccessStatusCode)
        {
            var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
            if (resultado!.Success == false)
                throw new InvalidOperationException(resultado.ErrorMessage);
        }
        else
        {
            throw new InvalidOperationException(response.ReasonPhrase);
        }
    }

    public async Task UpdateAsync(int id, TRequest request)
    {
        var response = await HttpClient.PutAsJsonAsync($"{BaseUrl}/{id}", request);
        if (response.IsSuccessStatusCode)
        {
            var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
            if (resultado!.Success == false)
                throw new InvalidOperationException(resultado.ErrorMessage);
        }
        else
        {
            throw new InvalidOperationException(response.ReasonPhrase);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var response = await HttpClient.DeleteAsync($"{BaseUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
            if (resultado!.Success == false)
                throw new InvalidOperationException(resultado.ErrorMessage);
        }
        else
        {
            throw new InvalidOperationException(response.ReasonPhrase);
        }
    }
}