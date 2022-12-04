using System.Threading.Tasks;
using AutoMapper;
using ASP34.Contracts.Models.Rooms;
using ASP34.Data.Models;
using ASP34.Data.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using ASP34.Data.Queries;
using Azure.Core;

namespace ASP34.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _repository.GetRooms();

            var resp = new GetRoomsResponse()
            {
                RoomAmount = rooms.Length,
                Rooms = _mapper.Map<Room[], RoomView[]>(rooms)
            };

            return StatusCode(200, resp);
        }
        
        
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }
        
        /// <summary>
        /// Обновление существующей комнаты
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit(
            [FromRoute] Guid id,
            [FromBody] EditRoomRequest request)
        {
            var room = await _repository.GetRoomById(id);
            if (room == null)
                return StatusCode(400, $"Ошибка: Комната с таким идентификатором не найдена.");

            await _repository.UpdateRoom(room, new EditRoomQuery()
            {
                NewName = request.Name,
                NewGasConnected = request.GasConnected,
                NewArea = request.Area,
                NewVoltage = request.Voltage
            });

            return StatusCode(200, $"Комната успешно обновлена!");
        }
    }
}