namespace SecimSimulasyonu
{
    internal class Ulke
    {
        private List<Sehir> sehirler;
        private List<Parti> partiler;

        // Ülkedeki toplam milletvekili sayısı
        private int MvSayisi;

        // Ülkedeki toplam geçerli oy sayısı
        private int OySayisi;

        public Ulke(List<Sehir> sehirler, List<Parti> partiler)
        {
            this.sehirler = new List<Sehir>(sehirler);
            this.partiler = partiler;
        }
        private void OySayisiHesapla()
        {
            sehirler.ForEach(i => OySayisi += i.OySayisi);
        }
        private void MvSayisiHesapla()
        {
            sehirler.ForEach(i => MvSayisi += i.MvSayisi);
        }
        public void UlkeSonuclariniYazdir()
        {
            OySayisiHesapla();
            MvSayisiHesapla();

            partiler = partiler.OrderByDescending(i=>i.OySayisi).ToList();

            Console.WriteLine("\nÜlke Geneli İstatistikler");
            Console.WriteLine($"Milletvekili Kontenjanı: {MvSayisi}");
            Console.WriteLine($"Toplam Geçerli Oy Sayısı: {OySayisi}");
            Console.WriteLine("\t\t Oy Sayısı\t Oy Yüzdesi\t Mv Sayısı\t Mv Yüzdesi");
            Console.WriteLine("\t\t ---------\t ----------\t ---------\t ----------");
            foreach (var parti in partiler)
            {
                float oyYuzdesi = ((float)parti.OySayisi / (float)this.OySayisi) * 100;
                float mvYuzdesi = ((float)parti.MvSayisi / (float)this.MvSayisi) * 100;

                Console.WriteLine($"{parti.Ad} Partisi\t {parti.OySayisi}\t\t " +
                    $"{oyYuzdesi.ToString("0.00")}\t\t {parti.MvSayisi}\t\t {mvYuzdesi.ToString("0.00")}");
            }

            Console.WriteLine($"\nİktidar partisi {partiler[0].Ad} partisidir.");
            Console.WriteLine($"Ana muhalefet partisi {partiler[1].Ad} partisidir.");

            Console.WriteLine("\nPartilerin Oy Sayılarına Göre İl Birincilikleri");
            var partiSiralamasi = partiler.OrderByDescending(i => i.BirincilikSayisi).ToList();
            foreach (var parti in partiSiralamasi)
            {
                if (parti.BirincilikSayisi > 0)
                {
                    Console.WriteLine($"{parti.Ad}: {parti.BirincilikSayisi}");
                }
            }
        }
    }
}
