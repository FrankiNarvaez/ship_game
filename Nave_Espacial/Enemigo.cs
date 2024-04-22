namespace Nave_Espacial;
using System.Drawing;

public enum TipoEnemigo
{
    Normal,
    Boss,
    menu
}

public class Enemigo
{
    enum DireccionEnemigo
    {
        Derecha,
        Izquierda,
        Arriba,
        Abajo
    }
    public bool vivo { get; set; }
    public float vida { get; set; }
    public Point position { get; set; }
    public Ventana ventanaC { get; set; }
    public ConsoleColor color { get; set; }
    public TipoEnemigo tipoEnemigoE { get; set; }
    public List<Point> posicionesEnemigo { get; set; }
    public List<Bala> balas { get; set; }
    public Nave naveC { get; set; }
    
    private DireccionEnemigo _direccion;
    private DateTime _tiempoDireccion;
    private float _tiempoDireccionAleatoria;
    private DateTime _tiempoMovimiento;

    private DateTime _tiempoDisparo;
    private float _tiempoDisparoAleatorio;
    
    public Enemigo(Point position, ConsoleColor color, Ventana ventana, TipoEnemigo tipoEnemigo, Nave naveC)
    {
        this.position = position;
        this.color = color;
        this.ventanaC = ventana;
        this.vida = 100;
        this.vivo = true;
        this.tipoEnemigoE = tipoEnemigo;
        _direccion = DireccionEnemigo.Derecha;
        _tiempoDireccion = DateTime.Now;
        _tiempoDireccionAleatoria = 1000;
        _tiempoMovimiento = DateTime.Now;
        _tiempoDisparo = DateTime.Now;
        _tiempoDisparoAleatorio = 200;
        posicionesEnemigo = new List<Point>();
        balas = new List<Bala>();
        this.naveC = naveC;
    }

    public void Dibujar()
    {
        switch (tipoEnemigoE)
        {
            case TipoEnemigo.Normal:
                DibujoNormal();
                break;
            case TipoEnemigo.Boss:
                DibujoBoss();
                break;
            case TipoEnemigo.menu:
                DibujoNormal();
                break;
        }
    }

    public void DibujoNormal()
    {
        Console.ForegroundColor = color;
        int x = position.X;
        int y = position.Y;
        
        Console.SetCursorPosition(x+1, y);
        Console.Write("\u2584\u2584");
        Console.SetCursorPosition(x, y+1);
        Console.Write("\u2588\u2588\u2588\u2588");
        Console.SetCursorPosition(x, y+2);
        Console.Write("\u2580  \u2580");
        
        posicionesEnemigo.Clear();
        
        posicionesEnemigo.Add(new Point(x+1,y));
        posicionesEnemigo.Add(new Point(x+2,y));
        posicionesEnemigo.Add(new Point(x,y+1));
        posicionesEnemigo.Add(new Point(x+1,y+1));
        posicionesEnemigo.Add(new Point(x+2,y+1));
        posicionesEnemigo.Add(new Point(x+3,y+1));
        posicionesEnemigo.Add(new Point(x,y+2));
        posicionesEnemigo.Add(new Point(x+3,y+2));
    }

