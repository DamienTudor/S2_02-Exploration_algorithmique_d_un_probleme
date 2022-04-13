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
    class AlgorithmeCroissant : Algorithme
    {
        public override string Nom
        {
            get => "Tournée Croissante";
        }

        public override void Executer(List<Lieu> listeLieux, List<Route> listeRoute)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            FloydWarshall.calculerDistances(listeLieux, listeRoute);

            foreach(Lieu l in listeLieux)
            {
                this.Tournee.Add(l);
                stopwatch.Stop();
                this.NotifyPropertyChanged("Tournee");
                stopwatch.Start();
            }

            this.TempsExecution = stopwatch.ElapsedMilliseconds;
           
        }

    }
}
