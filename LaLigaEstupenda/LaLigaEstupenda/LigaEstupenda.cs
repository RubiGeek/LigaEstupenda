using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Data.Linq;
using System.Reflection;

namespace LaLigaEstupenda
{
    public class LigaEstupenda
    {
        string path = Path.Combine(Environment.CurrentDirectory, @"Log files\", @"Excepciones.log");

        int introducir(Jugador jugador)
        {
            int id = jugador.Create(jugador);
            return id;
        }

        void eliminar(Jugador jugador)
        {
            jugador.Delete(jugador.Id);
        }

        List<Jugador> devolverEquipo(string nombreEquipo)
        {
            using (DataJugadorDataContext db = new DataJugadorDataContext())
            {
                var jugadoresEquipo = db.Jugadors.Where(jugador => jugador.Equipo == nombreEquipo);

                List<Jugador> equipo = new List<Jugador>();
                foreach (var jugador in jugadoresEquipo)
                {
                    equipo.Add(jugador);
                }
                return equipo;
            }
        }

        public void comprobarEquipo(string nombreEquipo)
        {
            using (DataJugadorDataContext db = new DataJugadorDataContext())
            {
                var jugadoresEquipo = db.Jugadors.Where(jugador => jugador.Equipo == nombreEquipo);
                try
                {
                    var numPorteros = jugadoresEquipo.Where(jugador => jugador.Posición == "Portero").Count();
                    if (numPorteros > 1)
                    {
                        throw new EquipoNoValido("Número máximo de porteros excedido.");
                    }

                    var numDefensas = jugadoresEquipo.Where(jugador => jugador.Posición == "Defensa").Count();
                    if (numDefensas > 4)
                    {
                        throw new EquipoNoValido("Número máximo de defensas excedido.");
                    }

                    var numMedios = jugadoresEquipo.Where(jugador => jugador.Posición == "Medio").Count();
                    if (numDefensas > 4)
                    {
                        throw new EquipoNoValido("Número máximo de medios excedido.");
                    }

                    var numDelanteros = jugadoresEquipo.Where(jugador => jugador.Posición == "Delantero").Count();
                    if (numDelanteros > 2)
                    {
                        throw new EquipoNoValido("Número máximo de delanteros excedido.");
                    }

                    var numExtracomunitarios = jugadoresEquipo.Where(jugador => jugador.Nacionalidad == "Extracomunitario").Count();
                    if (numExtracomunitarios > 3)
                    {
                        throw new EquipoNoValido("Número máximo de extracomunitarios excedido.");
                    }

                    var valorTotalEquipo = jugadoresEquipo.AsEnumerable().Sum(jugador => jugador.Valor);
                    int intValorTotalEquipo = Convert.ToInt32(Convert.ToDouble(valorTotalEquipo));
                    if (intValorTotalEquipo > 200)
                    {
                        throw new EquipoNoValido("Valor total del equipo excedido.");
                    }

                }
                catch (EquipoNoValido ex)
                {
                    using (StreamWriter writer = new StreamWriter(path , true))
                    {
                        writer.WriteLine(ex.Message);
                        throw ex;
                    }
                }
            }
        }

        List<Tuple<string, int>> promedioPorNacionalidad()
        {
            using (DataJugadorDataContext db = new DataJugadorDataContext())
            {
                var valorMedioNacionalidad =
                    (from jugadores in db.Jugadors
                     group jugadores by jugadores.Nacionalidad into grpNacionalidad
                     select new
                     {
                         Nacionalidad = grpNacionalidad.Key,
                         ValorMedio = grpNacionalidad.Average(jugador => jugador.Valor)
                     });
                List<Tuple<string, int>> promedioNacionalidad = new List<Tuple<string, int>>();
                foreach (var valorNacionalidad in valorMedioNacionalidad)
                {
                    promedioNacionalidad.Add(new Tuple<string, int>(valorNacionalidad.Nacionalidad, Convert.ToInt32(Convert.ToDouble(valorNacionalidad.ValorMedio))));
                }

                return promedioNacionalidad;
            }
        }
    }

    public class EquipoNoValido : System.Exception
    {
        public EquipoNoValido(string message)
        : base(String.Format("{0}", message))
        { }
    }
}
