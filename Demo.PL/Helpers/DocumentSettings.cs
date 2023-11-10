using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string folderName)
        {
            //1.get located folder path
            //string folderpath = "D:\\Route\\.NET\\material & assignments\\6-ASP .Net Core MVC\\5\\MVC Demo 3\\Demo.PL\\wwwroot\\files\\" + folderName;
            //string folderPath = Directory.GetCurrentDirectory()+ @"\wwwroot\files\"+folderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            //2.Get file name and make it uniqe
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get file path
            string filePath = Path.Combine(folderPath, fileName);

            //4.save file as stream : Data per time
            using var fileStream = new FileStream(filePath , FileMode.Create);

            fileStream.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName , string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files" , folderName , fileName);
            if (File.Exists(filePath) )
                File.Delete(filePath);
        }

         

    }
}
