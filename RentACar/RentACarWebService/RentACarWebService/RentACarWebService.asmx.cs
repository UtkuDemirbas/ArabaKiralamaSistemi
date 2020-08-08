using Database;
using RentACarWebService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace RentACarWebService
{
    /// <summary>
    /// Summary description for RentACarWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class RentACarWebService : System.Web.Services.WebService
    {
        private SqlConnection db;


        [WebMethod]
        public void AddCar(Arac item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spAddCar", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@aracMarka", item.AracMarka);
                cmd.Parameters.AddWithValue("@aracModel", item.AracModel);
                cmd.Parameters.AddWithValue("@yil", item.Yil);
                cmd.Parameters.AddWithValue("@gerekenEhliyetYasi", item.GerekenEhliyetYasi);
                cmd.Parameters.AddWithValue("@gunlukSinirKm", item.GunlukSinirKm);
                cmd.Parameters.AddWithValue("@anlikKm", item.AnlikKm);
                cmd.Parameters.AddWithValue("@airbag", item.Airbag);
                cmd.Parameters.AddWithValue("@bagajHacmi", item.BagajHacmi);
                cmd.Parameters.AddWithValue("@koltukSayisi", item.KoltukSayisi);
                cmd.Parameters.AddWithValue("@aracTipi", item.AracTipi);
                cmd.Parameters.AddWithValue("@klima", item.Klima);
                cmd.Parameters.AddWithValue("@yakitTuru", item.YakitTuru);
                cmd.Parameters.AddWithValue("@aracSinifi", item.AracSinifi);
                cmd.Parameters.AddWithValue("@vitesTuru", item.VitesTuru);
                cmd.Parameters.AddWithValue("@fiyat", item.Fiyat);
                cmd.Parameters.AddWithValue("@aracDurumu", item.AracDurumu);
                cmd.Parameters.AddWithValue("@aciklama", item.Aciklama);
                cmd.Parameters.AddWithValue("@image", item.Image);
                cmd.Parameters.AddWithValue("@sirketID", item.SirketID);

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public void AddAdress(Adres item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spAddAdress",db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("adresBilgileri", item.AdresBilgileri);

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public void AddUser(Kullanici item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spAddUser", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ad", item.Ad);
                cmd.Parameters.AddWithValue("@soyad", item.Soyad);
                cmd.Parameters.AddWithValue("@tcNumarasi", item.TcNumarasi);
                cmd.Parameters.AddWithValue("@eposta", item.Eposta);
                cmd.Parameters.AddWithValue("@username", item.Username);
                cmd.Parameters.AddWithValue("@sifre", item.Sifre);
                cmd.Parameters.AddWithValue("@tel", item.Tel);
                cmd.Parameters.AddWithValue("@roleID", item.RoleID);
                cmd.Parameters.AddWithValue("@adresID", item.AdresID);

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public void AddCompany(Sirket item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spAddCompany", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@sirketAdi", item.SirketAdi);
                cmd.Parameters.AddWithValue("@sirketTel", item.SirketTel);
                cmd.Parameters.AddWithValue("@username", item.Username);
                cmd.Parameters.AddWithValue("@sifre", item.Sifre);
                cmd.Parameters.AddWithValue("@adresID", item.AdresID);

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public void DeleteCar(int aracID)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spDeleteCar",db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@aracID", aracID);

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public void DeleteRentACar(int kiralikAracID,int aracID)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spDeleteRentACar", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@kiralikAracID", kiralikAracID);

                cmd.ExecuteNonQuery();

                string query = "update araclar set aracDurumu=@aracDurumu where aracID=@aracID";
                SqlCommand cmdd = new SqlCommand(query, db);

                cmdd.Parameters.AddWithValue("@aracDurumu", true);
                cmdd.Parameters.AddWithValue("@aracID",aracID);
                cmdd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public void DeleteRezervation(int rezervasyonID,int aracID)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spDeleteRezervation", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@rezervasyonID", rezervasyonID);

                cmd.ExecuteNonQuery();

                string query = "update araclar set aracDurumu=@aracDurumu where aracID=@aracID";
                SqlCommand cmdd = new SqlCommand(query, db);

                cmdd.Parameters.AddWithValue("@aracDurumu", true);
                cmdd.Parameters.AddWithValue("@aracID", aracID);
                cmdd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public Arac GetCar(int aracID)
        {
            using (db = Connection.Connect())
            {
                Arac item = new Arac();

                SqlCommand cmd = new SqlCommand("spGetCar", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@aracID", aracID);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    item.AracID = Convert.ToInt32(dr["aracID"].ToString());
                    item.AracMarka = dr["aracMarka"].ToString();
                    item.AracModel = dr["aracModel"].ToString();
                    item.Yil= Convert.ToInt32(dr["yil"].ToString());
                    item.GerekenEhliyetYasi= Convert.ToInt32(dr["gerekenEhliyetYasi"].ToString());
                    item.GunlukSinirKm = Convert.ToInt32(dr["gunlukSinirKm"].ToString());
                    item.AnlikKm = Convert.ToInt32(dr["anlikKm"].ToString());
                    item.Airbag = Convert.ToBoolean(dr["airbag"].ToString());
                    item.BagajHacmi = Convert.ToInt32(dr["bagajHacmi"].ToString());
                    item.KoltukSayisi = Convert.ToInt32(dr["koltukSayisi"].ToString());
                    item.AracTipi= dr["aracTipi"].ToString();
                    item.Klima = Convert.ToBoolean(dr["klima"].ToString());
                    item.YakitTuru = dr["yakitTuru"].ToString();
                    item.AracSinifi = dr["aracSinifi"].ToString();
                    item.VitesTuru = dr["vitesTuru"].ToString();
                    item.Fiyat = Convert.ToDecimal(dr["fiyat"].ToString());
                    item.AracDurumu = Convert.ToBoolean(dr["aracDurumu"].ToString());
                    item.Aciklama = dr["aciklama"].ToString();
                    item.Image = dr["image"].ToString();
                    item.SirketID= Convert.ToInt32(dr["sirketID"].ToString());
                }
                return item;
            }
        }
        [WebMethod]
        public KiralikArac GetCarInRentACar(int aracID)
        {
            using (db = Connection.Connect())
            {
                KiralikArac item = new KiralikArac();

                SqlCommand cmd = new SqlCommand("spGetCarInRentACar", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@aracID", aracID);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    item.KiralikAracID = Convert.ToInt32(dr["kiralikAracID"].ToString());
                    item.AracID = Convert.ToInt32(dr["aracID"].ToString());
                    item.KullaniciID = Convert.ToInt32(dr["kullaniciID"].ToString());
                    item.AracMarka = dr["aracMarka"].ToString();
                    item.AracModel = dr["aracModel"].ToString();
                    item.Image = dr["image"].ToString();
                    item.KiralanmaZamani = Convert.ToDateTime(dr["kiralanmaZamani"].ToString());
                    item.VerilisKm = Convert.ToInt32(dr["verilisKm"].ToString());
                    item.SonKilometre = Convert.ToInt32(dr["sonKilometre"].ToString());
                    item.Ucret = Convert.ToDecimal(dr["ucret"].ToString());
                    item.KacGun = Convert.ToInt32(dr["kacGun"].ToString());
                    item.AlisYeri = dr["alisYeri"].ToString();
                    item.VerisYeri = dr["verisYeri"].ToString();
                }
                return item;
               
            }
        }
        [WebMethod]
        public List<KiralikArac> GetRentACar(int kullaniciID)
        {
            using (db = Connection.Connect())
            {
                List<KiralikArac> araclar = new List<KiralikArac>();
                

                SqlCommand cmd = new SqlCommand("spGetRentACar", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@kullaniciID", kullaniciID);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    KiralikArac item = new KiralikArac();

                    item.KiralikAracID = Convert.ToInt32(dr["kiralikAracID"].ToString());
                    item.AracID = Convert.ToInt32(dr["aracID"].ToString());
                    item.KullaniciID = Convert.ToInt32(dr["kullaniciID"].ToString());
                    item.AracMarka= dr["aracMarka"].ToString();
                    item.AracModel = dr["aracModel"].ToString();
                    item.Image = dr["image"].ToString();
                    item.KiralanmaZamani = Convert.ToDateTime(dr["kiralanmaZamani"].ToString());
                    item.VerilisKm = Convert.ToInt32(dr["verilisKm"].ToString());
                    item.SonKilometre = Convert.ToInt32(dr["sonKilometre"].ToString());
                    item.Ucret = Convert.ToDecimal(dr["ucret"].ToString());
                    item.KacGun = Convert.ToInt32(dr["kacGun"].ToString());
                    item.AlisYeri = dr["alisYeri"].ToString();
                    item.VerisYeri = dr["verisYeri"].ToString();

                    araclar.Add(item);
                }
                return araclar;
            }
        }
        [WebMethod]
        public List<Rezervasyon> GetRezervation(int kullaniciID)
        {
            using (db = Connection.Connect())
            {
                DateTime nowTime = DateTime.Now;
                List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>();

                SqlCommand cmd = new SqlCommand("spGetRezervation", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@kullaniciID", kullaniciID);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Rezervasyon item = new Rezervasyon();
                    item.RezervasyonID = Convert.ToInt32(dr["rezervasyonID"].ToString());
                    item.AracID = Convert.ToInt32(dr["aracID"].ToString());
                    item.KullaniciID = Convert.ToInt32(dr["kullaniciID"].ToString());
                    item.AracMarka = dr["aracMarka"].ToString();
                    item.AracModel = dr["aracModel"].ToString();
                    item.Image = dr["image"].ToString();
                    item.AlisTarihi = Convert.ToDateTime(dr["alisTarihi"].ToString());
                    item.VerisTarihi = Convert.ToDateTime(dr["verisTarihi"].ToString());
                    item.AlisYeri = dr["alisYeri"].ToString();
                    item.VerisYeri = dr["verisYeri"].ToString();
                    if (nowTime > item.AlisTarihi)
                    {
                        DeleteRezervation(item.RezervasyonID, item.AracID);
                    }
                    else
                    {
                        rezervasyonlar.Add(item);
                    }
                }
                return rezervasyonlar;
            }
        }
        [WebMethod]
        public Rezervasyon GetRezervationID(int rezervasyonID)
        {
            using (db = Connection.Connect())
            {
                Rezervasyon item = new Rezervasyon();

                SqlCommand cmd = new SqlCommand("spGetRezervationID", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@rezervasyonID", rezervasyonID);

                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    
                    item.RezervasyonID = Convert.ToInt32(dr["rezervasyonID"].ToString());
                    item.AracID = Convert.ToInt32(dr["aracID"].ToString());
                    item.KullaniciID = Convert.ToInt32(dr["kullaniciID"].ToString());
                    item.AracMarka = dr["aracMarka"].ToString();
                    item.AracModel = dr["aracModel"].ToString();
                    item.Image = dr["image"].ToString();
                    item.AlisTarihi = Convert.ToDateTime(dr["alisTarihi"].ToString());
                    item.VerisTarihi = Convert.ToDateTime(dr["verisTarihi"].ToString());
                    item.AlisYeri = dr["alisYeri"].ToString();
                    item.VerisYeri = dr["verisYeri"].ToString();

                }
                return item;
            }
        }
        [WebMethod]
        public Kullanici GetUser(int kullaniciID)
        {
            using (db = Connection.Connect())
            {
                Kullanici item = new Kullanici();

                SqlCommand cmd = new SqlCommand("spGetUser", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@kullaniciID", kullaniciID);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    item.KullaniciID = Convert.ToInt32(dr["kullaniciID"].ToString());
                    item.Ad = dr["ad"].ToString();
                    item.Soyad = dr["soyad"].ToString();
                    item.TcNumarasi = dr["tcNumarasi"].ToString();
                    item.Eposta = dr["eposta"].ToString();
                    item.Username = dr["username"].ToString();
                    item.Sifre = dr["sifre"].ToString();
                    item.Tel = dr["tel"].ToString();
                    item.RoleID = Convert.ToInt32(dr["roleID"].ToString());
                    item.AdresID = Convert.ToInt32(dr["adresID"].ToString());
                }
                return item;
            }
        }
        [WebMethod]
        public int GetAdressID()
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spGetAdressID",db);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return Convert.ToInt32(dr["adresID"].ToString());
                }
                else
                    return -1;
            }
        }
        [WebMethod]
        public Adres GetAdress(int id)
        {
            using (db = Connection.Connect())
            {
                Adres adres = new Adres();

                SqlCommand cmd = new SqlCommand("spGetAdress", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@adresID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    adres.AdresID = Convert.ToInt32(dr["adresID"].ToString());
                    adres.AdresBilgileri = dr["adresBilgileri"].ToString();
                }
                return adres;
            }
            
        }
        [WebMethod]
        public Sirket GetCompany(int id)
        {
            using (db = Connection.Connect())
            {
                Sirket sirket = new Sirket();

                SqlCommand cmd = new SqlCommand("spGetCompany", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@sirketID", id);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    sirket.SirketID = Convert.ToInt32(dr["sirketID"].ToString());
                    sirket.SirketAdi = dr["sirketAdi"].ToString();
                    sirket.SirketTel = dr["sirketTel"].ToString();
                    sirket.Username = dr["username"].ToString();
                    sirket.Sifre = dr["sifre"].ToString();
                    sirket.AdresID = Convert.ToInt32(dr["adresID"].ToString());
                }
                return sirket;
            }

        }
        [WebMethod]
        public void UpdateCar(Arac item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spUpdateCar", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@aracMarka", item.AracMarka);
                cmd.Parameters.AddWithValue("@aracModel", item.AracModel);
                cmd.Parameters.AddWithValue("@yil", item.Yil);
                cmd.Parameters.AddWithValue("@gerekenEhliyetYasi", item.GerekenEhliyetYasi);
                cmd.Parameters.AddWithValue("@gunlukSinirKm", item.GunlukSinirKm);
                cmd.Parameters.AddWithValue("@anlikKm", item.AnlikKm);
                cmd.Parameters.AddWithValue("@airbag", item.Airbag);
                cmd.Parameters.AddWithValue("@bagajHacmi", item.BagajHacmi);
                cmd.Parameters.AddWithValue("@koltukSayisi", item.KoltukSayisi);
                cmd.Parameters.AddWithValue("@aracTipi", item.AracTipi);
                cmd.Parameters.AddWithValue("@klima", item.Klima);
                cmd.Parameters.AddWithValue("@yakitTuru", item.YakitTuru);
                cmd.Parameters.AddWithValue("@aracSinifi", item.AracSinifi);
                cmd.Parameters.AddWithValue("@vitesTuru", item.VitesTuru);
                cmd.Parameters.AddWithValue("@fiyat", item.Fiyat);
                cmd.Parameters.AddWithValue("@aracDurumu", item.AracDurumu);
                cmd.Parameters.AddWithValue("@aciklama", item.Aciklama);
                cmd.Parameters.AddWithValue("@image", item.Image);
                cmd.Parameters.AddWithValue("@aracID", item.AracID);

                cmd.ExecuteNonQuery();
                db.Close();

            }
        }
        [WebMethod]
        public void UpdateUser(Kullanici item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ad", item.Ad);
                cmd.Parameters.AddWithValue("@soyad", item.Soyad);
                cmd.Parameters.AddWithValue("@tcNumarasi", item.TcNumarasi);
                cmd.Parameters.AddWithValue("@eposta", item.Eposta);
                cmd.Parameters.AddWithValue("@username", item.Username);
                cmd.Parameters.AddWithValue("@sifre", item.Sifre);
                cmd.Parameters.AddWithValue("@tel", item.Tel);
                cmd.Parameters.AddWithValue("@kullaniciID", item.KullaniciID);

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public void UpdateAdress(Adres item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spUpdateAdress", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@adresBilgileri", item.AdresBilgileri);
                cmd.Parameters.AddWithValue("@adresID", item.AdresID);

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public void UpdateCompany(Sirket item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spUpdateCompany", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@sirketAdi", item.SirketAdi);
                cmd.Parameters.AddWithValue("@sirketTel", item.SirketTel);
                cmd.Parameters.AddWithValue("@username", item.Username);
                cmd.Parameters.AddWithValue("@sifre", item.Sifre);
                cmd.Parameters.AddWithValue("@sirketID", item.SirketID);

                cmd.ExecuteNonQuery();
                db.Close();
            }
        }
        [WebMethod]
        public List<Arac> ListCompanyCar(int sirketID)
        {
            using (db = Connection.Connect())
            {
                List<Arac> araclar = new List<Arac>();
                SqlCommand cmd = new SqlCommand("spListCompanyCar", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@sirketID", sirketID);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Arac arac = new Arac();

                    arac.AracID = Convert.ToInt32(dr["aracID"].ToString());
                    arac.AracMarka = dr["aracMarka"].ToString();
                    arac.AracModel = dr["aracModel"].ToString();
                    arac.Yil = Convert.ToInt32(dr["yil"].ToString());
                    arac.GerekenEhliyetYasi = Convert.ToInt32(dr["gerekenEhliyetYasi"].ToString());
                    arac.GunlukSinirKm = Convert.ToInt32(dr["gunlukSinirKm"].ToString());
                    arac.AnlikKm = Convert.ToInt32(dr["anlikKm"].ToString());
                    arac.Airbag = Convert.ToBoolean(dr["airbag"].ToString());
                    arac.BagajHacmi = Convert.ToInt32(dr["bagajHacmi"].ToString());
                    arac.KoltukSayisi = Convert.ToInt32(dr["koltukSayisi"].ToString());
                    arac.AracTipi= dr["aracTipi"].ToString();
                    arac.Klima = Convert.ToBoolean(dr["klima"].ToString());
                    arac.YakitTuru = dr["yakitTuru"].ToString();
                    arac.AracSinifi = dr["aracSinifi"].ToString();
                    arac.VitesTuru = dr["vitesTuru"].ToString();
                    arac.Fiyat = Convert.ToDecimal(dr["fiyat"].ToString());
                    arac.AracDurumu = Convert.ToBoolean(dr["aracDurumu"]);
                    arac.Aciklama = dr["aciklama"].ToString();
                    arac.Image = dr["image"].ToString();

                    araclar.Add(arac);
                }
                return araclar;
            }
        }
        [WebMethod]
        public List<KiralikArac> ListCustomerCar(int kullaniciID)
        {
            using (db = Connection.Connect())
            {
                List<KiralikArac> araclar = new List<KiralikArac>();
                SqlCommand cmd = new SqlCommand("spListCustomerCar", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@kullaniciID", kullaniciID);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    KiralikArac arac = new KiralikArac();

                    arac.AracID = Convert.ToInt32(dr["aracID"].ToString());
                    arac.KullaniciID = Convert.ToInt32(dr["kullaniciID"].ToString());
                    arac.KiralanmaZamani = Convert.ToDateTime(dr["kiralanmaZamani"].ToString());
                    arac.VerilisKm = Convert.ToInt32(dr["verilisKm"].ToString());
                    arac.SonKilometre = Convert.ToInt32(dr["sonKilometre"].ToString());
                    arac.Ucret = Convert.ToDecimal(dr["ucret"].ToString());
                    arac.KacGun = Convert.ToInt32(dr["kacGun"].ToString());
                    arac.AlisYeri =dr["alisYeri"].ToString();
                    arac.VerisYeri =dr["verisYeri"].ToString();

                    araclar.Add(arac);
                }
                return araclar;
            }
        }
        [WebMethod]
        public List<Arac> ListCar(string pickupLocation,string returnLocation)
        {
            using (db = Connection.Connect())
            {
                string CompanyName = "";
                string Alisyeri = "";
                string VerisYeri = "";
                string dosyayolu = "C:/Users/utkuf/OneDrive/Masaüstü/RentACar/RentACar/havaalanlari/havaalanlari.txt";
                FileStream fs = new FileStream(dosyayolu, FileMode.Open, FileAccess.Read);

                StreamReader sw = new StreamReader(fs);


                string havaalani = sw.ReadLine();
                while (havaalani != null)
                {
                    string[] tam = havaalani.Split('-');
                    if (pickupLocation == returnLocation)
                    {
                        if (tam[0] == returnLocation)
                        {
                            Alisyeri = VerisYeri = havaalani;
                            break;
                        }
                    }
                    else
                    {
                        if (tam[0] == pickupLocation)
                        {
                            Alisyeri = havaalani;
                        }
                        else if (tam[0] == returnLocation)
                        {
                            VerisYeri = havaalani;
                        }
                    }
                    havaalani = sw.ReadLine();
                }
                List<Arac> araclar = new List<Arac>();
                SqlCommand cmd = new SqlCommand("spListCar", db);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Arac arac = new Arac();
                    arac.AracID= Convert.ToInt32(dr["aracID"].ToString());
                    arac.AracMarka = dr["aracMarka"].ToString();
                    arac.AracModel = dr["aracModel"].ToString();
                    arac.Yil= Convert.ToInt32(dr["yil"].ToString());
                    arac.GerekenEhliyetYasi = Convert.ToInt32(dr["gerekenEhliyetYasi"].ToString());
                    arac.GunlukSinirKm = Convert.ToInt32(dr["gunlukSinirKm"].ToString());
                    arac.AnlikKm = Convert.ToInt32(dr["anlikKm"].ToString());
                    arac.Airbag = Convert.ToBoolean(dr["airbag"].ToString());
                    arac.BagajHacmi = Convert.ToInt32(dr["bagajHacmi"].ToString());
                    arac.KoltukSayisi = Convert.ToInt32(dr["koltukSayisi"].ToString());
                    arac.AracTipi= dr["aracTipi"].ToString();
                    arac.Klima = Convert.ToBoolean(dr["klima"].ToString());
                    arac.YakitTuru = dr["yakitTuru"].ToString();
                    arac.AracSinifi = dr["aracSinifi"].ToString();
                    arac.VitesTuru = dr["vitesTuru"].ToString();
                    arac.Fiyat = Convert.ToDecimal(dr["fiyat"].ToString());
                    arac.Aciklama = dr["aciklama"].ToString();
                    arac.Image = dr["image"].ToString();
                    arac.SirketID = Convert.ToInt32(dr["sirketID"].ToString());

                    CompanyName = GetCompany(arac.SirketID).SirketAdi;
                    if((Alisyeri.Contains(CompanyName) && VerisYeri.Contains(CompanyName)))
                    {
                        araclar.Add(arac);
                    }
                }
                return araclar;
            }
        }
        [WebMethod]
        public List<Arac> ListCarFilter(string pickupLocation, string returnLocation,Filtre filter)
        {
            List<Arac> araclarFilter = new List<Arac>();
            bool marka = true;

            foreach (var item in ListCar(pickupLocation, returnLocation))
            {
                if ((item.AracMarka ==filter.Marka || filter.Marka==null) && item.Fiyat >= filter.Min && item.Fiyat <= filter.Max && 
                    (item.AracTipi == filter.AracTipi || filter.AracTipi==null) && 
                    (item.AracSinifi == filter.AracSinifi || filter.AracSinifi==null) && 
                    (item.YakitTuru == filter.YakitTuru || filter.YakitTuru==null ) && 
                    (item.Yil == Convert.ToInt32(filter.AracYili) || filter.AracYili==null))
                    araclarFilter.Add(item);
            }

            return araclarFilter;
        }
        [WebMethod]
        public List<Kullanici> ListCustomer()
        {
            using (db = Connection.Connect())
            {
                List<Kullanici> kullanicilar = new List<Kullanici>();
                SqlCommand cmd = new SqlCommand("spListCustomer", db);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Kullanici kul = new Kullanici();

                    kul.KullaniciID = Convert.ToInt32(dr["kullaniciID"].ToString());
                    kul.Ad = dr["ad"].ToString();
                    kul.Soyad = dr["soyad"].ToString();
                    kul.TcNumarasi = dr["tcNumarasi"].ToString();
                    kul.Eposta = dr["eposta"].ToString();
                    kul.Username = dr["username"].ToString();
                    kul.Sifre = dr["sifre"].ToString();
                    kul.Tel = dr["tel"].ToString();
                    kul.RoleID = Convert.ToInt32(dr["roleID"].ToString());
                    kul.AdresID = Convert.ToInt32(dr["adresID"].ToString());
                    kullanicilar.Add(kul);
                }
                return kullanicilar;
            }
        }
        [WebMethod]
        public List<Sirket> ListCompany()
        {
            using (db = Connection.Connect())
            {
                List<Sirket> sirketler = new List<Sirket>();
                SqlCommand cmd = new SqlCommand("spListCompany", db);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Sirket kul = new Sirket();

                    kul.SirketID = Convert.ToInt32(dr["sirketID"].ToString());
                    kul.SirketAdi = dr["sirketAdi"].ToString();
                    kul.SirketTel = dr["sirketTel"].ToString();
                    kul.Username = dr["username"].ToString();
                    kul.Sifre = dr["sifre"].ToString();
                    kul.AdresID = Convert.ToInt32(dr["adresID"].ToString());
                    sirketler.Add(kul);
                }
                return sirketler;
            }
        }
        [WebMethod]
        public int Login(string username,string sifre)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spLogin",db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@sifre", sifre);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return Convert.ToInt32(dr["kullaniciID"].ToString());
                }
                else
                    return -1;
               
            }
        }
        [WebMethod]

        public int LoginCompany(string username,string sifre)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spLoginCompany", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@sifre", sifre);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return Convert.ToInt32(dr["sirketID"].ToString());
                }
                else
                    return -1;

            }
        }
        [WebMethod]
        public void RentACar(KiralikArac item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spAddRentACar",db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@aracID", item.AracID);
                cmd.Parameters.AddWithValue("@kullaniciID", item.KullaniciID);
                cmd.Parameters.AddWithValue("@aracMarka", item.AracMarka);
                cmd.Parameters.AddWithValue("@aracModel", item.AracModel);
                cmd.Parameters.AddWithValue("@image", item.Image);
                cmd.Parameters.AddWithValue("@kiralanmaZamani", item.KiralanmaZamani);
                cmd.Parameters.AddWithValue("@verilisKm", item.VerilisKm);
                cmd.Parameters.AddWithValue("@sonKilometre", item.SonKilometre);
                cmd.Parameters.AddWithValue("@ucret", item.Ucret);
                cmd.Parameters.AddWithValue("@kacGun", item.KacGun);
                cmd.Parameters.AddWithValue("@alisYeri", item.AlisYeri);
                cmd.Parameters.AddWithValue("@verisYeri", item.VerisYeri);

                cmd.ExecuteNonQuery();

                string query = "update araclar set aracDurumu=@aracDurumu where aracID=@aracID";
                SqlCommand cmdd = new SqlCommand(query, db);

                cmdd.Parameters.AddWithValue("@aracDurumu", false);
                cmdd.Parameters.AddWithValue("@aracID", item.AracID);
                cmdd.ExecuteNonQuery();

                db.Close();
            }
        }
        [WebMethod]
        public void Rezervation(Rezervasyon item)
        {
            using (db = Connection.Connect())
            {
                SqlCommand cmd = new SqlCommand("spAddRezervation", db);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@aracID", item.AracID);
                cmd.Parameters.AddWithValue("@kullaniciID", item.KullaniciID);
                cmd.Parameters.AddWithValue("@aracMarka", item.AracMarka);
                cmd.Parameters.AddWithValue("@aracModel", item.AracModel);
                cmd.Parameters.AddWithValue("@image", item.Image);
                cmd.Parameters.AddWithValue("@alisTarihi", item.AlisTarihi);
                cmd.Parameters.AddWithValue("@verisTarihi", item.VerisTarihi);
                cmd.Parameters.AddWithValue("@alisYeri", item.AlisYeri);
                cmd.Parameters.AddWithValue("@verisYeri", item.VerisYeri);

                cmd.ExecuteNonQuery();

                string query = "update araclar set aracDurumu=@aracDurumu where aracID=@aracID";
                SqlCommand cmdd = new SqlCommand(query, db);

                cmdd.Parameters.AddWithValue("@aracDurumu", false);
                cmdd.Parameters.AddWithValue("@aracID", item.AracID);
                cmdd.ExecuteNonQuery();

                db.Close();
            }
        }
    }
}
