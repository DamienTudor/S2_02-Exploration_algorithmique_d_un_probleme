using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoyageurDeCommerce.modele.distances;
using VoyageurDeCommerce.modele.lieux;

namespace VoyageurDeCommerce.modele.algorithmes.realisations
{
    class AlgoPlusProcheVoisin : Algorithme
    {
        List<Lieu> voisins;

        public override string Nom
        {
            get => "Algorithme du plus proche voisin";
        }

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Lieu depart = listeLieux[0];
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            Initialisation();
            while (voisins.Count != listeLieux.Count)
            {
                //listeLieux.Remove(depart);
                voisins.Add(depart);
                Tournee.Add(depart);
                stopwatch.Stop();
                this.NotifyPropertyChanged("Tournee");
                stopwatch.Start();
                depart = minimum(depart, listeLieux);
            }
            this.TempsExecution = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Initialise les variables
        /// </summary>
        public void Initialisation()
        {
            voisins = new List<Lieu>();
        }

        /// <summary>
        /// Permet de calculer le coût minimum entre le chemin de départ et ses voisins
        /// </summary>
        /// <param name="depart"> sommet de départ </param>
        /// <param name="listelieu"> liste des voisins du sommet de départ </param>
        /// <returns> Retourne le sommet avec la plus courte distance avec lui et  </returns>
        public Lieu minimum(Lieu depart, List<Lieu> listelieu)
        {
            Lieu newDepart = depart;
            Dictionary<Lieu, int> distance = new Dictionary<Lieu, int>();

            foreach(Lieu l in listelieu) // Pour tous les voisins du depart
            {   
                distance.Add(l,FloydWarshall.Distance(newDepart, l)); 
            }

            int minimum = int.MaxValue;
            foreach(Lieu l in distance.Keys)
            {
                if(!voisins.Contains(l) && distance[l] < minimum)
                {
                    minimum = distance[l];
                    newDepart = l;
                }
                
            }

            return newDepart;

        }

    }
}       
