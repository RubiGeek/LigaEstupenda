using Microsoft.VisualStudio.TestTools.UnitTesting;
using LaLigaEstupenda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLigaEstupenda.Tests
{
    [TestClass()]
    public class LigaEstupendaTests
    {
        [TestCleanup()]
        void BorraTablas()
        {
            DataJugadorDataContext db = new DataJugadorDataContext();
            db.Jugadors.DeleteAllOnSubmit(db.Jugadors);
            db.SubmitChanges();
        }

        [TestMethod()]
        [ExpectedException(typeof(EquipoNoValido),
            "Número máximo de porteros excedido.")]
        public void comprobarEquipoAlineacion()
        {
            // Arrange
            this.PueblaTablasAlineacion();

            // Act
            try
            {
                LigaEstupenda miLiga = new LigaEstupenda();
                miLiga.comprobarEquipo("Madrid");
            }
            finally
            {

                this.BorraTablas();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(EquipoNoValido),
            "Número máximo de extracomunitarios excedido.")]
        public void comprobarEquipoExtranjeros()
        {
            // Arrange
            this.PueblaTablasExtranjeros();

            // Act
            try
            {
                LigaEstupenda miLiga = new LigaEstupenda();
                miLiga.comprobarEquipo("Madrid");
            }
            finally
            {

                this.BorraTablas();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(EquipoNoValido),
            "Valor total del equipo excedido.")]
        public void comprobarEquipoValor()
        {
            // Arrange
            this.PueblaTablasValor();

            // Act
            try
            {

                LigaEstupenda miLiga = new LigaEstupenda();
                miLiga.comprobarEquipo("Madrid");
            }
            finally
            {
                this.BorraTablas();
            }
        }

        void PueblaTablasAlineacion()
        {

            DataJugadorDataContext db = new DataJugadorDataContext();
            Jugador Jugador1 = new Jugador(1, "Rubén", "Madrid", "Portero", 4, "Español");
            db.Jugadors.InsertOnSubmit(Jugador1);
            Jugador Jugador2 = new Jugador(2, "Emilio", "Madrid", "Portero", 48, "Español");
            db.Jugadors.InsertOnSubmit(Jugador2);
            db.SubmitChanges();
        }

        void PueblaTablasExtranjeros()
        {
            DataJugadorDataContext db = new DataJugadorDataContext();
            Jugador Jugador1 = new Jugador(7, "Ronaldo", "Madrid", "Delantero", 10, "Extracomunitario");
            db.Jugadors.InsertOnSubmit(Jugador1);
            Jugador Jugador2 = new Jugador(8, "Ricardo", "Madrid", "Defensa", 9, "Extracomunitario");
            db.Jugadors.InsertOnSubmit(Jugador2);
            Jugador Jugador3 = new Jugador(9, "Felipe", "Madrid", "Defensa", 9, "Extracomunitario");
            db.Jugadors.InsertOnSubmit(Jugador3);
            Jugador Jugador4 = new Jugador(10, "Ronaldo", "Madrid", "Defensa", 9, "Extracomunitario");
            db.Jugadors.InsertOnSubmit(Jugador4);
            db.SubmitChanges();
        }

        void PueblaTablasValor()
        {
            DataJugadorDataContext db = new DataJugadorDataContext();
            Jugador Jugador1 = new Jugador(3, "Ronaldo", "Madrid", "Delantero", 80, "Español");
            db.Jugadors.InsertOnSubmit(Jugador1);
            Jugador Jugador2 = new Jugador(4, "Ricardo", "Madrid", "Defensa", 90, "Español");
            db.Jugadors.InsertOnSubmit(Jugador2);
            Jugador Jugador3 = new Jugador(5, "Felipe", "Madrid", "Defensa", 50, "Español");
            db.Jugadors.InsertOnSubmit(Jugador3);
            Jugador Jugador4 = new Jugador(6, "Ronaldo", "Madrid", "Defensa", 100, "Español");
            db.Jugadors.InsertOnSubmit(Jugador4);
            db.SubmitChanges();
        }
    }
}