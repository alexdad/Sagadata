using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GDrive
{
    public class Ops
    {
        static string[] Scopes = { DriveService.Scope.DriveFile, DriveService.Scope.Drive, DriveService.Scope.DriveAppdata };
        static string ApplicationName = "Sagalingua1";
        static int s_result = 0;

        public static bool ExecuteDownload(DriveService service, string fileId, string target)
        {
            bool success = false;
            s_result = 0;
            try
            { 
                var request = service.Files.Get(fileId);
                var stream = new System.IO.MemoryStream();

                request.MediaDownloader.ProgressChanged +=

                    (Google.Apis.Download.IDownloadProgress progress) =>
                    {
                        switch (progress.Status)
                        {
                            case Google.Apis.Download.DownloadStatus.Downloading:
                                {
                                    s_result = 1;
                                    break;
                                }
                            case Google.Apis.Download.DownloadStatus.Completed:
                                {
                                    s_result = 2;
                                    break;
                                }
                            case Google.Apis.Download.DownloadStatus.Failed:
                                {
                                    s_result = 3;
                                    break;
                                }
                        }
                    };

                request.Download(stream);

                for (int i = 0; i < 20; i++)
                {
                    if (s_result > 1)
                        break;
                    Thread.Sleep(5000);
                }

                if (s_result == 2)
                {
                    using (StreamWriter sw = new StreamWriter(target))
                    {
                        int offset = 0, read = 0;
                        byte[] ba = new byte[1000];
                        char[] ca = new char[1000];
                        while ((read = stream.Read(ba, offset, 1000)) > 0)
                        {
                            for (int i = 0; i < read; i++)
                                ca[i] = Convert.ToChar(ba[i]);
                            offset += read;
                            sw.Write(ca, 0, read);
                        }
                    }
                    success = true;
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Failed to download: " + e.Message);
            }

            return success;
        }
        public static bool DownloadStudentsFile(string fileName, string target)
        {
            bool success = false;
            UserCredential credential;

            // Read access data
            using (var stream = new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, ".credentials/sagalingua1");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "sagalingua",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Find our file on the drive
            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            string fileId = null;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    string name = file.Name;
                    if (name != null && name.ToLower() == fileName.ToLower())
                    {
                        fileId = file.Id;
                        break;
                    }
                }
            }

            if (fileId != null)
            {
                if (System.IO.File.Exists(target))
                    System.IO.File.Delete(target);

                success = ExecuteDownload(service, fileId, target);
            }

            return success;
        }
    }
}
