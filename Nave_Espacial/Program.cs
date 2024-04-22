using Nave_Espacial;
using System.Drawing;

Ventana ventana;
Nave nave;
Enemigo enemigo1;
Enemigo enemigo2;
Enemigo enemigoBoss;

bool jugar = false;
bool bossFinal = false;
bool ejecucion = true;

void Iniciar()
{
    ventana = new Ventana(170, 45, ConsoleColor.Black,new Point(5,3), new Point(165,43) );
    ventana.DibujarMarco();
    nave = new Nave(new Point(80, 30), ConsoleColor.White, ventana);
    enemigo1 = new Enemigo(new Point(50, 10), ConsoleColor.DarkYellow, ventana, TipoEnemigo.Normal,nave);
    enemigo2 = new Enemigo(new Point(20, 12), ConsoleColor.DarkCyan, ventana, TipoEnemigo.Normal,nave);
    enemigoBoss = new Enemigo(new Point(100, 10), ConsoleColor.Magenta, ventana, TipoEnemigo.Boss, nave);

    nave.enemigos.Add(enemigo1);
    nave.enemigos.Add(enemigo2);
    nave.enemigos.Add(enemigoBoss);
}

void Reiniciar()
{
    Console.Clear();
    ventana.DibujarMarco();

    nave.vida = 100;
    nave.SobreCarga = 0;
    nave.balaEspecial = 0;
    nave.balas.Clear();

    enemigo1.vida = 100;
    enemigo1.vivo = true;
    enemigo2.vida = 100;
    enemigo2.vivo = true;
    enemigoBoss.vida = 100;
    enemigoBoss.vivo = true;
    enemigoBoss.posicionesEnemigo.Clear();

    bossFinal = false;
}

void Game()
{
    while (ejecucion)
    {
        ventana.Menu();
        ventana.Teclado(ref ejecucion, ref jugar);
        while (jugar)
        {
            if (!enemigo1.vivo && !enemigo2.vivo && !bossFinal)
            {
                bossFinal = true;
                ventana.Peligro();
            }

            if (bossFinal)
            {
                enemigoBoss.Mover();
                enemigoBoss.Informacion(140);
            }
            else
            {
                enemigo1.Mover();
                enemigo1.Informacion(120);
                enemigo2.Mover();
                enemigo2.Informacion(140);
            }
        
            nave.Mover(2);
            nave.disparar();
            if (nave.vida <= 0)
            {
                jugar = false;
                nave.MuerteNave();
                Reiniciar();
            }

            if (!enemigoBoss.vivo)
            {
                jugar = false;
                Reiniciar();
            }
        }
    }
}

Iniciar();
Game();