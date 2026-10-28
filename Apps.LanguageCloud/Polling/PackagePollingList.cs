using Apps.LanguageCloud.Models.ProjectPackages.Requests;
using Apps.LanguageCloud.Models.ProjectPackages.Responses;
using Apps.LanguageCloud.Polling.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using RestSharp;

namespace Apps.LanguageCloud.Polling;

[PollingEventList("Packages")]
public class PackagePollingList(InvocationContext invocationContext) : LanguageCloudInvocable(invocationContext)
{
    [PollingEvent("On package export finished", "Triggered when a package export reaches a terminal state.")]
    public async Task<PollingEventResponse<PackageOperationMemory, PackageExportStatusResponse>> OnPackageExportFinished(
        PollingEventRequest<PackageOperationMemory> request,
        [PollingEventParameter] PackageExportIdentifier input)
    {
        var statusRequest = new LanguageCloudRequest($"/packages/export/{input.ExportId}", Method.Get);
        var status = await Client.ExecuteWithErrorHandling<PackageExportStatusResponse>(statusRequest);

        return CreateResponse(request.Memory, status, status.Status);
    }

    [PollingEvent("On package import finished", "Triggered when a package import reaches a terminal state.")]
    public async Task<PollingEventResponse<PackageOperationMemory, PackageImportStatusResponse>> OnPackageImportFinished(
        PollingEventRequest<PackageOperationMemory> request,
        [PollingEventParameter] PackageImportIdentifier input)
    {
        var statusRequest = new LanguageCloudRequest($"/packages/import/{input.ImportId}", Method.Get);
        var status = await Client.ExecuteWithErrorHandling<PackageImportStatusResponse>(statusRequest);

        return CreateResponse(request.Memory, status, status.Status);
    }

    private static PollingEventResponse<PackageOperationMemory, T> CreateResponse<T>(
        PackageOperationMemory? memory,
        T result,
        string status)
    {
        var isTerminal = string.Equals(status, "Done", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(status, "Failed", StringComparison.OrdinalIgnoreCase);
        var shouldTrigger = isTerminal && memory?.Triggered != true;

        return new PollingEventResponse<PackageOperationMemory, T>
        {
            FlyBird = shouldTrigger,
            Result = shouldTrigger ? result : default,
            Memory = new PackageOperationMemory
            {
                Triggered = memory?.Triggered == true || isTerminal
            }
        };
    }
}
