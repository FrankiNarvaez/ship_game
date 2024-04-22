using Console = System.Console;

namespace Nave_Espacial;
using System.Drawing;

public class Ventana
{
    public int Ancho { get; set; }
    public int Altura { get; set; }
    public ConsoleColor colorFondo { get; set; }
    public Point limiteSuperior { get; set; }
    public Point limiteInferior { get; set; }

    private Enemigo _enemigo1;
    private Enemigo _enemigo2;

    private List<Bala> _balas;

public Ventana(int ancho, int alto, ConsoleColor  colorFondo, Point limiteSuperior, Point limiteInferior)
    {
        this.Ancho = ancho;
        this.Altura = alto;
        this.colorFondo = colorFondo;
        this.limiteSuperior = limiteSuperior;
        this.limiteInferior = limiteInferior;
        Init();
    }

    private void Init()
    {
        Console.SetWindowSize(Ancho, Altura);
        Console.Title = "Nave Espacial";
        Console.CursorVisible = false;
        Console.BackgroundColor = colorFondo;
        Console.Clear();
        _enemigo1 = new Enemigo(new Point(50, 10), ConsoleColor.DarkYellow, this, TipoEnemigo.menu, null);
        _enemigo2 = new Enemigo(new Point(100, 30), ConsoleColor.DarkCyan, this, TipoEnemigo.menu, null);
        _balas = new List<Bala>();
        CrearBalas(); 
    }

    public void DibujarMarco()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        
        for (int i = limiteSuperior.X; i <= limiteInferior.X; i++)
        {
            Console.SetCursorPosition(i, limiteSuperior.Y);
            Console.Write("\u2550");
            
            Console.SetCursorPosition(i, limiteInferior.Y);
            Console.Write("\u2550");
        }

        for (int i = limiteSuperior.Y; i <= limiteInferior.Y; i++)
        {
            Console.SetCursorPosition(limiteSuperior.X, i);
            Console.Write("\u2551");
            
            Console.SetCursorPosition(limiteInferior.X, i);
            Console.Write("\u2551");
        }
        
        Console.SetCursorPosition(limiteSuperior.X, limiteSuperior.Y);
        Console.Write("\u2554");
        Console.SetCursorPosition(limiteSuperior.X, limiteInferior.Y);
        Console.Write("\u255a");
        Console.SetCursorPosition(limiteInferior.X, limiteSuperior.Y);
        Console.Write("\u2557");
        Console.SetCursorPosition(limiteInferior.X, limiteInferior.Y);
        Console.Write("\u255d");
    }

    public void Peligro()
    {
        Console.Clear();
        for (int i = 0; i < 6; i++)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(limiteInferior.X/2 - 5, limiteInferior.Y/2);
            Console.Write("¡PELIGRO!");
            Thread.Sleep(200);
            Console.SetCursorPosition(limiteInferior.X/2 - 5, limiteInferior.Y/2);
            Console.Write("         ");
            Thread.Sleep(200);
        }
    }

    public void Menu()
    {
        _enemigo1.Mover();
        _enemigo2.Mover();
        MoverBalas();
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(limiteInferior.X/2 - 5, limiteInferior.Y/2 - 1);
        Console.Write("[Enter] JUGAR");
        Console.SetCursorPosition(limiteInferior.X/2 - 5, limiteInferior.Y/2);
        Console.Write("[Esc] SALIR");
    }

    public void Teclado(ref bool ejecucion, ref bool jugar)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                DibujarMarco();
                jugar = true;
            }
            if(tecla.Key == ConsoleKey.Escape)
            {
                ejecucion = false;
            }
        }
    }

    public void CrearBalas()
    {
        Bala bala1 = new Bala(new Point(0, 0), ConsoleColor.Red, TipoBala.menu);
        _balas.Add(bala1);
        Bala bala2 = new Bala(new Point(0, 0), ConsoleColor.DarkMagenta, TipoBala.menu);
        _balas.Add(bala2);
        Bala bala3 = new Bala(new Point(0,0), ConsoleColor.Cyan, TipoBala.menu);
        _balas.Add(bala3);
        Bala bala4 = new Bala(new Point(0, 0), ConsoleColor.DarkBlue, TipoBala.menu);
        _balas.Add(bala4);
        Bala bala5 = new Bala(new Point(0, 0), ConsoleColor.Gray, TipoBala.menu);
        _balas.Add(bala5);
        Bala bala6 = new Bala(new Point(0,0), ConsoleColor.Black, TipoBala.menu);
        _balas.Add(bala6);
        Bala bala7 = new Bala(new Point(0, 0), ConsoleColor.White, TipoBala.menu);
        _balas.Add(bala7);
        Bala bala8 = new Bala(new Point(0, 0), ConsoleColor.Blue, TipoBala.menu);
        _balas.Add(bala8);
        Bala bala9 = new Bala(new Point(0,0), ConsoleColor.DarkCyan, TipoBala.menu);
        _balas.Add(bala9);
        Bala bala10 = new Bala(new Point(0, 0), ConsoleColor.DarkGreen, TipoBala.menu);
        _balas.Add(bala10);
        Bala bala11 = new Bala(new Point(0, 0), ConsoleColor.DarkGray, TipoBala.menu);
        _balas.Add(bala11);
        Bala bala12 = new Bala(new Point(0, 0), ConsoleColor.DarkMagenta, TipoBala.menu);
        _balas.Add(bala12);
        Bala bala13 = new Bala(new Point(0,0), ConsoleColor.DarkCyan, TipoBala.menu);
        _balas.Add(bala13);
        Bala bala14 = new Bala(new Point(0, 0), ConsoleColor.DarkBlue, TipoBala.menu);
        _balas.Add(bala14);
        Bala bala15 = new Bala(new Point(0, 0), ConsoleColor.DarkYellow, TipoBala.menu);
        _balas.Add(bala15);
        Bala bala16 = new Bala(new Point(0, 0), ConsoleColor.DarkRed, TipoBala.menu);
        _balas.Add(bala16);
        Bala bala17 = new Bala(new Point(0,0), ConsoleColor.DarkYellow, TipoBala.menu);
        _balas.Add(bala17);
        Bala bala18 = new Bala(new Point(0, 0), ConsoleColor.DarkBlue, TipoBala.menu);
        _balas.Add(bala18);
        Bala bala19 = new Bala(new Point(0, 0), ConsoleColor.Gray, TipoBala.menu);
        _balas.Add(bala19);
        Bala bala20 = new Bala(new Point(0, 0), ConsoleColor.DarkMagenta, TipoBala.menu);
        _balas.Add(bala20);

        Random random = new Random();

        for (int i = 0; i < _balas.Count; i++)
        {
            PosicionesAleatorias(_balas[i]);
            int numeroAleatorio = random.Next(limiteSuperior.Y + 1, limiteInferior.Y);
            _balas[i].posicion = new Point(_balas[i].posicion.X, numeroAleatorio);
        }
    }

    public void PosicionesAleatorias(Bala bala)
    {
        Random random = new Random();
        int numeroAleatorio = random.Next(limiteSuperior.X + 1, limiteInferior.X);
        bala.posicion = new Point(numeroAleatorio, limiteInferior.Y);
    }

    public void MoverBalas()
        {
            for (int i = 0; i < _balas.Count; i++)
            {
                if(_balas[i].Mover(1, limiteSuperior.Y))
                    PosicionesAleatorias(_balas[i]);
            }
        }
}