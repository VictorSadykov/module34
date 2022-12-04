using System.Threading.Tasks;
using ASP34.Data.Models;
using ASP34.Data.Queries;

namespace ASP34.Data.Repos
{
    /// <summary>
    /// Интерфейс определяет методы для доступа к объектам типа Room в базе 
    /// </summary>
    public interface IRoomRepository
    {
        Task<Room> GetRoomByName(string name);
        Task AddRoom(Room room);
        Task<Room[]> GetRooms();
        Task<Room> GetRoomById(Guid id);
        Task UpdateRoom(Room room, EditRoomQuery editRoomQuery);
    }
}