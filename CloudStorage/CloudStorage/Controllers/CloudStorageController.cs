using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;

namespace webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudStorageController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> GetFileContent()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=lab1diag679;AccountKey=TtymI1zwujPy40v9NvlAApX1o04SSA4Jnj2TAH4VvlnoOiPbCym4Rt9qLZwVRgeKgPIHskJBcRtcW0Rt9vxFZw==;EndpointSuffix=core.windows.net");
            CloudFileClient fileClient = storageAccount.CreateCloudFileClient();
            CloudFileShare share = fileClient.GetShareReference("lab1-fs");
            if (share.ExistsAsync().Result)
            {
                // Get a reference to the root directory for the share.
                CloudFileDirectory rootDir = share.GetRootDirectoryReference();

                // Get a reference to the directory we created previously.
                CloudFile file = rootDir.GetFileReference("Test1.txt");

                if (file.ExistsAsync().Result)
                {
                    // Write the contents of the file to the console window.
                    return file.DownloadTextAsync().Result;
                }
            }
            return "file not found";
        }
    }
}
