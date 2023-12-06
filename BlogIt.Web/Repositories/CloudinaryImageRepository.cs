
using CloudinaryDotNet;

namespace BlogIt.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            // had to remove way done during development
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();
            this.configuration = configuration;
            account = new Account(
                config["CloudName"],
                config["ApiKey"],
                config["ApiSecret"]);
        }

        public async Task<string?> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }

            return null;
        }
    }
}
