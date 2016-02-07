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
        static string[] Scopes = {
            DriveService.Scope.DriveFile,
            DriveService.Scope.Drive,
            DriveService.Scope.DriveMetadata
        };

        const string ApplicationName = "Sagalingua1";
        const string User = "sagalingua";
        static int s_result = 0;

        public enum Direction {  Up, Down };

        public static bool DownloadStudentsFile(string cloudName, string target)
        {
            return TransferStudentsFile(cloudName, target, Direction.Down);
        }
        public static bool UploadStudentsFile(string localPath, string cloudName)
        {
            return TransferStudentsFile(cloudName, localPath, Direction.Up);
        }

        public static bool TransferStudentsFile(string cloudName, string target, Direction direction)
        {
            bool success = false;
            UserCredential credential;

            // Read access data
            using (var stream = new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, ".credentials/sagalingua1214");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    User,
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
            string fileId = null;

            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    string name = file.Name;
                    if (name != null && name.ToLower() == cloudName.ToLower())
                    {
                        fileId = file.Id;
                        break;
                    }
                }
            }

            if (fileId != null)
            {
                // We have found the file. Lets move it.
                if (direction == Direction.Down)
                {
                    if (System.IO.File.Exists(target))
                        System.IO.File.Delete(target);
                    success = ExecuteDownload(service, fileId, target);
                }
                else
                    success = ExecuteUpload(service, fileId, target, cloudName);
            }

            return success;
        }

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
                        long sz = stream.Length;
                        byte[] ba = stream.ToArray();
                        char[] ca = new char[sz];
                        for (int i = 0; i < sz; i++)
                            ca[i] = Convert.ToChar(ba[i]);
                        sw.Write(ca);
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

        public static bool ExecuteUpload(DriveService service, string fileId, string localPath, string cloudName)
        {
            byte[] byteArray = System.IO.File.ReadAllBytes(localPath);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);

            Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File();

            // Here we may need to add metadata as body.something 

            FilesResource.UpdateMediaUpload request =  service.Files.Update(body, fileId, stream, GetMimeType(localPath));
            request.Upload();
            Google.Apis.Drive.v3.Data.File response = request.ResponseBody;
            return (response != null);
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}
