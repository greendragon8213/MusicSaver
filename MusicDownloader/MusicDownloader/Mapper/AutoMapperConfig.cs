using AutoMapper;
using Logic.Models;
using MusicDownloader.Models;

namespace MusicDownloader.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper ServiceMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ContactMeMessageVM, ContactMeMessageDTO>();
            });

            return config.CreateMapper();
        }
    }
}
