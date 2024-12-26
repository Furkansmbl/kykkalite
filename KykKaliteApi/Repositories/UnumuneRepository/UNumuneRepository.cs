using Dapper;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.HMPNvalueDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.UnumuneRepository
{
    public class UNumuneRepository : IUnumuneRepository
    {
        private readonly Context _context;
        public UNumuneRepository(Context context)
        {
            _context = context;
        }

        public async void CreateUnumune(CreateUnumuneDto createUnumuneDto)
        {
            string query = @"
DECLARE @numuneID INT;


INSERT INTO Unumune (urunID, SiraNo, UretimTarihi, KontrolSaati, NumuneSeriNoSarjNo, MudahaleVarmi, Aciklama, OnayDurumu, AmirOnayDurumu, OlusturmaTarihi, PersonelSicilNo, Token, Trend, HatAdiAciklamasi)
VALUES (@urunId, @siraNo, @UretimTarihi, @kontrolSaati, @numuneSeriNoSarjNo, @mudahaleVarmi, @aciklama, @onayDurumu, @amirOnayDurumu, @olusturmaTarihi, @personelSicilNo, @token, @trend, @hatAdiAciklamasi);


SET @numuneID = SCOPE_IDENTITY();


INSERT INTO UPNvalue (UPAtamaKodu, NumuneID, Versiyon, Value, OlusturmaTarihi, PersonelSicilNo)
SELECT upWithRowNum.UPAtamaKodu, @numuneID, @versiyon, v.[value], @olusturmaTarihi, @personelSicilNo
FROM (
    -- urunler, upatamaAktif ve fabrikalar tablolarını birleştiriyoruz ve burada fabrikaID'yi filtreliyoruz
    SELECT upatama.UPAtamaKodu, ROW_NUMBER() OVER (ORDER BY upatama.OlusturmaTarihi) AS RowNum
    FROM urunler AS urun
    INNER JOIN upatamaAktif AS upatama ON urun.urunID = upatama.urunID
    INNER JOIN fabrikalar AS fabrika ON fabrika.fabrikaID = upatama.fabrikaID
    WHERE urun.urunID = @urunID
    AND fabrika.fabrikaID = @FabrikaID
    AND upatama.kullanimDurumu = 1
) AS upWithRowNum
JOIN (

    VALUES 
    (@value1, 1), (@value2, 2), (@value3, 3), (@value4, 4), (@value5, 5), (@value6, 6), (@value7, 7),
    (@value8, 8), (@value9, 9), (@value10, 10), (@value11, 11), (@value12, 12), (@value13, 13),
    (@value14, 14), (@value15, 15)
) AS v([value], RowNum) 
ON upWithRowNum.RowNum = v.RowNum;

 ";

            var parameters = new DynamicParameters();
            parameters.Add("@urunId", createUnumuneDto.UrunId);
            parameters.Add("@siraNo", createUnumuneDto.SiraNo);
            parameters.Add("@fabrikaId", createUnumuneDto.FabrikaId);
            parameters.Add("@UretimTarihi", createUnumuneDto.UretimTarihi);
            parameters.Add("@kontrolSaati", createUnumuneDto.KontrolSaati);
            parameters.Add("@numuneSeriNoSarjNo", createUnumuneDto.NumuneSeriNoSarjNo);
            parameters.Add("@mudahaleVarmi", createUnumuneDto.MudahaleVarmi);
            parameters.Add("@aciklama", createUnumuneDto.Aciklama);
            parameters.Add("@onayDurumu", createUnumuneDto.OnayDurumu);
            parameters.Add("@amirOnayDurumu", createUnumuneDto.AmirOnayDurumu);
            parameters.Add("@olusturmaTarihi", createUnumuneDto.OlusturmaTarihi);
            parameters.Add("@versiyon", createUnumuneDto.Versiyon);
            parameters.Add("@personelSicilNo", createUnumuneDto.PersonelSicilNo);
            parameters.Add("@value", createUnumuneDto.Value);
            parameters.Add("@value1", createUnumuneDto.Value1);
            parameters.Add("@value2", createUnumuneDto.Value2);
            parameters.Add("@value3", createUnumuneDto.Value3);
            parameters.Add("@value4", createUnumuneDto.Value4);
            parameters.Add("@value5", createUnumuneDto.Value5);
            parameters.Add("@value6", createUnumuneDto.Value6);
            parameters.Add("@value7", createUnumuneDto.Value7);
            parameters.Add("@value8", createUnumuneDto.Value8);
            parameters.Add("@value9", createUnumuneDto.Value9);
            parameters.Add("@value10", createUnumuneDto.Value10);
            parameters.Add("@value11", createUnumuneDto.Value11);
            parameters.Add("@value12", createUnumuneDto.Value12);
            parameters.Add("@value13", createUnumuneDto.Value13);
            parameters.Add("@value14", createUnumuneDto.Value14);
            parameters.Add("@value15", createUnumuneDto.Value15);
            parameters.Add("@token", createUnumuneDto.Token);
            parameters.Add("@trend", createUnumuneDto.Trend);
            parameters.Add("@hatAdiAciklamasi", createUnumuneDto.HatAdiAciklamasi);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async void CreateUnumuneManuel(CreateUnumuneManuelDto createUnumuneManuelDto )
        {
            string query = @"
DECLARE @numuneID INT;


INSERT INTO Unumune (urunID, SiraNo, UretimTarihi, KontrolSaati, NumuneSeriNoSarjNo, MudahaleVarmi, Aciklama, OnayDurumu, AmirOnayDurumu, OlusturmaTarihi, PersonelSicilNo, Token, Trend, HatAdiAciklamasi)
VALUES (@urunId, @siraNo, @UretimTarihi, @kontrolSaati, @numuneSeriNoSarjNo, @mudahaleVarmi, @aciklama, @onayDurumu, @amirOnayDurumu, @olusturmaTarihi, @personelSicilNo, @token, @trend, @hatAdiAciklamasi);


SET @numuneID = SCOPE_IDENTITY();


INSERT INTO UPNvalue (UPAtamaKodu, NumuneID, Versiyon, Value, OlusturmaTarihi, PersonelSicilNo)
SELECT upWithRowNum.UPAtamaKodu, @numuneID, @versiyon, v.[value], @olusturmaTarihi, @personelSicilNo
FROM (
    -- urunler, upatamaAktif ve fabrikalar tablolarını birleştiriyoruz ve burada fabrikaID'yi filtreliyoruz
    SELECT upatama.UPAtamaKodu, ROW_NUMBER() OVER (ORDER BY upatama.OlusturmaTarihi) AS RowNum
    FROM urunler AS urun
    INNER JOIN upatamaAktif AS upatama ON urun.urunID = upatama.urunID
    INNER JOIN fabrikalar AS fabrika ON fabrika.fabrikaID = upatama.fabrikaID
    WHERE urun.urunID = @urunID
    AND fabrika.fabrikaID = @FabrikaID
    AND upatama.kullanimDurumu = 1
) AS upWithRowNum
JOIN (

    VALUES 
    (@value1, 1), (@value2, 2), (@value3, 3), (@value4, 4), (@value5, 5), (@value6, 6), (@value7, 7),
    (@value8, 8), (@value9, 9), (@value10, 10), (@value11, 11), (@value12, 12), (@value13, 13),
    (@value14, 14), (@value15, 15)
) AS v([value], RowNum) 
ON upWithRowNum.RowNum = v.RowNum;

 ";

            var parameters = new DynamicParameters();
            parameters.Add("@urunId", createUnumuneManuelDto.UrunId);
            parameters.Add("@siraNo", createUnumuneManuelDto.SiraNo);
            parameters.Add("@fabrikaId", createUnumuneManuelDto.FabrikaId);
            parameters.Add("@UretimTarihi", createUnumuneManuelDto.UretimTarihi);
            parameters.Add("@kontrolSaati", createUnumuneManuelDto.KontrolSaati);
            parameters.Add("@numuneSeriNoSarjNo", createUnumuneManuelDto.NumuneSeriNoSarjNo);
            parameters.Add("@mudahaleVarmi", createUnumuneManuelDto.MudahaleVarmi);
            parameters.Add("@aciklama", createUnumuneManuelDto.Aciklama);
            parameters.Add("@onayDurumu", createUnumuneManuelDto.OnayDurumu);
            parameters.Add("@amirOnayDurumu", createUnumuneManuelDto.AmirOnayDurumu);
            parameters.Add("@olusturmaTarihi", createUnumuneManuelDto.OlusturmaTarihi);
            parameters.Add("@versiyon", createUnumuneManuelDto.Versiyon);
            parameters.Add("@personelSicilNo", createUnumuneManuelDto.PersonelSicilNo);
            parameters.Add("@value", createUnumuneManuelDto.Value);
            parameters.Add("@value1", createUnumuneManuelDto.Value1);
            parameters.Add("@value2", createUnumuneManuelDto.Value2);
            parameters.Add("@value3", createUnumuneManuelDto.Value3);
            parameters.Add("@value4", createUnumuneManuelDto.Value4);
            parameters.Add("@value5", createUnumuneManuelDto.Value5);
            parameters.Add("@value6", createUnumuneManuelDto.Value6);
            parameters.Add("@value7", createUnumuneManuelDto.Value7);
            parameters.Add("@value8", createUnumuneManuelDto.Value8);
            parameters.Add("@value9", createUnumuneManuelDto.Value9);
            parameters.Add("@value10", createUnumuneManuelDto.Value10);
            parameters.Add("@value11", createUnumuneManuelDto.Value11);
            parameters.Add("@value12", createUnumuneManuelDto.Value12);
            parameters.Add("@value13", createUnumuneManuelDto.Value13);
            parameters.Add("@value14", createUnumuneManuelDto.Value14);
            parameters.Add("@value15", createUnumuneManuelDto.Value15);
            parameters.Add("@token", createUnumuneManuelDto.Token);
            parameters.Add("@trend", createUnumuneManuelDto.Trend);
            parameters.Add("@hatAdiAciklamasi", createUnumuneManuelDto.HatAdiAciklamasi);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async void DeleteUnumune(int id)
        {
            string query = "DELETE FROM UPNvalue WHERE NumuneID = @numuneId;\r\n    DELETE FROM Unumune WHERE NumuneId = @numuneId;";
            var parameters = new DynamicParameters();
            parameters.Add("numuneId", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }
        public async void SendUnumune(CreateUnumuneDto createUnumuneDto)
        {
            var recipientEmails = new List<string>();

            if (createUnumuneDto.FabrikaId == 1)
            {
                recipientEmails.Add("bekira@kyk.com.tr");
            }
            else if (createUnumuneDto.FabrikaId == 2)
            {
                recipientEmails.Add("onuro@kyk.com.tr");

            }
            else if (createUnumuneDto.FabrikaId == 3)
            {
                recipientEmails.Add("onuro@kyk.com.tr");

            }
            else
            {
                recipientEmails.Add("bekira@kyk.com.tr");
            }


            string query = @"
    EXEC habasDB.dbo.spGenel_EmailGonder_INSERT
        @SicilNo = 'IPKSV',
        @GonderiIstenenZaman = @GonderiIstenenZaman,
        @AliciEmail = @AliciEmail,
        @AliciAdSoyad = @AliciAdSoyad,
        @Baslik = @Baslik,
        @Mesaj = @Mesaj,
        @EkliDosya = '',
        @GonderiSonrasiDosyayiSil = @GonderiSonrasiDosyayiSil;
    ";

            foreach (var recipientEmail in recipientEmails)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GonderiIstenenZaman", new DateTime(2011, 1, 1));
                parameters.Add("@AliciEmail", recipientEmail);
                parameters.Add("@AliciAdSoyad", createUnumuneDto.PersonelSicilNo);
                parameters.Add("@Baslik", createUnumuneDto.Aciklama);
                parameters.Add("@GonderiSonrasiDosyayiSil", 0);
                parameters.Add("@SiraNo", createUnumuneDto.SiraNo);
                parameters.Add("@MudahaleVarmi", createUnumuneDto.MudahaleVarmi);
                parameters.Add("@Aciklama", createUnumuneDto.Aciklama);
                decimal value1, alt1, ust1;
                string fontWeight;
                string color;
                var message = $@"
<!DOCTYPE html>
<html>
<head>
    <title>Email Template</title>
</head>
<body>
    <p>Numune şartlı onay / red amir onaylama mailidir.</p>
    <p>Numunede sorun yoksa ONAYLA butonuna basınız. </p>
    <p>Aksi durumda numune şartlı onay / red statüsünde kalacaktır.</p>
    <p>Nümune Adı: {createUnumuneDto.malzemeAciklamasi}</p>
    <p>Numune Id: {createUnumuneDto.LatestNumuneID}  (Bulamama durumunda 1 veya 2 fazlasınıda kontrol ediniz.)</p>
    <p>Mudahale var mı?  :{createUnumuneDto.MudahaleVarmi}</p>
    <p>Aciklama: {createUnumuneDto.Aciklama}</p>";

                for (int i = 1; i <= 15; i++)
                {
                    decimal value = decimal.TryParse(createUnumuneDto.GetType().GetProperty($"Value{i}")?.GetValue(createUnumuneDto)?.ToString(), out value1) ? value1 : 0;
                    decimal alt = decimal.TryParse(createUnumuneDto.GetType().GetProperty($"AltSartliKabulSiniri{i}")?.GetValue(createUnumuneDto)?.ToString(), out alt1) ? alt1 : 0;
                    decimal ust = decimal.TryParse(createUnumuneDto.GetType().GetProperty($"UstSartliKabulSiniri{i}")?.GetValue(createUnumuneDto)?.ToString(), out ust1) ? ust1 : 0;
                    decimal altOnay = decimal.TryParse(createUnumuneDto.GetType().GetProperty($"AltOnaySiniri{i}")?.GetValue(createUnumuneDto)?.ToString(), out alt1) ? alt1 : 0;
                    decimal ustOnay = decimal.TryParse(createUnumuneDto.GetType().GetProperty($"UstOnaySiniri{i}")?.GetValue(createUnumuneDto)?.ToString(), out ust1) ? ust1 : 0;

                    bool isValueInRange = value >= altOnay && value <= ustOnay;
                    bool isYellow = (value > ustOnay && value < ust) || (value < altOnay && value > alt);

                    if (isValueInRange)
                    {
                        fontWeight = "normal";
                        color = "black";
                    }
                    else if (isYellow)
                    {
                        fontWeight = "normal";
                        color = "green";
                    }
                    else
                    {
                        fontWeight = "bold";
                        color = "red";
                    }

                    message += $@"
    <p style='font-weight: {fontWeight}; color: {color};'>
        KontrolParametresi: {createUnumuneDto.GetType().GetProperty($"KontrolParametresi{i}")?.GetValue(createUnumuneDto)} 
        AOS= {createUnumuneDto.GetType().GetProperty($"AltOnaySiniri{i}")?.GetValue(createUnumuneDto)}, 
        AŞOS=  {createUnumuneDto.GetType().GetProperty($"AltSartliKabulSiniri{i}")?.GetValue(createUnumuneDto)}, 
        ÜŞOS= {createUnumuneDto.GetType().GetProperty($"UstSartliKabulSiniri{i}")?.GetValue(createUnumuneDto)}, 
        ÜOS= {createUnumuneDto.GetType().GetProperty($"UstOnaySiniri{i}")?.GetValue(createUnumuneDto)} 
        Value= {createUnumuneDto.GetType().GetProperty($"Value{i}")?.GetValue(createUnumuneDto)}
    </p>";
                }
                string token = createUnumuneDto.Token;

                message += $@"
    <a href='https://ipk.kyk.com.tr/Unumune/UpdateAmir?token={token}'
       style='display: inline-block; padding: 10px 20px; font-size: 16px; font-weight: bold; color: #fff; background-color: #007BFF; border: none; border-radius: 5px; text-decoration: none; text-align: center;'>
        Onayla
    </a>
</body>
</html>";



                parameters.Add("@Mesaj", message);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }
            }
        }



        public async void TrendMailUnumune(CreateUnumuneDto createUnumuneDto)
        {
            var recipientEmails = new List<string>();

            if (createUnumuneDto.FabrikaId == 1)
            {
                recipientEmails.Add("bekira@kyk.com.tr");
                recipientEmails.Add("hanifem@kyk.com.tr");

            }
            else if (createUnumuneDto.FabrikaId == 2)
            {
                recipientEmails.Add("onuro@kyk.com.tr");
                recipientEmails.Add("hanifem@kyk.com.tr");

            }
            else if (createUnumuneDto.FabrikaId == 3)
            {
                recipientEmails.Add("onuro@kyk.com.tr");
                recipientEmails.Add("hanifem@kyk.com.tr");

            }
            else
            {
                recipientEmails.Add("bekira@kyk.com.tr");
                recipientEmails.Add("hanifem@kyk.com.tr");

            }


            string query = @"
    EXEC habasDB.dbo.spGenel_EmailGonder_INSERT
        @SicilNo = 'IPKSV',
        @GonderiIstenenZaman = @GonderiIstenenZaman,
        @AliciEmail = @AliciEmail,
        @AliciAdSoyad = @AliciAdSoyad,
        @Baslik = @Baslik,
        @Mesaj = @Mesaj,
        @EkliDosya = '',
        @GonderiSonrasiDosyayiSil = @GonderiSonrasiDosyayiSil;
    ";

            foreach (var recipientEmail in recipientEmails)
            {
                var parameters = new DynamicParameters();
                parameters.Add("@GonderiIstenenZaman", new DateTime(2011, 1, 1));
                parameters.Add("@AliciEmail", recipientEmail);
                parameters.Add("@AliciAdSoyad", createUnumuneDto.PersonelSicilNo);

                string fabrikaAdi = createUnumuneDto.FabrikaId switch
                {
                    1 => "Eskişehir",
                    2 => "Adana",
                    3 => "Diyarbakır",
                    4 => "Samsun",
                    5 => "Aydın",
                    _ => "Unknown" 
                };

                string baslik = $"{createUnumuneDto.malzemeAciklamasi}, {fabrikaAdi}";
                parameters.Add("@Baslik", baslik);

                parameters.Add("@GonderiSonrasiDosyayiSil", 0);
                parameters.Add("@SiraNo", createUnumuneDto.SiraNo);
                parameters.Add("@fabrikaId", createUnumuneDto.FabrikaId);

            var message = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <title>Email Template</title>
    </head>
    <body>
        <p>{fabrikaAdi} fabrikada TREND varlığı uyarı mailidir.</p>
        <p>Ürün Adı: {createUnumuneDto.malzemeAciklamasi}</p>
        <p>Numune Id: {createUnumuneDto.LatestNumuneID}  (Bulamama durumunda 1 veya 2 fazlasınıda kontrol ediniz.)</p>
        <p>Kontrol Parametresi: {createUnumuneDto.TrendKontrol}</p>
        <p>Trend Yönü: {createUnumuneDto.TrendYonu}</p>
    </body>
    </html>";

                parameters.Add("@Mesaj", message);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }
            }

        }
            public async Task<List<ResultUnumuneDto>> GetAllUnumuneAsync()
        {
            string query = "SELECT *, u.MalzemeAciklamasi,\r\nUnumune.PersonelSicilNo AS UnPersonel\r\nFROM Unumune JOIN Urunler u ON u.UrunID = Unumune.UrunID; ";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultUnumuneDto>(query);
                return values.ToList();
            }
        }

        public async Task<AmirOnayDurumuUnumuneDto> GetDataByToken(string token)
        {
            string query = "SELECT * FROM Unumune WHERE Token = @token;";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<AmirOnayDurumuUnumuneDto>(query, new { token });
            }
        }

        public async Task UpdateAmir(AmirOnayDurumuUnumuneDto amirOnayDurumuUnumuneDto)
        {
            string query = "UPDATE Unumune SET AmirOnayDurumu = 'SartliOnay' WHERE Token = @token;";
            var parameters = new DynamicParameters();
            parameters.Add("@token", amirOnayDurumuUnumuneDto.Token);         
            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }

        public async void UpdateUnumune(UpdateUnumuneDto updateUnumuneDto)
        {
            string query = "Update Unumune Set UrunId=@urunId,SiraNo=@siraNo,UretimTarihi=@uretimTarihi,KontrolSaati=@kontrolSaati,NumuneSeriNoSarjNo=@numuneSeriNoSarjNo,MudahaleVarmi=@mudahaleVarmi,Aciklama=@aciklama,OnayDurumu=@onaydurumu,AmirOnayDurumu=@amirOnayDurumu,OlusturmaTarihi=@olusturmaTarihi,GuncellenmeTarihi = @guncellenmeTarihi ,PersonelSicilNo=@personelSicilNo, Token = @token, Trend = @trend, HatAdiAciklamasi = @hatAdiAciklamasi where NumuneId=@numuneId";
            var parameters = new DynamicParameters();
            parameters.Add("@numuneId", updateUnumuneDto.NumuneId);
            parameters.Add("@urunId", updateUnumuneDto.UrunId);
            parameters.Add("@siraNo", updateUnumuneDto.SiraNo);
            parameters.Add("@uretimTarihi", updateUnumuneDto.UretimTarihi);
            parameters.Add("@kontrolSaati", updateUnumuneDto.KontrolSaati);
            parameters.Add("@numuneSeriNoSarjNo", updateUnumuneDto.NumuneSeriNoSarjNo);
            parameters.Add("@mudahaleVarmi", updateUnumuneDto.MudahaleVarmi);
            parameters.Add("@aciklama", updateUnumuneDto.Aciklama);
            parameters.Add("@onayDurumu", updateUnumuneDto.OnayDurumu);
            parameters.Add("@amirOnayDurumu", updateUnumuneDto.AmirOnayDurumu);
            parameters.Add("@olusturmaTarihi", updateUnumuneDto.OlusturmaTarihi);
            parameters.Add("@guncellenmeTarihi", updateUnumuneDto.GuncellenmeTarihi);
            parameters.Add("@personelSicilNo", updateUnumuneDto.PersonelSicilNo);
            parameters.Add("@hatAdiAciklamasi", updateUnumuneDto.HatAdiAciklamasi);
            parameters.Add("@token", updateUnumuneDto.Token);
            parameters.Add("@trend", updateUnumuneDto.Trend);
            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
    }
}
