using System;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;


namespace Conecta_4
{
    internal class Program
    {

        public static int FILAS = 6;
        public static int COLUMNAS = 7;
        public static int CONECTA = 4;
        static string jugador1 = "x";
        static string jugador2 = "o";
        public static string vacio = " ";
        static int modo_jugadorvsCPU = 1;
        static int modo_jugadorvsjugador = 2;
        static int historial = 3;
        static int salir = 4;
        static string jugadorCPU = jugador2;
        public static int NoConecta = 100000;
        public static int Columnanoganadora = 100000;
        public static int ErrorNinguno = 1000000;
        public static int filanoencontrada = 1000000;
        static int cont = 0;
        static int cont1 = 0;
        static int contgan = 0;
        static string gandor = " ";
        static string[] partidas = new string[10];



        static void Main(string[] args)
        {
            Stopwatch st = new Stopwatch();

            bool continuar = false;
            while(!continuar)
            {
                Color customColor = Color.FromArgb(12, 26, 23, 255);
                ConsoleColor consoleCustomColor = (ConsoleColor)customColor.ToArgb();

                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;

                    Console.WriteLine("---------------CONECTA 4----------------");
                    Console.WriteLine("-------Ingrese la opcion de juego-------");
                    Console.WriteLine($"{modo_jugadorvsCPU}. Jugador VS CPU");
                    Console.WriteLine($"{modo_jugadorvsjugador}. Jugador1 VS Jugador2");
                    Console.WriteLine($"{historial}. Historial de partidas jugadas");
                    Console.WriteLine($"{salir}. Salir del menu");
                    int modo = Convert.ToInt32(Console.ReadLine());
                    if (modo != modo_jugadorvsCPU && modo != modo_jugadorvsjugador && modo != historial)
                    {
                        Console.Clear();

                        if (salir != modo)
                        {
                            Console.WriteLine("Debe ingresar un numero que contenga el menu");
                        }
                        else
                        {
                            Console.WriteLine("saliendo del juego...");
                            continuar = true;
                        }
                        Console.ReadKey();
                        Console.Clear();



                    }
                    else if (modo == historial)
                    {
                        Console.Clear();

                        string tiempo = st.Elapsed.ToString();
                        bool unos = false;
                        histori(gandor, contgan, tiempo);

                        

                        st.Reset();

                        cont = 0;
                        cont1 = 0;
                        contgan = 0;




                    }
                    else
                    {
                        st.Start();
                        Console.Clear();
                        jugar(modo);
                        st.Stop();


                    }

                    Console.ReadKey();
                    Console.Clear();
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Debe ingresar un elemento correcto");
                    Console.ReadKey();
                    Console.Clear();
                }
                

                
               

            }


        }


