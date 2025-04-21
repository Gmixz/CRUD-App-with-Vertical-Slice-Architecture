using MediatR;
using Microsoft.AspNetCore.Mvc;
using VideoGameApiVsa.Data;

namespace VideoGameApiVsa.Features.VideoGames
{
    public static class DeleteGame
    {
        public record Command(int Id) : IRequest<bool>;
     
        public class Handler(VideoGameDbContext context) : IRequestHandler<Command, bool>
        {
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var videoGame = await context.VideoGames.FindAsync(request.Id);
                if(videoGame == null)
                {
                    return false;
                }

                context.VideoGames.Remove(videoGame);

                await context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
    }

    [ApiController]
    [Route("api/games")]

    public class DeleteGameControlller(ISender sender) : ControllerBase
    {
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteGame(int id)
        {
            var response = await sender.Send(new DeleteGame.Command(id));
            if (!response)
            {
                return NotFound("Video game with given Id not found");
            }

            return NoContent();
        }
    }
}
