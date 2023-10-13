using AutoMapper;

namespace OnlineShop.Library.Mapping
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile);
    }
}
