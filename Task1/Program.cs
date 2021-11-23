using AutoMapper;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TestTaskLib.Models.DataDb;
using static TestTaskLib.DTOs.RandomUserDto;

namespace Task1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var mapper = GetMapper();

            var users = GetUsers();
            users.Wait();

            using (var dbContext = new ApplicationContext())
            {
                foreach(var user in users.Result.Results)
                {
                    var dbUser = mapper.Map<RandomUser>(user);
                    DownloadFileDataToUser(ref dbUser);
                    dbContext.RandomUsers.Add(dbUser);
                }
                dbContext.SaveChanges();
            }
        }

        public static RandomUser DownloadFileDataToUser(ref RandomUser user)
        {
            using (var client = new WebClient())
            {
                var largePictureData = client.DownloadData(user.Picture.Large);
                var mediumPictureData = client.DownloadData(user.Picture.Medium);
                var tumbnailPictureData = client.DownloadData(user.Picture.Thumbnail);

                user.Files.Add( new File
                {
                    Name = user.Picture.Large.Substring(user.Picture.Large.LastIndexOf('/') + 1),
                    Data = largePictureData,
                    FileType = FileType.Large,
                    RandomUser = user
                });

                user.Files.Add(new File
                {
                    Name = user.Picture.Medium.Substring(user.Picture.Medium.LastIndexOf('/') + 1),
                    Data = largePictureData,
                    FileType = FileType.Medium,
                    RandomUser = user
                });

                user.Files.Add(new File
                {
                    Name = user.Picture.Thumbnail.Substring(user.Picture.Thumbnail.LastIndexOf('/') + 1),
                    Data = largePictureData,
                    FileType= FileType.Thumbnail,
                    RandomUser = user
                });

                return user;
            }
        }

        public static async Task<Response> GetUsers()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://randomuser.me/api/?results=1000");
            var result = await response.Content.ReadAsStringAsync();
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(result);

            return user;
        }

        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Result, RandomUser>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(dst => dst.Name.First))
                .ForMember(d => d.LastName, opt => opt.MapFrom(dst => dst.Name.Last))
                .ForMember(d => d.UserName, opt => opt.MapFrom(dst => dst.Login.Username))
                .ForMember(d => d.Salt, opt => opt.MapFrom(dst => dst.Login.Salt))
                .ForMember(d => d.MD5, opt => opt.MapFrom(dst => dst.Login.MD5))
                .ForMember(d => d.SHA1, opt => opt.MapFrom(dst => dst.Login.SHA1))
                .ForMember(d => d.SHA256, opt => opt.MapFrom(dst => dst.Login.SHA256))
                .ForMember(d => d.DateOfBirthday, opt => opt.MapFrom(dst => dst.DoB.Date))
                .ForMember(d => d.Registered, opt => opt.MapFrom(dst => dst.Registered.Date))
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ReverseMap()
            );

            return new Mapper(config);
        }
    }
}
