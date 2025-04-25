using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace projetcamion
{
    public class VisualiseurGrapheSkia
    {
        private Dictionary<string, (double lat, double lon)> coordonneesGPS;

        public VisualiseurGrapheSkia()
        {
            // Exemple de coordonnées (latitude, longitude) de villes en France
            coordonneesGPS = new Dictionary<string, (double, double)>
            {
                { "Paris", (48.8566, 2.3522) },
                { "Lyon", (45.7640, 4.8357) },
                { "Marseille", (43.2965, 5.3698) },
                { "Toulouse", (43.6047, 1.4442) },
                { "Lille", (50.6292, 3.0573) },
                { "Strasbourg", (48.5734, 7.7521) },
                { "Rennes", (48.1173, -1.6778) },
                { "Nantes", (47.2184, -1.5536) },
                { "Nice", (43.7102, 7.2620) },
                { "Bordeaux", (44.8378, -0.5792) },
                { "Dax", (43.7106, -1.0539) },
                { "Pau", (43.2951, -0.3708) },
                { "Montpellier", (43.6111, 3.8767) },
                { "Nimes", (43.8367, 4.3601) },
                { "Grenoble", (45.1885, 5.7245) },
                { "Dunkerque", (51.0344, 2.3770) },
                { "Limoges", (45.8336, 1.2611) },
                { "Le Havre", (49.4944, 0.1079) },
                { "Mulhouse", (47.7508, 7.3359) },
                { "Brest",     (48.2400,-4.2860)},
                { "Troyes",     (48.1760,4.4600)},
                { "Perpignan" , (42.6800,2.9800) }

                // ajoute les autres villes ici
            };
        }

        public void Visualiser(Graphe graphe, string cheminFichier)
        {
            int largeur = 850;
            int hauteur = 850;

            using var surface = SKSurface.Create(new SKImageInfo(largeur, hauteur));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);


            string nomFichier ="France-Fond-carte.jpg";
            
            string cheminjpg = Path.Combine(AppContext.BaseDirectory, nomFichier);
            Console.WriteLine(cheminjpg);
            // Charger l'image de fond (carte de la France)
            using var bitmap = SKBitmap.Decode(cheminjpg);
            // Dessiner l'image sur le fond
            canvas.DrawBitmap(bitmap, 0, 0, new SKPaint { FilterQuality = SKFilterQuality.High });

            // Projection GPS → XY
            var positionsXY = new Dictionary<Noeud, SKPoint>();
            

            // Calcule les bornes GPS
            
            double  minLat = 42;
            double maxLat = 52;
            double minLon = -5.5;
            double maxLon = 8.5;

            



            // Convertit chaque ville
            foreach (var noeud in graphe.Noeuds.Values)
            {
                if (coordonneesGPS.TryGetValue(noeud.Ville, out var coord))
                {
                    float x = (float)((coord.lon - minLon) / (maxLon - minLon) * (largeur)-20) ;
                    float y = (float)((1 - (coord.lat - minLat) / (maxLat - minLat)) * (hauteur +45)-40);
                    positionsXY[noeud] = new SKPoint(x, y);
                }
            }

            var paintLine = new SKPaint { Color = SKColors.Black, StrokeWidth = 2, IsAntialias = true };
            var paintText = new SKPaint { Color = SKColors.DarkBlue, TextSize = 16, IsAntialias = true };
            var paintNumber = new SKPaint { Color = SKColors.Red, TextSize = 20, IsAntialias = true };
            var paintNode = new SKPaint { Color = SKColors.LightSkyBlue, IsAntialias = true };
            var paintNodeBorder = new SKPaint { Color = SKColors.Black, Style = SKPaintStyle.Stroke, StrokeWidth = 2, IsAntialias = true };

            // Dessine les liens
            foreach (var lien in graphe.Liens)
            {
                if (positionsXY.TryGetValue(lien.VilleDep, out var p1) &&
                    positionsXY.TryGetValue(lien.VilleArr, out var p2))
                {
                    canvas.DrawLine(p1, p2, paintLine);
                    float midX = (p1.X + p2.X) / 2;
                    float midY = (p1.Y + p2.Y) / 2;
                    canvas.DrawText($"{lien.Distance}", midX, midY, paintText);
                }
            }

            // Dessine les noeuds
            foreach (var kvp in positionsXY)
            {
                var point = kvp.Value;
                canvas.DrawCircle(point, 15, paintNode);
                canvas.DrawCircle(point, 15, paintNodeBorder);
                canvas.DrawText(kvp.Key.Ville, point.X + 25, point.Y - 5, paintText);
                canvas.DrawText("9", point.X-6, point.Y +5, paintNumber);
            }

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(cheminFichier);
            data.SaveTo(stream);

            Console.WriteLine("Graphe projeté et visualisé dans : " + cheminFichier);
        }
    }
}
