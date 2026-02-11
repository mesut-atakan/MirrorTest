# Teknik Tasarım Dökümanı: You Die Today (Mobile)
## 1. Genel Bakış ve Teknoloji Yığını
- **Hedef Platform:** Mobil (IOS & Android)
- **Oyun Motoru:** Unity 6.3.X (LTS)
- **Render Pipeline:** Universal Render Pipeline (URP). _Mobil cihazlarda yüksek performans için en ideal seçim._
- **Kamera Perspektifi:** 3D Envioroment, 2D Perspective (Orthograpich)

## 2. Sistem Mimarisi (Architecture)
### 2.1. Game State Manager (Oyun Durum Yönetimi)
Oyunun akışını (Menü, Karakter Seçimi, Zindan, Zar Atma Sekansı, Ölüm Ekranı) yöneten bir **Finite State Machine (FSM)** kullanılacaktır.
- `State_CharacterSelect`: Karakter sınıflarının (Warrior vb.) seçimi.
- `State_Dungeon_Event`: Kart seçimi veya hikaye anlatımı (Scroll ekranı).
- `State_Combat`: Sıra tabanlı savaş ve zar mekanikleri.

### 2.2. Dice Engine (Zar Motoru - Teknik Detay)
Görsellerde gördüğümüz D4, D6, D8, D12 ve D20 zarları için iki aşamalı bir sistem:
1. **Fiziksel Simülasyon:** 3D ortamda zarın atılması (Rigidbody & Torque).
2. **RNG Validation:** Zar durduğunda `Raycast` veya `Vector3.up` kontrolü ile gelen sayının okunması ve bu değerin `CombatManager`'a iletilmesi.

## 3. Veri Yapıları ve Nesne Modelleri
### 3.1. Kart Sistemi (ScriptableObjects)
Her kart bir `ScriptableObject` olarak tanımlanır. Bu, bellek yönetimini optimize eder. | Parametre | Tip | Açıklama | | :--- | :--- | :--- | | `CardID` | int | Benzersiz kimlik. | | `RequiredDice` | Enum | D4, D6, D20 vb. | | `EffectValue` | int | Hasar veya blok miktarı. | | `Modifier` | String | CON, DEX, WIS gibi nitelik bağımlılıkları. |

### 3.2. Karakter ve Düşman Verisi
Karakterlerin (Warrior, Elf vb.) can (`HP`), savunma (`AC`) ve seviye (`Level`) verileri merkezi bir `PlayerData` sınıfında tutulur.

## 4. Oynanış Mekanikleri (Technical Implementation)
### 4.1. Combat Logic (Savaş Mantığı)
Sıra tabanlı (Turn-based) bir yapı.

- **Zar Kontrolü (Difficulty Class - DC):** Görseldeki "DC 12 Negotiate" gibi mekanikler için `PlayerRoll + AttributeModifier >= DC` formülü kodlanacaktır.

- **Sıra Sonu:** `End Turn` butonuna basıldığında düşman yapay zekası (AI) `EnemyActionQueue` üzerinden tetiklenir.

### 4.2. Narrative System (Hikaye Sistemi)

- **JSON tabanlı Dialog Sistemi:** Metinler ve seçim sonuçları (Örn: -5 altın) harici bir JSON dosyasından okunur. Bu, lokalizasyon (dil desteği) için kolaylık sağlar.

## 5. Grafik ve UI (Mobil Optimizasyon)
### 5.1. 3D Ortamda 2D Bakış (2.5D)
- **Layering:** Arka plandaki 3D zindan modelleri ile ön plandaki 2D UI (kartlar ve ikonlar) farklı `Canvas` ve `Camera Layer`'larda tutulur.

- **Post-Processing:** Mobil performansı için sadece "Bloom" ve "Color Grading" aktif edilecektir.

### 5.2. UI/UX Yönetimi
- **Input System:** Mobil dokunmatik ekranlar için `Input System Package` kullanılacak.
- **Responsive Layout:** Farklı ekran oranları (16:9, 19.5:9 vb.) için `Anchored Positioning` kullanılacaktır.

## 6. Kayıt ve Kalıcılık (Save System)
- **Local Save:** `BinaryFormatter` veya `Newtonsoft.Json` kullanılarak oyuncu destesi, mevcut canı ve zindandaki ilerlemesi kaydedilir.
- **Anti-Cheat:** Zar sonuçlarının ve altın miktarının basitçe değiştirilmemesi için veriler şifrelenmiş (Encrypted) şekilde saklanacaktır.
