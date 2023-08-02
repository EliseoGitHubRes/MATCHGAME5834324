using System.Timers;//agregamos este using para poder utlizars el timers
namespace MATCHGAME5834324;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        //Cuando se ejecute el programa se inicializara lo siguiente:
        InitializeComponent();

        Grid1.IsVisible = false;
        Grid1.IsEnabled = false;
        SetUpGame();
        lblTem.IsVisible = false;
        lblCom.IsVisible = false;
        btnreinicar.IsVisible = false;
    }

    //Metodo que asigna aleateriamente los emojis
    private void SetUpGame()
    {
        //Se crea lista de emojis de animales
        List<string> animalEmoji = new List<string>()
        {
            "🐶","🐶",
            "🙈","🙈",
            "🐢","🐢",
            "🦆","🦆",
            "🦓","🦓",
            "🙀","🙀",
            "🕷","🕷",
            "🦈","🦈",
        };
        //Se crea obj random para mezclar aletoriamente los emojis
        Random random = new Random();

        //A cada boton del grid se le aigna un emoji aleatorio
        foreach (Button view in Grid1.Children)
        {
            int index = random.Next(animalEmoji.Count);
            string nextEmoji = animalEmoji[index];
            view.Text = nextEmoji;
            animalEmoji.RemoveAt(index);
        }
    }

    //Se declaran dos variables
    Button ultimoButtonClicked;
    bool encontradoMatch = false;
    //se ejecutara al presionar el boton 
    private void Button_Clicked(object sender, EventArgs e)
    {
        Button button = sender as Button;
        if (encontradoMatch == false)
        {
            //Si se encuentra con el match se oculta el boton y se guarda como el ultimo boton presionado
            button.IsVisible = false;
            ultimoButtonClicked = button;
            encontradoMatch = true;
        }
        else if (button.Text == ultimoButtonClicked.Text)
        {
            //Si los Match coincide con el ultimo boton presionado se oculta el boton actual 
            button.IsVisible = false;
            encontradoMatch = false;
        }
        else
        {
            //Si el match no coiciden  con el ultimo boton presionado se muestra de nuevo el ultimo boton
            ultimoButtonClicked.IsVisible = true;
            encontradoMatch = false;
        }
        if (!EmojisVisibles())
        {
            //Verifica si quedan emojis visibles y si es asi llama al metodo tiemJuegoComple()
            tiemJuegoComple();
            btnreinicar.IsVisible = true;
        }
    }

    //Metodo booleano que verifica si hay emojis visibles en el grid
    private bool EmojisVisibles()
    {
        foreach (Button view in Grid1.Children)
        {
            if (view.IsVisible)
            {
                return true;
            }
        }
        return false;
    }

    //Creamos obj llamado temJuego
    private System.Timers.Timer temJuego;
    private int segTranscurridos;
    private void iniciarTem()
    {
        //inicia el temporizador con un intervalo de 10000ms(1seg)
        temJuego = new System.Timers.Timer(1000);
        temJuego.Elapsed += contarTiem;
        temJuego.AutoReset = true;
        temJuego.Enabled = true;
    }


    private void contarTiem(object sender, ElapsedEventArgs e)
    {
        //si hay emojis visibles  se incrementa el tiempo y muestra el tiempo transcurrido
        if (EmojisVisibles())
        {
            segTranscurridos++;
            Device.BeginInvokeOnMainThread(() =>
            {
                mostrarTiem();

            });

        }
        else
        {
            //si todos los emojis estan ocultos el tiempo se detiene y llama al metodo tiemJuegoComple()
            temJuego.Stop();
            tiemJuegoComple();

        }
    }

    //Mettodo que muestra cuanto se tardo en completar el juego
    private void tiemJuegoComple()
    {
        //Muestra el tiempo de cuanto se tardo
        Device.BeginInvokeOnMainThread(() =>
        {
            lblCom.Text = $"Game Completed in: {segTranscurridos} s";
            lblCom.IsVisible = true;
        });
    }

    //Metodo para mostrar el tiempo transcurrido 
    private void mostrarTiem()
    {
        lblTem.Text = $"Timer: {segTranscurridos} s";
    }
    
    //Se ejecutara al presionar el boton iniciar 
    private void btniniciar_Clicked(object sender, EventArgs e)
    {
        //al presionar el boton se inicia el temporizado, se hacen visibles el grid, el label y los botones con emojis tambien el mismo boton
        btniniciar.IsVisible = false;
        iniciarTem();
        btniniciar.Text = "Start Game";
        Grid1.IsEnabled = true;
        Grid1.IsVisible = true;
        lblTem.IsVisible = true;

        foreach (Button view in Grid1.Children)
        {
            view.IsVisible = true;
        }
    }

    //se ejecuta el presionar el boton reiniciar
    private void btnreinicar_Clicked(object sender, EventArgs e)
    {
        //Detiene el temporizador, restablece los segudnos a cero, oculta los label y el mismo boton y se muestra de nuevo el boton iniciar
        //para que el judador empieze otra vez
        temJuego.Stop();
        segTranscurridos = 0;
        lblTem.IsVisible = false;
        lblTem.Text = "Timer: 0 s";
        lblCom.IsVisible = false;
        encontradoMatch = false;
        btniniciar.IsVisible = true;
        btniniciar.Text = "Start Game";

        foreach (Button view in Grid1.Children)
        {
            view.IsVisible = false;
        }


        btnreinicar.IsVisible = false;
    }
}