        public static void histori(string ganador, int turnos, string tiempo)
        {
           
          

            for (int i = 9; i >= 1; i--)
            {
                partidas[i] = partidas[i - 1];
            }

            partidas[0] = $"|El ganador es: {ganador} | Turnos: {turnos} | Tiempo de partida {tiempo}";

            
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(partidas[i] ?? "(sin datos)");
            }

        }



        public static void tablerovacio(string[,] tablero)
        {
            for(int i = 0; i < FILAS; i++)
            {
                for(int j = 0; j <COLUMNAS; j++)
                {
                    tablero[i, j] = vacio;
                }
            }
        }

        public static void imprimirTablero(string[,] tablero)
        {
            for (int i = 0; i < FILAS; i++)
            {
                for (int j = 0; j < COLUMNAS; j++)
                {
                    Console.Write($"[{tablero[i,j]}]");
                   
                }
                Console.WriteLine();
            }


            int[] colum = new int[COLUMNAS];
            for (int f= 0 ; f<COLUMNAS; f++)
            {
                colum[f] = f+1;
                Console.Write(" "+colum[f] + " ");
            }
            Console.WriteLine("");
        }

        public static int pociciondeficha()
        {
            Console.WriteLine("Ingrese la columna en que colocara su ficha");
            int columna = Convert.ToInt32(Console.ReadLine());

            columna--;
            return columna;
        }

        public static string colocarpieza(string jugador, int columna, string[,] tablero)
        {

            for (int i = 5; i >= 0; i--)
            {
                if (!tablero[i, columna].Contains(jugador1) && !tablero[i, columna].Contains(jugador2))
                {
                    tablero[i, columna] = jugador;
                    return tablero[i, columna];
                }



            }
            

            return "";

        }

        

        

        static string jugadorazar()
        {
            Random r = new Random();
            int numero = r.Next(1, 3);

            if(numero == 1)
            {
                return jugador1;
            }
            else
            {
                return jugador2;
            }

        }
        public static string obteneroponente(string jugador)
        {
            if(jugador == jugador1)
            {
                return jugador2;
            }
            else
            {
                return jugador1;
            }
        }

        static bool esEmpate(string[,] tablero)
        {
            for (int i = 0; i < COLUMNAS; ++i)
            {
                int resultado = obtenerFilaDesocupada(i, tablero);
                if (resultado != filanoencontrada)
                {
                    return false;
                }
            }
            return true;
        }

        static int obtenerFilaDesocupada(int columna, string[,] tablero)
        {

            for (int i = tablero.GetLength(0)-1; i >= 0; i--)
            {
                if (tablero[i, columna] == vacio)
                {
                    return i;
                }
            }
            return filanoencontrada;
        }
        static int Filadesocupada(int columna, string[,] tablero, out bool columnaLlena)
        {
            columnaLlena = true;
            for (int i = tablero.GetLength(0) - 1; i >= 0; i--)
            {
                if (tablero[i, columna] == vacio)
                {
                    columnaLlena = false;
                    return i;
                }
            }
            return filanoencontrada;
        }

        public static int ganador(string jugador, string[,] tablero)
        {

            for(int y= 0; y < FILAS; y++)
            {
                for (int x = 0; x < COLUMNAS; x++)
                {
                    int conectavertical = conecta.conectavertical(x,y,jugador,tablero);
                    if (conectavertical >= CONECTA)
                    {
                        if (jugador == jugador2)
                        {
                            tablero[y, x] = "♠";
                            tablero[y - 1, x] = "♠";
                            tablero[y - 2, x] = "♠";
                            tablero[y - 3, x] = "♠";
                        }
                        else if (jugador == jugador1)
                        {
                            tablero[y, x] = "♦";
                            tablero[y - 1, x] = "♦";
                            tablero[y - 2, x] = "♦";
                            tablero[y - 3, x] = "♦";
                        }
                        return 10000;
                    }else if(conecta.conectahorizontal(x,y,jugador,tablero) >= CONECTA)
                    {
                        if (jugador == jugador2)
                        {
                            tablero[y, x] = "♠";
                            tablero[y, x + 1] = "♠";
                            tablero[y, x+2] = "♠";
                            tablero[y, x+3] = "♠";
                        }
                        else if (jugador == jugador1)
                        {
                            tablero[y, x] = "♦";
                            tablero[y, x+1] = "♦";
                            tablero[y, x+2] = "♦";
                            tablero[y, x+3] = "♦";
                        }
                        return 10000;
                    }else if(conecta.contardiagonalD(x,y,jugador,tablero) >= CONECTA)
                    {
                        if (jugador == jugador2)
                        {
                            tablero[y, x] = "♠";
                            tablero[y - 1, x + 1] = "♠";
                            tablero[y - 2, x + 2] = "♠";
                            tablero[y - 3, x + 3] = "♠";
                        }
                        else if (jugador == jugador1)
                        {
                            tablero[y, x] = "♦";
                            tablero[y - 1, x + 1] = "♦";
                            tablero[y - 2, x + 2] = "♦";
                            tablero[y - 3, x + 3] = "♦";
                        }
                        return 10000;
                    }else if(conecta.contadorI(x,y,jugador,tablero) >= CONECTA)
                    {
                        if (jugador == jugador2)
                        {
                            tablero[y, x] = "♠";
                            tablero[y + 1, x + 1] = "♠";
                            tablero[y + 2, x + 2] = "♠";
                            tablero[y + 3, x + 3] = "♠";
                        }
                        else if (jugador == jugador1)
                        {
                            tablero[y, x] = "♦";
                            tablero[y + 1, x + 1] = "♦";
                            tablero[y + 2, x + 2] = "♦";
                            tablero[y + 3, x + 3] = "♦";
                        }
                        return 10000;
                    } 


                }
            }

            

            return NoConecta;

        } 


        static async void jugar(int modo)
        {
            bool continuar = false;

            while (!continuar)
            {
                
                string[,] tablero = new string[FILAS, COLUMNAS];
                tablerovacio(tablero);
                string jugadoractual = jugadorazar();
                string nombreactualjugador = "";
                string name1 = "";
                string name2 = "";
                if (modo == modo_jugadorvsjugador)
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Ingrese el nombre del jugador 1");
                        name1 = Console.ReadLine();
                        if(name1 == "")
                        {
                            Console.WriteLine("\nEl nombre no debe estar vacío");
                            Console.ReadKey();
                        }
                    }
                    while (name1 == "");

                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Ingrese el nombre del jugador 2");
                        name2 = Console.ReadLine();
                        if (name2 == "")
                        {
                            Console.WriteLine("\nEl nombre no debe estar vacío");
                            Console.ReadKey();
                        }
                    } while (name2 == "");
                    
                    
                }
                else if (modo == modo_jugadorvsCPU)
                {

                    Console.WriteLine("Ingrese el nombre del jugador 1");
                    name1 = Console.ReadLine();
                }

                bool seguir = false;

                while (!seguir)
                {
                    try
                    {
                        int columna = 0;

                        Console.WriteLine();
                        Console.Clear();
                        imprimirTablero(tablero);


                        if (modo == modo_jugadorvsjugador)
                        {
                            

                            bool dato = false;

                            while (!dato)
                            {
                                if (jugadoractual == jugador1)
                                {
                                    Console.WriteLine($"Turno del jugador " + name1);

                                }
                                else if (jugadoractual == jugador2)
                                {
                                    Console.WriteLine($"Turno del jugador " + name2);



                                }

                                if (continuar == false)
                                {
                                    if (jugadoractual == jugador1)
                                    {
                                        cont++;

                                    }
                                    else
                                    {
                                        cont1++;

                                    }

                                }

                                columna = pociciondeficha();
                                
                                
                                if (columna < 0 || columna >= COLUMNAS)
                                {
                                    Console.Clear() ;
                                    Console.WriteLine("-------Debe ingresar una columna correcta-------");
                                    Console.ReadKey();
                                    Console.Clear();
                                    imprimirTablero(tablero);
                                }
                                else
                                {
                                    dato = true;
                                }


                            }


                        }
                        else if (modo == modo_jugadorvsCPU)
                        {
                            if (jugadoractual == jugador1)
                            {
                                Console.WriteLine($"Turno del jugador " + name1);

                            }
                            else if (jugadoractual == jugador2)
                            {

                                Console.WriteLine($"Turno de la COPUTADORA");
                                Console.WriteLine($"pensando...");




                            }

                            if (jugadoractual == jugadorCPU)
                            {

                                columna = CPU.eleccionCPU(jugadoractual, tablero);
                                Thread.Sleep(2000);


                            }
                            else if (jugadoractual == jugador1)
                            {
                                bool da = false;
                                while (!da)
                                {
                                    columna = pociciondeficha();

                                    if (columna < 0 || columna > COLUMNAS)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("-------Debe ingresar una columna correcta-------");
                                        Console.ReadKey();
                                        Console.Clear();
                                        imprimirTablero(tablero);
                                        if (jugadoractual == jugador1)
                                        {
                                            Console.WriteLine($"Turno del jugador " + name1);

                                        }
                                    }
                                    else
                                    {
                                        da = true;
                                    }
                                    
                                    
                                }
                            }

                            if (continuar == false)
                            {
                                if (jugadoractual == jugador1)
                                {
                                    cont++;

                                }
                                else
                                {
                                    cont1++;

                                }

                            }


                        }


                        

                        bool columnaLlena = false;
                        int fila = Filadesocupada(columna, tablero, out columnaLlena);

                        while (columnaLlena)
                        {
                            Console.Clear();
                            Console.WriteLine("COLUMNA LLENA. Por favor, elija otra columna.");
                            columna = int.Parse(Console.ReadLine()) - 1;
                            fila = Filadesocupada(columna, tablero, out columnaLlena);
                        }

                        colocarpieza(jugadoractual, columna, tablero);

                        int g = ganador(jugadoractual, tablero);

                        

                        if (g != NoConecta)
                        {
                            
                            

                            
                            imprimirTablero(tablero);
                            
                            if (modo == modo_jugadorvsjugador)
                            {
                                if (jugadoractual == jugador1)
                                {
                                    Console.WriteLine($"Gana el jugador " + name1);
                                    gandor = name1;


                                }
                                else if (jugadoractual == jugador2)
                                {
                                    Console.WriteLine($"Gana el jugador " + name2);
                                    gandor = name2;


                                }
                                if (continuar == false)
                                {
                                    if (jugadoractual == jugador1)
                                    {
                                        contgan = cont;


                                    }
                                    else
                                    {
                                        contgan = cont1;


                                    }

                                }




                            }
                            else if (modo == modo_jugadorvsCPU)
                            {
                                if (jugadoractual == jugador1)
                                {
                                    Console.WriteLine($"Gana el jugador " + name1);
                                    gandor = name1;

                                }
                                else if (jugadoractual == jugador2)
                                {
                                    Console.WriteLine($"Gana la COPUTADORA");
                                    gandor = "COMPUTADORA";

                                }


                                if (continuar == false)
                                {
                                    if (jugadoractual == jugador1)
                                    {
                                        contgan = cont;


                                    }
                                    else
                                    {
                                        contgan = cont1;


                                    }

                                }


                            }

                            break;
                        }
                        else if (esEmpate(tablero))
                        {
                            imprimirTablero(tablero);
                            Console.WriteLine("Empate");
                            break;
                        }



                        jugadoractual = obteneroponente(jugadoractual);
                        nombreactualjugador = obteneroponente(nombreactualjugador);
                    }
                    catch(Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("Debe ingresar la columna");
                        Console.ReadKey();
                        Console.Clear();

                    }




                }


                continuar = true;

            }



        }

        




    }
}
