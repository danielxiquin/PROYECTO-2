using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conecta_4
{
    internal class CPU
    {
        public static string[,] clonarMatriz(string[,] tableroOriginal)
        {
            return tableroOriginal.Clone() as string[,];
        }

        public static int Columnaganadora(string jugador, string[,] tableroOriginal)
        {
            string[,] tablero = new string[Program.FILAS, Program.COLUMNAS];

            for(int i =0; i< Program.COLUMNAS-1; i++)
            {
                tablero = clonarMatriz(tableroOriginal);
                string resultado = Program.colocarpieza(jugador, i, tablero);

                int gana = Program.ganador(jugador, tablero);
                if (gana != Program.NoConecta)
                {
                    return i;
                }
                

            }
            return Program.Columnanoganadora;
        }

        public static int filallena(int columna, string[,] tablero)
        {
            for (int i = 0; i < Program.FILAS; ++i)
            {
                if (tablero[i, columna] != Program.vacio)
                {
                    return i;
                }
            }

            return Program.filanoencontrada;
        }

        public static int columnaaleatoria(string jugador, string[,] tableroOriginal) 
        {
            while (true)
            {
                string [,] tablero = new string[Program.FILAS, Program.COLUMNAS];
                tablero = clonarMatriz(tableroOriginal);
                Random re = new Random();
                int columna = re.Next(0, Program.COLUMNAS-1);
                string resultado = Program.colocarpieza(jugador,columna,tablero);
                if(resultado != null)
                {
                    return columna;
                }
            }
        }

        static int ColumnaCentral(string jugador, string[,] tableroOriginal)
        {
            string[,] tablero = new string[Program.FILAS, Program.COLUMNAS];
            tablero = clonarMatriz(tableroOriginal);
            int mitad = (Program.FILAS - 1) / 2;
            string resultado = Program.colocarpieza(jugador, mitad, tablero);
            if (resultado != null)
            {
                return mitad;
            }
            return Program.Columnanoganadora;
        }

        public static int eleccionCPU(string jugador, string[,] tablero)
        {
            int columganadora = Columnaganadora(jugador, tablero);
            if(columganadora != Program.Columnanoganadora)
            {

                Console.Write("*Gano*\n");
                return columganadora;
            }

            string oponente = Program.obteneroponente(jugador);
            int columoponente = Columnaganadora(oponente, tablero);
            if(columoponente != Program.Columnanoganadora)
            {
                List<string> frases = new List<string>();
                frases.Add("*Anda palla bobo*\n");
                frases.Add("*Tranquilo que soy listo*\n");
                Random rnd = new Random();
                string fraseAleatoria = frases[rnd.Next(frases.Count)];
                Console.WriteLine(fraseAleatoria);
                return columoponente;
            }

            int columna = columnaaleatoria(jugador,tablero);
            if(columna != Program.Columnanoganadora)
            {
                List<string> frases = new List<string>();
                frases.Add("*Nada es verdad todo esta permitido*\n");
                frases.Add("*que sea lo que tenga que ser*\n");
                frases.Add("*Un hombre fuerte no necesita saber su futuro, crea el suyo propio*\n");
                frases.Add("*Necesito un arma*\n");
                frases.Add("*Que sabes de sufrimiento si no has intentado jugar con lag!!*\n");
                



                Random rnd = new Random();
                string fraseAleatoria = frases[rnd.Next(frases.Count)];
                Console.WriteLine(fraseAleatoria);
                return columna;
            }

            int columcentral = ColumnaCentral(jugador,tablero);

            if(columcentral!= Program.Columnanoganadora)
            {
                Console.Write("*comienzo como pro*\n");
                return columcentral;
            }



            return 1000000;

        }


    }


}
