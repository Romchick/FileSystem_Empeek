using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace HomeTestAplication.Controllers
{
    public class FileSystemController : ApiController
    {
        string _currentPath = @"D:\";
        // Method that returnd file and directory structure in list
        [HttpGet]
        public IEnumerable<string> GetAllSubfolers(string id)
        {
            List<string> subdirectoryList = new List<string>();
            _currentPath += id;
            var allSubfolders = Directory.GetDirectories(_currentPath);
            var files = Directory.GetFiles(_currentPath);
            string filesStr = "";
            string uStr = "";
            foreach (string subFold in allSubfolders)
            {
                uStr = subFold.Substring(_currentPath.Length);
                subdirectoryList.Add(uStr);
            }

            foreach (string subFiles in files)
            {
                filesStr = subFiles.Substring(_currentPath.Length);
                subdirectoryList.Add(filesStr);
            }
            return subdirectoryList;
        }

        // Method that counts amount of small, medium and large files in selected directory
        [HttpGet]
        public int[] GetFilesSize(string id)
        {
            int small, medium, large;
            small = medium = large = 0;
            _currentPath += id;
            string[] fileList = Directory.GetFiles(_currentPath, "*.*", SearchOption.AllDirectories);

            foreach (string file in fileList)
            {
                FileInfo fileInf = new FileInfo(file);
                if (fileInf.Length < 1024000)
                {
                    small += 1;
                }
                else if (fileInf.Length >= 1024000 && fileInf.Length < 5120000)
                {
                    medium += 1;
                }
                else if (fileInf.Length >= 5120000)
                {
                    large += 1;
                }

            }
            int[] fileSizeArray = new int[3] { small, medium, large };
            return fileSizeArray;
        }
  
   }
}
