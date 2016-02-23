using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaEstupenda
{
    interface IJugadorRepository
    {
        int Create(Jugador jugador);
        List<Jugador> Read();
        Jugador Read(int id);
        void Update(int id, Jugador jugador);
        void Delete(int id);
    }
}
