using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Http.Routing.Constraints;

namespace PracticeTest.Controllers
{
    /// <summary>
    /// This is Excercise one - use of async programming to download three resources
    /// </summary>
    public class ResourcesController : ApiController
    {
        List<string> addressList = new List<string>();
        CancellationTokenSource cts;

        public ResourcesController()
        {
            addressList.Add("https://www.microsoft.com/en-nz/windows");
            addressList.Add("https://www.microsoft.com/en-nz/windows/devices");
            addressList.Add("https://www.microsoft.com/en-nz/windows/windows-10-apps");

            //create new cancellation token source object 
            cts = new CancellationTokenSource();
        }

        // Api call - http://localhost:57975/api/resources/downloadresources
        [HttpGet]
        [Route("api/resources/downloadResources")]
        public async Task<string> GetDownloadResourcesAsync()
        {
            try
            {
                // Create http client 
                HttpClient client = new HttpClient();

                // To cancel the operation cancellation token source
                cts = new CancellationTokenSource();

                //Operation may be cancelled after 1000ms/1seconds if the download is not completed 
                //Try with 5000ms /5seconds as well so the download can be seen complete
                cts.CancelAfter(5000);

                IEnumerable<Task<int>> query =
                    from url in addressList
                    select DownloadResource(url, client, cts.Token);

                // Use ToArray to execute the query and start the download tasks.  
                Task<int>[] downloadTasks = query.ToArray();

                // Aggregate the Length of all downloaded tasks
                int[] lengths = await Task.WhenAll(downloadTasks);
                return ("Download Complete: " + (lengths.Sum() / 3));
            }
            catch (OperationCanceledException)
            {
                return ("Operation Cancelled");
            }
            catch (Exception)
            {
                return ("Exception Occured!");
            }
        }

        private async Task<int> DownloadResource(string address, HttpClient client, CancellationToken ct)
        {
            // sends a get request along with cancellation token
            HttpResponseMessage response = await client.GetAsync(address, ct);
            return (await response.Content.ReadAsByteArrayAsync()).Length;
        }
    }
}
