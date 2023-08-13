namespace SecimSimulasyonu
{
    class Sehir
    {
        // Her şehirde her bir partinin oy sayısı ve mv sayısı bilgileri saklanmalı,
        // bunları her veri tipi için ayrı bir dizi şeklinde tutmak yerine
        // bir sınıf içinde birlikte tutmaya karar verdim.
        class SehirParti
        {
            public Parti parti;

            // Partinin şehirde aldığı oy sayısı
            public float OySayisi;

            // Partinin şehirde aldığı milletvekili sayısı
            public int MvSayisi;
        }
        public string Ad { get; set; }

        // Şehirdeki toplam milletvekili sayısı
        public int MvSayisi { get; set; }

        // Şehirdeki toplam geçerli oy sayısı
        public int OySayisi { get; set; }

        List<SehirParti> SehirPartiListesi = new List<SehirParti>();

        public Sehir(List<Parti> partiler)
        {
            foreach (var partiObj in partiler)
            {
                SehirParti var = new SehirParti
                {
                    parti = partiObj,
                    OySayisi = 0,
                    MvSayisi = 0,
                };
                SehirPartiListesi.Add(var);
            }
        }
        public void SehirAdiAl()
        {
            Console.Write("Şehir adı giriniz: ");
            string isim = Console.ReadLine();
            Ad = isim;
        }
        public void MvSayisiAl()
        {
            int mv;
            Console.Write("Şehrin milletvekili sayısını giriniz: ");
            mv = Convert.ToInt32(Console.ReadLine());
            MvSayisi = mv;
        }
        public void OySayisiAl()
        {
            int oy;
            foreach (var sehirParti in SehirPartiListesi)
            {
                Console.Write($"{sehirParti.parti.Ad} partisi için oy sayısı giriniz: ");
                oy = Convert.ToInt32(Console.ReadLine());

                // Partinin bu şehirde aldığı oy sayısı
                sehirParti.OySayisi = oy;

                // Alınan oyları partinin genel oy sayısına ekle
                sehirParti.parti.OySayisi += oy;

                // Şehirde alınan toplam genel oy sayısına ekle
                OySayisi += oy;
            }
        }
        public void SecimHesapla()
        {
            for (int i = MvSayisi; i > 0; i--)
            {
                // en yüksek oyu bul, 2ye böl, milletvekilini ver, devam et.
                SehirPartiListesi = SehirPartiListesi.OrderByDescending(i => i.OySayisi).ToList();
                SehirPartiListesi[0].MvSayisi++;
                SehirPartiListesi[0].parti.MvSayisi++;
                SehirPartiListesi[0].OySayisi /= 2;
            }

            // Toplam alınan Oy Sayılarını tekrar eski hâline getir.
            SehirPartiListesi.ForEach(i => { i.OySayisi *= 
                Convert.ToInt32(Math.Pow(2, i.MvSayisi)); });

            BirinciPartiHesapla();
        }
        public void SonucYazdir()
        {
            Console.WriteLine($"\n{this.Ad} şehri için sonuçlar");
            Console.WriteLine($"Milletvekili Kontenjanı: {MvSayisi}");
            Console.WriteLine($"Geçerli Oy Sayısı: {OySayisi}");
            Console.WriteLine("\t\t Oy Sayısı\t Oy Yüzdesi\t Mv Sayisi");
            Console.WriteLine("\t\t ---------\t ----------\t ---------");
            foreach (var sehirParti in SehirPartiListesi.OrderByDescending(i => i.OySayisi))
            {
                float oyYuzdesi = ((float)sehirParti.OySayisi / (float)this.OySayisi) * 100;
                Console.WriteLine($"{sehirParti.parti.Ad} Partisi\t {sehirParti.OySayisi}\t\t " +
                    $"{oyYuzdesi.ToString("0.00")}\t\t {sehirParti.MvSayisi}");
            }
        }
        private void BirinciPartiHesapla()
        {
            var liste = SehirPartiListesi.OrderByDescending(i => i.OySayisi).ToList();
            
            if (liste[0].OySayisi > liste[1].OySayisi)
            {
                liste[0].parti.BirincilikSayisi++;
            }
        }
    }
}
