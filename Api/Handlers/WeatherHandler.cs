using Api.Commands;

namespace Api.Handlers
{
    public class WeatherHandler : ICommandHandler<WeatherCommand>
    {
        public void Handle(WeatherCommand command)
        {
            // lógica de alteração de estado
            // ex: salvar no banco, disparar evento, etc.
        }
    }

}
