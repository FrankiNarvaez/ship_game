using System.Data;

namespace Nave_Espacial;
using System.Drawing;

public enum TipoBala
{
    Normal,
    Especial,
    Enemigo,
    menu
}

public class Bala
{
    public Point posicion { get; set; }
    public ConsoleColor color { get; set; }
    public TipoBala tipoBalaB { get; set; }
    public List<Point> posicionesBala { get; set; }
    private DateTime _tiempo;
    
    public Bala(Point posicion, ConsoleColor color, TipoBala tipoBala)
    {
        this.posicion = posicion;
        this.color = color;
        this.tipoBalaB = tipoBala;
        this.posicionesBala = new List<Point>();
        _tiempo = DateTime.Now;
    }
    
    public void Dibujar()
    {
        Console.ForegroundColor = color;
        int x = posicion.X;
        int y = posicion.Y;
        
        posicionesBala.Clear();

        switch (tipoBalaB)
        {
            case TipoBala.Normal:
                Console.SetCursorPosition(x, y);
                Console.Write("o");
                posicionesBala.Add(new Point(x, y));
                break;
            case TipoBala.Especial:
                Console.SetCursorPosition(x+1,y);
                Console.Write("_");
                Console.SetCursorPosition(x, y+1);
                Console.Write("( )");
                Console.SetCursorPosition(x+1, y+2);
                Console.Write("W");
                posicionesBala.Add(new Point(x+1, y));
                posicionesBala.Add(new Point(x, y+1));
                posicionesBala.Add(new Point(x+2, y+1));
                posicionesBala.Add(new Point(x+1, y+2));
                break;
            case TipoBala.Enemigo:
                Console.SetCursorPosition(x,y);
                Console.Write("\u2588");
                posicionesBala.Add(new Point(x,y));
                break;
            case TipoBala.menu:
                Console.SetCursorPosition(x,y);
                Console.Write("!");
                posicionesBala.Add(new Point(x,y));
                break;
        }
    }

    public void Borrar()
    {
        foreach (Point item in posicionesBala)
        {
            Console.SetCursorPosition(item.X, item.Y);
            Console.Write(" ");
        }
    }
    
    public bool Mover(int velocidad, int limite, List<Enemigo> enemigos)
    {
        if (DateTime.Now > _tiempo.AddMilliseconds(20))
        {
            Borrar();
            switch (tipoBalaB)
            {
                case TipoBala.Normal:
                    posicion = new Point(posicion.X, posicion.Y - velocidad);
                    if (posicion.Y <= limite)
                        return true;

                    foreach (Enemigo enemigo in enemigos)
                    {
                        foreach (Point posicionE in enemigo.posicionesEnemigo)
                        {
                            if (posicionE.X == posicion.X && posicionE.Y == posicion.Y)
                            {
                                enemigo.vida -= 7;
                                if (enemigo.vida <= 0)
                                {
                                    enemigo.vida = 0;
                                    enemigo.vivo = false;
                                    enemigo.Muerte();
                                }
                                return true;
                            }
                        }
                    }
                    break;
                case TipoBala.Especial:
                    posicion = new Point(posicion.X, posicion.Y - velocidad);
                    if (posicion.Y <= limite)
                        return true;

                    foreach (Enemigo enemigo in enemigos)
                    {
                        foreach (Point posicionesE in enemigo.posicionesEnemigo)
                        {
                            foreach (Point posicionB in posicionesBala)
                            {
                                if (posicionesE.X == posicionB.X && posicionesE.Y == posicionB.Y)
                                {
                                    enemigo.vida -= 40;
                                    if (enemigo.vida <= 0)
                                    {
                                        enemigo.vida = 0;
                                        enemigo.vivo = false;
                                        enemigo.Muerte();
                                    }
                                    return true;
                                }
                            }
                        }
                    }
                    break;
            }

            Dibujar();
            _tiempo = DateTime.Now;
        }
        return false;
    }
    
    public bool Mover(int velocidad, int limite, Nave nave)
    {
        if (DateTime.Now > _tiempo.AddMilliseconds(20))
        {
            Borrar();
            posicion = new Point(posicion.X, posicion.Y + velocidad);
            if (posicion.Y >= limite) 
                return true;

            foreach (Point posicionN in nave.posicionesNave)
            {
                if(posicionN.X == posicion.X && posicionN.Y == posicion.Y)
                {
                    nave.vida -= 5;
                    nave.colorAux = color;
                    nave.tiempoColision = DateTime.Now;
                    return true;
                }
            }

            Dibujar();
            _tiempo = DateTime.Now;
        }
        return false;
    }
    
    public bool Mover(int velocidad, int limite)
    {
        if (DateTime.Now > _tiempo.AddMilliseconds(20))
        {
            Borrar();
            posicion = new Point(posicion.X, posicion.Y - velocidad);
            if (posicion.Y <= limite) 
                return true;

            Dibujar();
            _tiempo = DateTime.Now;
        }
        return false;
    }
}