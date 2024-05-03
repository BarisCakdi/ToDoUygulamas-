namespace ToDoUygulaması
{
    internal class Program
    {
        public class Todo
        {
            public string? Gorevler { get; set; } //Gorevler
            public bool Tamamlanan { get; set; } // tamamlandı mı?
            public DateTime Tarih { get; set; } // Tarih 
            public DateTime Deadline { get; set; } // tamamlanması gereken tarih
            public Todo(string metin)
            {
                Gorevler = metin;
                Tarih = DateTime.Now;
                Deadline = DateTime.Now;
                Tamamlanan = false;
            }
        }
        static List<Todo> todos = new List<Todo>();
        static void TxtKaydet()
        {
            using StreamWriter writer = new StreamWriter("Todo.txt");
            foreach(var todo in todos)
            {
                writer.WriteLine(todo.Tarih.ToString());
                writer.WriteLine(todo.Gorevler);
            }
        }
        static void GorevEkle()
        {
            //Console.Clear();
            //DateTime bugun = DateTime.Today;
            //bool gunlukteKayitVarmi = todos.Any(p => p.Tarih.Date == bugun);
            //if (gunlukteKayitVarmi)
            //{
                //Console.WriteLine("Bugün kayıt eklediniz. Tekrardan eklemek istiyormusunuz? (E)vet/(H)ayır");
                //string tekrarKayıt = Console.ReadLine();
                //tekrarKayıt = tekrarKayıt.ToLower();
                //if (tekrarKayıt == "e")//Güncelleme tarihi hepsinde değişiyor onu araştır.
                //{
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Eklenicek Gorev: ");
                    Console.ResetColor();
                    string inputKayit = Console.ReadLine();
                    todos.Add(new Todo(inputKayit));
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Kayıt alındı.");
                    Console.ResetColor();
                    TxtKaydet();
                    AnaMenu();
                //}
                //AnaMenu();

            //}
        }
        static void Kayitsil(int i)
        {
            Console.Clear();
            Console.WriteLine("Kayıt silmek istediğinize eminmisiniz? (E)vet/(H)ayır");
            string kayitsil = Console.ReadLine();
            kayitsil = kayitsil.ToLower();

            if (kayitsil == "e")
            {
                Todo silinecekKayit = todos[i];
                todos.RemoveRange(i, 1);
                Console.WriteLine("Kayıt Silinmiştir!");
                TxtKaydet();
                MenuGoster();
            }
            MenuGoster();
        }
        static void GorevleriListele()
        {
            Console.Clear();
            Console.WriteLine("\nTüm Kayıtlar");
            Console.ResetColor();
            if (todos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Listelenecek Kayıt bulunamadı.");
                Console.ResetColor();
            }
            for (int i = 0; i < todos.Count; i++)
            {
                Console.Clear();
                todos[i].Tarih.ToString("ddMMyyyy");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Tamamlama Tarihi: {todos[i].Deadline}");
                Console.WriteLine($"Tarih: {todos[i].Tarih}");
                Console.WriteLine("==========================================");
                Console.ResetColor();
                Console.WriteLine($"{i + 1} - {todos[i].Gorevler}");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("==========================================");
                Console.WriteLine("(S)onraki Gorev || (A)na Menü || (D)üzenle || (X)Sil ");
                Console.ResetColor();
                string inputSecim = Console.ReadLine();
                inputSecim = inputSecim.ToLower();
                if (inputSecim == "a")
                {
                    AnaMenu();
                    break;
                }
                else if (inputSecim == "d")
                {

                    KayitDüzenle(i);
                    TxtKaydet();
                    AnaMenu();
                    break;
                }
                else if (inputSecim == "x")
                {
                    Kayitsil(i);
                    TxtKaydet();
                    AnaMenu();
                    break;
                }
                else if (inputSecim != "s")
                {
                    Console.Clear();
                    Console.WriteLine("Yanlış seçim!");
                    i--;
                    continue;
                }
            }
            Console.Clear();
            Console.WriteLine("Başka görev bulunamadı!");
            MenuGoster();
        }
        static void GorevleriYükle()
        {
            using StreamReader reader = new StreamReader("Todo.txt");

            string satir;
            while ((satir = reader.ReadLine()) != null)
            {
                string guncelKayit = satir;
                string metin = reader.ReadLine();
                DateTime kayitTarihi;
                if (DateTime.TryParse(guncelKayit, out kayitTarihi))
                {
                    todos.Add(new Todo(metin) { Tarih = kayitTarihi });
                }
            }
        }
        static void KayitDüzenle(int i)
        {
            Console.Clear();
            Todo duzenlenicekKayit = todos[i];
            Console.WriteLine("KAYIT DÜZENLEME");
            Console.WriteLine("=======================");
            Console.Write("Yeni metin giriniz: ");
            string yeniMetin = Console.ReadLine();
            duzenlenicekKayit.Gorevler = yeniMetin;
            DateTime yenitarih = DateTime.Today;
            duzenlenicekKayit.Tarih = yenitarih;
            MenuGoster();
        }

        static void AnaMenu()
        {
            Console.Clear();
            Console.WriteLine("To Do Uygulamasına hoş geldiniz.");
            Console.WriteLine("==================================");
            Console.WriteLine("1- Yapılacak işleri listeleyin ");
            Console.WriteLine("2- Yeni iş ekleyin");
            Console.WriteLine("3- İş listesini temizleyin");
            Console.WriteLine("4- Çıkış yapın");
            Console.Write("\nSeçiminiz: ");
            char inputSecim = Console.ReadKey().KeyChar;
                switch (inputSecim)
            {
                case '1':
                    GorevleriListele();
                    break;
                case '2':
                    GorevEkle();
                    break;
                case '3':
                    break;
                case '4':
                    break;
                default:
                    Console.WriteLine("\n Yanlış Tuşlama");
                    MenuGoster();
                    break;

            }
        }
        static void MenuGoster()
        {
            Console.WriteLine("\nMenüye dönmek için bir tuşa basınız.");
            Console.ReadKey(true);
            AnaMenu();
        }

        static void Main(string[] args)
        {
            GorevleriYükle();
            Console.WriteLine("Hello, World!");
            AnaMenu();
        }
    }
}
