using Dapper;
using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.HMnumuneDtos;
using KykKaliteApi.Dtos.UnumuneDtos;
using KykKaliteApi.Models.DapperContext;

namespace KykKaliteApi.Repositories.HMnumuneRepository
{
    public class HMnumuneRepository : IHMnumuneRepository
    {
        private readonly Context _context;
        public HMnumuneRepository(Context context)
        {
            _context = context;
        }

        public async Task<AmirOnayDurumuHMnumuneDto> GetDataByToken(string token)
        {
            string query = "SELECT * FROM HMnumune WHERE Token = @token;";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<AmirOnayDurumuHMnumuneDto>(query, new { token });
            }
        }

        public async Task UpdateAmir(AmirOnayDurumuHMnumuneDto amirOnayDurumuHMnumuneDto )
        {
            string query = "UPDATE HMnumune SET AmirOnayDurumu = 'SartliOnay' WHERE Token = @token;";
            var parameters = new DynamicParameters();
            parameters.Add("@token", amirOnayDurumuHMnumuneDto.Token);
            using (var connectiont = _context.CreateConnection())
            {
                await connectiont.ExecuteAsync(query, parameters);
            }
        }
        public async void TrendMailHMnumune(CreateHMnumuneDto createHMnumuneDto)
        {
            var recipientEmails = new List<string>();

            if (createHMnumuneDto.FabrikaId == 1)
            {
                recipientEmails.Add("bekira@kyk.com.tr");
                recipientEmails.Add("hanifem@kyk.com.tr");

            }
            else if (createHMnumuneDto.FabrikaId == 2)
            {
                recipientEmails.Add("onuro@kyk.com.tr");
                recipientEmails.Add("hanifem@kyk.com.tr");

            }
            else if (createHMnumuneDto.FabrikaId == 3)
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
                parameters.Add("@AliciAdSoyad", createHMnumuneDto.PersonelSicilNo);

                string fabrikaAdi = createHMnumuneDto.FabrikaId switch
                {
                    1 => "Eskişehir",
                    2 => "Adana",
                    3 => "Diyarbakır",
                    4 => "Samsun",
                    5 => "Aydın",
                    _ => "Unknown"
                };

                string baslik = $"{createHMnumuneDto.MalzemeAciklamasi}, {fabrikaAdi}";
                parameters.Add("@Baslik", baslik);

                parameters.Add("@GonderiSonrasiDosyayiSil", 0);
                parameters.Add("@SiraNo", createHMnumuneDto.SiraNo);
                parameters.Add("@fabrikaId", createHMnumuneDto.FabrikaId);

                var message = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <title>Email Template</title>
    </head>
    <body>
        <p>{fabrikaAdi} fabrikada TREND varlığı uyarı mailidir.</p>
        <p>Hammadde Adı: {createHMnumuneDto.MalzemeAciklamasi}</p>
        <p>Numune Id: {createHMnumuneDto.LatestNumuneID}  (Bulamama durumunda 1 veya 2 fazlasınıda kontrol ediniz.)</p>
        <p>Kontrol Parametresi: {createHMnumuneDto.TrendKontrol}</p>
        <p>Trend Yönü: {createHMnumuneDto.TrendYonu}</p>
    </body>
    </html>";
                parameters.Add("@Mesaj", message);

                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }
            }

        }
        public async void SentHMnumune(CreateHMnumuneDto createHMnumuneDto)
        {
            var recipientEmails = new List<string>();

            if (createHMnumuneDto.FabrikaId == 1)
            {
                recipientEmails.Add("bekira@kyk.com.tr");
            }
            else if (createHMnumuneDto.FabrikaId == 2)
            {
                recipientEmails.Add("onuro@kyk.com.tr");
                recipientEmails.Add("hanifem@kyk.com.tr");

            }
            else if (createHMnumuneDto.FabrikaId == 3)
            {
                recipientEmails.Add("onuro@kyk.com.tr");
                recipientEmails.Add("hanifem@kyk.com.tr");

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
                parameters.Add("@AliciAdSoyad", createHMnumuneDto.PersonelSicilNo);
                parameters.Add("@Baslik", createHMnumuneDto.Aciklama);
                parameters.Add("@GonderiSonrasiDosyayiSil", 0);
                parameters.Add("@SiraNo", createHMnumuneDto.SiraNo);
                parameters.Add("@THMID", createHMnumuneDto.THMID);
                parameters.Add("@Aciklama", createHMnumuneDto.Aciklama);
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
    <h1>Merhaba,</h1>
  <p>Numune şartlı onay / red amir onaylama mailidir.</p>
    <p>Numunede sorun yoksa ONAYLA butonuna basınız. </p>
    <p>Aksi durumda numune şartlı onay / red statüsünde kalacaktır.</p>
    <p>Nümune Adı: {createHMnumuneDto.MalzemeAciklamasi}</p>
    <p>Numune Id: {createHMnumuneDto.SiraNo}</p>
    <p>Mudahale var mı ?: {createHMnumuneDto.THMID}</p>
    <p>Aciklama: {createHMnumuneDto.Aciklama}</p>";

                for (int i = 1; i <= 15; i++)
                {
                    decimal value = decimal.TryParse(createHMnumuneDto.GetType().GetProperty($"Value{i}")?.GetValue(createHMnumuneDto)?.ToString(), out value1) ? value1 : 0;
                    decimal alt = decimal.TryParse(createHMnumuneDto.GetType().GetProperty($"AltSartliKabulSiniri{i}")?.GetValue(createHMnumuneDto)?.ToString(), out alt1) ? alt1 : 0;
                    decimal ust = decimal.TryParse(createHMnumuneDto.GetType().GetProperty($"UstSartliKabulSiniri{i}")?.GetValue(createHMnumuneDto)?.ToString(), out ust1) ? ust1 : 0;
                    decimal altOnay = decimal.TryParse(createHMnumuneDto.GetType().GetProperty($"AltOnaySiniri{i}")?.GetValue(createHMnumuneDto)?.ToString(), out alt1) ? alt1 : 0;
                    decimal ustOnay = decimal.TryParse(createHMnumuneDto.GetType().GetProperty($"UstOnaySiniri{i}")?.GetValue(createHMnumuneDto)?.ToString(), out ust1) ? ust1 : 0;

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
        KontrolParametresi: {createHMnumuneDto.GetType().GetProperty($"KontrolParametresi{i}")?.GetValue(createHMnumuneDto)} 
        AOS= {createHMnumuneDto.GetType().GetProperty($"AltOnaySiniri{i}")?.GetValue(createHMnumuneDto)}, 
        AŞOS=  {createHMnumuneDto.GetType().GetProperty($"AltSartliKabulSiniri{i}")?.GetValue(createHMnumuneDto)}, 
        ÜŞOS= {createHMnumuneDto.GetType().GetProperty($"UstSartliKabulSiniri{i}")?.GetValue(createHMnumuneDto)}, 
        ÜOS= {createHMnumuneDto.GetType().GetProperty($"UstOnaySiniri{i}")?.GetValue(createHMnumuneDto)} 
        Value= {createHMnumuneDto.GetType().GetProperty($"Value{i}")?.GetValue(createHMnumuneDto)}
    </p>";
                }
                string token = createHMnumuneDto.Token;

                message += $@"
    <a href='https://ipk.kyk.com.tr/HMnumune/UpdateAmir?token={token}'
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
        public async void CreateHMnumuneManuel(CreateHMnumuneManuelDto createHMnumuneManuelDto)
        {
            string query = @"
DECLARE @numuneID INT;

-- Insert into HMnumune and get the generated ID
INSERT INTO HMnumune(
    HammaddeID, SiraNo, Tarihi, Saat, IrsaliyeNo, MalzemeLotSeriNo, KYKBarkodNo, 
    MalzemeUretimTarihi, MalzemeSKT, MalzemeMiktarı, MiktarBirimi, Aciklama, 
    OnayDurumu, AmirOnayDurumu, OlusturmaTarihi, PersonelSicilNo, Token, Trend, THMID
)
VALUES (
    @hammaddeID, @siraNo, @tarihi, @saat, @irsaliyeNo, @malzemeLotSeriNo, 
    @kYKBarkodNo, @malzemeUretimTarihi, @malzemeSKT, @malzemeMiktarı, @miktarBirimi, 
    @aciklama, @onayDurumu, @amirOnayDurumu, @olusturmaTarihi, @personelSicilNo, 
    @token, @trend, @THMID
);

SET @numuneID = SCOPE_IDENTITY();

-- Insert into HMPNvalue by associating HMPAtamaKodu and values in correct order
INSERT INTO HMPNvalue(HMPAtamaKodu, NumuneID, Versiyon, Value, OlusturmaTarihi, PersonelSicilNo)
SELECT
    upWithRowNum.HMPAtamaKodu, 
    @numuneID, 
    @versiyon, 
    v.[value], 
    @olusturmaTarihi, 
    @personelSicilNo
FROM (
    -- Generate RowNum for HMPAtamaKodu based on OlusturmaTarihi
    SELECT 
        hp.HMPAtamaKodu, 
        ROW_NUMBER() OVER (ORDER BY hp.OlusturmaTarihi) AS RowNum
    FROM Hammaddeler AS h
    INNER JOIN HMPatamaAktif AS hp ON h.HammaddeID = hp.HammaddeID
    INNER JOIN fabrikalar AS fabrika ON fabrika.fabrikaID = hp.fabrikaID
    WHERE h.HammaddeID = @hammaddeID
    AND fabrika.fabrikaID = @FabrikaId
    AND hp.kullanimDurumu = 1
) AS upWithRowNum
-- Join with the list of values
JOIN (
    VALUES 
    (@value1, 1), (@value2, 2), (@value3, 3), (@value4, 4), (@value5, 5), (@value6, 6), 
    (@value7, 7), (@value8, 8), (@value9, 9), (@value10, 10), (@value11, 11), 
    (@value12, 12), (@value13, 13), (@value14, 14), (@value15, 15)
) AS v([value], RowNum) 
ON upWithRowNum.RowNum = v.RowNum;

    ";
            var parameters = new DynamicParameters();
            parameters.Add("@hammaddeId", createHMnumuneManuelDto.HammaddeId);
            parameters.Add("@fabrikaID", createHMnumuneManuelDto.FabrikaId);
            parameters.Add("@siraNo", createHMnumuneManuelDto.SiraNo);
            parameters.Add("@saat", createHMnumuneManuelDto.Saat);
            parameters.Add("@tarihi", createHMnumuneManuelDto.Tarihi);
            parameters.Add("@irsaliyeNo", createHMnumuneManuelDto.IrsaliyeNo);
            parameters.Add("@malzemeLotSeriNo", createHMnumuneManuelDto.MalzemeLotSeriNo);
            parameters.Add("@kykbarkodNo", createHMnumuneManuelDto.KYKBarkodNo);
            parameters.Add("@malzemeUretimTarihi", createHMnumuneManuelDto.MalzemeUretimTarihi);
            parameters.Add("@malzemeSkt", createHMnumuneManuelDto.MalzemeSkt);
            parameters.Add("@malzemeMiktarı", createHMnumuneManuelDto.MalzemeMiktarı);
            parameters.Add("@miktarBirimi", createHMnumuneManuelDto.MiktarBirimi);
            parameters.Add("@aciklama", createHMnumuneManuelDto.Aciklama);
            parameters.Add("@onayDurumu", createHMnumuneManuelDto.OnayDurumu);
            parameters.Add("@amirOnayDurumu", createHMnumuneManuelDto.AmirOnayDurumu);
            parameters.Add("@olusturmaTarihi", createHMnumuneManuelDto.OlusturmaTarihi);
            parameters.Add("@versiyon", createHMnumuneManuelDto.Versiyon);
            parameters.Add("@personelSicilNo", createHMnumuneManuelDto.PersonelSicilNo);
            parameters.Add("@token", createHMnumuneManuelDto.Token);
            parameters.Add("@THMID", createHMnumuneManuelDto.THMID);
            parameters.Add("@trend", createHMnumuneManuelDto.Trend);
            parameters.Add("@value", createHMnumuneManuelDto.Value);
            parameters.Add("@value1", createHMnumuneManuelDto.Value1);
            parameters.Add("@value2", createHMnumuneManuelDto.Value2);
            parameters.Add("@value3", createHMnumuneManuelDto.Value3);
            parameters.Add("@value4", createHMnumuneManuelDto.Value4);
            parameters.Add("@value5", createHMnumuneManuelDto.Value5);
            parameters.Add("@value6", createHMnumuneManuelDto.Value6);
            parameters.Add("@value7", createHMnumuneManuelDto.Value7);
            parameters.Add("@value8", createHMnumuneManuelDto.Value8);
            parameters.Add("@value9", createHMnumuneManuelDto.Value9);
            parameters.Add("@value10", createHMnumuneManuelDto.Value10);
            parameters.Add("@value11", createHMnumuneManuelDto.Value11);
            parameters.Add("@value12", createHMnumuneManuelDto.Value12);
            parameters.Add("@value13", createHMnumuneManuelDto.Value13);
            parameters.Add("@value14", createHMnumuneManuelDto.Value14);
            parameters.Add("@value15", createHMnumuneManuelDto.Value15);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
        public async void CreateHMnumune(CreateHMnumuneDto createHMnumuneDto)
        {
            string query = @"
DECLARE @numuneID INT;

-- Insert into HMnumune and get the generated ID
INSERT INTO HMnumune(
    HammaddeID, SiraNo, Tarihi, Saat, IrsaliyeNo, MalzemeLotSeriNo, KYKBarkodNo, 
    MalzemeUretimTarihi, MalzemeSKT, MalzemeMiktarı, MiktarBirimi, Aciklama, 
    OnayDurumu, AmirOnayDurumu, OlusturmaTarihi, PersonelSicilNo, Token, Trend, THMID
)
VALUES (
    @hammaddeID, @siraNo, @tarihi, @saat, @irsaliyeNo, @malzemeLotSeriNo, 
    @kYKBarkodNo, @malzemeUretimTarihi, @malzemeSKT, @malzemeMiktarı, @miktarBirimi, 
    @aciklama, @onayDurumu, @amirOnayDurumu, @olusturmaTarihi, @personelSicilNo, 
    @token, @trend, @THMID
);

SET @numuneID = SCOPE_IDENTITY();

-- Insert into HMPNvalue by associating HMPAtamaKodu and values in correct order
INSERT INTO HMPNvalue(HMPAtamaKodu, NumuneID, Versiyon, Value, OlusturmaTarihi, PersonelSicilNo)
SELECT
    upWithRowNum.HMPAtamaKodu, 
    @numuneID, 
    @versiyon, 
    v.[value], 
    @olusturmaTarihi, 
    @personelSicilNo
FROM (
    -- Generate RowNum for HMPAtamaKodu based on OlusturmaTarihi
    SELECT 
        hp.HMPAtamaKodu, 
        ROW_NUMBER() OVER (ORDER BY hp.OlusturmaTarihi) AS RowNum
    FROM Hammaddeler AS h
    INNER JOIN HMPatamaAktif AS hp ON h.HammaddeID = hp.HammaddeID
    INNER JOIN fabrikalar AS fabrika ON fabrika.fabrikaID = hp.fabrikaID
    WHERE h.HammaddeID = @hammaddeID
    AND fabrika.fabrikaID = @FabrikaId
    AND hp.kullanimDurumu = 1
) AS upWithRowNum
-- Join with the list of values
JOIN (
    VALUES 
    (@value1, 1), (@value2, 2), (@value3, 3), (@value4, 4), (@value5, 5), (@value6, 6), 
    (@value7, 7), (@value8, 8), (@value9, 9), (@value10, 10), (@value11, 11), 
    (@value12, 12), (@value13, 13), (@value14, 14), (@value15, 15)
) AS v([value], RowNum) 
ON upWithRowNum.RowNum = v.RowNum;

    ";
            var parameters = new DynamicParameters();
            parameters.Add("@hammaddeId", createHMnumuneDto.HammaddeId);
            parameters.Add("@fabrikaID", createHMnumuneDto.FabrikaId);
            parameters.Add("@siraNo", createHMnumuneDto.SiraNo);
            parameters.Add("@saat", createHMnumuneDto.Saat);
            parameters.Add("@tarihi", createHMnumuneDto.Tarihi);
            parameters.Add("@irsaliyeNo", createHMnumuneDto.IrsaliyeNo);
            parameters.Add("@malzemeLotSeriNo", createHMnumuneDto.MalzemeLotSeriNo);
            parameters.Add("@kykbarkodNo", createHMnumuneDto.KYKBarkodNo);
            parameters.Add("@malzemeUretimTarihi", createHMnumuneDto.MalzemeUretimTarihi);
            parameters.Add("@malzemeSkt", createHMnumuneDto.MalzemeSkt);
            parameters.Add("@malzemeMiktarı", createHMnumuneDto.MalzemeMiktarı);
            parameters.Add("@miktarBirimi", createHMnumuneDto.MiktarBirimi);
            parameters.Add("@aciklama", createHMnumuneDto.Aciklama);
            parameters.Add("@onayDurumu", createHMnumuneDto.OnayDurumu);
            parameters.Add("@amirOnayDurumu", createHMnumuneDto.AmirOnayDurumu);
            parameters.Add("@olusturmaTarihi", createHMnumuneDto.OlusturmaTarihi);
            parameters.Add("@versiyon", createHMnumuneDto.Versiyon);
            parameters.Add("@personelSicilNo", createHMnumuneDto.PersonelSicilNo);
            parameters.Add("@token", createHMnumuneDto.Token);
            parameters.Add("@THMID", createHMnumuneDto.THMID);
            parameters.Add("@trend", createHMnumuneDto.Trend);
            parameters.Add("@value", createHMnumuneDto.Value);
            parameters.Add("@value1", createHMnumuneDto.Value1);
            parameters.Add("@value2", createHMnumuneDto.Value2);
            parameters.Add("@value3", createHMnumuneDto.Value3);
            parameters.Add("@value4", createHMnumuneDto.Value4);
            parameters.Add("@value5", createHMnumuneDto.Value5);
            parameters.Add("@value6", createHMnumuneDto.Value6);
            parameters.Add("@value7", createHMnumuneDto.Value7);
            parameters.Add("@value8", createHMnumuneDto.Value8);
            parameters.Add("@value9", createHMnumuneDto.Value9);
            parameters.Add("@value10", createHMnumuneDto.Value10);
            parameters.Add("@value11", createHMnumuneDto.Value11);
            parameters.Add("@value12", createHMnumuneDto.Value12);
            parameters.Add("@value13", createHMnumuneDto.Value13);
            parameters.Add("@value14", createHMnumuneDto.Value14);
            parameters.Add("@value15", createHMnumuneDto.Value15);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteHMnumune(int id)
        {
            string query = "DELETE FROM HMPNvalue WHERE NumuneID = @numuneId  DELETE FROM HMnumune WHERE NumuneId = @numuneId;";
            var parameters = new DynamicParameters();
            parameters.Add("@numuneID", id);
            using (var connection = _context.CreateConnection())
            {
                int v = await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<ResultHMnumuneDto>> GetAllHMnumuneAsync()
        {
            string query = "SELECT *, u.MalzemeAciklamasi, t.UNVANI FROM HMnumune JOIN Hammaddeler u ON u.HammaddeID = HMnumune.HammaddeID JOIN TedarikciHammadde t ON HMnumune.THMID = t.THMID;";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultHMnumuneDto>(query);
                return values.ToList();
            }
        }

        public async void UpdateHMnumune(UpdateHMnumuneDto updateHMnumuneDto)
        {
            string query = @"UPDATE HMnumune 
                     SET 
                        SiraNo = @siraNo,
                        Tarihi = @tarihi,
                        IrsaliyeNo = @ırsaliyeNo,
                        MalzemeLotSeriNo = @malzemeLotSeriNo,
                        KykbarkodNo = @kykbarkodNo,
                        MalzemeUretimTarihi = @malzemeUretimTarihi,
                        MalzemeSkt = @malzemeSkt,
                        MalzemeMiktarı = @malzemeMiktarı,
                        MiktarBirimi = @miktarBirimi,
                        Aciklama = @aciklama,
                        AmirOnayDurumu = @amirOnayDurumu,
                        EklenmeTarihi = @eklenmeTarihi,
                        PersonelSicilNo = @personelSicilNo,
                        GuncellenmeTarihi = @guncellenmeTarihi
                     WHERE 
                        HammaddeId = @hammaddeId";

            var parameters = new DynamicParameters();
            parameters.Add("@siraNo", updateHMnumuneDto.SiraNo);
            parameters.Add("@tarihi", updateHMnumuneDto.Tarihi);
            parameters.Add("@ırsaliyeNo", updateHMnumuneDto.IrsaliyeNo);
            parameters.Add("@malzemeLotSeriNo", updateHMnumuneDto.MalzemeLotSeriNo);
            parameters.Add("@kykbarkodNo", updateHMnumuneDto.KykbarkodNo);
            parameters.Add("@malzemeUretimTarihi", updateHMnumuneDto.MalzemeUretimTarihi);
            parameters.Add("@malzemeSkt", updateHMnumuneDto.MalzemeSkt);
            parameters.Add("@malzemeMiktarı", updateHMnumuneDto.MalzemeMiktarı);
            parameters.Add("@miktarBirimi", updateHMnumuneDto.MiktarBirimi);
            parameters.Add("@aciklama", updateHMnumuneDto.Aciklama);
            parameters.Add("@amirOnayDurumu", true);
            parameters.Add("@eklenmeTarihi", updateHMnumuneDto.EklenmeTarihi);
            parameters.Add("@personelSicilNo", updateHMnumuneDto.PersonelSicilNo);
            parameters.Add("@hammaddeId", updateHMnumuneDto.HammaddeId);
            parameters.Add("@guncellenmeTarihi", updateHMnumuneDto.GuncellenmeTarihi);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }

   }
