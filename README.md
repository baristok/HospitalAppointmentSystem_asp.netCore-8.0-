<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Hastane Randevu Sistemi - README</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            line-height: 1.6;
            margin: 20px;
            color: #333;
        }
        h1, h2, h3 {
            color: #2c3e50;
        }
        ul {
            padding-left: 20px;
        }
        pre {
            background: #f4f4f4;
            padding: 10px;
            border-radius: 5px;
            overflow-x: auto;
        }
        code {
            color: #e74c3c;
        }
        a {
            color: #3498db;
            text-decoration: none;
        }
        a:hover {
            text-decoration: underline;
        }
        .btn {
            display: inline-block;
            background: #3498db;
            color: #fff;
            padding: 10px 15px;
            border-radius: 5px;
            text-decoration: none;
            margin-top: 10px;
        }
        .btn:hover {
            background: #2980b9;
        }
    </style>
</head>
<body>
    <h1>Hastane Randevu Sistemi</h1>
    <p><strong>Hastane Randevu Sistemi</strong>, <code>ASP.NET Core 8.0</code> kullanılarak geliştirilmiş bir web uygulamasıdır. Bu sistem, hastane randevularını yönetmek için hasta kaydı, doktor takvimi ve randevu oluşturma gibi işlevler sunar.</p>
    <h2>Özellikler</h2>
    <ul>
        <li>Hasta kaydı ve giriş işlemleri</li>
        <li>Doktor yönetimi ve takvim oluşturma</li>
        <li>Randevu oluşturma sistemi</li>
        <li>Kullanıcı dostu arayüz</li>
        <li>Rol tabanlı erişim kontrolü (Admin, Doktor, Hasta)</li>
    </ul>
    <h2>Gereksinimler</h2>
    <ul>
        <li><code>.NET 8.0 SDK</code></li>
        <li>Visual Studio 2022 veya Visual Studio Code</li>
        <li>SQL Server (veritabanı için)</li>
    </ul>
    <h2>Kurulum</h2>
    <ol>
        <li>Bu projeyi indirmek için aşağıdaki komutu çalıştırın:</li>
        <pre><code>git clone https://github.com/baristok/HospitalAppointmentSystem_asp.netCore-8.0-.git</code></pre>
        <li>Proje dizinine gidin:</li>
        <pre><code>cd HospitalAppointmentSystem_asp.netCore-8.0-</code></pre>
        <li>Gerekli bağımlılıkları yüklemek için:</li>
        <pre><code>dotnet restore</code></pre>
        <li>Uygulamayı çalıştırmak için:</li>
        <pre><code>dotnet run</code></pre>
    </ol>
    <h2>Bağlantılar</h2>
    <ul>
        <li>Proje deposu: <a href="https://github.com/baristok/HospitalAppointmentSystem_asp.netCore-8.0-" target="_blank">GitHub</a></li>
        <li>ASP.NET Core Dokümantasyonu: <a href="https://learn.microsoft.com/tr-tr/aspnet/core/" target="_blank">Microsoft Docs</a></li>
    </ul>
</body>
</html>
