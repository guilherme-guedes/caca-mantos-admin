using backend.Domain.Model;

namespace backend.Domain.Factories
{
    public class TimeFactory
    {
        public static Time CriarComId(Guid id) => new Time(id: id);
        public static Time CriarComIdentificador(string identificador) => new Time(id: Guid.Empty, identificador: identificador);
    }
}