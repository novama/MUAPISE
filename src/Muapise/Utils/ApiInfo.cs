namespace Muapise.Utils
{
    internal class ApiInfo
    {
        internal const string ApiSegmentName = "api";
        internal const string ApiDocsUiSegmentName = "api-docs-tool";
        internal const string ApiDocsSegmentName = "api-docs";
        internal const string CurrentApiVersion = "v1";
        internal const string ApiDescription = "Main API";

        internal const string DefaultApiRoute = ApiSegmentName + "/" + CurrentApiVersion + "/[controller]";
        internal const string DefaultApiSwaggerEndPoint = ApiDocsSegmentName + "/" + CurrentApiVersion + "/swagger.json";
        internal const string DefaultApiSwaggerRouteTemplate = ApiDocsSegmentName + "/{documentName}/swagger.json";
        internal static readonly string DefaultApiSwaggerName = AppInfo.AppName + " " + CurrentApiVersion;
        internal static string ApiWorkerSegment ="/workerapi/v1/";
    }
}
