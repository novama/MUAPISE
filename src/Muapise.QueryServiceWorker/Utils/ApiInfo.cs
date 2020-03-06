namespace Muapise.QueryServiceWorker.Utils
{
    internal class ApiInfo
    {
        internal const string ApiSegmentName = "workerapi";
        internal const string ApiDocsUiSegmentName = "api-docs-tool";
        internal const string ApiDocsSegmentName = "api-docs";
        internal const string CurrentApiVersion = "v1";
        internal const string ApiDescription = "Worker API";

        internal const string DefaultApiRoute = ApiSegmentName + "/" + CurrentApiVersion + "/[controller]";
        internal const string DefaultApiSwaggerEndPoint = ApiDocsSegmentName + "/" + CurrentApiVersion + "/swagger.json";
        internal const string DefaultApiSwaggerRouteTemplate = ApiDocsSegmentName + "/{documentName}/swagger.json";
        internal static readonly string DefaultApiSwaggerName = AppInfo.AppName + " " + CurrentApiVersion;
    }
}
