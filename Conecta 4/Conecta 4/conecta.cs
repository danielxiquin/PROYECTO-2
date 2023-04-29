using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Conecta_4
{
    internal class conecta
    {
        public static string[,] piezas = new string[Program.FILAS, Program.COLUMNAS];

        public static int conectavertical(int x, int y, string jugador, string[,] tablero)
        {
            int contador = 0;
            for (int i = y; i >= 0 && i >= y-Program.CONECTA+1; i--)
            {
                if (tablero[i,x] == jugador)
                {
                    piezas[i, y] = jugador;
                    contador++;
                }
                else
                {
                    break;
                }
            }
            return contador;
        }

        public static  int conectahorizontal(int x, int y, string jugador, string[,]tablero)
        {
            int contador=0;
            for(int i = x; i <= Program.COLUMNAS-1 && i <= x + Program.CONECTA -1; i++)
            {
                if (tablero[y,i]== jugador)
                {
                    contador++;
                }
                else
                {
                    break;
                }
            }
            return contador;    
        }


        public static int contardiagonalD(int x, int y, string jugador, string[,] tablero)
        {
            int contador = 0;
            for (int i = x, j = y; i <= Program.COLUMNAS-1 && j >= 0 && i <= x + Program.CONECTA - 1 && j >= y - Program.CONECTA + 1; i++, j--)
            {
                if (tablero[j, i] == jugador)
                {
                    contador++;
                }
                else
                {
                    break;
                }
            }
            return contador;
        }

        public static  int contadorI(int x, int y, string jugador, string[,] tablero)
        {
            int contador = 0;
            for (int i = x, j = y; i <= Program.COLUMNAS-1 && j <= Program.FILAS-1 && i <= x + Program.CONECTA - 1 && j <= y + Program.CONECTA - 1; i++, j++)
            {
                if (tablero[j, i] == jugador)
                {
                    contador++;
                }
                else
                {
                    break;
                }
            }
            return contador;
        }
        
    }
}
