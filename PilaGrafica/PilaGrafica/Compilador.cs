using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PilaGrafica
{
    internal class Compilador
    {
        // Atribut: Pila per gestionar els parèntesis
        private Pila<char> pila;

        // Constructor sense paràmetres
        public Compilador()
        {
            pila = new Pila<char>();  // Inicialitza la pila
        }

        // Constructor amb capacitat personalitzada per a la pila
        public Compilador(int capacity)
        {
            pila = new Pila<char>(capacity);  // Inicialitza la pila amb la capacitat especificada
        }

        // Constructor que rep una col·lecció de caràcters
        public Compilador(IEnumerable<char> coleccion)
        {
            pila = new Pila<char>(coleccion);  // Inicialitza la pila amb els elements de la col·lecció
        }

        /// <summary>
        /// Mètode per validar una expressió balancejada.
        /// </summary>
        /// <param name="expressio">L'expressió a validar.</param>
        /// <returns>True si l'expressió és balancejada, False si no ho és.</returns>
        public bool Validar(string expressio)
        {
            // Variable per indicar si l'expressió és vàlida
            bool esValida = true;

            // Recorrem l'expressió caràcter per caràcter
            foreach (char car in expressio)
            {
                // Si trobem un parèntesi d'obertura, el afegim a la pila
                if (car == '(' || car == '{' || car == '[')
                {
                    pila.Push(car);
                }
                // Si trobem un parèntesi de tancament, comprovem si correspon amb el de l'obertura
                else if (car == ')' || car == '}' || car == ']')
                {
                    // Si la pila està buida, no hi ha parèntesis d'obertura corresponents
                    if (pila.IsEmpty)
                    {
                        esValida = false;
                    }
                    else
                    {
                        // Traiem l'últim element de la pila (últim parèntesi obert)
                        char obert = pila.Pop();

                        // Comprovem si el parèntesi de tancament correspon al tipus d'obertura
                        if ((car == ')' && obert != '(') ||
                            (car == '}' && obert != '{') ||
                            (car == ']' && obert != '['))
                        {
                            esValida = false;
                        }
                    }
                }
            }
            // Si la pila no està buida al final, vol dir que hi ha parèntesis sense tancar
            if (!pila.IsEmpty)
            {
                esValida = false;
            }

            return esValida;
        }

        /// <summary>
        /// Mètode per validar un arxiu de text, validant cada línia de la mateixa manera.
        /// </summary>
        /// <param name="rutaArchivo">La ruta de l'arxiu de text a validar.</param>
        /// <returns>True si totes les línies són vàlides, False si alguna línia és invàlida.</returns>
        public bool ValidarArxiu(string rutaArxiu)
        {
            StreamReader reader = new StreamReader(rutaArxiu);

            if (rutaArxiu is null) throw new Exception("L'arxiu no pot estar buit");

            string linia;
            linia = reader.ReadLine();
            bool trobat = true;
            while (linia != null && !trobat)
            {
                // Validem cada línia llegida
                if (!Validar(linia))  // Si alguna línia no és vàlida
                {
                    reader.Close();
                    trobat = false;
                }

                linia = reader.ReadLine();

            }

            reader.Close(); // Tanquem el reader quan ja no es necessita
                            // Si totes les línies són vàlides
            return trobat;
        }
    }
}
