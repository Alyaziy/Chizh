using Chizh.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chizh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private User24Context _context;

        public TrainController(User24Context context)
        {
            _context = context;
        }

        [HttpGet] //Вывод тренировок
        public async Task<ActionResult<IEnumerable<Train>>> GetTrains()
        {
            if (_context.Trains == null)
            {
                return NotFound();
            }
            return await _context.Trains.ToListAsync();
        }

        [HttpGet("{id}")] //Вывод трени по айди
        public async Task<ActionResult<Train>> GetTrains(int id)
        {
            if (_context.Trains == null)
            {
                return NotFound();
            }
            var train = await _context.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound(train);

            }
            return train;
        }

        [HttpPost("AddTrain")] //Добавление трени
        public async void AddTrain(TrainDTO train)
        {
            _context.Add(new Train
            {
                TrTittle = train.TrTittle,
                TrDescription = train.TrDescription,
                TrTime = train.TrTime,
                IdPoze = train.IdPoze,
                IdMuscle = train.IdMuscle
            });
            _context.SaveChanges();
        }

        [HttpPut("{id}")] //Редактирование трени
        public async Task<ActionResult<Train>> EditTrain(int id, TrainDTO trainDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTrain = await _context.Trains.FirstOrDefaultAsync(s => s.Id == id);
            if (existingTrain == null)
            {
                return NotFound();
            }

            existingTrain.TrTittle = trainDTO.TrTittle;
            existingTrain.TrDescription = trainDTO.TrDescription;
            existingTrain.TrTime = trainDTO.TrTime;
            existingTrain.IdPoze = trainDTO.IdPoze;
            existingTrain.IdMuscle = trainDTO.IdMuscle;

            _context.Entry(existingTrain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")] //Удаление трени
        public async Task<IActionResult> DeleteTrain(int id)
        {
            if (_context.Trains == null)
            {
                return NotFound();
            }
            var train = await _context.Trains.FindAsync(id);
            if (train == null)
            {
                return NotFound(train);

            }
            _context.Trains.Remove(train);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TrainExists(int id) 
        {
            return (_context.Trains?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
