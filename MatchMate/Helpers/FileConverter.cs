
namespace MatchMate.Helpers
{
    public static class FileConverter
    {
        public static string ConvertFormFileToString(IFormFile file)
        {
            MemoryStream memoryStream = new MemoryStream();
            file.OpenReadStream().CopyTo(memoryStream);

            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}
