using backend.Domain.Model;

namespace backend.Domain.Factories
{
    public class TimeFactory
    {
        public static Time CriarComId(string id) => new Time(id: id);
        public static Time CriarComIdentificador(string identificador) => new Time(id:null, identificador: identificador);
    }
}