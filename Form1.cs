using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGm
{
    public partial class Form1 : Form
    {
        // firstClicked apunta al primer control Etiqueta
        // que el jugador haga clic, pero será nulo
        // si el jugador aún no ha hecho clic en una etiqueta
        Label firstClicked = null;

        // secondClicked apunta al segundo control Label
        // que el jugador hace clic
        Label secondClicked = null;
        // Usa este objeto aleatorio para elegir iconos aleatorios para los cuadrados
        Random random = new Random();

        // Cada una de estas letras es un ícono interesante
        // en la fuente Webdings,
        // y cada icono aparece dos veces en esta lista
        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    };
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

    }

        /// <summary>
        /// Asigna cada ícono de la lista de íconos a un cuadrado aleatorio
        /// </summary>
        private void AssignIconsToSquares()
            {

            // TableLayoutPanel tiene 16 etiquetas,
            // y la lista de íconos tiene 16 íconos,
            // entonces se extrae un ícono al azar de la lista
            // y agregado a cada etiqueta
            foreach (Control control in tableLayoutPanel1.Controls)
                {
                    Label iconLabel = control as Label;
                    if (iconLabel != null)
                    {
                        int randomNumber = random.Next(icons.Count);
                        iconLabel.Text = icons[randomNumber];
                        iconLabel.ForeColor = iconLabel.BackColor;
                        icons.RemoveAt(randomNumber);
                    }
                }
            }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Si la etiqueta en la que se hizo clic es negra, el jugador hizo clic
                // un ícono que ya ha sido revelado --
                //ignora el clic
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                clickedLabel.ForeColor = Color.Black;
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Verifica si el jugador ganó
                CheckForWinner();

                // Si el jugador hizo clic en dos íconos iguales, mantenlos
                // negro y restablecer firstClicked y secondClicked
                // para que el jugador pueda hacer clic en otro icono
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                // Establece su color en negro
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Si el jugador hizo clic en dos íconos iguales, mantenlos
                // negro y restablecer firstClicked y secondClicked
                // para que el jugador pueda hacer clic en otro icono
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }



                // Si el jugador llega hasta aquí, el jugador
                // hice clic en dos iconos diferentes, así que inicia el
                // temporizador (que esperará tres cuartos de
                // un segundo, y luego oculta los íconos)
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            timer1.Stop();

            // Esconder los iconos
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;


            // Restablecer el primer clic y el segundo clic
            // así la próxima vez que se coloque una etiqueta
            // hecho clic, el programa sabe que es el primer clic
            firstClicked = null;
            secondClicked = null;
            CheckForWinner();

        }
        /// <summary>
        /// Verifica cada ícono para ver si coincide, mediante
        /// comparando su color de primer plano con su color de fondo.
        /// Si todos los iconos coinciden, el jugador gana
        /// </summary>
        private void CheckForWinner()
        {

            // Revisa todas las etiquetas en TableLayoutPanel,
            // comprobando cada uno para ver si su icono coincide

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            // Si el bucle no regresó, no encontró
            // cualquier icono no coincidente
            // Eso significa que el usuario ganó. Mostrar un mensaje y cerrar el formulario.
            MessageBox.Show("Tu conseguiste emparejar las cartas!", "Felicidades");
            Close();

        }


    }

        }

    



