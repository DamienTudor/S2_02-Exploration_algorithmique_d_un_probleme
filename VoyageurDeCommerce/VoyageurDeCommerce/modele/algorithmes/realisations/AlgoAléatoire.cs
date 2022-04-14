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
    class AlgoAléatoire : Algorithme
    {
        public override string Nom => "Algorythme aléatoire";
        Random aleatoire;
        List<Lieu> voisins;

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Initialisation();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);
            Lieu depart = listeLieux[0];

            
            while (listeLieux.Count != 0)
            {
                listeLieux.Remove(depart);
                Tournee.Add(depart);
                stopwatch.Stop();
                this.NotifyPropertyChanged("Tournee");
                stopwatch.Start();
                depart = FindNewSommet(depart, listeLieux);

            }

            this.TempsExecution = stopwatch.ElapsedMilliseconds;
        }

        public void Initialisation()
        {
            aleatoire = new Random();
            voisins = new List<Lieu>();
        }

        public Lieu FindNewSommet(Lieu depart, List<Lieu> nbSommet)
        {
            Lieu newDepart = depart;
            newDepart = nbSommet[aleatoire.Next(1, nbSommet.Count-1)];   
            return newDepart;
        }

    }
}
