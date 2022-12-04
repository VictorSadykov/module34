using System.Linq;
using System.Threading.Tasks;
using ASP34.Data.Models;
using ASP34.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace ASP34.Data.Repos
{
    /// <summary>
    /// Репозиторий для операций с объектами типа "Room" в базе
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private readonly HomeApiContext _context;
        
        public RoomRepository (HomeApiContext context)
        {
            _context = context;
        }
        
        /// <summary>
        ///  Найти комнату по имени
        /// </summary>
        public async Task<Room> GetRoomByName(string name)
        {
            return  await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
        }
        
        /// <summary>
        ///  Добавить новую комнату
        /// </summary>
        public async Task AddRoom(Room room)
        {
            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);
            
            await _context.SaveChangesAsync();
        }

        public async Task<Room[]> GetRooms()
        {
            return await _context.Rooms
                .ToArrayAsync();
        }

        public async Task<Room> GetRoomById(Guid id)
        {
            return await _context.Rooms
                .Where(r => r.Id == id).FirstOrDefaultAsync();
        }


        public async Task UpdateRoom(Room room, EditRoomQuery editRoomQuery)
        {
            if (!string.IsNullOrEmpty(editRoomQuery.NewName))
                room.Name = editRoomQuery.NewName;

            room.Area = editRoomQuery.NewArea;
            room.GasConnected = editRoomQuery.NewGasConnected;
            room.Voltage = editRoomQuery.NewVoltage;

            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
            {
                _context.Rooms.Update(room);
            }

            await _context.SaveChangesAsync();
        }
    }
}