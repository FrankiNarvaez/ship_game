using System.Runtime.InteropServices.JavaScript;

namespace Nave_Espacial;
using System.Drawing;

public class Nave
{
    public float vida { get; set; }
    public Point position { get; set; }
    public ConsoleColor color { get; set; }
    public Ventana ventanaC { get; set; }
    public List<Point> posicionesNave { get; set; }
    public List<Bala> balas { get; set; }
    public float SobreCarga { get; set; }
    public bool sobreCargaCondicion { get; set; }
    public float balaEspecial { get; set; }
    public List<Enemigo>  enemigos { get; set; }
    public ConsoleColor colorAux { get; set; }
    public DateTime tiempoColision { get; set; }

    public Nave(Point position, ConsoleColor color, Ventana ventana)
    {
        this.position = position;
        this.color = color;
        this.ventanaC = ventana;
        this.vida = 100;
        posicionesNave = new List<Point>();
        balas = new List<Bala>();
        enemigos = new List<Enemigo>();
        colorAux = color;
        tiempoColision = DateTime.Now;
    }

    public void Dibujar()
    {
        if (DateTime.Now > tiempoColision.AddMilliseconds(1000))
            Console.ForegroundColor = color;
        else
            Console.ForegroundColor = colorAux;
        
        Console.ForegroundColor = color;
        int x = position.X;
        int y = position.Y;
        
        Console.SetCursorPosition(x+3, y);
        Console.Write("A");
        Console.SetCursorPosition(x+1,y+1);
        Console.Write("<{X}>");
        Console.SetCursorPosition(x,y+2);
        Console.Write("\u00b1 W W \u00b1");

        posicionesNave.Clear();
        
        posicionesNave.Add(new Point(x+3, y));
        
        posicionesNave.Add(new Point(x+1, y+1));
        posicionesNave.Add(new Point(x+2, y+1));
        posicionesNave.Add(new Point(x+3, y+1));
        posicionesNave.Add(new Point(x+4, y+1));
        posicionesNave.Add(new Point(x+5, y+1));
        
        posicionesNave.Add(new Point(x, y+2));
        posicionesNave.Add(new Point(x+2, y+2));
        posicionesNave.Add(new Point(x+4, y+2));
        posicionesNave.Add(new Point(x+6, y+2));
    }

    public void Borrar()
    {
        foreach (Point item in posicionesNave)
        {
            Console.SetCursorPosition(item.X, item.Y);
            Console.Write(" ");
        }
    }

    public void Teclado(ref Point distancia, int velocidad)
    {
        ConsoleKeyInfo tecla = Console.ReadKey(true);
        if (tecla.Key == ConsoleKey.W)
            distancia = new Point(0, -1);
        if (tecla.Key == ConsoleKey.S)
            distancia = new Point(0, 1);
        if (tecla.Key == ConsoleKey.D)
            distancia = new Point(1, 0);
        if (tecla.Key == ConsoleKey.A)
            distancia = new Point(-1, 0);

        distancia.X *= velocidad;
        distancia.Y *= velocidad;

        if (tecla.Key == ConsoleKey.RightArrow)
        {
            if (!sobreCargaCondicion)
            {
                Bala bala = new Bala(new Point(position.X + 6, position.Y + 2), ConsoleColor.DarkGreen, TipoBala.Normal);
                balas.Add(bala);
                
                SobreCarga += 0.8f;
                
                if (SobreCarga >= 100)
                {
                    sobreCargaCondicion = true;
                    SobreCarga = 100;
                }
            }
        }
        if (tecla.Key == ConsoleKey.LeftArrow)
        {
            if (!sobreCargaCondicion)
            {
                Bala bala = new Bala(new Point(position.X, position.Y + 2), ConsoleColor.DarkGreen, TipoBala.Normal);
                balas.Add(bala);
                SobreCarga += 0.8f;
                if (SobreCarga >= 100)
                {
                    sobreCargaCondicion = true;
                    SobreCarga = 100;
                }
            }
        }
        if (tecla.Key == ConsoleKey.UpArrow)
        {
            if (balaEspecial == 100)
            {
                Bala bala = new Bala(new Point(position.X + 2, position.Y - 2), ConsoleColor.Red, TipoBala.Especial);
                balas.Add(bala);
                balaEspecial = 0;
            }
        }
    }

    public void Mover(int velocidad)
    {
        if (Console.KeyAvailable)
        {
            Borrar();
            Point distancia = new Point();
            Teclado(ref distancia, velocidad);
            Colisiones(distancia);
        }
        Dibujar();
        Informacion();
    }

    public void Colisiones(Point distancia)
    {
        Point posicionAux = new Point(position.X + distancia.X, position.Y + distancia.Y);
        
        if (posicionAux.X <= ventanaC.limiteSuperior.X)
            posicionAux.X = ventanaC.limiteSuperior.X + 1;

        if (posicionAux.X + 6 >= ventanaC.limiteInferior.X)
            posicionAux.X = ventanaC.limiteInferior.X - 7;
        
        if(posicionAux.Y <= (ventanaC.limiteSuperior.Y) + 15)
            posicionAux.Y = (ventanaC.limiteSuperior.Y + 1) + 15;

        if (posicionAux.Y + 2 >= ventanaC.limiteInferior.Y)
            posicionAux.Y = ventanaC.limiteInferior.Y - 3;
        
        position = posicionAux;
    }

    public void Informacion()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(ventanaC.limiteSuperior.X, ventanaC.limiteSuperior.Y-1);
        Console.Write($"VIDA: {(int) vida}%  ");

        if (SobreCarga <= 0)
           SobreCarga = 0;
        else 
           SobreCarga -= 0.001f;

        if (SobreCarga <= 50)
            sobreCargaCondicion = false;

        if (sobreCargaCondicion)
            Console.ForegroundColor = ConsoleColor.Red;
        else
            Console.ForegroundColor = ConsoleColor.White;
        
        Console.SetCursorPosition(ventanaC.limiteSuperior.X+15, ventanaC.limiteSuperior.Y-1);
        Console.Write($"SOBRECARGA: {(int) SobreCarga}%  ");

        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(ventanaC.limiteSuperior.X+35, ventanaC.limiteSuperior.Y-1);
        Console.Write($"BALA ESPECIAL: {(int) balaEspecial}%  ");
        if (balaEspecial >= 100)
            balaEspecial = 100;
        else
            balaEspecial += 0.0004f;
    }

    public void disparar()
    {
        for (int i = 0; i < balas.Count; i++)
        {
            if(balas[i].Mover(1, ventanaC.limiteSuperior.Y, enemigos))
            {
                balas.Remove(balas[i]);
            }
        }
    }

    public void MuerteNave()
    {
        Console.ForegroundColor = color;
        foreach (Point item in posicionesNave)
        {
            Console.SetCursorPosition(item.X, item.Y);
            Console.Write("X");
            Thread.Sleep(200);
        }
        
        foreach(Point item in posicionesNave)
        {
            Console.SetCursorPosition(item.X, item.Y);
            Console.Write(" ");
            Thread.Sleep(200);
        }
    }
}