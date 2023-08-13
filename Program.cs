namespace SecimSimulasyonu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int PartiSayisi = 6;
            const int SehirSayisi = 5;

            List<Parti> partiler = new List<Parti>();
            List<Sehir> sehirler = new List<Sehir>();

            
            // Kullanıcıdan parti bilgilerini al
            for (int i = 0; i < PartiSayisi; i++)
            {
                string isim;
                Console.Write("Parti adı giriniz: ");
                isim = Console.ReadLine();
                partiler.Add(new Parti()
                {
                    Ad = isim                 
                });
            }
            
            // Kullanıcıdan şehir bilgilerini al
            for (int i = 0; i < SehirSayisi; i++)
            {
                Console.WriteLine();
                Sehir sehir = new Sehir(partiler);
                sehirler.Add(sehir);
                sehir.SehirAdiAl();
                sehir.MvSayisiAl();
                sehir.OySayisiAl();
                sehir.SecimHesapla();
            }

            Console.WriteLine();

            bool nextStep = false;
            int index = 0;
            while (!nextStep)
            {
                if (index < sehirler.Count) 
                {
                    Console.WriteLine($"{sehirler[index].Ad} şehrinin sonuçlarını görmek için G tuşuna basınız.");
                    char input = Console.ReadKey().KeyChar;

                    if (Char.ToUpper(input) == 'G')
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                        sehirler[index++].SonucYazdir();
                    }
                }
                else
                {
                    Console.WriteLine($"Ülke sonucunu görmek için G tuşuna basınız.");
                    char input = Console.ReadKey().KeyChar;

                    if (Char.ToUpper(input) == 'G')
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        ClearCurrentConsoleLine();
                        Ulke u = new Ulke(sehirler, partiler);
                        u.UlkeSonuclariniYazdir();
                        nextStep = true;
                    }
                }
            }
        }
        static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