    public void DibujoBoss()
    {
        Console.ForegroundColor = color;
        int x = position.X;
        int y = position.Y;
        
        posicionesEnemigo.Clear();
        
        Console.SetCursorPosition(x+1,y);
        Console.Write("\u2588\u2584\u2584\u2584\u2584\u2588");
        Console.SetCursorPosition(x,y+1);
        Console.Write("\u2588\u2588 \u2588\u2588 \u2588\u2588");
        Console.SetCursorPosition(x, y+2);
        Console.Write("\u2588\u2580\u2580\u2580\u2580\u2580\u2580\u2588");
        
        posicionesEnemigo.Add(new Point(x+1,y));
        posicionesEnemigo.Add(new Point(x+2,y));
        posicionesEnemigo.Add(new Point(x+3,y));
        posicionesEnemigo.Add(new Point(x+4,y));
        posicionesEnemigo.Add(new Point(x+5,y));
        posicionesEnemigo.Add(new Point(x+6,y));
        
        posicionesEnemigo.Add(new Point(x,y+1));
        posicionesEnemigo.Add(new Point(x+1,y+1));
        posicionesEnemigo.Add(new Point(x+3,y+1));
        posicionesEnemigo.Add(new Point(x+4,y+1));
        posicionesEnemigo.Add(new Point(x+6,y+1));
        posicionesEnemigo.Add(new Point(x+7,y+1));
        
        posicionesEnemigo.Add(new Point(x,y+2));
        posicionesEnemigo.Add(new Point(x+1,y+2));
        posicionesEnemigo.Add(new Point(x+2,y+2));
        posicionesEnemigo.Add(new Point(x+3,y+2));
        posicionesEnemigo.Add(new Point(x+4,y+2));
        posicionesEnemigo.Add(new Point(x+5,y+2));
        posicionesEnemigo.Add(new Point(x+6,y+2));
        posicionesEnemigo.Add(new Point(x+7,y+2));
    }

    public void Muerte()
    {
        if (tipoEnemigoE == TipoEnemigo.Normal)
        {
            MuerteNormal();
        }

        if (tipoEnemigoE == TipoEnemigo.Boss)
        {
            MuerteBoss();
        }
    }

    public void MuerteBoss()
    {
        Console.ForegroundColor = color;
        foreach (Point item in posicionesEnemigo)
        {
            Console.SetCursorPosition(item.X, item.Y);
            Console.Write("\u2593");
            Thread.Sleep(200);
        }

        foreach (Point item in posicionesEnemigo)
        {
            Console.SetCursorPosition(item.X, item.Y);
            Console.Write(" ");
            Thread.Sleep(200);
        }
        posicionesEnemigo.Clear();
        foreach (Bala item in balas)
        {
            item.Borrar();
        }
        balas.Clear();
    }

    public void MuerteNormal()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        int x = position.X;
        int y = position.Y;
        
        Console.SetCursorPosition(x+1, y);
        Console.Write("\u2584\u2584Zzz");
        Console.SetCursorPosition(x, y+1);
        Console.Write("\u2588\u2588\u2588\u2588");
        Console.SetCursorPosition(x, y+2);
        Console.Write("\u2580  \u2580");

        posicionesEnemigo.Clear();
        foreach (Bala item in balas)
        {
            item.Borrar();
        }

