<!DOCTYPE html>
<html lang="tr">
</head>
<body>
    <h1>Hastane Randevu Sistemi</h1>
    <p><strong>Hastane Randevu Sistemi</strong>, <code>ASP.NET Core 8.0 N katmanlı mimari yapısı ve SOLID prensiplerine</code> uygun olarak geliştirilmiş bir web uygulamasıdır. Bu sistem, hastane randevularını yönetmek için hasta kaydı, doktor takvimi ve randevu oluşturma gibi işlevler sunar.</p>
    <h2>Özellikler</h2>
    <ul>
        <li>Hasta kaydı ve giriş işlemleri</li>
        <li>Doktor yönetimi</li>
        <li>Randevu oluşturma sistemi</li>
        <li>Rol tabanlı erişim kontrolü (Admin, Doktor, Hasta)</li>
        <li>Admin: Tüm randevuları görüntüler, doktor ekleme, genel istatistik</li>
        <li>Doktor: Kendisine gelen hastaları yönetir</li>
        <li>Hasta: Randevu ekleme, randevuları görüntüleme</li>
    </ul>
    <h2>Gereksinimler</h2>
    <ul>
        <li><code>.NET 8.0 SDK</code></li>
        <li>Visual Studio 2022 veya Rider(IDE)</li>
        <li>MSSQL Server (veritabanı için)</li>
    </ul>
    <h2>Ekran Görüntüleri</h2>
    <p>Aşağıda uygulamanın bazı ekran görüntüleri bulunmaktadır:</p>
    <p>Hasta Paneli:</p>
    <img src="https://github.com/baristok/HospitalAppointmentSystem_asp.netCore-8.0-/blob/main/HastaneRandevuSistemi/HastaneRandevuSistemi/wwwroot/images/Hasta.png" alt="HastaPaneli">
    <p>Doktor Paneli:</p>
    <img src="https://github.com/baristok/HospitalAppointmentSystem_asp.netCore-8.0-/blob/main/HastaneRandevuSistemi/HastaneRandevuSistemi/wwwroot/images/Doktor.png" alt="DoktorPaneli">
    <p>Admin Paneli(Kişisel Datalar FakePersonGenerator tarafından sağlanmıştır):</p>
    <img src="https://github.com/baristok/HospitalAppointmentSystem_asp.netCore-8.0-/blob/main/HastaneRandevuSistemi/HastaneRandevuSistemi/wwwroot/images/Admin1.png" alt="AdminPaneli">
    <h2>Kurulum</h2>
    <ol>
        <li>Bu projeyi indirmek için aşağıdaki komutu çalıştırın:</li>
        <pre><code>git clone https://github.com/baristok/HospitalAppointmentSystem_asp.netCore-8.0-.git</code></pre>   
    </ol>
    <h2>Bağlantılar</h2>
    <ul>
        <li>Proje deposu: <a href="https://github.com/baristok/HospitalAppointmentSystem_asp.netCore-8.0-" target="_blank">GitHub</a></li>
        <li>ASP.NET Core Dokümantasyonu: <a href="https://learn.microsoft.com/tr-tr/aspnet/core/" target="_blank">Microsoft Docs</a></li>
    </ul>
</body>
</html>
