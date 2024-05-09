namespace BisleriumPvtLtdBackendSample1.Services
{
    public class FileUploadService
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            try
            {

                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                string fileExtension = Path.GetExtension(file.FileName);

                // Generate the new filename with the timestamp suffix
                string newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}_{timestamp}{fileExtension}";

                var filePath = Path.Combine($"{_webHostEnvironment.WebRootPath}/images", newFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                string resultPath = $"https://localhost:7055/images/{newFileName}";
                return resultPath;

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}