        balas.Clear();
    }
    
    public void Borrar()
    {
        foreach (Point item in posicionesEnemigo)
        {
            Console.SetCursorPosition(item.X, item.Y);
            Console.Write(" ");
        }
    }

    public void Mover()
    {
        if (!vivo)
        {
            Muerte();
            return;
        }
        int tiempo = 30;
        if (tipoEnemigoE == TipoEnemigo.Boss)
            tiempo = 20;
        if (DateTime.Now > _tiempoMovimiento.AddMilliseconds(tiempo))
        {
            Borrar();
            DireccionAleatoria();
            Point posicionAux = position;
            Movimiento(ref posicionAux);
            Colisiones(posicionAux);
            Dibujar();
            _tiempoMovimiento = DateTime.Now;;
        }

        if (tipoEnemigoE != TipoEnemigo.menu)
        {
            crearBalas();
            Disparar();
        }
    }

    public void Colisiones(Point posicionAux)
    {
        int ancho = 3;
        if (tipoEnemigoE == TipoEnemigo.Boss)
            ancho = 7;

        int limiteInferior = ventanaC.limiteSuperior.Y + 15;
        if(tipoEnemigoE == TipoEnemigo.menu)
            limiteInferior = ventanaC.limiteInferior.Y-1;

        if (posicionAux.X <= ventanaC.limiteSuperior.X)
        {
            _direccion = DireccionEnemigo.Derecha;
            posicionAux.X = ventanaC.limiteSuperior.X + 1;
        }

        if (posicionAux.X + ancho >= ventanaC.limiteInferior.X)
        {
            _direccion = DireccionEnemigo.Izquierda;
            posicionAux.X = ventanaC.limiteInferior.X - ancho - 1;
        }

        if (posicionAux.Y <= ventanaC.limiteSuperior.Y)
        {
            _direccion = DireccionEnemigo.Abajo;
            posicionAux.Y = ventanaC.limiteSuperior.Y + 1;
        }

        if (posicionAux.Y + 2 >= limiteInferior)
        {
            _direccion = DireccionEnemigo.Arriba;
            posicionAux.Y = limiteInferior - 2;
        }
        
        position = posicionAux;
    }

    public void Movimiento(ref Point posicionAux)
    {
        switch (_direccion)
        {
            case DireccionEnemigo.Derecha:
                posicionAux = new Point(posicionAux.X + 1, posicionAux.Y);
                break;
            case DireccionEnemigo.Izquierda:
                posicionAux = new Point(posicionAux.X - 1, posicionAux.Y);
                break;
            case DireccionEnemigo.Arriba:
                posicionAux = new Point(posicionAux.X, posicionAux.Y - 1);
                break;
            case DireccionEnemigo.Abajo:
                posicionAux = new Point(posicionAux.X, posicionAux.Y + 1);
                break;
        }
    }

    public void DireccionAleatoria()
    {
        if (DateTime.Now > _tiempoDireccion.AddMilliseconds(_tiempoDireccionAleatoria) &&
            (_direccion == DireccionEnemigo.Derecha || _direccion == DireccionEnemigo.Izquierda))
        {
            Random random = new Random();
            int numAleatorio = random.Next(1, 5);

            switch (numAleatorio)
            {
                case 1:
                    _direccion = DireccionEnemigo.Derecha;
                    break;
                case 2:
                    _direccion = DireccionEnemigo.Izquierda;
                    break;
                case 3:
                    _direccion = DireccionEnemigo.Arriba;
                    break;
                case 4:
                    _direccion = DireccionEnemigo.Abajo;
                    break;
            }
            _tiempoDireccion = DateTime.Now;
            _tiempoDireccionAleatoria = random.Next(1000, 2000);
        }
        if (DateTime.Now > _tiempoDireccion.AddMilliseconds(80) &&
            (_direccion == DireccionEnemigo.Arriba || _direccion == DireccionEnemigo.Abajo))
        {
            Random random = new Random();
            int numAleatorio = random.Next(1, 2);

            switch (numAleatorio)
            {
                case 1:
                    _direccion = DireccionEnemigo.Derecha;
                    break;
                case 2:
                    _direccion = DireccionEnemigo.Izquierda;
                    break;
            }
            _tiempoDireccion = DateTime.Now;
        }
    }

    public void crearBalas()
    {
        if(DateTime.Now > _tiempoDisparo.AddMilliseconds(_tiempoDisparoAleatorio))
        {
            Random random = new Random();
            
            if (tipoEnemigoE == TipoEnemigo.Normal)
            {
                Bala bala = new Bala(new Point(position.X + 1, position.Y + 2), color, TipoBala.Enemigo);
                balas.Add(bala);
                _tiempoDisparoAleatorio = random.Next(200, 500);
            }

            if (tipoEnemigoE == TipoEnemigo.Boss)
            {
                Bala bala = new Bala(new Point(position.X + 4, position.Y + 2), color, TipoBala.Enemigo);
                balas.Add(bala);
                _tiempoDisparoAleatorio = random.Next(100, 150);
            }

            _tiempoDisparo = DateTime.Now;
        }
    }

    public void Disparar()
    {
        for (int i = 0; i < balas.Count; i++)
        {
            if (balas[i].Mover(1, ventanaC.limiteInferior.Y, naveC))
                balas.Remove(balas[i]);
        }
    }

    public void Informacion(int distanciaX)
    {
        Console.ForegroundColor = color;
        Console.SetCursorPosition(ventanaC.limiteSuperior.X + distanciaX, ventanaC.limiteSuperior.Y - 1);
        Console.Write($"ENEMIGO: {(int) vida}%  ");
    }
}