using Microsoft.AspNetCore.Components;

namespace AdminBaker.Client.Shared;

public partial class LoadingComponent
{
    [Parameter]
    public bool IsLoading { get; set; }
}