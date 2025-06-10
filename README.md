# 🎮 Cactai2 — 2D Mobile Endless Runner

**Cactai2** to dynamiczna, proceduralnie generowana gra mobilna 2D, w której sterujesz postacią za pomocą **akcelerometru**. Twoim celem jest przetrwać jak najdłużej, unikając kolców, przeciwników i śliskich pułapek, a jednocześnie balansując między platformami, które pojawiają się losowo.

---

## 📱 Funkcje

- 📦 **Proceduralne generowanie poziomu** — nieskończona ilość kombinacji bloków
- 🎮 **Sterowanie akcelerometrem** — intuicyjne ruchy postaci
- ☠️ **Przeszkody i przeciwnicy** — kolce, lodowe platformy, chodzący wrogowie
- 🎛️ **Panel ustawień prefabów** — kontroluj szanse spawnu poszczególnych elementów
- 💾 **Zapis ustawień** — gra zapamiętuje Twoje preferencje po śmierci i restarcie
- 🔁 **Restart po śmierci** — szybka rozgrywka bez ekranów ładowania

---

## 🛠️ Technologia

- **Silnik**: Unity 6
- **Język**: C#
- **Platforma docelowa**: Android (iOS w przyszłości)
- **UI**: Unity Canvas, Slider, Button
- **Zarządzanie danymi**: `PlayerPrefs`



## 🧪 Uruchamianie projektu

1. Sklonuj repozytorium:
   ```bash
   git clone https://github.com/K3ster/Cactai2.git

2. Otwórz projekt w Unity (wersja 6 zalecana)
3. Upewnij się, że masz włączone:

   * `Accelerometer Input` (na urządzeniu mobilnym)
   * Włączone `EventSystem` w scenie
4. Zbuduj apkę na Androida i testuj na telefonie

---

## ⚙️ Personalizacja prefabów

W scenie ustawień znajdują się **5 suwaków**, które odpowiadają za prawdopodobieństwo pojawiania się prefabów:

* Normalna platforma
* Lodowa platforma
* Kolce
* Przeciwnik
* Częstotliwość pojawiania się platform
* Wykosość skoku
* Szybkość postaci

Po kliknięciu **Zapisz**, wartości są zachowane między sesjami.


## 🧑‍💻 Autor

Projekt stworzony przez Kester
Pomysły, kod, UI i system spawnu — wszystko w pełni autorskie 💡

---

## 📃 Licencja

MIT License — możesz dowolnie używać, modyfikować i rozpowszechniać z podaniem autora.

---









