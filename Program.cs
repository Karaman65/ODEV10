using System;

class HashTable
{
    // Hash tablosunun boyutu sabit olarak 100 belirleniyor
    const int TableSize = 100;

    // Hash tablosu için verileri tutacak dizi (int türünde)
    private int[] table;

    // Boşluk kontrolü yapmak için kullanılan bool dizisi
    private bool[] meşgulmu;

    // HashTable sınıfının yapıcı metodu
    public HashTable()
    {
        // table dizisini oluşturuyoruz, boyut 100
        table = new int[TableSize];

        // meşgulmu dizisini de oluşturuyoruz, boyut 100
        meşgulmu = new bool[TableSize];

        // Başlangıçta tüm diziler boş olacak şekilde ayarlanıyor
        for (int i = 0; i < TableSize; i++)
        {
            table[i] = -1; // -1, boş alanları temsil eder
            meşgulmu[i] = false; // Başlangıçta tüm alanlar boş
        }
    }

    // Division Method (Modüler Hashing) kullanarak hash hesaplama
    private int Hash(int key)
    {
        return key % TableSize; // Anahtarın tablo boyutuna göre kalanını döndürür
    }

    // Linear Probing ile çakışma çözme
    public bool InsertLinear(int key)
    {
        int index = Hash(key); // İlk olarak anahtar için bir hash değeri hesapla

        // Linear probing ile boş alan arama
        while (meşgulmu[index]) // Eğer mevcut indeks doluysa
        {
            if (table[index] == key) // Eğer anahtar zaten mevcutsa, ekleme yapılmaz
                return false;

            index = (index + 1) % TableSize; // Bir sonraki index'e geç (Linear Probing)
        }

        // Boş bir alan bulundu
        table[index] = key; // Anahtar bulunduğu alana eklenir
        meşgulmu[index] = true; // Bu alan dolu olarak işaretlenir
        return true; // Başarıyla eklenmiştir
    }

    // Quadratic Probing ile çakışma çözme
    public bool InsertQuadratic(int key)
    {
        int index = Hash(key); // İlk olarak anahtar için bir hash değeri hesapla
        int i = 1; // Probing sayısını takip etmek için 'i' başlatılır

        // Quadratic probing ile boş alan arama
        while (meşgulmu[index]) // Eğer mevcut indeks doluysa
        {
            if (table[index] == key) // Eğer anahtar zaten mevcutsa, ekleme yapılmaz
                return false;

            index = (index + i * i) % TableSize; // Quadratic Probing: i^2 kadar ilerle
            i++; // i'yi bir artır
        }

        // Boş bir alan bulundu
        table[index] = key; // Anahtar bulunduğu alana eklenir
        meşgulmu[index] = true; // Bu alan dolu olarak işaretlenir
        return true; // Başarıyla eklenmiştir
    }

    // Hash tablosunun içeriğini ekrana yazdırma
    public void DisplayTable()
    {
        // Tablodaki tüm indeksleri kontrol et
        for (int i = 0; i < TableSize; i++)
        {
            if (meşgulmu[i]) // Eğer bu indeks doluysa
            {
                Console.WriteLine($"Index {i}: {table[i]}"); // Anahtar ve indeks yazdırılır
            }
            else // Eğer bu indeks boşsa
            {
                Console.WriteLine($"Index {i}: boş"); // Boş olduğu belirtilir
            }
        }
    }
}

class Program
{
    static Random random = new Random(); // Rastgele sayılar üretmek için Random nesnesi

    static void Main(string[] args)
    {
        // Linear Probing yöntemiyle hash tablosu oluşturuluyor
        HashTable hashTableLinear = new HashTable();
        // Quadratic Probing yöntemiyle hash tablosu oluşturuluyor
        HashTable hashTableQuadratic = new HashTable();

        // 100 tane rastgele anahtar üretilip hash tablosuna ekleniyor
        Console.WriteLine("Linear Probing ile Hash Tablosu:");
        for (int i = 0; i < 100; i++)
        {
            int key = random.Next(0, 1000); // 0 ile 999 arasında rastgele bir anahtar üretilir
            hashTableLinear.InsertLinear(key); // Anahtar linear probing ile eklenir
        }
        hashTableLinear.DisplayTable(); // Linear probing tablosu yazdırılır

        // Quadratic Probing yöntemiyle hash tablosu oluşturuluyor
        Console.WriteLine("\nQuadratic Probing ile Hash Tablosu:");
        for (int i = 0; i < 100; i++)
        {
            int key = random.Next(0, 1000); // 0 ile 999 arasında rastgele bir anahtar üretilir
            hashTableQuadratic.InsertQuadratic(key); // Anahtar quadratic probing ile eklenir
        }
        hashTableQuadratic.DisplayTable(); // Quadratic probing tablosu yazdırılır
    }
}
