using AutoMapper;

namespace MusicDownloader.Mapper
{
    internal class Mapper
    {
        private static volatile IMapper _mapper;
        private static readonly object SyncRoot = new object();

        private Mapper() { }
        
        internal static IMapper MapperInstance
        {
            get
            {
                if (_mapper == null)
                {
                    lock (SyncRoot)
                    {
                        if (_mapper == null)
                            _mapper = AutoMapperConfig.ServiceMapper();
                    }
                }

                return _mapper;
            }
        }
    }
}